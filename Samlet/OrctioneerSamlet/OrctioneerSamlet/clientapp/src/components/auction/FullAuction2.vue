<template>
    <div id="full-auction">
        <div class="row">
            <div class="col">
                <img :src="urlTest">
            </div>
            <div class="col">
                <h3>{{currentAuction.Title}}</h3>
                <p id="description">{{currentAuction.DescriptionOfItem}}</p>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <h3>Current bid: </h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{currentHighestBid}}</h3>

            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <h3>Ends:</h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{currentAuction.ExpirationDate | moment("from") }}</h3>

            </div>
            <div class="col-6" id="bid-table">
                <p>Bid must be higher than: {{currentBids[0].Bid}} </p>
                <div class="row">
                    <div class="col-4">
                        How much:
                    </div>
                    <div class="col-4">
                        Your bid:
                    </div>
                    <div class="col-4">

                    </div>
                </div>
                <div class="row">

                    <div class="col-4 bidPrice">
                        <select v-model="newBid.bid">
                            <option disabled value="">Your bid</option>
                            <option>{{currentBids[0].Bid + 50}} Gold</option>
                            <option>{{currentBids[0].Bid + 100}} Gold</option>
                            <option>{{currentBids[0].Bid + 150}} Gold</option>
                            <option>{{currentBids[0].Bid + 200}} Gold</option>
                            <option>{{currentBids[0].Bid + 250}} Gold</option>
                            <option>{{currentBids[0].Bid + 300}} Gold</option>
                        </select>
                    </div>
                    <div class="col-4 bidPrice">
                         <input type="submit" placeholder="Your bid" v-model.lazy="newBid.bid">
                    </div>
                    <div class="col-4">
                        <button class="btn btn-secondary" @click="postBid">Make bid!</button>
                    </div>

                </div>
                <div v-show="bidSubmitted">
                    <h3>Bid posted!</h3>
                </div>
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
                <!--<div :key="bidComponentKey">
                    <div v-for="bid in currentBids.slice(0,4)" :key="bid.id">
                        <div class="row">
                            <div class="col-4">
                                {{bid.Bid}} Gold
                            </div>
                            <div class="col-4">
                                {{bid.Created | moment("dddd, MMMM Do YYYY")}}
                            </div>
                            <div class="col-4">
                                {{bid.Created | moment("h:mm:ss a")}}
                            </div>
                        </div>
                    </div>
                </div>-->
                <topBids-card :key="bidComponentKey">
                </topBids-card>
               

            </div>
            {{currentBids}}
        </div>

    </div>    
</template>

<script>
    //import axios from 'axios';
    //import moment from 'vue-moment'
    import TopBids from './TopBids.vue'
    export default {
        components: {
            'topBids-card': TopBids
        },
        data(){
            return {
                id: this.$route.params.id,

                newBid: {
                    bid:0,
                    userIdBuyer: "",
                    itemId: 0
                },

                bidSubmitted: false,
                auction: "",

                bidComponentKey: 0,
                }
            },
        methods:{
           
            loadCurrentAuction() {
                var payload = {
                    id: this.id
                }

                this.$store.dispatch('loadCurrentAuction', payload);
            },
            loadCurrentBids() {
                var payload = {
                    id: this.id
                }

                this.$store.dispatch('loadCurrentBids', payload);
            },
            forceRerenderBids() {
                this.bidComponentKey += 1;
            },
            //postBid: function () {
            //    var ref = this;
            //    axios.post('http://localhost:5000/api/BidEntities/newBid', {
            //        Bid: ref.newBid.bid,
            //        UserIdBuyer: "xd",
            //        ItemId: ref.id
            //    }).then(function (response) {
            //        window.console.log(response);
            //    });
            //        ref.bidSubmitted = true
            //},
            postBid: function() {
                var payload = {
                    bid: this.newBid.bid,
                    userIdBuyer: "xd",
                    itemId: this.id
                }
                this.$store.dispatch('postNewBid', payload)
                this.forceRerenderBids();
            }

            
        },
        computed:{

            currentHighestBid() {
                return this.$store.getters.getCurrentHighestBid;
            },
            currentAuction() {
                return this.$store.state.currentAuction;
            },
            urlTest() {
                return this.$store.state.currentPictureUrl;
            },
            currentBids() {
                return this.$store.state.currentBids;
            }

           
        },
        created() {
            this.loadCurrentAuction();
            this.loadCurrentBids();
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

.btn.btn-secondary {
    margin-top: 0px;
    object-position: center;
    float: left;
}
.bidPrice{
    margin-top: 10px;
}


</style>