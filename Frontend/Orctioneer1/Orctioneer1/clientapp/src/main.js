import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App.vue';
import VModal from 'vue-js-modal'
import axios from 'axios'
import VueAxios from 'vue-axios'
import ShowAuction from './components/ShowAuction.vue';
import AddAuction from './components/AddAuction.vue';
import Home from './components/Home.vue';
import ItemAuction from './components/Auction.vue'
import FullAuction from './components/FullAuction.vue'

Vue.use(VueRouter);

Vue.use(VModal);

Vue.use(VueAxios, axios)

const routes = [
    { path: '/', component: Home },
    { path: '/search', component: ShowAuction },
    { path: '/addAuction', component: AddAuction },
    { path: '/itemAuction', component: ItemAuction },
    { path: '/fullAuction/:id', component: FullAuction }
];


const router = new VueRouter({
    routes,
    mode: 'history'
})

//Fors�g p� at g�re data globalt 
/*const auctions = [
  {title: 'Stort sv�rd', url: require('./images/sv�rd.jpg'), bid: 1234, timeLeft: "1 day"},
  {title: 'Hjelm og brynje', url: require('./images/Hjelm_og_Brynje.jpg'), bid: 1234, timeLeft: "2 day"},
  {title: 'Faktisk �kse', url: require('./images/Faktisk_�kse.jpg'), bid: 1234, timeLeft: "3 day"},
  {title: '�n pil', url: require('./images/En_pil.jpg'), bid: 1234, timeLeft: "4 day"},
  ];*/

new Vue({
    el: '#app',
    /* auctions,*/
    axios,
    router,
    render: h => h(App),

})
