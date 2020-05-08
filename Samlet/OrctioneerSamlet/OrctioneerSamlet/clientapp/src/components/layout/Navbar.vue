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
                <modal-login id="login-modal"></modal-login>
            </div>
        </div>
    </div>
</template>

<script>
import ModalLogin from './LoginModal.vue'

export default {
  components:{
    'modal-login' : ModalLogin
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