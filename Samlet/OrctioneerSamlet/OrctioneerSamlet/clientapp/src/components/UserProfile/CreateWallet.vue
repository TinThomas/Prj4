<template>
    <div id="cWallet">
        <div class="row">
          <span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{cardWarning}}
                            </font>
                  </span><br/>
            <label>Kortnummber</label>
            <input type="text"
                   v-model="cardnumber">
        </div>
        <div>
        <span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{expireMwarning}}
                            </font>
                    </span><br/>
            <label>Udløbs månede</label>
            <input type="text"
            v-model="expireM">
        </div>
        <div>
            <span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{ExpireYWarning}}
                            </font>
                    </span><br/>
            <label>Udløbs år</label>
            <input type="text"
            v-model="expireY">
        </div>
        <div>
           <span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{CVVWarning}}
                            </font>
           </span><br/>
            <label>CVV</label>
            <input type="text"
            v-model="CVV">
        </div>
        <button class="btn btn-outline-primary"
        v-on:click="CreateWallet"
        :disabled="sendDisabled">Opret wallet</button>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        data(){
            return{
                cardnumber:'',
                cardWarning : '',
                cardInputOk : false,
                expireM:'',
                expireMwarning : '',
                expireMInputOk : false,
                expireY:'',
                ExpireYWarning : '',
                ExpireYInputOk : '',
                CVV:'',
                CVVWarning : '',
                CVVInputOk : false
            }
        },
        watch: {
            cardnumber: function (newValue) {
                if (newValue == '') {
                    this.cardWarning = "Can't be empty";
                    this.cardInputOk = false;
                } else {
                    this.cardWarning = '';
                    this.cardInputOk = true;
                }
            },
            expireM: function (newValue) {
                if (newValue == '') {
                    this.expireMwarning = "Can't be empty";
                    this.expireMInputOk = false;
                } else {
                    this.expireMwarning = '';
                    this.expireMInputOk = true;
                }
            },
            expireY: function (newValue) {
                if (newValue == '') {
                    this.ExpireYWarning = "Can't be empty";
                    this.ExpireYInputOk = false;
                } else {
                    this.ExpireYWarning = '';
                    this.ExpireYInputOk = true;
                }
            },
            CVV: function (newValue) {
                if (newValue == '') {
                    this.CVVWarning = "Can't be empty";
                    this.CVVInputOk = false;
                } else {
                    this.CVVWarning = '';
                    this.CVVInputOk = true;
                }
            }
        },        
        computed: {
            sendDisabled: function () {
                if (!this.cardInputOk || !this.CVVInputOk || !this.expireMInputOk || !this.ExpireYInputOk)
                    return true;
                return false;
            }
        },
        methods:{
            CreateWallet(){
                var ref = this;
                if(ref.cardnumber.length !== 16)
                {
                    this.cardWarning = 'Ugyldige kort oplysninger';
                    return;
                }
                else if(ref.CVV.length !== 3)
                {
                    this.CVVWarning = 'Ugyldigt CVV nummer';
                    return;
                }
                else{
                    let kort = {};
                    kort.ExpireMonth = ref.expireM;
                    kort.ExpireYear = ref.expireY;
                    kort.CardNumber = ref.cardnumber;
                        
                    axios.post('http://localhost:5000/api/Wallet/Create',{
                        card : kort,
                        Amount : 0
                    }).then(function (response) {
                        if (response !== 200) {
                            window.console.log("error");
                        }

                    }.catch(function (err){
                        window.console.log(err);
                    }))
                }
            }
        }
    }
</script>

<style scoped>
    #cWallet{
        max-width: 800px;
        min-height: 1000px;
        margin: 0 auto;
    }
</style>