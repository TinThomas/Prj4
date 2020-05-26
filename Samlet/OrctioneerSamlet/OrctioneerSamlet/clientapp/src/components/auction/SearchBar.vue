<template>
    <div>
        <h1>All auctions</h1>
        <div class="row">
            <div class="col-9">
                <input type="text" v-model="searchWord" placeholder="search auctions">
            </div>
            <div class="col-3">
                <label>Sort by:</label>
                <select v-model="sortBy" required id="sortSelector" @change="onChange($event)">
                    <option v-for="sort in sortOrders" :key="sort.id">{{sort}}</option>
                </select>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    methods:{
        onChange(event) {
            window.console.log(event.target.value);
            this.$store.dispatch('loadAuctions');
        }
    },
    computed:{
        searchWord: {
            get(){
                return this.$store.state.search;
            },
            set(value) {
                this.$store.dispatch('updateSearchWord', value);
            }
            
        },
        sortOrders() {
            return this.$store.state.sortOrders;
        },
        sortBy: {
            get(){
                return this.$store.state.sortBy;
            },
            set(value) {
                this.$store.dispatch('updateSortBy', value);
            }
        },

    },
    created() {
        this.$store.dispatch('updateSortBy', "Popularity")
    }
}
</script>

<style scoped>
    #sortSelector {
        margin-left: 5px;
    }
</style>