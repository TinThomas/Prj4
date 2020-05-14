<template>
    <div class="row">
    <nav class="sidenav">
        <router-link to="/">
            <a>placeholder</a>
        </router-link>
        <router-link to="/">
            <a>placeholder</a>
        </router-link>
        <router-link to="/">
            <a>placeholder</a>
        </router-link>
    </nav>
    <main>
        <div>
            <center><h2 style="padding-left: 5px">-Personlig information-</h2></center>
        </div>
        <div class="col" style="padding-left: 50px">
        <div class="row">
            <label>Navn: </label>
            <label style="padding-left: 5px">{{Name}}</label>
        </div>
            <div class="row">
                <label>Email:</label>
                <label style="padding-left: 5px">{{Email}}</label>
            </div>
            <div>
                <h2 style="padding-left: 5px">-Fakturerings information-</h2>
            </div>
            <div class="row">
                <label>Addresse:</label>
                <label style="padding-left: 5px">{{Address}}</label>
            </div>
            <div class="row">
                <label>by:</label>
                <label style="padding-left: 5px">{{City}}</label>
            </div>
            <div class="row">
                <label>Postnummer</label>
                <label style="padding-left: 5px">{{Zipcode}}</label>
            </div>
        </div>
    </main>
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
    /*From: https://www.w3schools.com/howto/howto_css_fixed_sidebar.asp*/
.sidenav{
    height:100%;
    width:160px;
    position: page;
    z-index:1;
    top:0;
    left:0;
    background-color:#eeeeee;
    overflow-x:hidden;
    padding-top:20px;
}
.sidenav a{
    padding: 6px 8px 6px 16px;
    text-decoration: none;
    font-size:25px;
    color: cornflowerblue;
    display:block;
}
    
    .sidenav a:hover{
        color: white;
        background: cornflowerblue;
    }
</style>