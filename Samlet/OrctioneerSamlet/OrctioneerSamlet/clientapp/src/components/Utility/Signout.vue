<template>
    <div>
        <button class="btn btn-outline-primary" @click.prevent="logout">Signout</button>
    </div>
</template>

<script>
    import axios from "axios";

    export default {
        methods:{
            logout() {
                axios.defaults.headers.common['Authorization'] = null;
                localStorage.removeItem('token');
                this.$store.dispatch("logout");
            }
        }
    }
</script>