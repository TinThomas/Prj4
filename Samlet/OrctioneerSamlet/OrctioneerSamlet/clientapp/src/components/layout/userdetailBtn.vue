<template>
    <div>
    <router-link to="/UserDetails">
        <button type="button" class="btn btn-outline-primary">Welcome {{name}}</button>
    </router-link>
    </div>
</template>

<script>
    import axios from "axios";

    export default {
        data(){
            return{
                name: ''
            }
        },
        created(){
            var ref = this;
            axios.get('http://localhost:5000/api/User/getName')
                .then(function(response){
                    if(response.status !== 200)
                    {
                        window.console.log("Err" + response.status);
                    }
                    ref.name = response.data;
                }).catch(function(err){
                window.console.log(err);
            });
        }
    }
</script>

<style scoped>

</style>