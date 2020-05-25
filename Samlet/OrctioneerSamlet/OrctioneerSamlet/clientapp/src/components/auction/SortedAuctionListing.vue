<template>
    <div id="auctionId">
        <div class="row">
            <auction-card v-for="auc in filterByWord" :key="auc.id" :auction="auc"></auction-card>
        </div>
    </div>
</template>

<script>
    import AuctionCard from './AuctionCard.vue'
    export default {
        components: {
            'auction-card': AuctionCard
        },
        data() {
            return {
                //search: '',
                //sortOrders: [
                //    "Popularity", "Newest", "Expiring"
                //],
                //sortBy: ""
            }
        },
        methods: {
            //loadListings() {
            //    this.$store.dispatch('loadPopAuctions');
            //}
        },
        computed: {
            filterByWord: function () {
                if (this.sortBy == "Popularity") {
                    return this.popAuctions.filter((auction) => {
                        return auction.Title.toLowerCase().match(this.searchWord.toLowerCase())
                    })
                }
                else if (this.sortBy == "Newest") {
                    return this.newAuctions.filter((auction) => {
                        return auction.Title.toLowerCase().match(this.searchWord.toLowerCase())
                    })
                }
                else if (this.sortBy == "Expiring") {
                    return this.expAuctions.filter((auction) => {
                        return auction.Title.toLowerCase().match(this.searchWord.toLowerCase())
                    })
                }
                else /*(this.sortBy == "")*/ {
                    return this.auctions.filter((auction) => {
                        return auction.Title.toLowerCase().match(this.searchWord.toLowerCase())
                    })
                }
            },
            auctions() {
                return this.$store.state.auctions;
            },
            popAuctions() {
                return this.$store.getters.getPopularAuctions(20);
            },
            newAuctions() {
                return this.$store.getters.getNewAuctions(20);
            },
            expAuctions() {
                return this.$store.getters.getExpAuctions(20);
            },
            searchWord() {
                return this.$store.state.search;
            },
            sortBy() {
                return this.$store.state.sortBy;    
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
