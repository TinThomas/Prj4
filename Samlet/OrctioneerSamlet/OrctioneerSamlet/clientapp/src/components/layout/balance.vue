<template>
    <div>
        <p>Balance {{amount}}</p>
    </div>
</template>

<script>
    import axios from 'axios'
    export default {
        data(){
            return{
                amount:0
            }
        },
        created(){
            var ref = this;
            axios.get('http://localhost:5000/api/Wallet/getBalance')
            .then(function(response){
                if(response.status !== 200)
                {
                    window.console.log("Err" + response.status);
                }
                ref.amount = response.data;
            }).catch(function(err){
                window.console.log(err);
            }); 
        }
    }
</script>

<style scoped>

</style>