<template>
    <el-header>
        <div class="l-content">
            <el-button @click="handleCollapse()" size="small">
                <el-icon :size="20">
                    <Menu></Menu>
                </el-icon>
            </el-button>
            <!-- 因为vuex里面的都已经全局注册了 -->
            <h3>{{$store.state.headTitle}}</h3>
        </div>
        <div class="r-content">
            <el-dropdown>
                <span class="el-dropdown-link">
                    <img class="user" src="../assets/img.jpg" alt="这是一张图片  ">
                    <el-icon class="el-icon--right">

                    </el-icon>
                </span>
                <template #dropdown>
                    <el-dropdown-menu>
                        <el-dropdown-item> 个人中心</el-dropdown-item>
                        <el-dropdown-item @click="exit()"> 退出</el-dropdown-item>
                    </el-dropdown-menu>
                </template>
            </el-dropdown>
        </div>
    </el-header>
</template>

<script>
    import { useStore, } from "vuex";
    import { getCurrentInstance, ref } from 'vue'
    import { useRouter } from 'vue-router'
    export default {
        setup() {
            const { proxy } = getCurrentInstance()
            const router = useRouter()
            let store = useStore()
            let getImgsrc = (user) => {
                // console.log(new URL(, import.meta.url).href)
                // console.log(new URL(`../assets/${user}.jpg`, import.meta.url).href)
                // return new URL(`../assets/${user}.jpg`, import.meta.url).href;
            }

            let handleCollapse = () => {
                store.commit("updateIsCollape")
            }
            const exit = () => {
                router.push('/login')
            }
            return {
                getImgsrc,
                handleCollapse,
                exit,
            }
        }
    }
</script>

<style lang="less" scoped>
    .r-content {

        img {
            height: 40px;
            width: 40px;
            border-radius: 50%;
        }
    }



    .el-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        /* background-color: #333; */
        background-color: #555;
        width: 100%;
    }

    .l-content {
        align-items: center;
        display: flex;

        &>h3 {
            margin: 10px;
            color: #fff;
        }
    }

    .el-menu {
        border-right: none;
    }
</style>