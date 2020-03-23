vm = new Vue({
    el: '#vueSignin',
    data: {
        introText: 'Please fill out the information to signin',
        Username: '',
        UsernameInputOk: false,
        UsernameValidationMsg: '',
        Password: '',
        PasswordInputOk: false,
        PasswordValidationMsg: ''
    },
    watch: {
        name: function(newValue) {
            if (newValue == '') {
                this.UsernameValidationMsg = "This field cannot be empty";
                this.UsernameInputOk = false;
            } else {
                this.UsernameValidationMsg = '';
                this.UsernameInputOk = true;
            }
        },
        Password: function(newValue) {
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
        sendDisabled: function() {
            if (this.isSend)
                return true;
            return !this.UsernameInputOk;
        }
    },
    methods: {
        sendApplication() {
            let login = {};
            login.username = this.username;
            login.password = this.password;
        }
    }
})