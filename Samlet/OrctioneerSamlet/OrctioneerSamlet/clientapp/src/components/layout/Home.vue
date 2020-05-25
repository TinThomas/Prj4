<template>
    <div id="home">   
        <auction-listing :key="updateListingKey">
            <!---->
        </auction-listing>
    </div>
</template>

<script>

    import AuctionListing from './../auction/AuctionListing.vue'

export default {
    components:{

        'auction-listing': AuctionListing,

        },
    methods: {
        loadListings() {
            this.$store.dispatch('loadAuctions');
            //this.$store.dispatch('loadPopAuctions');
            //this.$store.dispatch('loadNewAuctions');
            //this.$store.dispatch('loadExpAuctions');

        }
    },
    computed: {
        updateListingKey() {
            return this.$store.state.updateListing;
        }
    },
    created() {
        //this.$store.dispatch('loadAuctions'); 
        //this.$store.dispatch('updateListing');
        this.loadListings();
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
