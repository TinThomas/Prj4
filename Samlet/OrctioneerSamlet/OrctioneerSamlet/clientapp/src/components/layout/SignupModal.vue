<template>
    <div>
        <button class="btn btn-outline-primary" @click.prevent="show">Signup</button>

        <modal name="modal-signup" :width="600" :height="400">
            <div id="group">
                <div id="modal-div" class="row">
                    <div class="col-3">
                        <p>
                            Username
                        </p>
                    </div>
                    <div class="col">
                        <span class="text-danger">
                            <font color="red" size="1">
                                {{UsernameValidationMsg}}
                            </font>
                        </span>
                        <input type="text"
                               v-model="Username"
                               placeholder="Please enter username">
                    </div>
                </div>
                <div id="modal-div2" class="row">
                    <div class="col-3">
                        <p>
                            Email
                        </p>
                    </div>
                    <div class="col">
                        <span class="text-danger">
                            <font color="red" size="1">
                                {{EmailValidationMsg}}
                            </font>
                        </span>
                        <input type="text"
                               v-model="Email"
                               placeholder="Please enter Email">
                    </div>
                </div>
                <div id="modal-div3" class="row">
                    <div class="col-3">
                        <p>
                            Password
                        </p>
                    </div>
                    <div class="col">
                        <span class="text-danger">
                            <font color="red" size="1">
                                {{PasswordValidationMsg}}
                            </font>
                        </span>
                        <input type="Password"
                               v-model="Password"
                               placeholder="Please enter username"
                               :width="200"
                               :height="10">
                    </div>
                </div>
                <div id="modal-div4" class="row">
                    <div class="col-3">
                        <p>
                            Confirm password
                        </p>
                    </div>
                    <div class="col">
                        <span class="text-danger">
                            <font color="red" size="1">
                                {{CfmPasswordValidationMsg}}
                            </font>
                        </span>
                        <input type="Password"
                               v-model="CfmPassword"
                               placeholder="Please enter retype password"
                               :width="200"
                               :height="10">
                    </div>
                </div>
                <div id="modal-div5" class="row">
                    <div class="col-3">
                    </div>
                    <div class="col">
                        <button id="login-button" class="btn btn-outline-primary"
                                v-on:click="sendApplication"
                                :disabled="sendDisabled">Signup</button>
                    </div>
                </div>
            </div>
        </modal>
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
                Username: '',
                UsernameInputOk: false,
                UsernameValidationMsg: '',
                Email: '',
                EmailInputOk: false,
                EmailValidationMsg: '',
                Password: '',
                PasswordInputOk: false,
                PasswordValidationMsg: '',
                CfmPassword: '',
                CfmPasswordInputOk: false,
                CfmPasswordValidationMsg: ''
            }
        },
        watch: {
            Username: function (newValue) {
                if (newValue == '') {
                    this.UsernameValidationMsg = "Please fill the field";
                    this.UsernameInputOk = false;
                } else {
                    this.UsernameValidationMsg = '';
                    this.UsernameInputOk = true;
                }
            },
            Email: function(newValue) {
                if (newValue == '') {
                    this.EmailValidationMsg = "Please fill the field";
                    this.EmailInputOk = false;
                } else {
                    this.EmailValidationMsg = '';
                    this.EmailInputOk = true;
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
                if (!this.PasswordInputOk || !this.UsernameInputOk || !this.EmailInputOk)
                    return true;
                return false;
            }
        },
        methods: {
            show(){
                this.$modal.show('modal-signup');
            },
            hide(){
                this.$modal.hide('modal-signup');
            },
            sendApplication() {
                var ref = this;
                let signup = {};
                signup.userName = this.Username;
                signup.password = this.Password;
                signup.email = this.Email;
                if (ref.validatePassword() && ref.validateEmail()) {
                    window.console.log("Sending");
                    instance.post('/Signup/Create', {
                        Username: signup.userName,
                        Email : signup.email,
                        Password : signup.password
                    })
                        .then(function (response) {
                            if (response.status !== 200) {
                                if (response.data === "User") {
                                    this.UsernameValidationMsg = "User already exist";
                                    this.UsernameInputOk = false;
                                }
                                else {
                                    this.EmailValidationMsg = "Email already exist";
                                    this.EmailInputOk = false;
                                }

                                return;
                            }

                        })
                        .catch(function (err) {
                            var message = 'Fetch Error: ' + err;
                            window.console.log(message);

                        });
                }
                else {
                    window.console.log("Something went wrong");
                }

            },
            validatePassword() {

                //https://www.w3resource.com/javascript/form/letters-numbers-field.php for inspiration.
                //and for regexp: https://en.wikipedia.org/wiki/Regular_expression
                // var pass = this.Password;
                /* const isLowercase = () => /[a-z]/?.test();
                 const isUppercase = () => /[A-Z]/?.test();
                 const isNumber = () => /[0-9]/?.test();
                 window.console.log(pass);
                 window.console.log("is upper" + isUppercase(pass));
                 window.console.log("is number" + isNumber(pass));
                 window.console.log("is lower" + isLowercase(pass))*/
                if (this.Password.length >= 8) {
                    window.console.log("return true");
                    return true;
                }
                else {
                    return false;
                }

            },
            validateEmail: function () {
                if (this.Email.includes('@') && this.Email.includes('.')) {
                    window.console.log("return true");
                    return true;
                }
                else {
                    return false;
                }
            },

            annuller: function() {
            }
        }
    }
</script>
<style scoped>
    #group{
        padding: 30px 20px 0px 20px;
        border: 2px solid cornflowerblue;
    }


    .modal-signup{
        border: 2px solid black;
        padding-top: 20px;
    }

    #signup-button{
        margin-top:5px;
    }
</style>