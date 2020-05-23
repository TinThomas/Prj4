<template>
    <div>
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
</template>

<script>
    export default {
        
        methods: {
            loadCurrentBids() {
                var payload = {
                    id: this.currentAuction.ItemId
                }
                this.$store.dispatch('loadCurrentBids', payload);
            },
        },
        computed: {
            currentBids() {
                return this.$store.state.currentBids;
            },
            currentAuction() {
                return this.$store.state.currentAuction;
            }
        },
        created() {
            this.loadCurrentBids();
        }
    }
</script>

<style scoped>

</style>