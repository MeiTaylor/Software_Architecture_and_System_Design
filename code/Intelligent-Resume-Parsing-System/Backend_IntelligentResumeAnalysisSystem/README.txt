Backend_IntelligentResumeAnalysis/
│
├── Controllers/                 # 控制器目录，处理HTTP请求
│   ├── 
│   └── 
│
├── Models/                      # 模型目录，包含实体模型和数据库上下文
│   ├── Applicant.cs
│   ├── Job.cs
│   └── 
│
├── Data/                        # 数据访问层目录，包含与数据库交互的代码
│   ├── Repository/              # 包含所有的仓库实现
│   │   ├── IApplicantRepository.cs
│   │   ├── ApplicantRepository.cs
│   │   ├── IJobRepository.cs
│   │   └── JobRepository.cs
│   └── DbContextInitializer.cs  # 用于初始化数据库的代码
│
├── Services/                    # 服务目录，包含业务逻辑处理
│   ├── IApplicantService.cs
│   ├── ApplicantService.cs
│   ├── IJobService.cs
│   └── JobService.cs
│
├── Migrations/                  # Entity Framework迁移目录
│   └── ...                      # EF Core迁移文件
│
├── Dtos/                        # 数据传输对象目录，用于封装客户端和服务器之间的数据交换
│   ├── ApplicantDto.cs
│   └── JobDto.cs
│
├── Utilities/                   # 实用工具类目录
│   ├── Constants.cs
│   └── HelperFunctions.cs
│
├── wwwroot/                     # 静态文件和资源目录
│   ├──
