import axios from 'axios'
import config from '../config'
import { ElMessageBox } from 'element-plus'

const NETWORK_ERROR = '网络请求异常'
//创建一个axios实例
const service = axios.create({
    baseURL: config.baseApi //baseAPi: '/api'
})

//在请求之前做一些事情

service.interceptors.request.use((req) => {
    //可以自定义header
    //jwt-token认证的时候
    return req
})

//在请求之后做一些事情
service.interceptors.response.use((res) => {
    const { code, data, msg } = res.data
    console.log(res)
    //根据后端 协商 视情况而定
    if (code == 200) {
        return data
    } else {
        //网络请求错误
        ElMessageBox.error(msg || NETWORK_ERROR)
        return Promise.reject(msg || NETWORK_ERROR)
    }
})

//封装的核心函数
function request(options) {
    // options的格式如下
    // {
    //     method:'get',
    //     data:{

    //     },
    //     mock:false
    // }

    options.method = options.method || 'get'
    // if (options.method.toLowerCase() == 'get') {
    //     options.params = options.data //将传给后端的data换成自己data
    // }
    options.params = options.data
    //对mock的处理
    let isMock = config.mock
    if (typeof options.mock !== 'undefined') {
        isMock = options.mock
    }

    //对线上环境做处理
    if (config.env == 'prod') {
        //不给你用到mock的机会
        service.defaults.baseURL = config.baseApi
    } else {
        service.defaults.baseURL = isMock ? config.mockApi : config.baseApi
    }

    return service(options) //向后端发请求
}

export default request