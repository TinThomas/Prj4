<template>
    <div id="show-auctions">
        <h1>All auctions</h1>
        <input type="text" v-model="search" placeholder="search auctions">
        <item-auction :auctions="filteredAuctions"></item-auction>
        <span v-html="test"></span>
    </div>
</template>

<script>
import Auction from './Auction.vue'
import axios from 'axios';
export default {
     components:{
    'item-auction' : Auction
    },
    data(){
        return{
            auctions:[
                { title: 'Stort sværd', url: require('./../../images/sværd.jpg'), bid: 1234, timeLeft: "1 day"},
                { title: 'Hjelm og brynje', url: require('./../../images/Hjelm_og_Brynje.jpg'), bid: 1234, timeLeft: "2 day"},
                { title: 'Faktisk økse', url: require('./../../images/Faktisk_økse.jpg'), bid: 1234, timeLeft: "3 day"},
                { title: 'Én pil', url: require('./../../images/En_pil.jpg'), bid: 1234, timeLeft: "4 day"},
            ],
            search: '',
            test: ''
        }
    },
    methods:{
        
        },
    computed:{
        filteredAuctions: function(){
            return this.auctions.filter((auction)=>{
                return auction.title.toLowerCase().match(this.search.toLowerCase()) 
            })
        },
            
        },
        created() {
            var ref = this;
            axios.get('http://localhost:5000/Home/item/').then(function (response) {
            if (response.status != 200) {
                //error
            }
                var data = response.data;
                for (var i = 0; i < response.data.length; i++) {
                    ref.test += '<p>' + data[i].Title + '</p>';
                }
        })
            
    }
}
</script>

<style >
#show-auctions{
    max-width: 800px;
    min-height: 1000px;
    margin: 0 auto;
}

.single-auction{
    padding: 20px;
    margin: 20px;
    box-sizing: border-box;
    background: #eeeeee;
}
</style>