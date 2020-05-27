<script>
    import Vue from "vue";
    import Vuex from "vuex";
    

    import getters from './getters'
    import mutations from './mutations'
    import actions from './actions'
    
    
    Vue.use(Vuex);
    export default new Vuex.Store({
        state:{
            logged: false,

            auctions: [],
            updateListing: 0,

            picturePath: "",
            auctionSubmitted: false,

            currentAuction: [],
            currentPictureUrl: "",
            currentBids: [],
            bidSubmitted: false,
            currentBidTableKey: 0,

            search: '',
            sortOrders: [
                "Popularity", "Newest", "Expiring"
            ],
            sortBy: "",
            UpdateSortedAuctions: 0
            
        },
        getters,
        mutations,
        actions,
    });
</script>
