import os



# 用于检查和创建所有必要的目录和文件
def setup_resume_analysis_folders_and_files(resume_path):
    base_path = os.path.dirname(resume_path)
    
    parent_directory = os.path.dirname(base_path)

    # 需要创建的目录名称，排除了原始简历文件夹
    directories = [
        "Text_Conversions", 
        "Analysis_Results/Baidu_Analysis", 
        "Analysis_Results/Basic_Infos", 
        "Analysis_Results/GPT_Talent_Portraits", 
        "Analysis_Results/GPT_Job_Matching", 
        "Analysis_Results/Prompt_Texts"
    ]

    # 检查并创建目录
    for directory in directories:
        # 创建完整路径
        full_path = os.path.join(parent_directory, directory)
        # 检查目录是否存在
        if not os.path.exists(full_path):
            # 如果不存在，创建目录
            os.makedirs(full_path)
            print(f"已创建目录: {full_path}")
        else:
            print(f"目录已存在: {full_path}")

    # 检查并创建Prompt_Texts文件夹下的文件
    prompt_texts_path = os.path.join(parent_directory, "Analysis_Results", "Prompt_Texts")
    os.makedirs(prompt_texts_path, exist_ok=True)
    
    # 定义两个prompt的内容和文件名
    prompts = {
        "Job_Matching_Guide.txt": """
求职者匹配度评估指南

根据求职者的简历信息，对以下六个职位的要求进行详细匹配分析。为每个职位分配一个0到100的分数，以表示求职者的匹配程度。请提供明确的匹配理由，如果求职者不满足特定职位要求，分数应为0，并在匹配理由中详细说明。完成评估后，给出我一个json格式的回答来记录所有评分和理由。

职位及其要求如下：

{
  "1. 产品运营": {
    "要求": "至少2年的运营经验，电商背景优先。",
    "技能要求": "数据分析和项目管理能力。",
    "其他要求": "自我驱动力，逻辑思维清晰，强沟通能力。"
  },
  "2. 平面设计师": {
    "要求": "至少大专学历，1-2年相关工作经验。",
    "技能要求": "熟练掌握设计软件和视频剪辑。",
    "其他要求": "具备创新思维，良好的沟通能力和责任感。"
  },
  "3. 财务专员": {
    "要求": "本科及以上学历，金融或相关专业。",
    "技能要求": "了解财务、会计、税收政策知识，具备风控经验。",
    "其他要求": "具有数据分析能力和EXCEL的熟练操作。"
  },
  "4. 市场营销": {
    "要求": "本科及以上学历，至少3年相关经验。",
    "技能要求": "熟练使用办公软件，能够管理客户关系。",
    "其他要求": "有市场策划和拓展经验，以及良好的执行力。"
  },
  "5. 开发工程师": {
    "要求": "本科及以上学历，计算机相关专业。",
    "技能要求": "至少3年的软件开发经验，熟练使用JAVA，有APP开发经验。"
  },
  "6. 人力资源管理": {
    "要求": "大专及以上学历，至少1年相关经验。",
    "技能要求": "熟练使用办公软件，具备日常管理和档案处理能力。",
    "其他要求": "了解人力资源流程和法规，具有全面的沟通能力。"
  }
}

一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
JSON文件格式如下：

```json
{
  "产品运营": {
    "人岗匹配程度分数": 0,
    "人岗匹配的理由": ""
  },
  "平面设计师": {
    "人岗匹配程度分数": 0,
    "人岗匹配的理由": ""
  },
  "财务专员": {
    "人岗匹配程度分数": 0,
    "人岗匹配的理由": ""
  },
  "市场营销": {
    "人岗匹配程度分数": 0,
    "人岗匹配的理由": ""
  },
  "开发工程师": {
    "人岗匹配程度分数": 0,
    "人岗匹配的理由": ""
  },
  "人力资源管理": {
    "人岗匹配程度分数": 0,
    "人岗匹配的理由": ""
  }
}
```

请基于此指南和格式，结合求职者的简历信息，完成评估并生成

一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
""",
        "Talent_Portrait_Guide.txt": """

你是一个人力资源专员。您需要根据提供的简历内容，综合评价求职者的个人特性、技能和经验、成就和亮点。请您详细分析简历中的每一项内容，并依据以下指标和要求，给出您的评分和评价理由。评分系统是1到10分，1分代表非常弱，10分代表非常强。每项评分后需要附上具体的理由，这些理由必须是基于简历中的具体信息。完成评价后，请将结果整理成JSON格式输出。

以下是评价人才画像时应该参考的要求：
{
  "个人特性评价": {
    "自我驱动性": {
      "要求": "根据求职者展示的独立性和主动性来评分。",
      "具体例子": "请找出简历中反映这一特质的具体例子，并基于这些例子给出分数和理由。"
    },
    "适应能力": {
      "要求": "根据求职者适应新环境和新挑战的能力来评分。",
      "具体情况": "请提供简历中反映这一能力的具体情况，并据此给出分数和理由。"
    },
    "社交能力": {
      "要求": "根据求职者与人交往的能力来评分。",
      "实例": "请指出简历中体现这一能力的实例，并给出分数和理由。"
    }
  },
  "技能和经验评价": {
    "问题解决能力": {
      "要求": "根据求职者解决问题的能力来评分。",
      "具体案例": "请指出简历中体现这一技能的具体案例，并基于案例给出分数和理由。"
    },
    "团队协作能力": {
      "要求": "根据求职者在团队中的表现和协作能力来评分。",
      "相关经历": "请找出简历中的相关经历，并据此给出分数和理由。"
    },
    "创新思维": {
      "要求": "根据求职者展示的创意和创新能力来评分。",
      "具体例证": "请提供简历中的具体例证，并基于这些例证给出分数和理由。"
    }
  },
  "成就和亮点评价": {
    "领导潜力": {
      "要求": "根据求职者的领导经验和潜力来评分。",
      "具体例子": "请提供简历中的具体例子，如领导角色、成功的项目等，并给出分数和理由。"
    },
    "创新成果": {
      "要求": "根据求职者在创新方面的成果来评分。",
      "详情": "请提供求职者获得的奖项、认可或创新项目的详情，并据此给出分数和理由。"
    },
    "行业影响力": {
      "要求": "根据求职者在其专业领域的影响力和贡献来评分。",
      "相关成就和经历": "请指出简历中的相关成就和经历，并给出分数和理由。"
    }
  }
}


请确保您的评价准确、具体，并且完全基于简历中的信息。最后，请使用以下JSON格式输出您的评价结果：
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
```json
{
  "个人特性": {
    "自我驱动性": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    },
    "适应能力": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    },
    "社交能力": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    }
  },
  "技能和经验": {
    "问题解决能力": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    },
    "团队协

作能力": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    },
    "创新思维": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    }
  },
  "成就和亮点": {
    "领导潜力": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    },
    "创新成果": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    },
    "行业影响力": {
      "分数": "[在此处输入分数]",
      "原因": "[在此处输入评价理由]"
    }
  }
}
```

请根据简历中的实际内容替换"[在此处输入分数]"和"[在此处输入评价理由]"，以生成最终的评价报告。


一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
""",
        "Basic_Infos_Guide.txt": """
您好，您将接手的任务是对一份简历进行详细而具体的分析。该简历可能包含乱序排列的信息，但涵盖了所有必要的内容。请您根据以下详细要求，进行全面分析：

1. **姓名**：应包含完整姓名。
2. **性别**：标明是男性还是女性。
3. **年龄**：提供年龄数字。
4. **个人邮箱**：例如xxx@163.com或xxx@qq.com。
5. **手机号**：11位数字的手机号码。
6. **最高学历**：范围包括无、小学、初中、高中、中专、大专、本科、硕士、博士。
7. **毕业院校**：指明最高学历毕业院校。
8. **求职意向岗位**：明确所求工作类型。
9. **技能证书**：列出所获得的技能证书。
10. **获奖荣誉**：提及所获得的奖项和荣誉。
11. **教育背景**：详细描述教育经历。
12. **最高学历学校等级**：分类为985、211、普通一本、一本以下。
13. **工作稳定性程度**：根据以下标准评估——
    - 低：频繁工作跳槽，每份工作持续时间少于6个月。
    - 中下：较频繁工作跳槽，每份工作持续时间少于1年。
    - 中：偶尔工作跳槽，每份工作持续时间为1-2年。
    - 中上：稳定工作历程，每份工作持续时间为2-3年。
    - 高：非常稳定的工作历程，每份工作持续时间超过3年。
14. **工作稳定性判断的原因**：根据上述评分标准和求职者的工作历史，详细说明评价其工作稳定性的理由。请确保此处的说明不少于100字。

请确保您的分析结果与简历内容保持一致。最后，请按照以下JSON格式输出您的分析结果：
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
```json
{
    "姓名": "",
    "性别": "",
    "年龄": ,
    "个人邮箱": "",
    "手机号": "",
    "最高学历": "",
    "毕业院校": "",
    "求职意向岗位": "",
    "技能证书": "",
    "获奖荣誉": "",
    "教育背景": "",
    "最高学历学校等级": "",
    "工作稳定性程度": "",
    "工作稳定性判断的原因": ""
}


一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出
一定要保证输出为json格式！一定要保证输出为json格式！一定要有json格式的输出

"""
    }

    # 检查并创建文件
    for file_name, content in prompts.items():
        file_path = os.path.join(prompt_texts_path, file_name)
        if not os.path.exists(file_path):
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(content)
                print(f"文件 '{file_name}' 已创建.")
        else:
            print(f"文件 '{file_name}' 已存在.")






