<template>
    <div id=auctionId>
        <div class="row">
            <div v-for="auc in auctions" class="single-auction" :key="auc.id">
                <img :src="auc.url">
                <h5 style="display: in">{{ auc.Title }}</h5>
                <h5>Buy Out Price: {{ auc.BuyOutPrice }}</h5>
                <h5>Ends: {{ auc.ExpirationDate }}</h5>

                <button @click="navigateToAuction(auc.ItemId)" class="btn btn-secondary">See full auction</button>


            </div>
        </div>

    </div>
</template>

<script>
    export default{
        props:{
            auctions: {
                type: Array,
                required: true
            },
        },
        data(){
            return{
            }
        },

        methods:{
            navigateToAuction(path){
                this.$router.push('/fullAuction2/'+path);
            },
        },
    }
</script>

<style scoped>
    #auctionId {
        max-width: 1200px;
        margin: 10px;
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
