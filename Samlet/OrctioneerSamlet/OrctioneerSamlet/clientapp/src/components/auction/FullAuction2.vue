<template>
    <div id="full-auction">
        <div class="row">
            <div class="col">
                <!--<img :src="">-->
                <img :src="url">
            </div>
            <div class="col">
                <h3>{{auctions.Title}}</h3>
                <p id="description">{{auctions.DescriptionOfItem}}</p>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <h3>Current bid: </h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{getHighestBid(getHighestBidFirst[0])}}</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <h3>Ends:</h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{auctions.ExpirationDate | moment("from") }}</h3>

            </div>
            <div class="col-6" id="bid-table">
                <p>Bid must be higher than: {{auctions.BuyOutPrice}} </p>
                <input type="text" placeholder="Your bid">
                <button class="btn btn-secondary">Make a bid!</button>

            </div>
            <div class="col-6" id="bid-history">
                <div class="row">
                    <div class="col-4">
                        <b>Bid:</b>
                    </div>
                    <div class="col-4">
                        <b>Date Created:</b>
                    </div>
                    <div class="col-4">
                        <b>Time:</b>
                    </div>

                </div>
                <div v-for="bid in getHighestBidFirst" :key="bid.Id">
                    <div class="row">
                        <div class="col-4">
                            {{bid.Bid}}
                        </div>
                        <div class="col-4">
                            {{bid.Created | moment("dddd, MMMM Do YYYY")}}
                        </div>
                        <div class="col-4">
                            {{bid.Created | moment("h:mm:ss a")}}
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>    
</template>

<script>
    import axios from 'axios';
    //import moment from 'vue-moment'
export default {
    data(){
        return {
            id: this.$route.params.id,

            //auctions: [{
            //    Title: "",
            //    Bids: [{ Bid: 0 }]
            //}],
            auctions: "",
            highestBid: "",
            url: ""
                //require('./../../images/df457fc9-c541-405c-a707-ff1d20ffa9d5.jpg')

            }
        },
        methods:{
            //testFunction: function () {
            //    return this.auctions.filter((auction) => {
            //        return this.specificAuction = auction.title.match(this.id)
            //    })
            //getHighestBid: function () {
            //    var bids = this.auctions.Bids;
            //    window.console.log(bids);
            //    if (Array.isArray(bids) && bids.length) {
                    
            //        return bids[0].Bid + " Gold";
            //    }
            //    else {
            //        return "No bids";
            //    }

            //}

            getHighestBid: function (bid) {
                if (bid != null) {
                    return bid.Bid + " Gold";
                }
                else {
                    return " No Bids";
                }
            }
        },
        computed:{
            //testFunction: function(){
            //    return this.auctions.filter((auction)=>{
            //       return auction.ItemId.match(this.id)
            //    })
            //},
            getHighestBidFirst: function () {
                var bids = this.auctions.Bids;
                return bids.sort(function(bidA,bidB) {
                    return bidB.Bid-bidA.Bid
                }).splice(0,4)
                
            },
           
        },
        created() {
            var ref = this;
            axios.get('http://localhost:5000/api/ItemEntity/item/' + ref.id).then(function (response) {
                if (response.status != 200) {
                    //error
                }
                //ref.test = response.data.splice(0, 4);
                ref.auctions = response.data;
                ref.url = require("./../../images/" + response.data.Image);

                window.console.log(response.data.Image);
                window.console.log(ref.auctions);
            })
        }
    }   
</script>

<style scoped>
#full-auction{
    max-width: 800px;
    min-height: 1000px;
    margin: 10px auto;
}

#billede{
    display: inline;
    
}

#description{
    padding: 10px 20px;
    border:1px solid black;    
}

.auction-text{
    float: right;
    background: #eeeeee;
    text-decoration: underline black;
}

#bid-table{
    border: 1px solid black;
    padding-bottom: 10px;
    margin-left:5px;

}

#bid-history {
    border: 1px solid black;
    padding-bottom: 10px;
    margin-left: 5px;
    margin-top: 5px;
}

#bid-table button{
    margin-top:10px;
}

    img {
        width: 400px;
        height: 400px;
        box-shadow: grey 0 0 10px;
        border: 1px solid white;
        margin-top: 10px;
        margin-bottom: 10px;
        float: left;
        object-fit: contain;
        background-color: #eeeeee;
    }

</style>