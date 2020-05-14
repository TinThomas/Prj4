<template>
    <div id="show-auctions">
        <h1>All auctions</h1>
        <input type="text" v-model="search" placeholder="search auctions">
        <item-auction :auctions="filteredAuctions"></item-auction>
        New
        <item-auction2 :auctions="filteredAuctions2"></item-auction2>
        {{test}}
    </div>
</template>

<script>
    import Auction from './old/Auction.vue'
    import Auction2 from './Auction2.vue'
import axios from 'axios';
export default {
     components:{
        'item-auction': Auction,
        'item-auction2': Auction2
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
            test: []
            
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
        filteredAuctions2: function () {
            return this.test.filter((auction) => {
                return auction.Title.toLowerCase().match(this.search.toLowerCase())
            })
        },
            
        },
        created() {
            var ref = this;
            axios.get('http://localhost:5000/api/ItemEntity/item').then(function (response) {
            if (response.status != 200) {
                window.console.log(response.status)
            }
                //ref.test = response.data.splice(0, 4);
                if (ref.test != response.data) {
                    ref.test = response.data;
                }
                
        })     
    }
}
</script>

<style scoped>
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