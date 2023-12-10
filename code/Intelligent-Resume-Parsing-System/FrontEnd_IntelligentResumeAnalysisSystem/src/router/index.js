//路由的配置文件
import { createRouter, createWebHashHistory } from 'vue-router'

const routes = [
    {
        path: '/main',
        component: () => import('../views/Main.vue'),
        redirect: '/home',
        children: [
            {
                path: '/home',
                name: 'home',
                component: () => import('../views/home/Home.vue')
            },
            {
                path: '/uploadResume',
                name: 'uploadResume',
                component: () => import('../views/resumeAnalyse/uploadResume.vue')
            },
            {
                path: '/resumeInfo',
                name: 'resumeInfo',
                component: () => import('../views/resumeAnalyse/resumeInfo.vue')
            },
            {
                path: '/totalResumeAnalyse',
                name: 'totalResumeAnalyse',
                component: () => import('../views/resumeAnalyse/totalResumeAnalyse.vue')
            },
            {
                path: '/talentPortrait',
                name: 'page2',
                component: () => import('../views/resumeAnalyse/talentPortrait.vue')
            },

        ]
    },
    {
        path: '/',
        name: 'main',
        redirect: '/login',
        component: () => import('../views/login.vue'),
        children: [
            {
                path: '/login',
                name: 'login',
                component: () => import('../views/login/login.vue'),

            },
            {
                path: '/register',
                name: 'register',
                component: () => import('../views/login/register.vue'),

            },

        ]


    },
]

const router = createRouter(
    {
        history: createWebHashHistory(),
        routes
    }
)

export default router