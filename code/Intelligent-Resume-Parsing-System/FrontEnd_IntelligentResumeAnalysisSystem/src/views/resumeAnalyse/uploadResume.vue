<!-- 实现上传简历的组件 -->
<template>
    <div>
        <el-card shadow="always">
            <el-select style="margin-bottom:10px ;" v-model="selectJob" class="m-2" placeholder="选择该简历对应的岗位"
                size="large">
                <el-option v-for="item in jobList" :key="item.jobId" :label="item.jobName" :value="item.jobId" />
            </el-select>
            <el-upload class="upload-demo" drag action="https://run.mocky.io/v3/9d059bf9-4660-45f2-925d-ce80ad6c4d15"
                multiple :data="{'userId':userId} " :beforeUpload="handleUpload">
                <el-icon class="el-icon--upload"><upload-filled /></el-icon>
                <div class="el-upload__text">
                    拖动文件到此或 <em>点击上传</em>
                </div>
            </el-upload>
            <el-table :data="AllResumeData" height="455px" style="width: 100%" :scrollbar-always-on="true">
                <el-table-column prop="name" label="姓名" width="180" />
                <el-table-column prop="gender" label="性别" width="180" />
                <el-table-column prop="age" label="年龄" width="180" />
                <el-table-column prop="highestEducation" label="学历" />
                <el-table-column prop="phoneNumber" label="联系方式" />
                <el-table-column prop="jobIntention" label="求职意向" />
                <el-table-column prop="maxMatchingScore" label="匹配分数" />
            </el-table>
        </el-card>



        <!-- 插槽 -->
        <el-drawer v-model="visible" size="80%" :show-close="false">
            <template #header="{ close, titleId, titleClass }">
                <h4 :id="titleId" :class="titleClass">解析结果</h4>
                <!-- <el-button type="danger" @click="close">
                <el-icon class="el-icon--left">
                    <CircleCloseFilled />
                </el-icon>
                Close
            </el-button> -->
            </template>
            <template #default>
                <el-row :gutter="30">
                    <el-col :span="14">
                        <!-- <div :class="imgIsLoad?'img-isLoad':'img'">
                            <el-image scroll-container :infinite="true" :src="imgSrc" @load="imgIsLoad=true"
                                :lazy="true" />
                        </div> -->
                        <div :class="imgIsLoad?'img-isLoad' : 'img'">
                            <el-image style="width: 100%;" scroll-container :infinite="true" @load="imgIsLoad=true"
                                :src="imgSrc" :lazy="true" />
                        </div>
                    </el-col>
                    <el-col :span="10">
                        <div class="info">
                            <detailedResume :detailedResume="detailedResumeInfo"></detailedResume>
                            <!-- 在这里放一个按钮实现更新并修改 -->

                            <div style="display: flex; justify-content: end;">
                                <el-button type="primary" @click="ChangeDetaileResume">更新并退出</el-button>
                            </div>
                        </div>
                    </el-col>
                </el-row>

            </template>
        </el-drawer>
    </div>
</template>


<script setup>
    import { UploadFilled } from '@element-plus/icons-vue'
    import { ref, defineComponent, getCurrentInstance, onMounted, computed } from 'vue'
    import detailedResume from '../../components/detailedResume.vue'
    import axios from 'axios'
    import { useStore } from 'vuex'
    import { ElMessage } from 'element-plus'

    const store = useStore()
    const { proxy } = getCurrentInstance()

    //图片的思路
    //当获得ResumeId（visible来标识）后，发请求
    //修改src 此时的class还是原先的class
    //当load结束后再修改类为新的类
    //关闭时，也要将load修改为失败


    //从store里面拿出需要的数据
    const baseURL = store.state.baseURL
    const userId = store.state.userId

    let AllResumeData = ref([])
    let visible = ref(false)
    let selectJob = ref()

    let imgIsLoad = ref(false)
    let detailedResumeInfo = ref([])



    let Rid = ref()

    let imgSrc = computed(() => {
        //当成功上传时 URL才为正确的URL
        // return visible ? "" : baseURL + "/resume_analysis/getGraph?resumeId=" + Rid.value
        return baseURL + "/resume_analysis/getGraph?resumeId=" + Rid.value
    })


    let jobList = ref([])
    const getJobInfo = () => {
        axios.post(baseURL + '/Jobposition/first', { id: userId })
            .then((res) => {
                console.log(`output->job`, res.data)
                jobList.value = res.data.uploadNeedJobsInfo
            })
    }

    //获得近期上传的简历 开始时调用
    const getRecentResumesInfo = () => {
        axios.post(baseURL + '/Resume/AllSimpleReusmes', { 'id': userId })
            .then(res => {
                console.log(`output->res.data`, res.data)
                AllResumeData.value = res.data.simpleResumes.reverse()
            })
    }

    //处理上传文件
    const handleUpload = (file) => {
        if (selectJob.value === undefined) {
            ElMessage({
                showClose: true,
                message: '请选择对应的岗位.',
                type: 'error',
            })
            return false
        }
        ElMessage({
            message: '文件开始上传',
            type: 'success',
        })
        console.log(`output->file`, file)
        axios.post(baseURL + '/resume_analysis/UploadResume?userId=' + userId + '&jobId=' + selectJob.value, { 'file': file },
            {
                headers: {
                    'Content-Type': 'multipart/form-data' // 设置请求头为multipart/form-data
                }
            }).then((res) => {
                console.log(`output->resResume.data`, res.data)
                detailedResumeInfo.value = res.data.applicantInfo
                Rid.value = res.data.applicantInfo.id //修改了Rid src要用
                visible.value = true
            })
            .error((error) => {
                console.log(`output->网络错误`,)
            })


        return false //阻止文件上传

    }

    const ChangeDetaileResume = () => {
        console.log(`output->detailedResume.value`, detailedResumeInfo.value)
        axios.post(baseURL + '/Applicants/SecondUpload', { "code": 0, "applicantInfo": detailedResumeInfo.value })
            .then(res => {
                console.log(`output->secondUpload`, res.data)
                if (res.data.code === 20000) {
                    ElMessage({
                        message: '修改成功',
                        type: 'success',
                    })
                    getRecentResumesInfo()//重新更新一下页面上显示的简历
                    imgIsLoad.value = false
                    visible.value = false

                }
            })
    }



    onMounted(() => {
        getJobInfo()
        getRecentResumesInfo()
    })


</script>

<style>
    .img {
        /* height: 100%; */
        width: 100%;
        height: 630px;
        /* overflow-y: auto;
        overflow: scroll; */
        /* 隐藏溢出部分 */
    }

    .info {
        width: 100%;
        height: 630px;
        overflow-y: auto;
        overflow-x: hidden;
        overflow: scroll;
    }

    .img-isLoad {
        /* height: 100%; */
        width: 100%;
        height: 630px;
        overflow-y: auto;
        overflow: scroll;
        overflow-x: hidden;
        /* 隐藏溢出部分 */
    }
</style>