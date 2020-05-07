<template>
    <div>
        <web-header></web-header>
        <web-navbar></web-navbar>
        <router-view></router-view>
    </div>
</template>

<script>
    import Header from './components/layout/Header.vue'
    import Navbar from './components/layout/Navbar.vue'
    import axios from 'axios'


    export default {
        components: {
            'web-header': Header,
            'web-navbar': Navbar
        },
        created: function () {
            axios.interceptors.response.use(undefined, function (err) {
                if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
                    localStorage.removeItem('token');
                }
                throw err;
            });
        }
    }
</script>

<style>

    #app {
        font-family: 'Avenir', Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>