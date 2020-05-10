<template>
    <div>
        <web-header></web-header>
        <web-navbar></web-navbar>
        <router-view></router-view>
        <web-footer></web-footer>
    </div>
</template>

<script>
    import Header from './components/layout/Header.vue'
    import Navbar from './components/layout/Navbar.vue'
    import Footer from './components/layout/Footer.vue'
    
    export default {
        components: {
            'web-header': Header,
            'web-navbar': Navbar,
            'web-footer': Footer
        },
    }
</script>

<style>

    #app {
        /*font-family: 'Avenir', Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;*/
    }

    body {
        margin: 0;
        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
    }
</style>