<template>
    <div class="single-auction">
        <img :src="url" >
        <h5 class="title-format">{{ auction.Title }}</h5>
        <h5>
            
            Bid: <span> {{ getHighestBid }}</span>
        </h5>

        <h5>Ends: {{ auction.ExpirationDate | moment("from") }}</h5>

        <button @click="navigateToAuction(auction.ItemId)" class="btn btn-secondary" id="buttonId">See full auction</button>

    </div>
</template>

<script>
    export default {
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

        methods: {
            navigateToAuction(path){
                this.$router.push('/FullAuction/'+path);
            },
            checkImage() {
                if (this.auction.Image == null) {
                    this.url = require("./../../images/missingimage.jpg")
                }
                else {
                    this.url = require("./../../images/" + this.auction.Image)
                }
            },
            
            
        },
        computed: {
            getHighestBid() {
                return this.$store.getters.getHighestBid(this.auction.Bids);
            }
                        
        },
        created() {
            this.checkImage();
        }
    }
</script>

<style scoped>
    .single-auction {
        background: #eeeeee;
        width: 194px;
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
        width: 184px;
        height: 184px;
        border: 1px solid white;
    }
</style>
