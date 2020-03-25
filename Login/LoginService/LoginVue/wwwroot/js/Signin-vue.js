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
        Username: function(newValue) {
            if (newValue == '') {
                this.UsernameValidationMsg = "*This field cannot be empty*";
                this.UsernameInputOk = false;
            } else {
                this.UsernameValidationMsg = '';
                this.UsernameInputOk = true;
            }
        },
        Password: function(newValue) {
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
        sendDisabled: function() {
            if (this.isSend)
                return true;
            return (!this.PasswordInputOk && !this.UsernameInputOk);
        }
    },
    methods: {
        sendApplication() {
            let login = {};
            login.username = this.Username;
            login.password = this.Password;
            fetch('/api/Signin/', {
                method: 'POST',
                body: JSON.stringify(login),
                headers: new Headers({
                    'Content-Type': 'application/json'
                })
            }).then(function (response) {
                if (response.status !== 200) {
                    vm.message = 'Looks like there was a problem. Status Code: ' + response.status;
                    return;
                }
                // Build the html
                response.json().then(function (application) {
                        vm.isSend = true;
                    vm.introText = 'We have now received your job application - thank you';
                    location.href = "../Home/Index";
                })
                    .catch(function (err) {
                        vm.message = 'Fetch Error: ' + err;
                    });
            });
        },
        sendPageChange: function() {
            location.href = 'Signup/Signup';
        }
    }
})