vm = new Vue({
    el: '#vueSignin',
    data: {
        introText: 'Please fill out the information to signin',
        Username: '',
        UsernameInputOk: false,
        UsernameValidationMsg: '',
        Password: '',
        PasswordInputOk: false,
        PasswordValidationMsg: '',
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
            if (!this.PasswordInputOk || !this.UsernameInputOk)
                return true;
            return false;
        }
    },
    methods: {
        sendApplication() {
            let login = {};
            login.id = 1;
            login.msg = this.Username;
            fetch('/api/Signin/',
                {
                    method: 'POST',
                    body: JSON.stringify(login),
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                }).then(function(response) {
                    if (response.status !== 200) {
                        vm.UsernameValidationMsg = "User / Email doesn't exist";
                        vm.UsernameInputOk = false;
                        return;
                }
                response.json().then(function (application) {
                        vm.sendPassword();
                    })
                    .catch(function (err) {
                        vm.message = 'Fetch Error: ' + err;
                    });
            });

        },
        sendPassword() {
            let login = {};
            login.id = 2;
            login.msg = this.Password;
            fetch('/api/Signin/',
                {
                    method: 'POST',
                    body: JSON.stringify(login),
                    headers: new Headers({
                        'Content-Type': 'application/json'
                    })
                    }).then(function(response) {
                        if (response.status !== 200) {
                            vm.PasswordValidationMsg = "No password matching that account";
                            vm.PasswordInputOk = false;
                            return;
                        }
                        response.json().then(function (application) {
                            location.href = "../Home/Index";
                            this.isUser = true;
                        })
                            .catch(function(err) {
                                vm.message = 'Fetch Error: ' + err;
                            });
                    });
        

    },

        sendPageChange: function() {
                    location.href = 'Signup/Signup';
                }

    }
})