<template>
    <div id="home">
        <h1>Popular auctions:</h1>
        <item-auction2 :auctions="popularAuctions"></item-auction2>
        
        <h1>New auctions:</h1>
        <item-auction2 :auctions="newestAuctions"></item-auction2>

        <h1>Expiring soon:</h1>
        <item-auction2 :auctions="expiringAuctions"></item-auction2>

    </div>
</template>

<script>
    //import Auction from './../auction/old/Auction.vue'
    import Auction2 from './../auction/Auction2.vue'
import axios from 'axios';
export default {
    components:{
        //'item-auction': Auction,
        'item-auction2': Auction2
    },
    data(){
      return{
        //auctions:[
        //      { title: 'Stort sværd', url: require('./../../images/sværd.jpg'), bid: 1234, timeLeft: "1 day"},
        //      { title: 'Hjelm og brynje', url: require('./../../images/Hjelm_og_Brynje.jpg'), bid: 1234, timeLeft: "2 day"},
        //      { title: 'Faktisk økse', url: require('./../../images/Faktisk_økse.jpg'), bid: 1234, timeLeft: "3 day"},
        //      { title: 'Én pil', url: require('./../../images/En_pil.jpg'), bid: 1234, timeLeft: "4 day"},
        //        ],
          popularAuctions: [],
          newestAuctions: [],
          expiringAuctions: []
        }
    },
    created() {
        var ref = this;
        axios.get('http://localhost:5000/api/ItemEntity/item/pop').then(function (response) {
            if (response.status != 200) {
                window.console.log(response.status)
            }
            //ref.test = response.data.splice(0, 4);
            //if (ref.popularAuctions != response.data.splice(0, 4)) {
                ref.popularAuctions = response.data.splice(0, 4);
            //}
            
        });     
        axios.get('http://localhost:5000/api/ItemEntity/item/new').then(function (response) {
            if (response.status != 200) {
                window.console.log(response.status)
            }
            //if (ref.newestAuctions != response.data.splice(0, 4)) {
                ref.newestAuctions = response.data.splice(0, 4);
            //}
            //ref.test = response.data.splice(0, 4);
            
        });
        axios.get('http://localhost:5000/api/ItemEntity/item/expire').then(function (response) {
            if (response.status != 200) {
                window.console.log(response.status)
            }
            //if (ref.expiringAuctions != response.data.splice(0, 4)) {
                ref.expiringAuctions = response.data.splice(0, 4);
            //}
            //ref.test = response.data.splice(0, 4);
           
        });  
    }

}
</script>

<style scoped>
    #home{
        max-width: 800px;
        min-height: 1000px;
    margin: 0 auto;
    }

    h3{
        margin: 5px;
    }

</style>
