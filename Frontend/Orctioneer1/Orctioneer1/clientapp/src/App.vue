<template>
    <div>
        <web-header></web-header>
        <web-navbar></web-navbar>

        <router-view></router-view>

        <web-footer></web-footer>

    </div>
</template>

<script>
    import Header from './components/Header.vue'
    import Navbar from './components/Navbar.vue'
    import Footer from './components/Footer.vue'

    export default {
        components: {
            'web-header': Header,
            'web-navbar': Navbar,
            'web-footer': Footer
        },
        data() {
            return {
                auctions: [

                ]
            }
        },
        methods: {

        }
    }
</script>

<style>

    body {
        margin: 0;
        font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;
    }
</style>
