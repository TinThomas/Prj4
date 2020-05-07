<template>
    <div>
        <p><center>{{introText}}</center></p>
        <br />
        <center>
            <div>
                <div>
                    <form>
                        <div class="form-group">
                            <label />
                            <span class="text-danger">
                                <font color="red" size="1">
                                    {{UsernameValidationMsg}}
                                </font>
                            </span><br />
                            <label for="username">Username: </label>
                            <input type="text"
                                   v-model="Username"
                                   placeholder="Please enter username">*
                        </div>
                        <div class="form-group">
                            <label />
                            <span class="text-danger">
                                <font color="red" size="1">
                                    {{PasswordValidationMsg}}
                                </font>
                            </span><br />
                            <label for="password" style="padding: 3px">Password:</label>
                            <input type="Password"
                                   v-model="Password"
                                   placeholder="Please enter password">*
                        </div><br />
                        <div class="form-group">
                            <center>
                                <button type="button" class="button"
                                        v-on:click="sendApplication"
                                        :disabled="sendDisabled">
                                    Signin
                                </button>&nbsp;&nbsp;
                                <button type="button" class="button"
                                        v-on:click="sendPageChange">
                                    Signup
                                </button>&nbsp;&nbsp;
                            </center>
                        </div>
                    </form>
                    <div>
                    </div>
                    <button type="button" class="button"
                            v-on:click="sendToken">
                        send token
                    </button>&nbsp;&nbsp;
                </div>
                <button type="button" class="button"
                        v-on:click="logout">
                    logout
                </button>&nbsp;&nbsp;
            </div>
        </center>
    </div>
</template>

<script>
    import axios from 'axios';
    var instance = axios.create({
        baseURL: 'http://localhost:5000/api/',
    })
    export default {
        data() {
            return {
                introText: 'Please fill out the information to signin',
                Username: '',
                UsernameInputOk: false,
                UsernameValidationMsg: '',
                Password: '',
                PasswordInputOk: false,
                PasswordValidationMsg: '',
            }
        },
        watch: {
            Username: function (newValue) {
                if (newValue == '') {
                    this.UsernameValidationMsg = "*This field cannot be empty*";
                    this.UsernameInputOk = false;
                } else {
                    this.UsernameValidationMsg = '';
                    this.UsernameInputOk = true;
                }
            },
            Password: function (newValue) {
                if (newValue == '') {
                    this.PasswordValidationMsg = '*This field cannot be empty*';
                    this.PasswordInputOk = false;
                } else {
                    this.PasswordValidationMsg = '';
                    this.PasswordInputOk = true;
                }
            }
        },
        computed: {
            sendDisabled: function () {
                if (!this.PasswordInputOk || !this.UsernameInputOk)
                    return true;
                return false;
            }
        },

        methods: {
            sendPassword(id) {
                instance.post('/login/Pass', {
                    id: id,
                    msg: this.Password
                })
                    .then(function (response) {
                        if (response.status !== 200) {
                            this.PasswordValidationMsg = "No password matching that account";
                            this.PasswordInputOk = false;
                            return;
                        }
                        localStorage.setItem("token", response.data);
                        axios.defaults.headers.common['Authorization'] = 'Bearer ' + response.data;
                        instance.defaults.headers.common['Authorization'] = 'Bearer ' + response.data;
                    })
                    .catch(function (err) {
                        var message = 'Fetch Error: ' + err;
                        window.console.log(message);

                    });
            },
            sendApplication() {
                var ref = this;
                instance.post('/Login/UserN', {
                    msg: this.Username
                })
                    .then(function (response) {
                        if (response.status !== 200) {
                            this.UsernameValidationMsg = "User / Email doesn't exist";
                            this.UsernameInputOk = false;
                        }
                        ref.sendPassword(response.data);
                    })
                    .catch(function (err) {
                        var message = 'Fetch Error: ' + err;
                        window.console.log(message);
                    });

            },

            sendToken: function () {
                instance.post('/Login/Token', {
                    msg: localStorage.getItem('token')
                }).then(function (response) {
                    if (response.status !== 200) {
                        window.console.log("Welp");
                    }
                })
            },
            sendPageChange: function () {
            },
            logout: function () {
                localStorage.removeItem('token');
            }

        }

    }
</script>