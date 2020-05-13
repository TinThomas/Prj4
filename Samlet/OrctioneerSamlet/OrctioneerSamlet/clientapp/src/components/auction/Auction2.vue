<template>
    <div id=auctionId>
        <div class="row">
            <div v-for="auc in auctions" class="single-auction" :key="auc.id">
                <img :src="auc.url">
                <h5>{{ auc.Title }}</h5>
                <h5>
                    Bid: <span > {{ getHighestBid(auc) }}</span>
                </h5>
                
                <h5>Ends: {{ auc.ExpirationDate | moment("from") }}</h5>

                <button @click="navigateToAuction(auc.ItemId)" class="btn btn-secondary" id="buttonId">See full auction</button>


            </div>
        </div>

    </div>
</template>

<script>
    //import moment from 'vue-moment'

    export default{
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
            navigateToAuction(path){
                this.$router.push('/fullAuction2/'+path);
            },
            calcEndDate() {
                const moment = this.$moment().add(this.endDate, 'days').format();
                window.console.log(this.endDate);
                this.auction.expirationDate = moment;
            },
            getHighestBid: function (auction) {
                if (Array.isArray(auction.Bids) && auction.Bids.length) {
                    var bids = auction.Bids;
                    bids.sort(function (bidA, bidB) {
                        return bidB.Bid - bidA.Bid
                    })
                    return bids[0].Bid + " Gold";
                }
                else {
                    return "No bids";
                }

            }
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
        margin: 10px;
    }
    #buttonId{

    }

    .single-auction {
        background: #eeeeee;
        max-width: 190px;
        padding: 5px;
        margin: 5px;
    }

    img {
        object-fit: cover;
        width: 180px;
        height: 180px;
        border: 1px solid white;
    }
</style>
