<template>
    <div>
        <button class="btn btn-outline-primary" @click.prevent="show">Login</button>

        <modal name="modal-login" :width="400" :height="231">
            <div id="group">
                <div id="modal-div" class="row">
                    <div class="col">
                            <div class="row">
                    <div class="col-3">
                        <p>
                            Username/
                            Email
                        </p>
                    </div>
                    <div class="col-6">
                        <input type="text"
                               v-model="Username"
                               placeholder="Please enter username or email"
                               class="inputs">
                        <center><span class="text-danger" style="white-space: nowrap">
                        <font color="red" size="1">
                            {{UsernameValidationMsg}}
                        </font>
                    </span></center>
                    </div>
                </div>
                </div>
            </div>
                <div id="modal-div2" class="row">
                    <div class="col">
                        <div class="row">
                      <div class="col-3">
                        <p class="block">Password</p>
                    </div>
                    <div class="col-6">
                        <input type="password"
                               v-model="Password"
                               placeholder="Please enter password"
                               class="inputs">
                        <center><span class="text-danger" style="display: inline-block"><font color="red" size="1">{{PasswordValidationMsg}}</font></span></center>
                        <p></p>
                    </div>
                </div>
            </div>
        </div>
                <div id="modal-div3" class="row">
                    <div class="col">
                        <button id="login-button" class="btn btn-outline-primary"
                                v-on:click="sendApplication"
                                :disabled="sendDisabled">Login</button>
                    </div>
                </div>
            </div>
        </modal>
    </div>
</template>

<script>
        import axios from 'axios';
        import signup from './SignupModal';
    var instance = axios.create({
        baseURL: 'http://localhost:5000/api/',
    })
export default {
        comments:{
            'Signup-modal' : signup
        },
    data(){
        return{
            showLoginModal: false,
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
        show() {
            this.$modal.show('modal-login');
        },
        hide() {
            this.$modal.hide('modal-login');
        },
        sendPassword(id) {
            let ref = this;
            instance.post('/login/Pass', {
                UserId: id,
                Password: this.Password
            })
                .then(function (response) {
                    if (response.status !== 200) {
                        this.PasswordValidationMsg = "No password matching that account";
                        this.PasswordInputOk = false;
                        return;
                    }
                    localStorage.setItem("token", response.data);
                    axios.defaults.headers.common['Authorization'] = 'Bearer ' + response.data;
                    ref.hide();
                    ref.$store.dispatch("login");
                })
                .catch(function (err) {
                    var message = 'Fetch Error: ' + err;
                    window.console.log(message);

                });
        },
        sendApplication() {
            var ref = this;
            instance.post('/Login/UserN', {
                Username: this.Username
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
    }
}

</script>

<style scoped>
#group{
    padding: 30px 20px 20px 20px;
    border: 2px solid cornflowerblue;
}


.modal-login{
    border: 2px solid black;
    padding-top: 20px;
}

#login-button{
    margin-top:5px; 
}
.inputs{
    height:30px;
    width:200px;
}
</style>