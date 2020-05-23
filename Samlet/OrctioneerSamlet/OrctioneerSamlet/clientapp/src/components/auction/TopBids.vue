<template>
    <div>
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
        </div>
        <div class="row">
            <div class="col-6" id="bid-table">

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
                            <option>{{currentBids[0].Bid + 50}}</option>
                            <option>{{currentBids[0].Bid + 100}}</option>
                            <option>{{currentBids[0].Bid + 150}}</option>
                            <option>{{currentBids[0].Bid + 200}}</option>
                            <option>{{currentBids[0].Bid + 250}}</option>
                            <option>{{currentBids[0].Bid + 300}}</option>
                        </select>
                    </div>
                    <div class="col-4 bidPrice">
                        <input type="submit" placeholder="Your bid" v-model.lazy="newBid.bid">
                    </div>
                    <div class="col-4">
                        <button class="btn btn-secondary" @click.prevent="postBid">Make bid!</button>
                    </div>

                </div>
                <p>Bid must be higher than: {{currentBids[0].Bid}} </p>
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
            </div>
            {{currentBids}}
        </div>

    </div>
</template>

<script>
    export default {
        props: {
            id: {
                type: String,
                required: true
            },
        },
        data() {
            return {

                newBid: {
                    bid: 0,
                    userIdBuyer: "",
                    itemId: 0
                },
            }
            
        },
        
        methods: {
        loadCurrentBids() {
            var payload = {
                id: this.id
            }
            this.$store.dispatch('loadCurrentBids', payload);
        },
        postBid: function() {
            var payload = {
                bid: this.newBid.bid,
                userIdBuyer: "xd",
                itemId: this.id
            }
            this.$store.dispatch('postNewBid', payload)
            //this.forceRerenderBids();
        }
        },
        computed: {
            currentBids() {
                return this.$store.state.currentBids;
            },
            currentAuction() {
                return this.$store.state.currentAuction;
            },
            bidSubmitted() {
                return this.$store.state.bidSubmitted;
            },
            currentHighestBid() {
                return this.$store.getters.getCurrentHighestBid;
            },
        },
        created() {
            this.loadCurrentBids();
        }
    }
</script>

<style scoped>
    .btn.btn-secondary {
        margin-top: 0px;
        object-position: center;
        float: left;
    }

    .bidPrice {
        margin-top: 10px;
    }
    #bid-table {
        border: 1px solid black;
        padding-bottom: 10px;
        margin-left: 5px;
    }

    #bid-history {
        border: 1px solid black;
        padding-bottom: 10px;
        margin-left: 5px;
        margin-top: 5px;
    }

    #bid-table button {
        margin-top: 10px;
    }
    .auction-text {
        float: right;
        background: #eeeeee;
        text-decoration: underline black;
    }
</style>