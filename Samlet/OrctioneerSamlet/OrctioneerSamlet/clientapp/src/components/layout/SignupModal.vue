<template>
    <div>
        <button class="btn btn-outline-primary" @click.prevent="show">Signup</button>
        <modal name="modal-signup" :width="350" :height="342">
            <div id="group">
                <div id="modal-div" class="row">
                    <div class="col-3">
                        <br/>
                        <p>
                            Username
                        </p>
                    </div>
                    <div class="col">
                        <center><span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{UsernameValidationMsg}}
                            </font>
                        </span></center>
                        <input type="text"
                               v-model="Username"
                               placeholder="Please enter username"
                        class="inputs">
                    </div>
                </div>
                <div id="modal-div2" class="row">
                    <div class="col-3">
                        <br/>
                        <p>
                            Email
                        </p>
                    </div>
                    <div class="col">
                        <center><span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{EmailValidationMsg}}
                            </font>
                        </span></center>
                        <input type="text"
                               v-model="Email"
                               placeholder="Please enter Email"
                        class="inputs">
                    </div>
                </div>
                <div id="modal-div3" class="row">
                    <div class="col-3">
                        <br/>
                        <p>
                            Password
                        </p>
                    </div>
                    <div class="col">
                       <center><span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{PasswordValidationMsg}}
                            </font>
                        </span></center>
                        <input type="Password"
                               v-model="Password"
                               placeholder="Please enter username"
                               class="inputs">
                    </div>
                </div>
                <div id="modal-div4" class="row">
                    <div class="col-3">
                        <br/>
                        <p>
                            Confirm password
                        </p>
                    </div>
                    <div class="col">
                        <center><span class="text-danger" style="display: inline-block">
                            <font color="red" size="1">
                                {{CfmPasswordValidationMsg}}
                            </font>
                        </span></center>
                        <input type="Password"
                               v-model="CfmPassword"
                               placeholder="Please enter retype password"
                               class="inputs">
                    </div>
                </div>
                <div id="modal-div5" class="row">
                    <div class="col">
                        <button id="login-button" class="btn btn-outline-primary"
                                v-on:click="sendApplication"
                                :disabled="sendDisabled">Signup</button>
                    </div>
                    <div>
                        <center><span style="display: inline-block; background: springgreen">{{succes}}</span></center>
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
                CfmPasswordValidationMsg: '',
                succes: ''
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
                var checkPass = ref.validatePassword();
                var checkEmail = ref.validateEmail();
                var comparePass = (this.Password === this.CfmPassword);
                if (checkPass && checkEmail && comparePass) {
                    window.console.log("Sending");
                    instance.post('/Signup/Create', {
                        Username: signup.userName,
                        Email : signup.email,
                        Password : signup.password
                    })
                        .then(function (response) {
                            if (response.status !== 200) {
                                if (response.data === "User") {
                                    ref.UsernameValidationMsg = "User already exist";
                                    ref.UsernameInputOk = false;
                                    return;
                                }
                                else {
                                    ref.EmailValidationMsg = "Email already exist";
                                    ref.EmailInputOk = false;
                                    return;
                                }
                            }
                            ref.succes = "Kontoen er oprettet";
                        })
                        .catch(function (err) {
                            var message = 'Fetch Error: ' + err;
                            window.console.log(message);

                        });
                }
                else {
                    if (!checkPass) {
                        ref.PasswordValidationMsg = "Password er ikke sikkert";
                        ref.PasswordInputOk = false;
                    }
                    if (!checkEmail) {
                        ref.EmailValidationMsg = "Ugyldig email addresse";
                        ref.EmailInputOk = false;
                    }
                    if (!comparePass) {
                        ref.CfmPasswordValidationMsg = "Passwords matcher ikke";
                        ref.CfmPasswordInputOk = false;
                    }
                }

            },
            validatePassword() {

                //https://www.w3resource.com/javascript/form/letters-numbers-field.php for inspiration.
                //and for regexp: https://en.wikipedia.org/wiki/Regular_expression
                 var pass = this.Password;
                 const isLowercase = /[a-z]/.test(pass);
                 const isUppercase = /[A-Z]/.test(pass);
                 const isNumber = /[0-9]/.test(pass);
                if (this.Password.length >= 8 && isLowercase && isUppercase && isNumber) {
                    return true;
                }
                else {
                    return false;
                }

            },
            validateEmail: function () {
                if (this.Email.includes('@') && this.Email.includes('.')) {
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
        padding: 10px 20px 10px 20px;
        border: 2px solid cornflowerblue;
    }


    .modal-signup{
        border: 2px solid black;
    }

    #signup-button{
        margin-top:5px;
        margin-bottom:5px;
    }

    .inputs{
        height:30px;
        width:200px;
    }
</style>