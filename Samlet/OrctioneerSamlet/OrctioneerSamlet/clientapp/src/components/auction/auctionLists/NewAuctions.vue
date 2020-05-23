<template>
    <div id="auctionId">
        <h1>New auctions:</h1>
        <div class="row" >
            <auction-card class="single-auction" v-for="auc in newAuctionsTest.slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
        </div>
    </div>
</template>

<script>
    import AuctionCard from './../AuctionCard.vue'
    export default {
        components: {
            'auction-card': AuctionCard
        },
        data() {
            return {

            };
        },
        methods: {

        },

        computed: {
            newAuctions() {
                return this.$store.getters.getNewAuctions;
            },
            newAuctionsTest() {
                return this.$store.state.newAuctions;
            },
            
            
        },
        created() {
            this.$store.dispatch('loadNewAuctions');
        }

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
