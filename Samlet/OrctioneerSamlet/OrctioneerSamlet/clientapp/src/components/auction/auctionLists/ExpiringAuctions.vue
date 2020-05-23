<template>
    <div id="auctionId" :key="componentKey">
        <h1>Expiring auctions:</h1>
        <div class="row" >
            <auction-card class="single-auction" v-for="auc in expAuctions.slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
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
                componentKey: 0,
            };
        },
        methods: {
            forceRerender() {
                this.componentKey += 1;
            }
        },

        computed: {
            expAuctions() {
                return this.$store.getters.getExpAuctions;
            }
        },
        created() {
            this.$store.dispatch('loadAuctions');
            this.forceRerender();
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
