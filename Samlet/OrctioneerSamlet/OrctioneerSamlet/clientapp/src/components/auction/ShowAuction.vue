<template>
    <div id="show-auctions">
        <h1>All auctions</h1>
        <input type="text" v-model="search" placeholder="search auctions">
        <div class="row">
            <auction-card v-for="auc in filterByWord" :key="auc.id" :auction="auc"></auction-card>
        </div>
    </div>
</template>

<script>
    import AuctionCard from './AuctionCard.vue'
export default {
     components:{
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