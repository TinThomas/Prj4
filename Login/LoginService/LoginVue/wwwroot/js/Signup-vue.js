vm = new Vue({
    el: '#vueSignin',
    data: {
        introText: 'Please fill out the information to signup',
        Username: '',
        UsernameInputOk: false,
        UsernameValidationMsg: '',
        Email: '',
        EmailInputOk: false,
        EmailValidationMsg: '',
        Password: '',
        PasswordInputOk: false,
        PasswordValidationMsg: ''
    },
    watch: {
        name: function (newValue) {
            if (newValue == '') {
                this.UsernameValidationMsg = "Please fill the field";
                this.UsernameInputOk = false;
            } else {
                this.UsernameValidationMsg = '';
                this.UsernameInputOk = true;
            }
        },
        email: function(newValue) {
            if (newValue == '') {
                this.EmailValidationMsg = "Please fill the field";
                this.EmailInputOk = false;
            } else {
                this.EmailValidationMsg = '';
                this.EmailInputOk = true;
            }
        },
        password: function (newValue) {
            if (newValue == '') {
                this.PasswordValidationMsg = 'This field cannot be empty';
                this.PasswordInputOk = false;
            } else {
                this.PasswordValidationMsg = '';
                this.PasswordInputOk = true;
            }
        }
    },
    computed: {
        sendDisabled: function () {
            if (this.isSend)
                return true;
            return !this.UsernameInputOk;
        }
    },
    methods: {
        sendApplication() {
            let userData = {};
            userData.username = this.username;
            userData.email = this.email;
            userData.password = this.password;
        },
        annuller() {
            let data = {

            }
    }
})