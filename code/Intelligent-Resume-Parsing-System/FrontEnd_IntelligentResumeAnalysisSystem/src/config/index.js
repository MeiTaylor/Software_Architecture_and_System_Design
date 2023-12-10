// 环境配置文件
// 一般在企业级项目里面有三个环境
// 开发环境
// 测试环境
// 线上环境

//当前的环境
const env = import.meta.MODE || 'development'

const EnvConfig = {
    development: {
        baseApi: 'https://www.fastmock.site/mock/e444a789312a567a964a0e08bd956a55/api',
        mockApi: 'https://www.fastmock.site/mock/e444a789312a567a964a0e08bd956a55/api',
    },
}
// console.log("这里是env", env)
export default {
    env,
    //mock的总开关
    mock: true,
    ...EnvConfig[env]
}
