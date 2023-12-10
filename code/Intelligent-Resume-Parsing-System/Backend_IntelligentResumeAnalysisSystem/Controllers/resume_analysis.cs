using Backend_IntelligentResumeAnalysisSystem.Services;
using Microsoft.AspNetCore.Mvc;

using Backend_IntelligentResumeAnalysisSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using Backend_IntelligentResumeAnalysisSystem.Models;
using Microsoft.AspNetCore.StaticFiles;
using Backend_IntelligentResumeAnalysisSystem.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Win32;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Backend_IntelligentResumeAnalysisSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class resume_analysis : ControllerBase
    {


        private readonly resume_converter_analysis _resumeAnalysisService;

        private readonly MyDbContext _context;



        public resume_analysis(MyDbContext context)
        {
            _resumeAnalysisService = new resume_converter_analysis();

            _context = context;

        }




        [HttpGet("getGraph")]
        public IActionResult GetFile(int resumeId)
        {

            var resume = _context.Resumes.Find(resumeId);
            string resumepath = resume.FilePath;
            

            //string filePath = "E:\\study\\C_Sharp\\BigHomework\\Intelligent-Resume-Parsing-System\\Backend_IntelligentResumeAnalysisSystem\\Resumes\\2023\\11\\28\\2966355fe288b1ea4c78df6b1bd11d8d.docx";
            // 获取文件所在的目录
            string directory = Path.GetDirectoryName(resumepath);

            // 获取不包括后缀的文件名
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(resumepath);

            string imagePath = Path.Combine(directory, fileNameWithoutExtension + ".jpg");

            FileStream fileStream = new FileStream(imagePath, FileMode.Open);

            string mimeType;
            new FileExtensionContentTypeProvider().TryGetContentType(imagePath, out mimeType); // 获取文件的MIME类型

            return new FileStreamResult(fileStream, mimeType); // 设置 MIME 类型为二进制流
        }


        [HttpPost("UploadResume")] // HTTP POST请求处理简历上传
        public async Task<FirstAddResumeModelClass> UploadResumeAsync(IFormFile file, int jobId, int userId)
        {
            if (file == null || file.Length == 0)
            {
                return new FirstAddResumeModelClass
                {
                    Code = 60201,
                    applicantInfo = new ApplicantInfo()
                }; // 检查文件是否为空
            }


            // 获取文件名
            string fileName = Path.GetFileName(file.FileName);
            // 设置文件存储根目录
            string staticFileRoot = "Resumes";
            // 创建文件存储路径，基于当前日期
            string fileUrlWithoutFileName = @$"{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}";
            // 创建目录
            Directory.CreateDirectory($"{staticFileRoot}/{fileUrlWithoutFileName}");

            // 创建MD5哈希对象
            MD5 md5 = MD5.Create();
            // 计算文件的哈希值
            byte[] hashBytes = md5.ComputeHash(file.OpenReadStream());
            // 将哈希值转换为字符串
            string hashedFileName = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            // 创建新的文件名，避免重复
            string newFileName = hashedFileName + "." + fileName.Split('.').Last();

            // 创建完整的文件路径
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"{staticFileRoot}\{fileUrlWithoutFileName}", newFileName);

            // 创建文件流并保存文件
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // 其他简历处理逻辑...

            Console.WriteLine("***开始将简历文件转换为txt***");

            bool conversionSuccess = _resumeAnalysisService.ResumeConverterService(filePath);

            if (!conversionSuccess)
            {
                Console.WriteLine("文件转换失败");
                return new FirstAddResumeModelClass
                {
                    Code = 60202,
                    applicantInfo = new ApplicantInfo()
                };
            }

            Console.WriteLine("***文件转换成功，开始进行简历分析...***");

            // 定义三种解析器类型
            string[] parserTypes = new string[] { "basicinfo", "talentprofile", "jobmatch", "educationandworkexperience" };
            List<Task<bool>> analysisTasks = new List<Task<bool>>();

            // 为每种解析器类型创建一个异步分析任务
            foreach (var parserType in parserTypes)
            {
                Console.WriteLine($"启动异步分析任务: {parserType}");
                var task = Task.Run(() => _resumeAnalysisService.ResumeAnalysisService(filePath, parserType));
                analysisTasks.Add(task);
            }

            // 等待所有分析任务完成
            var results = await Task.WhenAll(analysisTasks);

            // 检查所有任务是否成功完成
            if (results.All(result => result))
            {
                Console.WriteLine("所有简历分析任务成功完成");
                //return Ok("所有简历分析完成");
            }
            else
            {
                Console.WriteLine("一个或多个简历分析任务失败");
                //return BadRequest("一个或多个简历分析任务失败");
            }



            //    // 检查所有任务是否成功完成
            //    if (results.All(result => result))
            //    {
            //        Console.WriteLine("所有简历分析任务成功完成");
            //        return Ok("所有简历分析完成");
            //    }
            //    else
            //    {
            //        Console.WriteLine("一个或多个简历分析任务失败");
            //        return BadRequest("一个或多个简历分析任务失败");
            //    }


            //    // return Ok(new { Message = "File uploaded successfully.", FilePath = filePath }); // 返回成功消息和文件路径

            //}







            ////[HttpGet("fileConversion")]
            ////public IActionResult TestFileConversion()
            ////{
            ////    string filePath = @"E:\down\test\resume\1.docx";
            ////    bool success = _resumeAnalysisService.ResumeConverterService(filePath);

            ////    if (success)
            ////    {
            ////        return Ok("文件转换测试完成");
            ////    }
            ////    else
            ////    {
            ////        return BadRequest("文件转换测试失败");
            ////    }
            ////}


            //[HttpGet("analyzeResume")]
            //public IActionResult AnalyzeResume()
            //{
            //    string originalFilePath = @"E:\down\test\resume\1.docx";
            //    string parserType = "basicinfo";
            //    /*
            //     * basicinfo
            //     * talentprofile
            //     * jobmatch
            //     */

            //    bool success = _resumeAnalysisService.ResumeAnalysisService(originalFilePath, parserType);
            //    if (success)
            //    {
            //        return Ok("简历分析完成");
            //    }
            //    else
            //    {
            //        return BadRequest("简历分析失败");
            //    }
            //}




            //[HttpGet("tttt")]
            //public IActionResult tttt()
            //{

            //    // 假设这是您的原始简历文件路径
            //    string originalResumePath = @"E:\study\C_Sharp\BigHomework\Intelligent-Resume-Parsing-System\Backend_IntelligentResumeAnalysisSystem\Resumes\2023\11\28\1e49c67ceff5311a7e62ec06feeaeb98.docx";
            string originalResumePath = filePath;


            // 获取不带扩展名的文件名
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalResumePath);

            // 获取原始文件的目录（父目录）
            var directoryInfo = new DirectoryInfo(Path.GetDirectoryName(originalResumePath));
            string parentDirectory = directoryInfo.Parent.FullName;

            // 构建到JSON文件的路径
            string basicInfo_jsonFilePath = Path.Combine(parentDirectory, $"Analysis_Results\\Basic_Infos\\{fileNameWithoutExtension}.json");

            string educationandworkexperience_jsonFilePath = Path.Combine(parentDirectory, $"Analysis_Results\\Baidu_Analysis\\{fileNameWithoutExtension}.json");

            string jobMatch_jsonFilePath = Path.Combine(parentDirectory, $"Analysis_Results\\GPT_Job_Matching\\{fileNameWithoutExtension}.json");


            string talentProfile_jsonFilePath = Path.Combine(parentDirectory, $"Analysis_Results\\GPT_Talent_Portraits\\{fileNameWithoutExtension}.json");


            //string jobMatchJsonContent = System.IO.File.ReadAllText(jobMatch_jsonFilePath);
            //JObject jobMatchParsedJson = JObject.Parse(jobMatchJsonContent);



            Console.WriteLine("***************************");


            string basicInfo_jsonContent = System.IO.File.ReadAllText(basicInfo_jsonFilePath);
            JObject basicInfos = JObject.Parse(basicInfo_jsonContent);

            string educationandworkexperience_jsonContent = System.IO.File.ReadAllText(educationandworkexperience_jsonFilePath);
            JObject educationandworkexperienceInfos = JObject.Parse(educationandworkexperience_jsonContent);

            ////// 提取所需信息

            JArray educationInfos = (JArray)educationandworkexperienceInfos["results"][0]["extract"]["education_infos"];
            JArray workInfos = (JArray)educationandworkexperienceInfos["results"][0]["extract"]["work_infos"];


            // 创建 Applicant 对象
            Applicant applicant = new Applicant
            {
                // 赋值 basicInfos
                Name = basicInfos["姓名"]?.ToString(),
                Gender = basicInfos["性别"]?.ToString(),
                Age = basicInfos["年龄"]?.ToObject<int>() ?? 0, // 使用 ?? 0 来处理可能的空值
                EmailAddress = basicInfos["个人邮箱"]?.ToString(),
                PhoneNumber = basicInfos["手机号"]?.ToString(),
                HighestDegree = basicInfos["最高学历"]?.ToString(),
                School = basicInfos["毕业院校"]?.ToString(),
                JobIntention = basicInfos["求职意向岗位"]?.ToString(),
                Skills = basicInfos["技能证书"]?.ToString(),
                Honors = basicInfos["获奖荣誉"]?.ToString(),
                EducationBackground = basicInfos["教育背景"]?.ToString(),
                SchoolLevel = basicInfos["最高学历学校等级"]?.ToString(),
                WorkStability = basicInfos["工作稳定性程度"]?.ToString(),
                WorkStabilityReason = basicInfos["工作稳定性判断的原因"]?.ToString(),

                // 初始化集合
                Educations = new List<Education>(),
                WorkExperiences = new List<WorkExperience>(),
                //Projects = new List<Project>()

                // ... 其他字段 ...
            };



            Console.WriteLine("***************************");

            // 转换 education_infos
            // 在处理 education_infos 之前，确保它不是 null
            if (educationInfos != null)
            {
                foreach (var item in educationInfos)
                {
                    var educationDict = item.ToObject<Dictionary<string, string>>();
                    Education education = new Education
                    {
                        ApplicantID = applicant.ID, // 设置 ApplicantID
                        School = educationDict.GetValueOrDefault("school"),
                        College = educationDict.GetValueOrDefault("college"),
                        Major = educationDict.GetValueOrDefault("major"),
                        SchoolingRecord = educationDict.GetValueOrDefault("schooling_record"),
                        Degree = educationDict.GetValueOrDefault("degree"),
                        StartTime = educationDict.GetValueOrDefault("start_time"),
                        EndTime = educationDict.GetValueOrDefault("end_time"),
                        IsIn = educationDict.GetValueOrDefault("is_in"),
                        Gpa = educationDict.GetValueOrDefault("gpa"),
                        Rank = educationDict.GetValueOrDefault("rank")
                    };
                    //applicant.Educations.Add(education);
                }
            }

            // 转换 work_infos

            if (workInfos != null)
            {
                foreach (var item in workInfos)
                {
                    var workDict = item.ToObject<Dictionary<string, string>>();
                    WorkExperience workExperience = new WorkExperience
                    {
                        ApplicantID = applicant.ID, // 设置 ApplicantID
                        Company = workDict.GetValueOrDefault("company"),
                        Location = workDict.GetValueOrDefault("location"),
                        Job = workDict.GetValueOrDefault("job"),
                        Package = workDict.GetValueOrDefault("package"),
                        StartTime = workDict.GetValueOrDefault("start_time"),
                        EndTime = workDict.GetValueOrDefault("end_time"),
                        IsIn = workDict.GetValueOrDefault("is_in"),
                        Describe = workDict.GetValueOrDefault("describe")
                    };
                    //applicant.WorkExperiences.Add(workExperience);
                }
            }



            using var transaction = _context.Database.BeginTransaction(); // 开始事务


            try
            {
                // 添加 Applicant 实例到数据库
                _context.Applicants.Add(applicant);
                _context.SaveChanges(); // 保存更改，这时 EF Core 会填充 applicant.ID



                Console.WriteLine("***************************");

                // 处理并添加 Education 记录
                if (educationInfos != null)
                {
                    foreach (var item in educationInfos)
                    {
                        var educationDict = item.ToObject<Dictionary<string, string>>();
                        Education education = new Education
                        {
                            ApplicantID = applicant.ID,
                            School = educationDict.ContainsKey("school") ? educationDict["school"] : null,
                            College = educationDict.ContainsKey("college") ? educationDict["college"] : null,
                            Major = educationDict.ContainsKey("major") ? educationDict["major"] : null,
                            SchoolingRecord = educationDict.ContainsKey("schooling_record") ? educationDict["schooling_record"] : null,
                            Degree = educationDict.ContainsKey("degree") ? educationDict["degree"] : null,
                            StartTime = educationDict.ContainsKey("start_time") ? educationDict["start_time"] : null,
                            EndTime = educationDict.ContainsKey("end_time") ? educationDict["end_time"] : null,
                            IsIn = educationDict.ContainsKey("is_in") ? educationDict["is_in"] : null,
                            Gpa = educationDict.ContainsKey("gpa") ? educationDict["gpa"] : null,
                            Rank = educationDict.ContainsKey("rank") ? educationDict["rank"] : null
                        };
                        applicant.Educations.Add(education);
                    }
                }

                // 处理并添加 WorkExperience 记录
                if (workInfos != null)
                {
                    foreach (var item in workInfos)
                    {
                        var workDict = item.ToObject<Dictionary<string, string>>();
                        WorkExperience workExperience = new WorkExperience
                        {
                            ApplicantID = applicant.ID,
                            Company = workDict.ContainsKey("company") ? workDict["company"] : null,
                            Location = workDict.ContainsKey("location") ? workDict["location"] : null,
                            Job = workDict.ContainsKey("job") ? workDict["job"] : null,
                            Package = workDict.ContainsKey("package") ? workDict["package"] : null,
                            StartTime = workDict.ContainsKey("start_time") ? workDict["start_time"] : null,
                            EndTime = workDict.ContainsKey("end_time") ? workDict["end_time"] : null,
                            IsIn = workDict.ContainsKey("is_in") ? workDict["is_in"] : null,
                            Describe = workDict.ContainsKey("describe") ? workDict["describe"] : null
                        };

                        applicant.WorkExperiences.Add(workExperience);
                    }
                }





                Console.WriteLine("***************************");


                //Console.WriteLine("applicant.ID:", applicant.ID);

                //if (applicant.ID == 16)
                //{
                //    Console.WriteLine("数字16");
                //}


                // 创建 ApplicantProfile 实例
                ApplicantProfile applicantProfile = new ApplicantProfile
                {
                    ApplicantID = applicant.ID,
                    JobMatches = new List<JobMatch>(), // 初始化 JobMatches 集合
                                                       // 初始化其他集合
                    PersonalCharacteristics = new PersonalCharacteristics { Characteristics = new List<Characteristic>() },
                    SkillsAndExperiences = new SkillsAndExperiences { Characteristics = new List<Characteristic>() },
                    AchievementsAndHighlights = new AchievementsAndHighlights { Characteristics = new List<Characteristic>() }
                };



                _context.ApplicantProfiles.Add(applicantProfile);
                _context.SaveChanges(); // 保存更改，填充 applicantProfile.ID



                // 解析职位匹配（Job Match）数据
                string jobMatchJsonContent = System.IO.File.ReadAllText(jobMatch_jsonFilePath);
                JObject jobMatchParsedJson = JObject.Parse(jobMatchJsonContent);

                foreach (var jobMatchEntry in jobMatchParsedJson)
                {
                    string jobTitle = jobMatchEntry.Key;
                    int score = jobMatchEntry.Value["人岗匹配程度分数"].Value<int>();
                    string reason = jobMatchEntry.Value["人岗匹配的理由"].Value<string>();

                    JobMatch jobMatch = new JobMatch
                    {
                        JobTitle = jobTitle,
                        Score = score,
                        Reason = reason,
                        ApplicantProfileID = applicantProfile.ID
                    };

                    applicantProfile.JobMatches.Add(jobMatch); // 直接添加到 ApplicantProfile 的 JobMatches 集合
                }
                _context.SaveChanges(); // 保存更改


                // 解析人才特性（Talent Profile）数据
                string talentProfileJsonContent = System.IO.File.ReadAllText(talentProfile_jsonFilePath);
                JObject talentProfileParsedJson = JObject.Parse(talentProfileJsonContent);


                // 解析并填充个人特性
                JObject personalCharacteristicsJson = (JObject)talentProfileParsedJson["个人特性"];
                foreach (var item in personalCharacteristicsJson)
                {
                    Characteristic characteristic = new Characteristic
                    {
                        Name = item.Key,
                        Score = Convert.ToInt32(item.Value["分数"]),
                        Reason = item.Value["原因"].ToString()
                    };
                    applicantProfile.PersonalCharacteristics.Characteristics.Add(characteristic);
                }

                // 解析并填充技能和经验
                JObject skillsAndExperiencesJson = (JObject)talentProfileParsedJson["技能和经验"];
                foreach (var item in skillsAndExperiencesJson)
                {
                    Characteristic characteristic = new Characteristic
                    {
                        Name = item.Key,
                        Score = Convert.ToInt32(item.Value["分数"]),
                        Reason = item.Value["原因"].ToString()
                    };
                    applicantProfile.SkillsAndExperiences.Characteristics.Add(characteristic);
                }

                // 解析并填充成就和亮点
                JObject achievementsAndHighlightsJson = (JObject)talentProfileParsedJson["成就和亮点"];
                foreach (var item in achievementsAndHighlightsJson)
                {
                    Characteristic characteristic = new Characteristic
                    {
                        Name = item.Key,
                        Score = Convert.ToInt32(item.Value["分数"]),
                        Reason = item.Value["原因"].ToString()
                    };
                    applicantProfile.AchievementsAndHighlights.Characteristics.Add(characteristic);
                }

                // 由于 PersonalCharacteristics、SkillsAndExperiences 和 AchievementsAndHighlights
                // 是 ApplicantProfile 的一部分，因此无需单独保存
                // 只需保存 ApplicantProfile 即可
                _context.SaveChanges(); // 保存更改





                // 1. 获取与这个Resume相关的Company对象，假设你已经有了companyId或者其他标识符
                int companyId = 1;
                Company company = _context.Companies.FirstOrDefault(c => c.ID == companyId);

                if (company != null)
                {
                    // 2. 将Resume对象与Company对象关联
                    Resume resume = new Resume
                    {
                        ApplicantID = applicant.ID,
                        CreatedDate = DateTime.Now,
                        Filename = newFileName,
                        FilePath = filePath,
                        JobPositionID = jobId,
                        // ... 其他 Resume 字段的初始化 ...
                    };

                    // 3. 确保Company的Resumes集合不为null
                    if (company.Resumes == null)
                    {
                        company.Resumes = new List<Resume>();
                    }

                    // 添加 Resume 到数据库
                    _context.Resumes.Add(resume);

                    // 将新创建的Resume添加到Company的Resumes集合中
                    company.Resumes.Add(resume);

                    // 保存更改
                    _context.SaveChanges();
                }





                Console.WriteLine("***************************");

                var applicantInfo = new ApplicantInfo
                {
                    ID = applicant.ID,
                    Name = applicant.Name,
                    Gender = applicant.Gender,
                    Age = applicant.Age,
                    EmailAddress = applicant.EmailAddress,
                    PhoneNumber = applicant.PhoneNumber,
                    HighestDegree = applicant.HighestDegree,
                    School = applicant.School,
                    JobIntention = applicant.JobIntention,
                    Skills = applicant.Skills,
                    Honors = applicant.Honors,
                    EducationBackground = applicant.EducationBackground,
                    SchoolLevel = applicant.SchoolLevel,
                    WorkStability = applicant.WorkStability,
                    WorkStabilityReason = applicant.WorkStabilityReason,
                    Educations = applicant.Educations,
                    WorkExperiences = applicant.WorkExperiences
                };
                //return FirstAddResumeModelClass
                transaction.Commit(); // 提交事务
                return new FirstAddResumeModelClass
                {
                    Code = 20000,
                    applicantInfo = applicantInfo
                };

            }
            catch (Exception ex)
            {
                transaction.Rollback(); // 出错时回滚事务
                                        // 错误处理: 记录错误日志或返回错误消息
                return new FirstAddResumeModelClass
                {
                    Code = 60204,
                    applicantInfo = new ApplicantInfo()
                };
            }


            //// 输出 applicant 的所有属性信息以进行验证
            //Console.WriteLine("Applicant Information:");
            //Console.WriteLine($"Name: {applicant.Name}");
            //Console.WriteLine($"Gender: {applicant.Gender}");
            //Console.WriteLine($"Age: {applicant.Age}");
            //Console.WriteLine($"Current Duration: {applicant.CurrentDuration}");
            //Console.WriteLine($"Highest Degree: {applicant.HighestDegree}");
            //Console.WriteLine($"Work Begin Year: {applicant.WorkBeginYear}");
            //Console.WriteLine($"Current Company: {applicant.CurrentCompany}");
            //Console.WriteLine($"Current Job: {applicant.CurrentJob}");
            //Console.WriteLine($"Job Intention: {applicant.JobIntention}");
            //Console.WriteLine($"Phone Number: {applicant.PhoneNumber}");
            //Console.WriteLine($"Email Address: {applicant.EmailAddress}");
            //Console.WriteLine($"School: {applicant.School}");
            //Console.WriteLine($"Major: {applicant.Major}");
            //Console.WriteLine($"Self Evaluation: {applicant.SelfEvaluation}");
            //Console.WriteLine($"Skills: {applicant.Skills}");
            //// 打印其他可能的字段，如 IdNumber, Location, BirthDate 等，如果已经赋值的话

            //// 输出 Education 信息
            //Console.WriteLine("\nEducation Details:");
            //foreach (var edu in applicant.Educations)
            //{
            //    Console.WriteLine($"  School: {edu.School}, Major: {edu.Major}, Start Time: {edu.StartTime}, End Time: {edu.EndTime}");
            //    // 打印其他 Education 字段，如 Degree, SchoolingRecord 等，如果已经赋值的话
            //}

            //// 输出 WorkExperience 信息
            //Console.WriteLine("\nWork Experience Details:");
            //foreach (var work in applicant.WorkExperiences)
            //{
            //    Console.WriteLine($"  Company: {work.Company}, Job: {work.Job}, Start Time: {work.StartTime}, End Time: {work.EndTime}, Description: {work.Describe}");
            //    // 打印其他 WorkExperience 字段，如 Location, Package 等，如果已经赋值的话
            //}




            //// 返回处理结果
            //return Ok("可以了");
        }


        [HttpGet("DataBaseInitialize")]
        public IActionResult TestFileConversion()
        {

            // 创建一个新的 Company 实例
            Company newCompany = new Company
            {
                Name = "武汉大学", // 替换为实际公司名称
                Email = "xxx@163.com", // 替换为实际公司电子邮箱
            };

            // 将新的 Company 添加到数据库
            _context.Companies.Add(newCompany);
            

            var user = new User
            {
                CompanyID = newCompany.ID,
                Account = "1",
                Password = "1",
                Role = "admin",
                Company = newCompany,
            };
            _context.Users.Add(user);
            _context.SaveChanges(); 

            // 示例 JSON 字符串，实际应替换为您的 JSON 数据
            string jsonPositions = "{\r\n  \"产品运营\": {\r\n    \"要求\": \"至少2年的运营经验，电商背景优先。\",\r\n    \"技能要求\": \"数据分析和项目管理能力。\",\r\n    \"其他要求\": \"自我驱动力，逻辑思维清晰，强沟通能力。\"\r\n  },\r\n  \"平面设计师\": {\r\n    \"要求\": \"至少大专学历，1-2年相关工作经验。\",\r\n    \"技能要求\": \"熟练掌握设计软件和视频剪辑。\",\r\n    \"其他要求\": \"具备创新思维，良好的沟通能力和责任感。\"\r\n  },\r\n  \"财务专员\": {\r\n    \"要求\": \"本科及以上学历，金融或相关专业。\",\r\n    \"技能要求\": \"了解财务、会计、税收政策知识，具备风控经验。\",\r\n    \"其他要求\": \"具有数据分析能力和EXCEL的熟练操作。\"\r\n  },\r\n  \"市场营销\": {\r\n    \"要求\": \"本科及以上学历，至少3年相关经验。\",\r\n    \"技能要求\": \"熟练使用办公软件，能够管理客户关系。\",\r\n    \"其他要求\": \"有市场策划和拓展经验，以及良好的执行力。\"\r\n  },\r\n  \"开发工程师\": {\r\n    \"要求\": \"本科及以上学历，计算机相关专业。\",\r\n    \"技能要求\": \"至少3年的软件开发经验，熟练使用JAVA，有APP开发经验。\"\r\n  },\r\n  \"人力资源管理\": {\r\n    \"要求\": \"大专及以上学历，至少1年相关经验。\",\r\n    \"技能要求\": \"熟练使用办公软件，具备日常管理和档案处理能力。\",\r\n    \"其他要求\": \"了解人力资源流程和法规，具有全面的沟通能力。\"\r\n  }\r\n}"; // 这里填入您的JSON数据
            JObject jobPositions = JObject.Parse(jsonPositions);

            foreach (var position in jobPositions)
            {
                string title = position.Key;
                string requirements = position.Value["要求"]?.ToString();
                string skillRequirements = position.Value["技能要求"]?.ToString();
                string otherRequirements = position.Value["其他要求"]?.ToString();

                // 拼接描述信息
                string description = $"要求: {requirements}\n技能要求: {skillRequirements}";
                if (!string.IsNullOrEmpty(otherRequirements))
                {
                    description += $"\n其他要求: {otherRequirements}";
                }

                JobPosition jobPosition = new JobPosition
                {
                    Title = title, 
                    Description = description,
                    CreatedDate = DateTime.Now, // 设置当前时间为岗位创建日期
                    CompanyID = 1,                            // 设置其他必要的属性，如 CompanyID
                                               
                };

                // 添加岗位到数据库
                _context.JobPositions.Add(jobPosition);
            }

            _context.SaveChanges(); // 保存更改

            return Ok("数据库初始化完成");

        }

        [HttpPost("AddApplicant")]
        public FirstAddResumeModelClass AddApplicant()
        {
            string basicInfo_jsonFilePath = @"D:\visualStudio workspace\Intelligent-Resume-Parsing-System\Backend_IntelligentResumeAnalysisSystem\Resumes\2023\11\Analysis_Results\Basic_Infos\8fca77db48ab2a2cc86ba3be7ee348f9.json";
            string jobMatch_jsonFilePath = @"D:\visualStudio workspace\Intelligent-Resume-Parsing-System\Backend_IntelligentResumeAnalysisSystem\Resumes\2023\11\Analysis_Results\GPT_Job_Matching\8fca77db48ab2a2cc86ba3be7ee348f9.json";
            string talentProfile_jsonFilePath = @"D:\visualStudio workspace\Intelligent-Resume-Parsing-System\Backend_IntelligentResumeAnalysisSystem\Resumes\2023\11\Analysis_Results\GPT_Talent_Portraits\8fca77db48ab2a2cc86ba3be7ee348f9.json";
            string educationandworkexperience_jsonFilePath = @"D:\visualStudio workspace\Intelligent-Resume-Parsing-System\Backend_IntelligentResumeAnalysisSystem\Resumes\2023\11\Analysis_Results\Baidu_Analysis\8fca77db48ab2a2cc86ba3be7ee348f9.json";
            string newFileName = "test.docx";
            string filePath = "test"; 
            int jobId = 1;

            string basicInfo_jsonContent = System.IO.File.ReadAllText(basicInfo_jsonFilePath);
            JObject basicInfos = JObject.Parse(basicInfo_jsonContent);
            string educationandworkexperience_jsonContent = System.IO.File.ReadAllText(educationandworkexperience_jsonFilePath);
            JObject educationandworkexperienceInfos = JObject.Parse(educationandworkexperience_jsonContent);

            ////// 提取所需信息

            JArray educationInfos = (JArray)educationandworkexperienceInfos["results"][0]["extract"]["education_infos"];
            JArray workInfos = (JArray)educationandworkexperienceInfos["results"][0]["extract"]["work_infos"];

            // 创建 Applicant 对象
            Applicant applicant = new Applicant
            {
                // 赋值 basicInfos
                Name = basicInfos["姓名"]?.ToString(),
                Gender = basicInfos["性别"]?.ToString(),
                Age = basicInfos["年龄"]?.ToObject<int>() ?? 0, // 使用 ?? 0 来处理可能的空值
                EmailAddress = basicInfos["个人邮箱"]?.ToString(),
                PhoneNumber = basicInfos["手机号"]?.ToString(),
                HighestDegree = basicInfos["最高学历"]?.ToString(),
                School = basicInfos["毕业院校"]?.ToString(),
                JobIntention = basicInfos["求职意向岗位"]?.ToString(),
                Skills = basicInfos["技能证书"]?.ToString(),
                Honors = basicInfos["获奖荣誉"]?.ToString(),
                EducationBackground = basicInfos["教育背景"]?.ToString(),
                SchoolLevel = basicInfos["最高学历学校等级"]?.ToString(),
                WorkStability = basicInfos["工作稳定性程度"]?.ToString(),
                WorkStabilityReason = basicInfos["工作稳定性判断的原因"]?.ToString(),

                // 初始化集合
                Educations = new List<Education>(),
                WorkExperiences = new List<WorkExperience>(),
            };

            using var transaction = _context.Database.BeginTransaction(); // 开始事务


            try
            {
                // 添加 Applicant 实例到数据库
                _context.Applicants.Add(applicant);
                _context.SaveChanges(); // 保存更改，这时 EF Core 会填充 applicant.ID

                Console.WriteLine("***************************");
                // 处理并添加 Education 记录
                if (educationInfos != null)
                {
                    foreach (var item in educationInfos)
                    {
                        var educationDict = item.ToObject<Dictionary<string, string>>();
                        Education education = new Education
                        {
                            ApplicantID = applicant.ID,
                            School = educationDict.ContainsKey("school") ? educationDict["school"] : null,
                            College = educationDict.ContainsKey("college") ? educationDict["college"] : null,
                            Major = educationDict.ContainsKey("major") ? educationDict["major"] : null,
                            SchoolingRecord = educationDict.ContainsKey("schooling_record") ? educationDict["schooling_record"] : null,
                            Degree = educationDict.ContainsKey("degree") ? educationDict["degree"] : null,
                            StartTime = educationDict.ContainsKey("start_time") ? educationDict["start_time"] : null,
                            EndTime = educationDict.ContainsKey("end_time") ? educationDict["end_time"] : null,
                            IsIn = educationDict.ContainsKey("is_in") ? educationDict["is_in"] : null,
                            Gpa = educationDict.ContainsKey("gpa") ? educationDict["gpa"] : null,
                            Rank = educationDict.ContainsKey("rank") ? educationDict["rank"] : null
                        };

                        applicant.Educations.Add(education);
                    }
                }

                // 处理并添加 WorkExperience 记录
                if (workInfos != null)
                {
                    foreach (var item in workInfos)
                    {
                        var workDict = item.ToObject<Dictionary<string, string>>();
                        WorkExperience workExperience = new WorkExperience
                        {
                            ApplicantID = applicant.ID,
                            Company = workDict.ContainsKey("company") ? workDict["company"] : null,
                            Location = workDict.ContainsKey("location") ? workDict["location"] : null,
                            Job = workDict.ContainsKey("job") ? workDict["job"] : null,
                            Package = workDict.ContainsKey("package") ? workDict["package"] : null,
                            StartTime = workDict.ContainsKey("start_time") ? workDict["start_time"] : null,
                            EndTime = workDict.ContainsKey("end_time") ? workDict["end_time"] : null,
                            IsIn = workDict.ContainsKey("is_in") ? workDict["is_in"] : null,
                            Describe = workDict.ContainsKey("describe") ? workDict["describe"] : null
                        };

                        applicant.WorkExperiences.Add(workExperience);
                    }
                }

                // 创建 ApplicantProfile 实例
                ApplicantProfile applicantProfile = new ApplicantProfile
                {
                    ApplicantID = applicant.ID,
                    JobMatches = new List<JobMatch>(), // 初始化 JobMatches 集合
                                                       // 初始化其他集合
                    PersonalCharacteristics = new PersonalCharacteristics
                    {
                        Characteristics = new List<Characteristic>() // 初始化 Characteristics 集合
                    },
                    SkillsAndExperiences = new SkillsAndExperiences { Characteristics = new List<Characteristic>() },
                    AchievementsAndHighlights = new AchievementsAndHighlights{ Characteristics = new List<Characteristic>() }
                };

                _context.ApplicantProfiles.Add(applicantProfile);
                _context.SaveChanges(); // 保存更改
                // 解析职位匹配（Job Match）数据
                string jobMatchJsonContent = System.IO.File.ReadAllText(jobMatch_jsonFilePath);
                JObject jobMatchParsedJson = JObject.Parse(jobMatchJsonContent);

                foreach (var jobMatchEntry in jobMatchParsedJson)
                {
                    string jobTitle = jobMatchEntry.Key;
                    int score = jobMatchEntry.Value["人岗匹配程度分数"].Value<int>();
                    string reason = jobMatchEntry.Value["人岗匹配的理由"].Value<string>();

                    JobMatch jobMatch = new JobMatch
                    {
                        JobTitle = jobTitle,
                        Score = score,
                        Reason = reason,
                        ApplicantProfileID = applicantProfile.ID
                    };

                    applicantProfile.JobMatches.Add(jobMatch); // 直接添加到 ApplicantProfile 的 JobMatches 集合
                }



                // 解析人才特性（Talent Profile）数据
                string talentProfileJsonContent = System.IO.File.ReadAllText(talentProfile_jsonFilePath);
                JObject talentProfileParsedJson = JObject.Parse(talentProfileJsonContent);


                // 解析并填充个人特性
                JObject personalCharacteristicsJson = (JObject)talentProfileParsedJson["个人特性"];
                foreach (var item in personalCharacteristicsJson)
                {
                    Characteristic characteristic = new Characteristic
                    {
                        Name = item.Key,
                        Score = Convert.ToInt32(item.Value["分数"]),
                        Reason = item.Value["原因"].ToString()
                    };
                    applicantProfile.PersonalCharacteristics.Characteristics.Add(characteristic);
                }
                applicantProfile.PersonalCharacteristics.ApplicantProfileID = applicantProfile.ID;
                _context.SaveChanges(); // 保存更改

                // 解析并填充技能和经验
                JObject skillsAndExperiencesJson = (JObject)talentProfileParsedJson["技能和经验"];
                foreach (var item in skillsAndExperiencesJson)
                {
                    Characteristic characteristic = new Characteristic
                    {
                        Name = item.Key,
                        Score = Convert.ToInt32(item.Value["分数"]),
                        Reason = item.Value["原因"].ToString()
                    };
                    applicantProfile.SkillsAndExperiences.Characteristics.Add(characteristic);
                }
                applicantProfile.SkillsAndExperiences.ApplicantProfileID = applicantProfile.ID;
                _context.SaveChanges(); // 保存更改

                // 解析并填充成就和亮点
                JObject achievementsAndHighlightsJson = (JObject)talentProfileParsedJson["成就和亮点"];
                foreach (var item in achievementsAndHighlightsJson)
                {
                    Characteristic characteristic = new Characteristic
                    {
                        Name = item.Key,
                        Score = Convert.ToInt32(item.Value["分数"]),
                        Reason = item.Value["原因"].ToString()
                    };
                    applicantProfile.AchievementsAndHighlights.Characteristics.Add(characteristic);
                }
                applicantProfile.AchievementsAndHighlights.ApplicantProfileID = applicantProfile.ID;

                // 由于 PersonalCharacteristics、SkillsAndExperiences 和 AchievementsAndHighlights
                // 是 ApplicantProfile 的一部分，因此无需单独保存
                // 只需保存 ApplicantProfile 即可



                _context.SaveChanges(); // 保存更改



                // 1. 获取与这个Resume相关的Company对象，假设你已经有了companyId或者其他标识符
                int companyId = 1;
                Company company = _context.Companies.FirstOrDefault(c => c.ID == companyId);

                if (company != null)
                {
                    // 2. 将Resume对象与Company对象关联
                    Resume resume = new Resume
                    {
                        ApplicantID = applicant.ID,
                        CreatedDate = DateTime.Now,
                        Filename = newFileName,
                        FilePath = filePath,
                        JobPositionID = jobId,
                        // ... 其他 Resume 字段的初始化 ...
                    };

                    // 3. 确保Company的Resumes集合不为null
                    if (company.Resumes == null)
                    {
                        company.Resumes = new List<Resume>();
                    }

                    // 添加 Resume 到数据库
                    _context.Resumes.Add(resume);

                    // 将新创建的Resume添加到Company的Resumes集合中
                    company.Resumes.Add(resume);

                    // 保存更改
                    _context.SaveChanges();
                }

                Console.WriteLine("***************************");

                var applicantInfo = new ApplicantInfo
                {
                    ID = applicant.ID,
                    Name = applicant.Name,
                    Gender = applicant.Gender,
                    Age = applicant.Age,
                    EmailAddress = applicant.EmailAddress,
                    PhoneNumber = applicant.PhoneNumber,
                    HighestDegree = applicant.HighestDegree,
                    School = applicant.School,
                    JobIntention = applicant.JobIntention,
                    Skills = applicant.Skills,
                    Honors = applicant.Honors,
                    EducationBackground = applicant.EducationBackground,
                    SchoolLevel = applicant.SchoolLevel,
                    WorkStability = applicant.WorkStability,
                    WorkStabilityReason = applicant.WorkStabilityReason,
                    Educations = applicant.Educations,
                    WorkExperiences = applicant.WorkExperiences
                };
                //return FirstAddResumeModelClass
                transaction.Commit(); // 提交事务
                return new FirstAddResumeModelClass
                {
                    Code = 20000,
                    applicantInfo = applicantInfo
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // 出错时回滚事务
                                        // 错误处理: 记录错误日志或返回错误消息
                return new FirstAddResumeModelClass
                {
                    Code = 60204,
                    applicantInfo = new ApplicantInfo()
                };
            }
        }


        [HttpPost("test")]
        public ActionResult test()
        {
            try
            {
                string originalResumePath = @"E:\study\C_Sharp\BigHomework\Intelligent-Resume-Parsing-System\Backend_IntelligentResumeAnalysisSystem\Resumes\2023\11\30\66d346157e1cc76b5370fb0d760ff7f2.docx"; // 应该根据实际情况设置路径


                // 获取不带扩展名的文件名
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalResumePath);

                // 获取原始文件的目录（父目录）
                var directoryInfo = new DirectoryInfo(Path.GetDirectoryName(originalResumePath));
                string parentDirectory = directoryInfo.Parent.FullName;

                string jsonFilePath = Path.Combine(parentDirectory, $"Analysis_Results\\Baidu_Analysis\\{fileNameWithoutExtension}.json");

                // 从 JSON 文件读取内容
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var educationandworkexperienceInfos = JObject.Parse(jsonData);

                // 提取教育和工作信息
                JArray educationInfos = (JArray)educationandworkexperienceInfos["results"][0]["extract"]["education_infos"];
                JArray workInfos = (JArray)educationandworkexperienceInfos["results"][0]["extract"]["work_infos"];

                Console.WriteLine(educationInfos);
                Console.WriteLine("*********************");
                Console.WriteLine(workInfos);

                // 这里可以进一步处理提取的信息

                return Ok("解析成功");
            }
            catch (Exception ex)
            {
                // 错误处理
                return BadRequest($"解析错误: {ex.Message}");
            }
        }




    }
}
