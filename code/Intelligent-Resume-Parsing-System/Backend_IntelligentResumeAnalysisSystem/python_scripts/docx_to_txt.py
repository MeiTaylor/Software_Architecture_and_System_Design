import docx2txt
from setup_resume_analysis_folders_and_files import *
import sys

from resume_to_image import *

def docx_to_txt(input_docx_path):


    # 基础目录是简历目录的上一级
    base_directory = os.path.dirname(os.path.dirname(input_docx_path))

    # 定义文本转换的输出目录
    text_conversions_directory = os.path.join(base_directory, "Text_Conversions")

    # 确保文本转换目录存在
    os.makedirs(text_conversions_directory, exist_ok=True)

    # 准备输出文本文件路径
    file_name = os.path.basename(input_docx_path)
    output_txt_path = os.path.join(text_conversions_directory, os.path.splitext(file_name)[0] + ".txt")



    # 先设置好文件夹和文件
    setup_resume_analysis_folders_and_files(input_docx_path)

    # 使用docx2txt库读取docx文件内容
    text = docx2txt.process(input_docx_path)

    # 删除重复行
    lines = text.split('\n')
    unique_lines = []
    for line in lines:
        if line not in unique_lines:
            unique_lines.append(line)
    text = '\n'.join(unique_lines)

    # 将读取到的问题内容写入txt文件
    with open(output_txt_path, 'w', encoding='utf-8') as file:
        file.write(text)

    # 将docx转换为图片
    convert_docx_to_image(input_docx_path)    



if __name__ == "__main__":
    # 检查是否有足够的命令行参数
    if len(sys.argv) != 2:
        print("命令行输入错误")
        sys.exit(1)

    input_docx_path = sys.argv[1]
    docx_to_txt(input_docx_path)