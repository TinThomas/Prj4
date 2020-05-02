<template>
    <div id="full-auction">
        <div class="row">
            <div class="col">
                <img :src="testFunction[0].url">
            </div>
            <div class="col">
                <h3>{{testFunction[0].title}}</h3>
                <p id="description">{{testFunction[0].description}}</p>
            </div>
        </div>
        <div class="row" >
            <div class="col-3">
                <h3 >Current bid: </h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{testFunction[0].bid}} kr</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <h3>Time left:</h3>
            </div>
            <div class="col-3">
                <h3 class="auction-text">{{testFunction[0].timeLeft}}</h3>
            </div>
            <div class="col-6" id="bid-table">
                <p>Bid must be higher than: {{testFunction[0].bid}} kr</p>
                <input type="text" placeholder="Your bid">
                <button class="btn btn-secondary">Make a bid!</button>
            </div>
        </div>
    </div>    
</template>

<script>

export default {
    data(){
        return {
            id: this.$route.params.id,
            auctions: [
                {
                    title: 'Stort sværd', url: require('./../images/sværd.jpg'), bid: 1234, timeLeft: "1 day",
                    description: 'Det er et sort sværd, makker! Hvad er det du ikke har fortået? Et stort sværd. Det skal du da ha !Hvis du virkelig ikke kan se hvorfor du selvfølgelig bør eje et stort sværd. Ja.. Så er du måske bedre tjent med et liv som kvinde? Hva?? Nej, det tænkte jeg nok.. Selvfølgelig skal du da have et stort sværd. Jeg kunne se det på dig med det samme! Jamen hvad venter du på!? Byd forhelvede! Og træd ind i de voksnes rækker. Et stort sværd til en stor mand.'
                },
                { title: 'Hjelm og brynje', url: require('./../images/Hjelm_og_Brynje.jpg'), bid: 1234, timeLeft: "2 day" },
                { title: 'Faktisk økse', url: require('./../images/Faktisk_økse.jpg'), bid: 1234, timeLeft: "3 day" },
                { title: 'Én pil', url: require('./../images/En_pil.jpg'), bid: 1234, timeLeft: "4 day" },
            ],


            }
        },
        methods:{
            //testFunction: function () {
            //    return this.auctions.filter((auction) => {
            //        return this.specificAuction = auction.title.match(this.id)
            //    })
        },
        computed:{
            testFunction: function(){
                return this.auctions.filter((auction)=>{
                   return auction.title.match(this.id)
                })
            },
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