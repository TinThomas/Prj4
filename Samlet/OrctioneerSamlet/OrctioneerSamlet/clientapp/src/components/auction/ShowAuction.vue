<template>
    <div id="show-auctions">
        <search-bar></search-bar>     
        <sorted-auctions :key="updateSortedKey"></sorted-auctions>
    </div>
</template>

<script>
    import SortedAuctions from './SortedAuctionListing.vue'
    import SearchBar from './SearchBar.vue'
export default {
     components:{
        'sorted-auctions': SortedAuctions,
         'search-bar': SearchBar
    },
    computed:{
        updateSortedKey() {
            return this.$store.state.UpdateSortedAuctions;
        }
    },
    created() {
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
#sortSelector{
    margin-left: 5px;
}

</style>