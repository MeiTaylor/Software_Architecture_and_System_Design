<!-- 在这个界面可以查看所有以及上传的简历,实现查询等 -->
<template>
    <el-card shadow="always" :body-style="{'width':'100%'}">
        <el-row :gutter="10">
            <el-col :span="6">
                <el-input-number v-model="searchByAge" style="width: 75%;height: 40px;" :min="0" :max="100"
                    placeholder="按年龄大于X检索" />
            </el-col>
            <el-col :span="6">
                <el-select v-model="searchByEducation" class="m-2" clearable placeholder="按学历检索" size="large">
                    <el-option v-for="item in EducationOptions" :key="item.value" :label="item.label"
                        :value="item.value" />
                </el-select>
            </el-col>
            <el-col :span="6">
                <el-input-number v-model="searchByScore" style="width: 75%;height: 40px;" :min=" 0" :max="100"
                    placeholder="按匹配分数大于X检索" />
            </el-col>
            <!-- 测试
            <el-col :span="6">
                <el-button type="success" @click="openDrawer()">弹出抽屉</el-button>
                <el-button type="success" @click="consoleDetailedResume()">显示修改后的数据</el-button>
            </el-col> -->
        </el-row>

        <el-row :gutter="10" style="margin-top: 10px; margin-bottom: 10px;">
            <el-col :span="6">
                <el-input v-model="searchByName" placeholder="按姓名检索" />
            </el-col>
            <el-col :span="6">
                <el-input v-model="searchByJobIntention" placeholder="按求职意向检索" />
            </el-col>
            <el-col :span="8">
                <el-button type="primary" @click="search()" style="width: 55%;">搜索</el-button>
            </el-col>
        </el-row>

        <el-table @row-click="gotoTalentPortrait" :data="searchResumes" height="500px" style="width: 100%"
            :scrollbar-always-on="true">
            <el-table-column prop="name" label="姓名" width="180" />
            <el-table-column prop="gender" label="性别" width="180" />
            <el-table-column prop="age" label="年龄" width="180" />
            <el-table-column prop="highestEducation" label="学历" />
            <!-- <el-table-column prop="graduate" label="毕业院校" /> -->
            <el-table-column prop="phoneNumber" label="联系方式" />
            <el-table-column prop="jobIntention" label="求职意向" />
            <el-table-column prop="maxMatchingScore" label="匹配分数" />
        </el-table>
    </el-card>

    <!-- 插槽 -->
    <el-drawer v-model="visible" size="40%" :show-close="false">
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
            <detailedResume :detailedResume="detailedResume"></detailedResume>
            <!-- 在这里放一个按钮实现更新并修改 -->

            <div style="display: flex; justify-content: end;">
                <el-button type="primary" @click="visible=false">更新并退出</el-button>
            </div>
        </template>
    </el-drawer>
</template>

