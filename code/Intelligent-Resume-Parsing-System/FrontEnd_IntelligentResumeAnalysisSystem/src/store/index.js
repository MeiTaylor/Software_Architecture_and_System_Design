import { number } from 'echarts';
import { createStore } from 'vuex'
export default createStore({
    state: {
        isCollapse: true,
        userId: number,
        rId: number,
        headTitle: '首页',
        baseURL: 'https://localhost:7106/api',
    },
    mutations: {
        updateIsCollape(state, payload) {
            state.isCollapse = !state.isCollapse
        },
        updateUserId(state, newID) {
            localStorage.setItem('userId', JSON.stringify(newID))
            state.userId = newID
        },
        updateHeadTitle(state, newHeadTitle) {
            localStorage.setItem('headTitle', JSON.stringify(newHeadTitle))
            state.headTitle = newHeadTitle;
        },
        updateRId(state, rId) {
            localStorage.setItem('rId', rId)
            state.rId = rId
        }
        ,
        getLocalStorage(state) {
            if (!localStorage.getItem('userId') || !localStorage.getItem('headTitle')) {
                return
            }
            state.userId = JSON.parse(localStorage.getItem('userId'))
            state.headTitle = JSON.parse(localStorage.getItem('headTitle'))
            if (localStorage.getItem('rId')) {
                state.rId = JSON.parse(localStorage.getItem("rId"))
            }
        }
    }
})