'''
pip install pdfminer.six
'''
import sys
# 导入所需的库

from setup_resume_analysis_folders_and_files import *

from pdfminer.pdfparser import PDFParser
from pdfminer.pdfdocument import PDFDocument
from pdfminer.pdfpage import PDFPage


# from pdfminer.pdfparser import PDFParser, PDFDocument
from pdfminer.pdfdevice import PDFDevice
from pdfminer.pdfinterp import PDFResourceManager, PDFPageInterpreter
from pdfminer.converter import PDFPageAggregator
from pdfminer.layout import LTTextBoxHorizontal, LAParams
# from pdfminer.pdfinterp import PDFTextExtractionNotAllowed
from pdfminer.pdfpage import PDFTextExtractionNotAllowed

from resume_to_image import *

# 定义函数，接受输入的PDF文件路径和输出的txt文件路径
def pdf_to_txt(input_pdf_path):

    # 基础目录是简历目录的上一级
    base_directory = os.path.dirname(os.path.dirname(input_pdf_path))

    # 定义文本转换的输出目录
    text_conversions_directory = os.path.join(base_directory, "Text_Conversions")

    # 确保文本转换目录存在
    os.makedirs(text_conversions_directory, exist_ok=True)

    # 准备输出文本文件路径
    file_name = os.path.basename(input_pdf_path)
    output_txt_path = os.path.join(text_conversions_directory, os.path.splitext(file_name)[0] + ".txt")



    # 先设置好文件夹和文件
    setup_resume_analysis_folders_and_files(input_pdf_path)

    open(output_txt_path, 'w').close()  # 清空txt文件

    # 定义内部函数parse，处理PDF文件
    def parse(input_pdf_file, output_txt_file):
        
        # 用文件对象创建一个PDF文档分析器
        parser = PDFParser(input_pdf_file)
        # 创建一个PDF文档对象并将分析器作为参数传递
        doc = PDFDocument(parser)

        # 检查文档是否可以转成TXT，如果不可以就忽略
        if not doc.is_extractable:
            raise PDFTextExtractionNotAllowed
        else:
            # 创建PDF资源管理器，来管理共享资源
            rsrcmgr = PDFResourceManager()
            # 创建一个PDF设备对象
            laparams = LAParams()
            # 将资源管理器和设备对象聚合
            device = PDFPageAggregator(rsrcmgr, laparams=laparams)
            # 创建一个PDF解释器对象
            interpreter = PDFPageInterpreter(rsrcmgr, device)

            # 循环遍历列表，每次处理一个page内容
            for page in PDFPage.create_pages(doc):
                interpreter.process_page(page)
                # 接收该页面的LTPage对象
                layout = device.get_result()
                
                for x in layout:
                    try:
                        if isinstance(x, LTTextBoxHorizontal):
                            with open(output_txt_file, 'a', encoding='utf-8-sig') as f:
                                result = x.get_text()
                                # 删除任何前导/尾随的空格
                                result = result.strip()
                                # 如果行不为空，则写入文件
                                if result != '':
                                    f.write(result + "\n")
                    except:
                        print("Failed")

    convert_pdf_to_image(input_pdf_path)
    # 打开并处理PDF文件
    with open(input_pdf_path, 'rb') as pdf_file:
        parse(pdf_file, output_txt_path)





if __name__ == "__main__":
    # 检查是否有足够的命令行参数
    # if len(sys.argv) != 2:
    #     print("命令行传入错误")
    #     sys.exit(1)

    # input_pdf_path = sys.argv[1]
    input_pdf_path = r"E:\study\C_Sharp\BigHomework\Intelligent-Resume-Parsing-System\Backend_IntelligentResumeAnalysisSystem\Resumes\2023\11\30\8fca77db48ab2a2cc86ba3be7ee348f9.pdf"
    pdf_to_txt(input_pdf_path)