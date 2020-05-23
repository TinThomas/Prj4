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
                <div v-if="!loginState" style="float: right" class="row">
                    <modal-login id="login-modal" style="padding-right: 20px"></modal-login>
                    <modal-signup id="modal-signup" style="padding-right: 20px"></modal-signup>
                </div>
                <div v-if="loginState">
                    <signout style="float: right; padding-right: 20px"></signout>
                    <userBtn style="float: right; padding-right: 20px"></userBtn>
                    <balance style="float: right; padding-right: 20px"></balance>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import ModalLogin from './LoginModal.vue'
import ModalSignup from './SignupModal'
import Signout from '../Utility/Signout.vue'
import Userbtn from './userdetailBtn.vue'
import Balance from './balance.vue'

export default {
    components: {
        'modal-login': ModalLogin,
        'modal-signup': ModalSignup,
        'signout': Signout,
        'userBtn': Userbtn,
        'balance' : Balance
    },
    computed:{
        loginState(){
            return this.$store.state.logged;
        }
        
    },
    
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