<template>
    <div id="show-auctions">
        <h1>All auctions</h1>
        <input type="text" v-model="search" placeholder="search auctions">
        <item-auction :auctions="filteredAuctions"></item-auction>
    </div>
</template>

<script>
import Auction from './Auction.vue'

export default {
     components:{
    'item-auction' : Auction
    },
    data(){
        return{
            auctions:[
            {title: 'Stort sværd', url: require('./../images/sværd.jpg'), bid: 1234, timeLeft: "1 day"},
            {title: 'Hjelm og brynje', url: require('./../images/Hjelm_og_Brynje.jpg'), bid: 1234, timeLeft: "2 day"},
            {title: 'Faktisk økse', url: require('./../images/Faktisk_økse.jpg'), bid: 1234, timeLeft: "3 day"},
            {title: 'Én pil', url: require('./../images/En_pil.jpg'), bid: 1234, timeLeft: "4 day"},
            ],
            search: ''
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