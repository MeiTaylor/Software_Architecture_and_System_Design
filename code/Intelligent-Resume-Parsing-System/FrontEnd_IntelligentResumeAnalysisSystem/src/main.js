import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import ElementPlus from 'element-plus'
import router from './router'
import 'element-plus/dist/index.css'
import store from './store/index.js'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import './api/mock.js'
import api from './api/api'
const app = createApp(App)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}

app.config.globalProperties.$api = api
app.use(ElementPlus)
app.use(router).use(store)
app.mount('#app')
