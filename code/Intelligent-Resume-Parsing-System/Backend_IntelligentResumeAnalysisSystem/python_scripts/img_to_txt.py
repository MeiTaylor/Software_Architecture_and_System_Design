from ecloud import CMSSEcloudOcrClient
import json
import sys

from setup_resume_analysis_folders_and_files import *
from resume_to_image import *

accesskey = '4863f884aef84ea4a4af9895285b75ec' 
secretkey = '249b66cddeaa453f8c3689761476b08a'
url = 'https://api-wuxi-1.cmecloud.cn:8443'


def img_to_txt(input_img_path):

	# 先设置好文件夹和文件
    setup_resume_analysis_folders_and_files(input_img_path)
    
    # 基础目录是简历目录的上一级
    base_directory = os.path.dirname(os.path.dirname(input_img_path))

    # 定义文本转换的输出目录
    text_conversions_directory = os.path.join(base_directory, "Text_Conversions")

    # 确保文本转换目录存在
    os.makedirs(text_conversions_directory, exist_ok=True)

    # 准备输出文本文件路径
    file_name = os.path.basename(input_img_path)
    output_txt_path = os.path.join(text_conversions_directory, os.path.splitext(file_name)[0] + ".txt")



    print("正在从图片转化为txt")
    print(input_img_path)
    print(output_txt_path)
    requesturl = '/api/ocr/v1/webimage'
    try:
        ocr_client = CMSSEcloudOcrClient(accesskey, secretkey, url)
        response = ocr_client.request_ocr_service_file(requestpath=requesturl, imagepath=input_img_path)

        response_json = json.loads(response.text)  # 解析JSON
        words_info = response_json['body']['content']['prism_wordsInfo']  # 取出所有识别出的文字的信息

        with open(output_txt_path, 'w', encoding='utf-8') as file:
            for word_info in words_info:
                file.write(word_info['word'] + '\n')  # 将识别出的文字写入到文件中
    except ValueError as e:
        print(e)

    convert_to_jpg(input_img_path)  # 将图片转换为jpg格式



if __name__ == "__main__":
    # 检查是否有足够的命令行参数
    if len(sys.argv) != 2:
        print("命令行传入错误")
        sys.exit(1)

    input_img_path = sys.argv[1]
    img_to_txt(input_img_path)