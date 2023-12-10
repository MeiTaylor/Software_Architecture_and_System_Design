<template>


    <el-aside :width="$store.state.isCollapse ?  '200px' : '64px'">
        <h3 v-show="$store.state.isCollapse">智能简历解析系统</h3>
        <h3 v-show="!$store.state.isCollapse"><img src="../assets/img.jpg" alt=""></h3>
        <el-menu class="el-menu-vertical-demo" background-color="#545c64" text-color="#fff"
            :collapse="!$store.state.isCollapse" :collapse-transition="false">

            <el-menu-item :index="item.path" v-for="item in nochildren()" :key="item.path" @click="clickMenu(item)">
                <!-- <component class="icons" :is="item.icon"></component> 图标 -->
                <span>{{item.label}}</span>
            </el-menu-item>

            <el-sub-menu :index="item.path" v-for="item in hasChildren()" :key="item.path">
                <template #title>
                    <component :is="item.icon" class="icons"></component>
                    <span>{{item.label}}</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item :index="subItem.path" v-for="subItem in item.children" :key="subItem.path"
                        @click="clickMenu(subItem)">
                        <!-- <component :is=" subItem.icon" class="icons"></component> 图标 -->
                        <span>{{subItem.label}}</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-sub-menu>

        </el-menu>
    </el-aside>
</template>

<script>
    import { useRouter } from "vue-router";
    import { useStore } from 'vuex';
    export default {
        setup() {
            const list = [
                {
                    path: "/home",
                    name: "home",
                    label: "首页",
                    // icon: "user",
                    // url: "UserManage/UserManage",
                },
                {
                    path: "/other",
                    // icon: "location",
                    label: "简历分析",
                    children: [
                        {
                            path: "/resumeInfo",
                            name: "resumeInfo",
                            label: "简历信息",
                            // icon: "setting",
                            // url: "Other/PageOne",
                        },
                        {
                            path: "/uploadResume",
                            name: "uploadResume",
                            label: "上传简历",
                            // icon: "setting",
                            // url: "Other/Pagetwo",
                        },
                        {
                            path: "/totalResumeAnalyse",
                            name: "totalResumeAnalyse",
                            label: "统计可视化",
                            // icon: "setting",
                            // url: "Other/Pagetwo",
                        },
                        // {
                        //     path: "/talentPortrait",
                        //     name: "talentPortrait",
                        //     label: "人才画像",
                        //     // icon: "setting",
                        //     // url: "Other/Pagetwo",
                        // },
                    ]
                }
            ]
            const store = useStore()
            const router = useRouter()

            const nochildren = () => {
                return list.filter((item) => !item.children)
            }

            const hasChildren = () => {
                return list.filter((item) => item.children)
            }

            const clickMenu = (item) => {
                store.commit('updateHeadTitle', item.label)
                router.push(item.name)
            }
            return {
                nochildren,
                hasChildren,
                clickMenu,
            }
        },
    }
</script>

<style lang="less" scoped>
    h3 {
        text-align: center;
        color: white;
    }

    img {
        width: 40px;
        height: 40px;
    }

    .el-menu {
        border-right: none;
    }

    .icons {
        width: 18px;
        height: 18px;
    }
</style>