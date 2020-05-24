<template>
<div class="dropdown">
    <button class="dropbtn btn btn-outline-primary">Welcome {{name}}</button>
    <div class="dropdown-content">
        <router-link to="/UserDetails">
            <a>Bruger profil</a>
        </router-link>
        <router-link to="/wallet">
            <a>Wallet</a>
        </router-link>
        <router-link to="/UpdateUser">
            <a>Opdater profil</a>
        </router-link>
    </div>
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
    .dropdown {
        position: relative;
        display: inline-block;
    }
    
    .dropdown-content {
        display: none;
        position: absolute;
        background-color: #f1f1f1;
        min-width: 160px;
        box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
        z-index: 1;
    }
    
    .dropdown-content a {
        color: black;
        padding: 12px 16px;
        text-decoration: none;
        display: block;
    }
    
    .dropdown-content a:hover {background-color: #ddd;}
    
    .dropdown:hover .dropdown-content {display: block;}
    
    .dropdown:hover .dropbtn {background-color: cornflowerblue;}
</style>