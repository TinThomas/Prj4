using System;
using System.Text.Json;
using System.Threading.Tasks;
using Login;
using Login.SigninControl.Interfaces;
using Login.SignupControl;
using Login.WalletControl.Modules;
using Login.WalletControl.Interfaces;

namespace Login
{
    public class CoreService
    {
        private ISigninControl _signin;
        private ISignup _signup;
        private IWalletControl _wallet;

        public CoreService(ISigninControl signin, ISignup signup)
        {
            _signin = signin;
            _signup = signup;
        }

        public CoreService(IWalletControl wallet)
        {
            _wallet = wallet;
        }

        public async Task<int> validateUsername(string b)
        {
            int check = _signin.validateUsername(b);
            return check;
        }

        public async Task<bool> validatePassword(int id,string msg)
        {
            return _signin.validatePW(id,msg);
        }

        public async Task<string> CreateUser(string msg)
        {
            return _signup.ConvertUserdata(msg);
        }

        public async Task<int> getAmount(int id)
        {
            return _wallet.getAmount(id);
        }

        public async Task<UserDataModel> getUser(int id)
        {
            return _wallet.getUser(id);
        }

        public async Task<Address> getAddress(int id)
        {
            return new Address();
        }

        public async void updateAmount(int id, int value)
        {
            _wallet.setAmount(id,value);
                
        }
    }
}