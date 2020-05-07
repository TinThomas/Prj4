<template>
    <div id="header">
        <div class="row">
            <div class="col-md-4" id="logo" @click="navigateToAction">
                <center><h1>Hello World</h1></center>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        methods: {
            navigateToAction() {
                this.$router.push('/');
            }
        }
    }
</script>