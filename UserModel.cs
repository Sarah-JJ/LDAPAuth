﻿// ReSharper disable ConvertToAutoProperty

namespace LDAPClient
{
    public class UserModel
    {
        public UserModel(string dn, string uid, string ou, string userPassword)
        {
            _dn = dn;
            _uid = uid;
            _ou = ou;
            _userPassword = userPassword;
        }

        public string Dn => _dn;
        public string Uid => _uid;
        public string Ou => _ou;
        public string UserPassword => _userPassword;

        private readonly string _dn;
        private readonly string _uid;
        private readonly string _ou;
        private readonly string _userPassword;
    }
}
