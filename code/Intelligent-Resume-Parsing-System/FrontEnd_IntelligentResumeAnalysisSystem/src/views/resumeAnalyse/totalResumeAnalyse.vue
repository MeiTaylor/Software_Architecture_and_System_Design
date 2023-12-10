<!-- 这是简历统计可视化的组件 -->
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


    <el-row :gutter="10" class="row-line-chart">
        <el-col :span="8">
            <el-card shadow="always" class="line-chart"
                :body-style="{ 'width':'90%','margin-top':'20px','margin-left':'5px'}">
                <ChartAgeGroups></ChartAgeGroups>
            </el-card>
        </el-col>
        <el-col :span="8">
            <el-card shadow="always" class="line-chart" :body-style="{ 'width':'90%','margin-top':'20px'}">
                <Chart></Chart>
            </el-card>
        </el-col>
        <el-col :span="8">
            <el-card shadow="always" class="line-chart" :body-style="{ 'width':'90%','margin-top':'20px'}">
                <ChartWorkStability></ChartWorkStability>
            </el-card>
        </el-col>
    </el-row>

    <el-row :gutter="20" class="row-last">
        <el-col :span="12">
            <el-card shadow="always" class="newAddResume" :body-style="{width:'90%'}">
                <ChartMatchScores></ChartMatchScores>
            </el-card>
        </el-col>
        <el-col :span="12">
            <el-card shadow="always" style="padding-left: 30px;" class="newAddResume" :body-style="{width:'86%'}">
                <ChartEducation></ChartEducation>
            </el-card>
        </el-col>
    </el-row>

</template>

<script>
    import { defineComponent, onBeforeMount, onMounted, ref, getCurrentInstance } from 'vue'
    import { useStore } from 'vuex'
    import axios from 'axios'
    import ChartAgeGroups from '../../components/Chart/ChartAgeGroups.vue'
    import Chart from '../../components/Chart/Chart.vue'
    import ChartWorkStability from '../../components/Chart/ChartWorkStability.vue'
    import ChartMatchScores from '../../components/Chart/ChartMatchScores.vue'
    import ChartEducation from '../../components/Chart/ChartEducation.vue'
    export default defineComponent({
        components: {
            ChartAgeGroups,
            Chart,
            ChartWorkStability,
            ChartMatchScores,
            ChartEducation
        },
        setup() {
            const { proxy } = getCurrentInstance()
            const store = useStore()
            let resumesCount = ref()
            let jobsCount = ref()
            let newAddResumeList = ref([]) //新添加的resume

            const getHomepageInfo = () => {
                axios.post(store.state.baseURL + '/Home/statistics', { id: store.state.userId })
                    .then(res => {
                        resumesCount.value = res.data.totalResumes
                        jobsCount.value = res.data.totalJobs
                    })
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
        height: 450px;
        margin-bottom: 20px;

        .el-col {
            height: 450px;

            .line-chart {
                height: 450px;
            }
        }
    }

    .row-last {
        height: 450px;

        .el-col {
            height: 450px;

            .newAddResume {
                height: 450px;
            }
        }
    }

    .card-header {
        border-bottom: 1px #e4e7ed solid;
        padding-bottom: 10px;
        margin-bottom: 10px;
    }
</style>