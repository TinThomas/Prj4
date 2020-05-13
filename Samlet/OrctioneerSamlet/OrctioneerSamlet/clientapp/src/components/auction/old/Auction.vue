<template>
    <div id=auctionId>
        <div class="row">
            <div v-for="auc in auctions" class="single-auction" :key="auc.id" >
                <img :src="auc.url" >
                <h5 >{{ auc.title }}</h5>
                <h5>Current bid: {{ auc.bid }}</h5>
                <h5>Time left: {{ auc.timeLeft }}</h5>
                <button @click="navigateToAuction(auc.title)" class="btn btn-secondary">See full auction</button>
                
                
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
                this.$router.push('/fullAuction/'+path);
            },
        },
    }
</script>

<style scoped>
    #auctionId{
         max-width: 1200px;
         margin: 10px;
    }
    /*.single-auction{
        background: #eeeeee;;
        padding:5px;
        margin: 5px;
    }*/
    img{
        object-fit: cover;
        width: 180px;
        height: 180px;
        border: 1px solid white;
    }
</style>
