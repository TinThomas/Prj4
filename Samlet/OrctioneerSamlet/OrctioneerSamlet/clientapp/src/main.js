import Vue from 'vue';
import VueRouter from 'vue-router';
import VueMoment from 'vue-moment';
import App from './App.vue';
import VModal from 'vue-js-modal'
import axios from 'axios'
import VueAxios from 'vue-axios'
import ShowAuction from './components/auction/ShowAuction.vue';
import AddAuction from './components/auction/AddAuction.vue';
import Home from './components/layout/Home.vue';
import FullAuction from './components/auction/FullAuction.vue'
import store from './components/Utility/Store'
import UserDetail from './components/UserProfile/UserDetails'
import WalletUpdate from './components/UserProfile/UpdateWallet'
import WalletCreate from './components/UserProfile/CreateWallet'
import UserUpdate from './components/UserProfile/UpdateUser'

Vue.use(VueRouter);
Vue.use(VModal);
Vue.use(VueMoment);
Vue.use(VueAxios, axios);

const routes = [
    { path: '/', name:'Home',component: Home },
    { path: '/search', component: ShowAuction },
    { path: '/addAuction', component: AddAuction },
    { path: '/fullAuction/:id', component: FullAuction },
    { path: '/fullAuction2/:id', component: FullAuction2 },
    { path: '/UserDetails', component: UserDetail},
    {path: '/Wallet', component: WalletUpdate},
    {path: '/CreateWallet', component: WalletCreate},
    {path: '/UpdateUser', component: UserUpdate}
];

const router = new VueRouter({
    routes,
    mode: 'history'
});


//Removing token on app start up - requiring user to login.
axios.defaults.headers.common['Authorization'] = null;
localStorage.removeItem('token');

Vue.config.productionTip = false;

window.vm = new Vue({
    axios,
    VueMoment,
    router,
    store,
    render: h => h(App),
}).$mount('#app');