<template>
    <div id="uDetail">
        <div>
            <center><h2>-Personlig information-</h2></center>
        </div>
       <center><div class="col">
        <div class="row">
            <label>Navn: </label>
            <label style="padding-left: 5px">{{Name}}</label>
        </div>
            <div class="row">
                <label>Email: </label>
                <label style="padding-left: 5px">{{Email}}</label>
            </div>
            <div>
                <h2>-Fakturerings information-</h2>
            </div>
            <div class="row">
                <label>Addresse: </label>
                <label style="padding-left: 5px">{{Address}}</label>
            </div>
            <div class="row">
                <label>by: </label>
                <label style="padding-left: 5px">{{City}}</label>
            </div>
            <div class="row">
                <label>Postnummer: </label>
                <label style="padding-left: 5px">{{Zipcode}}</label>
            </div>
       </div></center>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data(){
            return{
                Name:'',
                Email:'',
                Address:'',
                City: '',
                Zipcode: '',
                Contry: ''
            }
        },
        created: function(){
            var ref = this;
            axios.get('http://localhost:5000/api/User/UserDetails')
            .then(function(response){
                if(response.status !== 200){
                    window.console.log("Err" + response.status);
                }
                ref.Name = response.data.name;
                ref.Email = response.data.email;
                ref.Address = response.data.address;
                ref.City = response.data.city;
                ref.Zipcode = response.data.zipcode;
                ref.Contry = response.data.contry;
            }).catch(function(err){
                window.console.log(err);
            });
        },
    }
</script>

<style scoped>
    #uDetail{
        max-width: 800px;
        min-height: 1000px;
        margin: 0 auto;
    }
</style>