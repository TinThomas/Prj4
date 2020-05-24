<template>
<div id="uWallet">
    <div v-if="!wallet">
        <p>Ser ud til du ikke har en konto, ønsker du at oprette en?</p>
        <router-link to="/CreateWallet">
            <button class="btn btn-outline-primary">Opret wallet</button>
        </router-link>
    </div>
    <div v-if="wallet">
        <div class="row">
            <label>Balance:</label>
            <label style="padding-left: 5px">{{balance}}</label>
        </div>
        <div class="row">
            <label>Kortnummer:</label>
            <label style="padding-left: 5px">{{kortnummer}}</label>
        </div>
        <div class="row">
            <label>Udløbs dato:</label>
            <label style="padding-left: 5px">{{expireM}}/{{expireY}}</label>
        </div>
        <div class="row">
            <div class="col-3">
                <p class="block">Beløb der skal tankes</p>
            </div>
            <div class="col-6">
                <input type="text"
                       v-model="Amount"
                       class="inputs">
        </div>
            <div>
                <button class="btn btn-outline-primary"
                v-on:click="SendMoney">Tilføj penge</button>
                <br/><br/><button class="btn btn-outline-primary"
                        v-on:click="DeleteWallet">Fjern kort</button>
            </div>
    </div>
</div>
</div>
</template>

<script>
    import axios from "axios";

    export default {
        data(){
            return{
                balance:'',
                kortnummer:'',
                expireM:'',
                expireY:'',
                Amount:'',
                wallet: false
            }
        },
        created(){
            var ref = this;
            axios.get('http://localhost:5000/api/Wallet/getWallet')
                .then(function(response){
                    if(response.status !== 200){
                        ref.wallet = false;
                    }
                    ref.balance = response.data.amount;
                    ref.kortnummer = response.data.card.cardNumber;
                    ref.expireM = response.data.card.expireMonth;
                    ref.expireY = response.data.card.expireYear;
                    ref.wallet = true;
                }).catch(function(err){
                window.console.log(err);
            });
        },
        methods:{
            SendMoney(){
                var ref = this;
                axios.post('http://localhost:5000/api/Wallet/addMoney',{
                    Amount : this.Amount
                })
                    .then(function(response){
                        if(response.status !== 200){
                            window.console.log("Something went wrong" + response.status);
                        }
                        axios.get('http://localhost:5000/api/Wallet/getBalance')
                            .then(function(response){
                                if(response.status !== 200)
                                {
                                    window.console.log("Err" + response.status);
                                }
                                ref.balance = response.data;
                            }).catch(function(err){
                            window.console.log(err);
                        });
                    }).catch(function(err){
                    window.console.log(err);
                });
            },
            DeleteWallet(){
                axios.delete('http://localhost:5000/api/Wallet')
                .then(function(response){
                    if(response.status !== 200)
                    {
                        window.console.log("Error" + response.status);
                        return;
                    }
                }).catch(function(err){
                    window.console.log(err);
                })
            }
        }
    }
</script>

<style scoped>
    #uWallet{
        max-width: 800px;
        min-height: 1000px;
        margin: 0 auto;
    }
</style>