<script>
    import { defineComponent, onMounted, ref, getCurrentInstance } from 'vue'
    import detailedResume from '../../components/detailedResume.vue'
    import { useStore } from 'vuex'
    import axios from 'axios'
    import { useRouter } from 'vue-router'
    export default defineComponent({
        setup() {
            const { proxy } = getCurrentInstance()
            const store = useStore()
            const router = useRouter()

            let searchByAge = ref()
            let searchByEducation = ref()
            let searchByScore = ref()
            let searchByName = ref('')
            let searchByJobIntention = ref('')
            let EducationOptions = ref([
                {
                    label: '博士',
                    value: '博士'
                },
                {
                    label: '硕士',
                    value: '硕士'
                },

                {
                    label: '本科',
                    value: '本科'
                },

                {
                    label: '大专',
                    value: '大专'
                },
                {
                    label: '高中',
                    value: '高中'
                },
                {
                    label: '中专',
                    value: '中专'
                },
            ])
            let AllResumeData = ref([])
            let visible = ref(false) //是否打开抽屉
            let detailedResume = ref({
                "id": 0,
                "name": "string",
                "email": "string",
                "phoneNumber": "string",
                "age": 0,
                "gender": "string",
                "jobIntention": "string",
                "selfEvaluation": "string",
                "highestEducation": "string",
                "workStabilityReason": "string",
                "workStability": "string",
                "matchingScore": 0,
                "matchingReason": "string",
                "talentTraits": "string",
                "awards": [
                    {
                        "awardName": "string"
                    }
                ],
                "workExperience": [
                    {
                        "id": 0,
                        "applicantID": 0,
                        "companyName": "string",
                        "position": "string",
                        "time": "string",
                        "task": "string"
                    }
                ],
                "skillCertificate": [
                    {
                        "id": 0,
                        "applicantID": 0,
                        "skillName": "string"
                    }
                ],
                "educationBackgrounds": [
                    {
                        "id": 0,
                        "applicantID": 0,
                        "time": "string",
                        "school": "string",
                        "major": "string"
                    }
                ]
            })
            let searchResumes = ref([])
            const userId = store.state.userId
            const baseURL = store.state.baseURL
            const consoleDetailedResume = () => {
                console.log(`修改后的output->detailedResume`, detailedResume)
            }
            const openDrawer = () => {
                visible.value = true
            }

            const getAllInfo = () => {

                axios.post(baseURL + '/Resume/AllSimpleReusmes', { "id": userId })
                    .then(res => {
                        console.log(`output->ALlres.data`, res.data.simpleResumes)
                        AllResumeData.value = res.data.simpleResumes,
                            searchResumes.value = res.data.simpleResumes.reverse()

                    })
                // const res = await proxy.$api.getAllSimpleResume({ 'userId': userId })
                // console.log(`output->res`, res)
                // AllResumeData.value = res.simpleResumes
                // searchResumes.value = res.simpleResumes
                // console.log(`output->AllResumeData`, AllResumeData.value)
            }

            const search = () => {
                //先按年龄进行检索
                let tempResumes = AllResumeData.value
                console.log(`output->searchByAge.value`, searchByAge.value)
                if (searchByAge.value !== undefined && searchByAge.value !== null) {
                    tempResumes = tempResumes.filter(item => {

                        return item.age >= parseInt(searchByAge.value)
                    })
                }
                //接着按照学历检索
                console.log(`output->searchByEducation.value`, searchByEducation.value)
                if (searchByEducation.value !== undefined && searchByEducation.value !== '') {
                    tempResumes = tempResumes.filter(item => {
                        return item.highestEducation === searchByEducation.value
                    })
                }
                //接着按照匹配分数来检索

                if (searchByScore.value !== undefined && searchByScore.value !== null) {
                    tempResumes = tempResumes.filter(item => {
                        return parseInt(item.maxMatchingScore) >= parseInt(searchByScore.value)
                    })
                }

                //按姓名来匹配
                if (searchByName.value !== undefined && searchByName.value !== '') {
                    tempResumes = tempResumes.filter(item => {
                        return item.name === searchByName.value
                    })
                }

                //按求职意向来匹配
                if (searchByJobIntention.value !== undefined && searchByJobIntention.value !== '') {
                    tempResumes = tempResumes.filter(item => {
                        return item.jobIntention === searchByJobIntention.value
                    })
                }
                searchResumes.value = tempResumes
            }


            const updateDetailedResume = async () => {
                //先发送请求
                const res = await proxy.$api.updateDetailedResume(detailedResume.value)
                //然后关闭抽屉
                visible.value = false
            }

            const gotoTalentPortrait = (row) => {
                console.log(`output->searchResume`, row.rid)
                console.log(`output->row`, row)
                store.commit('updateRId', row.rid)
                router.push('/talentPortrait')
            }
            onMounted(() => {
                getAllInfo()
            })
            return {
                searchByAge,
                searchByEducation,
                searchByJobIntention,
                searchByName,
                searchByScore,
                searchResumes,
                EducationOptions,
                AllResumeData,
                visible,
                openDrawer,
                detailedResume,
                consoleDetailedResume,
                search,
                gotoTalentPortrait,
            }

        }
        , components: {
            detailedResume,
        }
    }
    )
</script>

<style lang="less" scoped>
    .el-card {
        height: 640px;

    }
</style>