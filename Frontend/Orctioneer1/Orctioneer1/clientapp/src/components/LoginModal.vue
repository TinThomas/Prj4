<template>
    <div>
        <button class="btn btn-outline-primary" @click.prevent="show">Login</button>
        
        <modal name="modal-login" :width="400" :height="208">
        <div id="group">
            <div id="modal-div" class="row">
                <div class="col-3">
                <p>Email</p>
                </div>
                <div class="col">
                <input>
                </div>
            </div>
            <div id="modal-div2" class="row">
                <div class="col-3">
                <p class="block">Password</p>
                </div>
                <div class="col">
                <input type="password" id="password">
                </div>
            </div>
            <div id="modal-div3" class="row">
                <div class="col-3">
                </div>
                <div class="col">
                <button id="login-button" class="btn btn-outline-primary">Login</button>
                </div>
            </div>    
            <div id="modal-div4" class="row">
                <div class="col-3">
                </div>
                <div class="col">
                    <p>No account yet?  <button class="btn btn-outline-primary">Sign-up!</button></p>
                </div>
            </div>  
        </div>           
        </modal>
    </div>
</template>

<script>
export default {
    data(){
        return{
            showLoginModal: false
        }
    },
    methods: {
    show () {
        this.$modal.show('modal-login');
    },
    hide () {
        this.$modal.hide('modal-login');
    }
}
}

</script>

<style scoped>
#group{
    padding: 30px 20px 0px 20px;
    border: 2px solid cornflowerblue;
}


.modal-login{
    border: 2px solid black;
    padding-top: 20px;
}

#login-button{
    margin-top:5px; 
}
</style>