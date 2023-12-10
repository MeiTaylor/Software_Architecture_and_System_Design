<!-- <template>
    <div class="common-layout">
        <el-container>
            <CommonAside></CommonAside>
            <el-container>
                <CommonHeader></CommonHeader>
                <el-main><router-view></router-view></el-main>
            </el-container>
        </el-container>
    </div>
</template> -->
<template>
    <div class="common-layout">
        <el-container>
            <CommonAside></CommonAside>
            <el-container>
                <CommonHeader></CommonHeader>
                <el-main>
                    <router-view></router-view>
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>



<script>
    //引入对应组件
    import { defineComponent } from 'vue';
    import CommonHeader from '../components/CommonHeader.vue';
    import CommonAside from '../components/CommonAside.vue';
    export default defineComponent({
        components: {
            CommonHeader,
            CommonAside,
        },
    });
</script>

<style lang="less" scoped>
    .el-container {
        width: 100%;

        &>.el-container {
            flex-wrap: wrap;
            display: flex;
            align-content: flex-start;
        }
    }

    .common-layout {
        display: flex;

        flex-wrap: wrap;
        height: 100%;
        width: 100%;

        &>.el-container {
            height: 100%;

            &>.el-aside {
                background-color: #545c64;
            }
        }
    }
</style>