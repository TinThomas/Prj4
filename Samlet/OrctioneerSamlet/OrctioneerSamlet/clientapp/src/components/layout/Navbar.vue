<template>
    <div id="navbar">
        <div class="row">
            <div class="col-6">
                <router-link to="/">
                    <button type="button" class="btn btn-secondary">Home</button>
                </router-link>
                <router-link to="/Login">
                    <button type="button" class="btn btn-secondary">Login</button>
                </router-link>
                <router-link to="/Signup">
                    <button type="button" class="btn btn-secondary">Signup</button>
                </router-link>
                <router-link to="/Wallet">
                    <button type="button" class="btn btn-secondary">Wallet</button>
                </router-link>
                <router-link to="/test">
                    <button type="button" class="btn btn-secondary">test</button>
                </router-link>
            </div>
        </div>
    </div>
</template>

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