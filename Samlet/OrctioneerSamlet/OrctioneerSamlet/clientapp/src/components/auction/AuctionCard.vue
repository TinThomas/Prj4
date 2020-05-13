<template>
    <div class="single-auction">
        <img :src="url">
        <h5 class="title-format">{{ auction.Title }}</h5>
        <h5>
            Bid: <span> {{ getHighestBid(auction) }}</span>
        </h5>

        <h5>Ends: {{ auction.ExpirationDate | moment("from") }}</h5>

        <button @click="navigateToAuction(auction.ItemId)" class="btn btn-secondary" id="buttonId">See full auction</button>
    </div>
</template>

<script>
    //import moment from 'vue-moment'

    export default{
        props:{
            auction: {
                type: Object,
                required: true
            },
        },
        data(){
            return {
                url: ""
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
        created() {
            this.url = require("./../../images/" + this.auction.Image);
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
    .single-auction {
        background: #eeeeee;
        max-width: 194px;
        padding: 5px;
        margin: 5px;
        box-sizing: border-box;
    }

    .title-format {
        line-height: 2.5ex;
        height: 7.5ex;
        overflow: hidden;
    }
    img {
        object-fit: cover;
        width: 180px;
        height: 180px;
        border: 1px solid white;
    }
</style>
