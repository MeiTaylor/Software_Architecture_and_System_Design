<!-- 人才画像--成就与亮点 -->
<template>
    <div>
        <div class="chart-container" ref="chart"></div>
        <div class="my-able">
            <el-table :data="tableData" style="width: 100% " height="220">
                <el-table-column prop="name" label="成就与亮点" width="100">
                </el-table-column>
                <el-table-column prop="score" label="分数" width="60px">
                </el-table-column>
                <el-table-column prop="reason" label="得分原因" width="500px">
                </el-table-column>
            </el-table>
        </div>
    </div>
</template>

<script>
    //待解决
    import * as echarts from 'echarts';
    import axios from 'axios';

    export default {
        name: 'ChartAchievementsAndHighlights',
        mounted() {
            this.renderChart();
        },
        data() {
            return {
                tableData: []
            }
        },
        props: ['Rid'],//从父组件活得userId
        methods: {
            renderChart() {
                const chartDom = this.$refs.chart;
                const myChart = echarts.init(chartDom);
                var option;


                axios.post(this.$store.state.baseURL + '/Applicants/graph/AchievementsAndHighlights', { id: this.Rid })
                    .then(response => {
                        console.log(response.data);
                        var data = response.data.characteristics;
                        console.log(data);


                        //将返回的数据赋予给data
                        for (var i = 0; i < 3; i++) {
                            this.tableData.push({ name: data[i].name, score: data[i].score, reason: data[i].reason })
                        }

                        console.log("===============", this.tableData)


                        var scoreData = []

                        //将这个动态赋值
                        for (var i = 0; i < 3; i++) {
                            if (data[i].name === "领导潜力") { scoreData.push(data[i].score); }
                            //if(data[i].name==="适应能力") {scoreData.push(data[i].score);continue;}
                            //if(data[i].name==="社交能力") {scoreData.push(data[i].score);continue;}

                        }
                        for (var i = 0; i < 3; i++) {
                            //if(data[i].name==="自我驱动性") {scoreData.push(data[i].score);}
                            if (data[i].name === "创新成果") { scoreData.push(data[i].score); continue; }
                            //if(data[i].name==="社交能力") {scoreData.push(data[i].score);continue;}

                        }
                        for (var i = 0; i < 3; i++) {
                            //if(data[i].name==="自我驱动性") {scoreData.push(data[i].score);}
                            //if(data[i].name==="适应能力") {scoreData.push(data[i].score);continue;}
                            if (data[i].name === "行业影响力") { scoreData.push(data[i].score); continue; }

                        }

                        option = {
                            title: {
                                top: '5%',
                                text: '人才画像--成就与亮点'
                            },
                            grid: {
                                top: '0%',
                                bottom: '5%',
                                left: '10%',
                                right: '10%'
                            },
                            // legend: {
                            //     //bottom: 'bottom',
                            //     top: "5%",
                            //     right: '0',
                            //     // orient: 'horizontal',
                            //     // top: 'bottom',
                            //     // left: 'center',
                            //     data: ['成就与亮点']
                            // },
                            radar: {
                                indicator: [
                                    { name: '领导潜力', max: 10 },
                                    { name: '创新成果', max: 10 },
                                    { name: '行业影响力', max: 10 }
                                ],
                                center: ['48.5%', '70%'],
                                radius: '71.5%',
                                triggerEvent: true // 开启触发事件
                            },
                            tooltip: {
                                // 添加提示框配置
                                trigger: 'item',
                                // formatter: function(params) {
                                // var value = params.value; // 获取数据项的值
                                // // 根据需要进行处理和展示
                                // return '信息值: ' + value[1];
                                // }
                                axisPointer: {
                                    // 坐标轴指示器，坐标轴触发有效
                                    type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
                                }
                            },
                            series: [
                                {
                                    top: "20px",
                                    name: '成就与亮点',
                                    type: 'radar',

                                    data: [
                                        {
                                            value: scoreData,
                                            name: '成就与亮点'
                                        }
                                    ]
                                }
                            ]
                        };



                        option && myChart.setOption(option);
                    })
            }
        }

    };
</script>

<style scoped>
    .chart-container {
        width: 100%;
        height: 250px;
        margin-bottom: 0;
        /* //    background-color: blue; */
    }

    .my-table {
        margin-top: 0;
    }
</style>