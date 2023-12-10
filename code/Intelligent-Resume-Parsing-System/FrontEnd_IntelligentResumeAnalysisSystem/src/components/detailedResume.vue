<template>
    <div class="input">
        <span>姓名：</span><el-input style="width: 70%;" v-model="props.detailedResume.name" placeholder="Please input" />
    </div>
    <div class="input">
        <span>年龄：</span><el-input style="width: 70%;" v-model="props.detailedResume.age" placeholder="Please input" />
    </div>
    <div class="input">
        <span>性别：</span><el-input style="width: 70%;" v-model="props.detailedResume.gender"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>意向岗位：</span><el-input style="width: 70%;" v-model="props.detailedResume.jobIntention"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>电话号码：</span><el-input style="width: 70%;" v-model="props.detailedResume.phoneNumber"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>邮箱：</span><el-input style="width: 70%;" v-model="props.detailedResume.emailAddress"
            placeholder="Please input" />
    </div>
    <!-- <div class="input">
        <span>自我评价：</span><el-input style="width: 70%;" v-model="props.detailedResume.selfEvaluation"
            placeholder="Please input" />
    </div> -->
    <div class="input">
        <span>最高学历：</span><el-input style="width: 70%;" v-model="props.detailedResume.highestDegree"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>毕业学校：</span><el-input style="width: 70%;" v-model="props.detailedResume.school"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>学校等级：</span><el-input style="width: 70%;" v-model="props.detailedResume.schoolLevel"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>教育背景：</span><el-input style="width: 70%;" v-model="props.detailedResume.educationBackground"
            placeholder="Please input" />
    </div>
    <!-- <div>
        教育经历：
        <div style="margin-left: 50px;">
            经历1:<el-input style="width: 70%;" v-model="props.detailedResume.educationBackground"
                placeholder="Please input" />
        </div>
    </div> -->
    <div class="input">
        <span>工作稳定性：</span><el-input style="width: 70%;" v-model="props.detailedResume.workStability"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>工作稳定性理由：</span><el-input autosize type="textarea" style="width: 70%;"
            v-model="props.detailedResume.workStabilityReason" placeholder="Please input" />
    </div>
    <!-- <div class="input">
        <span>人岗匹配分数：</span><el-input style="width: 70%;" v-model="props.detailedResume.matchingScore"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>人岗匹配原因：</span><el-input style="width: 70%;" v-model="props.detailedResume.matchingReason"
            placeholder="Please input" />
    </div> -->
    <!-- <div class="input">
        <span>个人特点：</span><el-input style="width: 70%;" v-model="props.detailedResume.talentTraits"
            placeholder="Please input" />
    </div> -->
    <div class="input">
        <span>获奖荣誉 ：</span><el-input autosize type="textarea" style="width: 70%;" v-model="props.detailedResume.honors"
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>教育经历 ：</span><el-input autosize type="textarea" style="width: 70%;" v-model="educationExperience "
            placeholder="Please input" />
    </div>
    <div class="input">
        <span>工作经历 ：</span><el-input autosize type="textarea" style="width: 70%;" v-model="workExperience "
            placeholder="Please input" />
    </div>

    <!-- <div v-for="(award,index) in props.detailedResume.workExperiences" :key="index">
        <div class="input">
            <span> 工作公司{{index+1}}名称：</span><el-input style="width: 70%;"
                v-model="props.detailedResume.workExperience[index].companyName" placeholder="Please input" />
        </div>
        <div class="input">
            <span>工作公司{{index+1}}地点 ：</span><el-input style="width: 70%;"
                v-model="props.detailedResume.workExperience[index].position" placeholder="Please input" />
        </div>
        <div class="input">
            <span>工作经历{{index+1}}起止时间 ：</span><el-input style="width: 70%;"
                v-model="props.detailedResume.workExperience[index].time" placeholder="Please input" />
        </div>
        <div class="input">
            <span>工作经历{{index+1}}职位 ：</span><el-input style="width: 70%;"
                v-model="props.detailedResume.workExperience[index].task" placeholder="Please input" />
        </div>
    </div> -->
    <div class="input">
        <span>个人技能： </span><el-input style="width: 70%;" autosize type="textarea" v-model="props.detailedResume.skills"
            placeholder="Please input" />
    </div>

    <!-- <div v-for="(award,index) in props.detailedResume.educations" :key="index">
        <div class="input">
            <span> 教育经历{{index+1}}起止时间：</span><el-input style="width: 70%;"
                v-model="props.detailedResume.educationBackgrounds[index].time" placeholder="Please input" />
        </div>
        <div class="input">
            <span>教育经历{{index+1}}学校 ：</span><el-input style="width: 70%;"
                v-model="props.detailedResume.educationBackgrounds[index].school" placeholder="Please input" />
        </div>
        <div class="input">
            <span>教育经历{{index+1}}攻读学位 ：</span><el-input style="width: 70%;"
                v-model="props.detailedResume.educationBackgrounds[index].major" placeholder="Please input" />
        </div>
    </div> -->

