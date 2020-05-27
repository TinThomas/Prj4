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
        <bid-card :id="id" :key="bidTableKey">
        </bid-card>
    </div>
</template>

<script>
    import BidCard from './BidCard.vue'
    export default {
        components: {
            'bid-card': BidCard
        },
        data(){
            return {
                id: this.$route.params.id,
                }
            },
        methods:{
           
            loadCurrentAuction() {
                var payload = {
                    id: this.id
                }

                this.$store.dispatch('loadCurrentAuction', payload);
            },            
        },
        computed:{
            currentAuction() {
                return this.$store.state.currentAuction;
            },
            urlTest() {
                return this.$store.state.currentPictureUrl;
            },
            bidTableKey(){
                return this.$store.state.currentBidTableKey;
            }
            
        },
        created() {
            this.loadCurrentAuction();

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