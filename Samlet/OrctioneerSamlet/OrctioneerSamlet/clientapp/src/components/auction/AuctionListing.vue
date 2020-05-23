<template>
    <div id="auctionId">
        <h1>Popular auctions:</h1>
        <div class="row">
            <auction-card class="single-auction" v-for="auc in popAuctions.slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
        </div>
        <h1>New auctions:</h1>
        <div class="row" >
            <auction-card class="single-auction" v-for="auc in newAuctions.slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
        </div>
        <h1>Expiring auctions:</h1>
        <div class="row">
            <auction-card v-for="auc in expAuctions.slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
        </div>
    </div>
</template>

<script>
    import AuctionCard from './AuctionCard.vue'
    export default {
        components: {
            'auction-card': AuctionCard
        },
        computed: {
            popAuctions() {
                return this.$store.state.popAuctions;
            },
            newAuctions() {
                return this.$store.state.newAuctions;
            },
            expAuctions() {
                return this.$store.state.expAuctions;
            },
        },
        created() {
            this.$store.dispatch('loadPopAuctions');
            this.$store.dispatch('loadNewAuctions');
            this.$store.dispatch('loadExpAuctions');
        }

    }
</script>

<style scoped>
    #auctionId {
        max-width: 1200px;
        margin: 0;
    }
</style>