</template>

<script setup>
    import { ref } from 'vue'
    //prop的数据流是单向的
    //一般来说，prop传进来的不可以修改，但是，如果传进来的是对象，数组，此时props绑定的不可以修改，但是，数组和对象里面的值却可以修改，并且会影响到父组件
    let props = defineProps({ 'detailedResume': Object })
    const num = 1;
    //console.log(`output->props.detailedResume`, props.detailedResume)
    let educationExperience = ref('')
    let workExperience = ref('')
    let educationInfo = ''
    let workInfo = '';
    //生成教育经历
    for (let i = props.detailedResume.educations.length - 1; i >= 0; i--) {
        educationInfo = educationInfo + `教育经历${props.detailedResume.educations.length - i}：\n`
        if (props.detailedResume.educations[i].isIn === "0") {
            educationInfo = educationInfo + `     ${props.detailedResume.educations[i].startTime}  至  ${props.detailedResume.educations[i].endTime}\n`
            educationInfo = educationInfo + `     ${props.detailedResume.educations[i].school}  ${props.detailedResume.educations[i].major}   ${props.detailedResume.educations[i].degree}\n`
        } else {
            educationInfo = educationInfo + `     ${props.detailedResume.educations[i].startTime}至今\n`
            educationInfo = educationInfo + `     ${props.detailedResume.educations[i].school}  ${props.detailedResume.educations[i].major}   ${props.detailedResume.educations[i].degree}\n`
        }
    }

    //生成工作经历
    for (let i = props.detailedResume.workExperiences.length - 1; i >= 0; i--) {
        workInfo = workInfo + `工作经历${props.detailedResume.workExperiences.length - i}：\n`
        if (props.detailedResume.workExperiences[i].isIn === "0") {
            workInfo = workInfo + `      ${props.detailedResume.workExperiences[i].startTime}  至  ${props.detailedResume.workExperiences[i].endTime}\n`
            workInfo = workInfo + `      ${props.detailedResume.workExperiences[i].company}     ${props.detailedResume.workExperiences[i].job}\n`
            workInfo = workInfo + `      详细描述：${props.detailedResume.workExperiences[i].describe}\n`
        } else {
            workInfo = workInfo + `      ${props.detailedResume.workExperiences[i].startTime}至今\n`
            workInfo = workInfo + `      ${props.detailedResume.workExperiences[i].company}  至  ${props.detailedResume.workExperiences[i].job}\n`
            workInfo = workInfo + `      详细描述：${props.detailedResume.workExperiences[i].describe}\n`
        }
    }
    workExperience.value = workInfo
    educationExperience.value = educationInfo
</script>

<style lang="less" scoped>
    .input {
        display: flex;
        margin-bottom: 10px;

        span {
            width: 130px;
            text-align: justify;
        }
    }
</style>