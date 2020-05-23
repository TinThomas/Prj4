<template>
    <div id="auctionId">
        <h1>Pop auctions:</h1>
        <div class="row" >
            <auction-card class="single-auction" v-for="auc in getPopAuctions().slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
        </div>
    </div>
</template>

<script>
    import AuctionCard from './../AuctionCard.vue'
    export default {
        data() {
            return {
                auctions: {}
            };
        },
        components: {
            'auction-card': AuctionCard
        },
        methods: {
            getPopAuctions() {
                return this.$store.getters.getPopularAuctions;
            }
        },
        computed: {
            //popAuctions() {
            //    return this.$store.getters.getPopularAuctions;
            //}
        },
        created() {
            this.$store.dispatch('loadAuctions');
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
