<template>
    <div id="show-auctions">
        <h1>All auctions</h1>
        <input type="text" v-model="search" placeholder="search auctions">
        <!--<item-auction2 :auctions="filterByWord"></item-auction2>-->
        <div class="row">
            <auction-card v-for="auc in filterByWord" :key="auc.id" :auction="auc"></auction-card>
        </div>
    </div>
</template>

<script>
    //import Auction2 from './Auction2.vue'
    import AuctionCard from './AuctionCard.vue'
//import axios from 'axios';
export default {
     components:{
        //'item-auction2': Auction2
        'auction-card': AuctionCard
    },
    data(){
        return{
            search: '',       
        }
    },
    methods:{
        
        },
    computed:{
        filterByWord: function () {
            return this.auctions.filter((auction) => {
                return auction.Title.toLowerCase().match(this.search.toLowerCase())
            })
        },
        showSearch: function () {
            if (this.search === "") {
                return false;
            }
            else {
                return true;
            }
        },
        auctions() {
            return this.$store.state.auctions;
        }
    },
    async mounted() {
            this.$store.dispatch('loadAuctions');
    }
}
</script>

<style scoped>
#show-auctions{
    max-width: 800px;
    min-height: 1000px;
    margin: 0 auto;
}

</style>