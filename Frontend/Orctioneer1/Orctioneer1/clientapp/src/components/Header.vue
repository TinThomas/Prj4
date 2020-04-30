<template>
    <div id="header">
        <div class="row" >
            <div class="col-md-4" id="logo" @click="navigateToAuction"></div>
        </div>   
    </div>
</template>

<script>
    export default{       
        methods:{
            navigateToAuction(){
                this.$router.push('/');
            },
        },
    }
</script>

<style scoped>
    h1 {
        color: azure;
        font-size: 50px;
        font-family: Georgia, Georgia, 'Times New Roman', Times, serif;
        margin-left: 20px;
        }


    #header {
        background-image: url("../images/orcbackround2.jpg");
        background-size: 100%;
        background-position: center;
        background-repeat: no;
        background-position: bottom;
        height: 200px;
        padding: 10px;
        }

    #logo{
        background-image: url("../images/OrctioneerLogo1.png");
        background-size: 100%;
        background-repeat: repeat-x;
        height: 150px;
        padding: 10px;
       
    }
</style>
