<template>
    <div id="auctionId">
        <div class="row">
            <div v-for="auc in auctions" class="single-auction" :key="auc.id">
                
                <auction-card :auction="auc"></auction-card>

            </div>
        </div>
    </div>
</template>

<script>
    //import moment from 'vue-moment'
    import AuctionCard from './AuctionCard.vue'
    export default {
        components: {
            'auction-card': AuctionCard
        },
        props:{
            auctions: {
                type: Array,
                required: true
            },
        },
        data(){
            return {
                
            }
        },

        methods:{
            
        },
      
        //computed: {
        //    getHighestBid: function (auction) {
        //        var bids = auction.Bids;
        //        bids.sort(function (bidA, bidB) {
        //            return bidB.Bid - bidA.Bid
        //        })
        //        return bids[0];
        //    }
        //},
    }
</script>

<style scoped>
    #auctionId {
        max-width: 1200px;
        margin: 0;
    }

</style>
