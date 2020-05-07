<template>
    <div>
        <p><center>{{introText}}</center></p>
        <center>
            <div>
                <div>
                    <form>
                        <div class="form-group">
                            <label for="username">Username: </label>
                            <input type="text"
                                   v-model="Username"
                                   placeholder="Please enter username">*
                            <span class="text-danger">{{UsernameValidationMsg}}</span>
                        </div>
                        <div class="form-group">
                            <label for="email" style="padding: 19px">Email:</label>
                            <input type="text"
                                   v-model="Email"
                                   placeholder="Please enter email">*
                            <span class="text-danger">{{EmailValidationMsg}}</span>
                        </div>
                        <div class="form-group">
                            <label for="Password" style="padding: 3px">Password:</label>
                            <input type="password"
                                   v-model="Password"
                                   placeholder="Please enter password">*
                            <span class="text-danger">{{PasswordValidationMsg}}</span>
                        </div>
                        <div class="form-group">
                            <label for="cfmPassword" style="padding: 3px">Confirm Password:</label>
                            <input type="password"
                                   v-model="CfmPassword"
                                   placeholder="Please enter password">*
                            <span class="text-danger">{{CfmPasswordValidationMsg}}</span>
                        </div>
                        <div class="form-group">
                            <center>
                                <button type="button" class="button"
                                        v-on:click="sendApplication"
                                        :disabled="sendDisabled">
                                    Signup
                                </button>&nbsp;&nbsp;
                                <button type="button" class="button"
                                        v-on:click="annuller">
                                    Annuller
                                </button>
                            </center>
                        </div>
                    </form>
                </div>
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
        introText: 'Please fill out the information to signup',
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
        sendApplication() {
            var ref = this;
            let signup = {};
                signup.userName = this.Username;
                signup.password = this.Password;
                signup.email = this.Email;
            if (ref.validatePassword() && ref.validateEmail()) {
                window.console.log("Sending");
                let data = JSON.stringify(signup);
                instance.post('/Signup/Create', {
                    id: 0,
                    msg: data
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