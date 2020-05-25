<template>
    <div id="auctionId">
        <h1>Popular auctions:</h1>
        <div class="row" >
            <auction-card v-for="auc in popAuctions.slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
        </div>
        <h1>New auctions:</h1>
        <div class="row">
            <auction-card v-for="auc in newAuctions.slice(0, 4)" :key="auc.id" :auction="auc"></auction-card>
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
        methods: {
            
        },
        computed: {
            popAuctions() {
                return this.$store.getters.getPopularAuctions(4);
            },
            newAuctions() {
                return this.$store.getters.getNewAuctions(4);
            },
            expAuctions() {
                return this.$store.getters.getExpAuctions(4);
            },
        },
        created() {
            
        }
    }
</script>

<style scoped>
    #auctionId {
        max-width: 1200px;
        margin: 0;
    }
</style>
