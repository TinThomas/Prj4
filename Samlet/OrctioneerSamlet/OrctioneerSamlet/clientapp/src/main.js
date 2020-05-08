import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App.vue';
import VModal from 'vue-js-modal'
import Axios from 'axios'
import VueAxios from 'vue-axios'
import login from './components/login/Login.vue';
import signup from './components/login/Signup.vue';
import ShowAuction from './components/auction/ShowAuction.vue';
import AddAuction from './components/auction/AddAuction.vue';
import Home from './components/layout/Home.vue';
import ItemAuction from './components/auction/Auction.vue'
import FullAuction from './components/auction/FullAuction.vue'


Vue.use(VueRouter);
Vue.use(VModal);
Vue.use(VueAxios, Axios);

const routes = [
    { path: '/Login', component: login},
    { path: '/Signup', component: signup },
    { path: '/', component: Home },
    { path: '/search', component: ShowAuction },
    { path: '/addAuction', component: AddAuction },
    { path: '/itemAuction', component: ItemAuction },
    { path: '/fullAuction/:id', component: FullAuction }
];

const router = new VueRouter({
    routes,
    mode: 'history'
});

const token = localStorage.getItem('token');
if (token) {
    Axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
}

Vue.config.productionTip = false;

window.vm = new Vue({
    Axios,
    router,
    render: h => h(App),
}).$mount('#app');