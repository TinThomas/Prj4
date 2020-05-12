<template>
    <div id="full-auction">
        <div class="row">
            <div class="col">
                <!--<img :src="">-->
                <img src="./../../images/sværd.jpg">
            </div>
            <div class="col">
                <h3>{{auctionTest.Title}}</h3>
                <p id="description">{{auctionTest.DescriptionOfItem}}</p>
            </div>
        </div>
        <div class="row" >
            <div class="col-3">
                <h3 >Current bid: </h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{auctionTest.BuyOutPrice}}kr</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <h3>Ends:</h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{auctionTest.ExpirationDate | moment("from", now)}}</h3>
                
            </div>
            <div class="col-6" id="bid-table">
                <p>Bid must be higher than: {{auctionTest.BuyOutPrice}} kr</p>
                <input type="text" placeholder="Your bid">
                <button class="btn btn-secondary">Make a bid!</button>
            </div>
        </div>
    </div>    
</template>

<script>
    import axios from 'axios';
    import moment from 'vue-moment'
export default {
    data(){
        return {
            id: this.$route.params.id,
            auctions: [
                { "ItemId": 1, "Title": "Elven Bow BUY NOW", "BuyOutPrice": 5000, "ExpirationDate": "2020-12-24T00:00:00", "DateCreated": "2020-05-07T10:21:47.6980913", "UserIdSeller": 12, "DescriptionOfItem": "Powerful bow that is best at the range of 30-50 meters", "Sold": false, "Images": null, "Tags": null, "Bids": null }
            ],

            auctionTest: ''

            }
        },
        methods:{
            //testFunction: function () {
            //    return this.auctions.filter((auction) => {
            //        return this.specificAuction = auction.title.match(this.id)
            //    })
        },
        computed:{
            //testFunction: function(){
            //    return this.auctions.filter((auction)=>{
            //       return auction.ItemId.match(this.id)
            //    })
            //},
        },
        created() {
            var ref = this;
            axios.get('http://localhost:5000/api/ItemEntity/item/' + ref.id).then(function (response) {
                if (response.status != 200) {
                    //error
                }
                //ref.test = response.data.splice(0, 4);
                ref.auctionTest = response.data;
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

#bid-table button{
    margin-top:10px;
}

img{
    width: 400px;
    height: 400px;
    box-shadow: grey 0 0 10px;
    border: 1px solid white;
    margin-top: 10px;
    margin-bottom: 10px;
    float: left;
}

</style>