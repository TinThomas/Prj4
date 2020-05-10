<template>
    <div id="navbar">
        <div class="row">
            <div class="col-6">
                <router-link to="/">
                    <button type="button" class="btn btn-secondary">Home</button>
                </router-link>
                <router-link to="/search">
                    <Button type="button" class="btn btn-secondary">Search</Button>
                </router-link>
                <router-link to="/addAuction">
                    <Button type="button" class="btn btn-secondary">New Auction</Button>
                </router-link>
            </div>
            <div class="col-6">
                <div class="row">
                    <modal-login id="login-modal" v-show="!loginState"></modal-login>
                    <modal-signup id="modal-signup" v-show="!loginState"></modal-signup>
                </div>
                <signout v-show="loginState"></signout>
            </div>
        </div>
    </div>
</template>

<script>
import ModalLogin from './LoginModal.vue'
import ModalSignup from './SignupModal'
import Signout from '../Utility/Signout.vue'

export default {
    components: {
        'modal-login': ModalLogin,
        'modal-signup': ModalSignup,
        'signout': Signout
    },
    computed:{
        loginState(){
            return this.$store.state.logged
        }
    }
    
}
</script>

<style scoped>
    #navbar {
        background: #eeeeee;
        padding: 10px 10px 10px 10px;
        box-sizing: border-box;
        text-align: center;
    }

    .btn-secondary {
        margin-right: 15px;
    }
</style>