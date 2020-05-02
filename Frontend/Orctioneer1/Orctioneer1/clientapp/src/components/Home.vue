<template>
    <div id="home">
        <h1>Popular auctions:</h1>
        <item-auction :auctions="auctions"></item-auction>
        
        <h1>New auctions:</h1>
        <item-auction :auctions="auctions"></item-auction>

        <h1>Expiring soon:</h1>
        <item-auction :auctions="auctions"></item-auction>

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
            {title: 'Stort sværd', url: require('./../images/sværd.jpg'), bid: 1234, timeLeft: "1 day"},
            {title: 'Hjelm og brynje', url: require('./../images/Hjelm_og_Brynje.jpg'), bid: 1234, timeLeft: "2 day"},
            {title: 'Faktisk økse', url: require('./../images/Faktisk_økse.jpg'), bid: 1234, timeLeft: "3 day"},
            {title: 'Én pil', url: require('./../images/En_pil.jpg'), bid: 1234, timeLeft: "4 day"},
                ],
            testArray:[]
        }
    },
    created(){
        axios.get('https://jsonplaceholder.typicode.com/posts')
        //.then(function (response) {
        //console.log(response);
        //})
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
