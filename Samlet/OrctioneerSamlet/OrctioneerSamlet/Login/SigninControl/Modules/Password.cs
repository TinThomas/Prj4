using Login.Repos.PassRepo;
using Login.SigninControl.Interfaces;

namespace Login.SigninControl.Modules
{
    public class Password : IPassword
    {
        private PassRepo _pass;
        public Password()
        {
            _pass = PassRepo.GetInstance();
        }
        public bool validatePW(int id, string password)
        {
            bool check = _pass.CheckPassword(id, password);
            return check;
        }

    }
}
