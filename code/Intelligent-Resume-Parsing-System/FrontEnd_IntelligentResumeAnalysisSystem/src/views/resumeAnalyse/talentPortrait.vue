<template>
    <el-row class="container-all" :gutter="20">
        <el-col :span="12">
            <el-card shadow="always">
                <div class="button">
                    <el-switch @change="switchChange" v-model="isImage" class="ml-2" size="large" active-text="简历"
                        inactive-text="解析结果" />
                </div>
                <div v-if="isImage" :class="isLoad?'img-isLoad' : 'img'">
                    <el-image scroll-container :infinite="true" :src="imgSrc" @load="changeLoad" :lazy="true" />
                </div>
                <div v-if="!isImage" class="img-isLoad">
                    <detailedResume :detailedResume="detailedResumes"></detailedResume>
                </div>
            </el-card>
        </el-col>
        <el-col style="height: 650px;overflow: auto;" :span="12">
            <el-row class="talent-row" :gutter="10">
                <el-col class="talent-col" :span="12">
                    <el-card class="talent-card" :body-style="{'width':'80%','height':'80%'}" shadow="always">
                        <ChartAchievementsAndHighlights :Rid="RId"></ChartAchievementsAndHighlights>
                    </el-card>
                </el-col>
                <el-col class="talent-col" :span="12">
                    <el-card class="talent-card" shadow="always">
                        <ChartCharacteristics :Rid="RId"></ChartCharacteristics>
                    </el-card>
                </el-col>
            </el-row>
            <el-row class="talent-row" :gutter="10">
                <el-col class="talent-col" :span="12">
                    <el-card class="talent-card" shadow="always">
                        <ChartJobMatchResult :RId="RId"></ChartJobMatchResult>
                    </el-card>
                </el-col>
                <el-col class="talent-col" :span="12">
                    <el-card class="talent-card" shadow="always">
                        <ChartSkillsAndExperiences :RId="RId"></ChartSkillsAndExperiences>
                    </el-card>
                </el-col>
            </el-row>
        </el-col>
    </el-row>
</template>

<script setup>
    import { ref, computed, onMounted } from 'vue'
    import { useStore } from 'vuex'
    import axios from 'axios'
    import detailedResume from '../../components/detailedResume.vue'
    import ChartAchievementsAndHighlights from '../../components/Chart/ChartAchievementsAndHighlights.vue'
    import ChartCharacteristics from '../../components/Chart/ChartCharacteristics.vue'
    import ChartJobMatchResult from '../../components/Chart/ChartJobMatchResult.vue'
    import ChartSkillsAndExperiences from '../../components/Chart/ChartSkillsAndExperiences.vue'
    const store = useStore()
    let isImage = ref(true)
    // let props = defineProps({ "rId": Number, })
    const RId = ref(store.state.rId)
    const baseURL = store.state.baseURL
    let detailedResumes = ref([])
    const imgSrc = computed(() => {
        return baseURL + '/resume_analysis/getGraph?resumeId=' + RId.value
    })
    let loadSuccess = ref(false)

    const switchChange = () => {
        loadSuccess.value = false
        console.log(`output->switchChange`)
    }
    //开局获得详细信息
    const getDetailedResume = () => {
        axios.post(baseURL + '/Resume/OneDetailedResumeInfo', { "id": RId.value })
            .then((res) => {
                console.log(`output->res.data`, res.data)
                detailedResumes.value = res.data
            })
    }
    const changeLoad = () => {
        loadSuccess.value = true
    }
    let isLoad = computed(() => {
        return isImage.value && loadSuccess.value
    })

    const test = () => {
        console.log(`output->test`, RId.value)
    }
    onMounted(() => {
        getDetailedResume()
    })

</script>
<style lang="less" scoped>
    .button {
        height: 20%;
        /* border-bottom: 1px #999 solid; */
    }

    .img {
        /* height: 100%; */
        width: 100%;
        height: 600px;
        /* overflow-y: auto;
        overflow: scroll; */
        /* 隐藏溢出部分 */
    }

    .img-isLoad {
        /* height: 100%; */
        width: 100%;
        height: 600px;
        overflow-y: auto;
        overflow: scroll;
        /* 隐藏溢出部分 */
    }



    .talent-row {
        width: 100%;
        height: 500px;
        margin-bottom: 10px;

        .talent-col {
            width: 100%;
            height: 500px;
        }
    }

    .container-all {
        height: 650px;


        .el-col {
            height: 100%;
            width: 100%;


            .el-card {
                height: 100%;
                width: 100%;
            }
        }
    }
</style>