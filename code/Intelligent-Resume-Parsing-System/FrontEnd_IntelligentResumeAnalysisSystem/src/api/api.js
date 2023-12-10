/**
 * 对整个项目API的管理
 */
import request from "./request";
export default {
    //home组件 除图表外的所有所需信息
    getHomepageInfo(params) {
        return request({
            url: '/homepageInfo',
            method: 'get',
            data: params, //此时数据为{userId: }
        })
    },
    //resume部分=================
    // 获得所有简历的简单信息
    getAllSimpleResume(params) {
        return request({
            url: '/Resume/AllSimpleReusmes',
            method: 'get',
            data: params,//此时数据为{userId:},
        })
    },
    updateDetailedResume(params) {
        return request({
            url: '/Resume/secondUpload',
            method: 'post',
            data: params,
        })
    },
    login(params) {
        return request({
            url: '/login',
            method: 'get',
            data: params
        })
    }
}