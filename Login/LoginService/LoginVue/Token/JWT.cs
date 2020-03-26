using System;
using System.IdentityModel;
using System.Security;
using System.Security.Cryptography;

namespace LoginVue.Token
{
    public class JWT
    {
        public string _secretKey { get; set; }

        public JWT()
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);
            _secretKey = key;
        }


    }
}