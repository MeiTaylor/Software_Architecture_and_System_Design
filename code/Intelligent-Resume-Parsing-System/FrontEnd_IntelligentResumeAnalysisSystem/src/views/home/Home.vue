<template>
    <el-row :gutter="20" class="row-Count">
        <el-col :span="12">
            <el-card shadow="always" class="Counts" :body-style="{width:'100%'}">
                <!-- 简历数量 -->
                <el-row>
                    <el-col :span="12">
                        <Document style="width: 50%; height: 50%; margin-left: 40px; margin-top: 30px" />
                    </el-col>
                    <el-col :span="12">
                        <p style="color: #A8ABB2;font-size: 30px; margin-top: 25px; margin-bottom: 10px;">简历数量

                        </p>
                        <h1 style="margin-top: 0px;">
                            {{resumesCount}}
                        </h1>


                    </el-col>
                </el-row>
            </el-card>
        </el-col>
        <el-col :span="12">
            <!-- 岗位数量 -->
            <el-card shadow="always" class="Counts" :body-style="{width:'100%'}">
                <el-row :gutter=" 20">
                    <el-col :span="12">
                        <User style="width: 50%; height: 50%; margin-left: 40px; margin-top: 30px" />
                    </el-col>
                    <el-col :span="12">
                        <p style="color: #A8ABB2;font-size: 30px; margin-top: 25px; margin-bottom: 10px;">岗位数量

                        </p>
                        <h1 style="margin-top: 0px;">
                            {{jobsCount}}
                        </h1>
                    </el-col>
                </el-row>
            </el-card>
        </el-col>
    </el-row>


    <el-row class="row-line-chart">
        <el-col :span="24">
            <el-card shadow="always" class="line-chart" :body-style="{ 'width':'100%','margin-top':'40px'}">

                <ChartNewAdd />
            </el-card>
        </el-col>
    </el-row>

    <el-row :gutter="20" class="row-last">
        <el-col :span="12">
            <el-card shadow="always" class="newAddResume" :body-style="{width:'100%'}">
                <div class="card-header">
                    <span>近期上次简历</span>
                </div>
                <!-- 最近七天新添加的简历数 -->
                <ElTable :data="newAddResumeList" :border="false" style="width: 100%;" max-height="300px"
                    :show-header="true">
                    <el-table-column prop="resumeName" label="姓名" />
                    <el-table-column prop="highestEducation" label="最高学历" />
                    <el-table-column prop="jobIntention" label="求职意向" />
                    <el-table-column prop="uploadDate" label="上传时间" />
                </ElTable>
            </el-card>
        </el-col>
        <el-col :span="12">
            <el-card shadow="always" class="newAddResume" :body-style="{width:'100%'}">
                <chart></chart>
            </el-card>
        </el-col>
    </el-row>

</template>

<script>
    import { defineComponent, onBeforeMount, onMounted, ref, getCurrentInstance } from 'vue'
    import { useStore } from 'vuex'
    import axios from 'axios'
    import ChartNewAdd from '../../components/Chart/ChartNewAdd.vue'
    import Chart from '../../components/Chart/Chart.vue'
    export default defineComponent({
        components: {
            ChartNewAdd,
            Chart
        },
        setup() {
            const { proxy } = getCurrentInstance()
            const store = useStore()
            const baseURL = store.state.baseURL
            let resumesCount = ref()
            let jobsCount = ref()
            let newAddResumeList = ref([]) //新添加的resume


            const getHomepageInfo = () => {
                axios.post(baseURL + '/Home/statistics', {
                    "id": store.state.userId
                })
                    .then(res => {
                        console.log(`output->res.data`, res.data)
                        resumesCount.value = res.data.totalResumes
                        jobsCount.value = res.data.totalJobs
                        newAddResumeList.value = res.data.resumeHistory
                    })

                // const res = await proxy.$api.getHomepageInfo({ userId: 2 })
                // resumesCount.value = res.resumeCount
                // jobsCount.value = res.jobCount
                // newAddResumeList.value = res.newAddResumesList
                // console.log(`output->res.resumeCount`, res.resumeCount)
                // console.log(`output->res`, res)
            }
            onBeforeMount(() => {
                getHomepageInfo();
            })
            return {
                resumesCount,
                jobsCount,
                newAddResumeList
            }
        }
    })
</script>

<style lang="less" scoped>
    .row-Count {
        height: 200px;
        margin-bottom: 20px;

        .el-col {
            height: 200px;

            .Counts {
                height: 200px;
                width: 100%;
                display: flex;


            }
        }
    }

    .row-line-chart {
        height: 500px;
        margin-bottom: 20px;

        .el-col {
            height: 500px;

            .line-chart {
                height: 500px;
            }
        }
    }

    .row-last {
        height: 400px;

        .el-col {
            height: 400px;

            .newAddResume {
                height: 400px;
            }
        }
    }

    .card-header {
        border-bottom: 1px #e4e7ed solid;
        padding-bottom: 10px;
        margin-bottom: 10px;
    }
</style>