using System;
using System.Collections.Generic;
using System.Linq;


namespace LDAPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 4)
            {
                Console.WriteLine("Usage: LDAPClient <username> <domain> <password> <LDAP server URL>");
                Environment.Exit(1);
            }

            var client = new Client(args[0], args[1], args[2], args[3]);

            try
            {
                //Adding a user
                client.AddUser(new UserModel("uid=sampleuser,ou=users,dc=example,dc=com",
                    "sampleuser", "users", "plaintextpass"));
            } catch(Exception e)
            {
                //The user may already exist
                Console.WriteLine(e);
            }

            //Searching for all users
            var searchResult = client.Search("ou=users,dc=example,dc=com", "objectClass=*");
            foreach(Dictionary<string, string> d in searchResult)
            {
                Console.WriteLine(String.Join("\r\n", d.Select(x => x.Key + ": " + x.Value).ToArray()));
            }

            //Validating credentials
            if(client.ValidateUser("sampleuser", "plaintextpass"))
            {
                Console.WriteLine("Valid credentials");
            }
            else
            {
                Console.WriteLine("Invalid credentials");
            }

            //Validating credentials using LDAP bind
            //For this to work the server must be configured to map users correctly to its internal database
            if(client.ValidateUserByBind("sampleuser", "plaintextpass"))
            {
                Console.WriteLine("Valid credentials (bind)");
            }
            else
            {
                Console.WriteLine("Invalid credentials (bind)");
            }

            //Modifying a user
            client.ChangeUserUid("sampleuser", "newsampleuser");

            //Deleting a user
            client.Delete("uid=newsampleuser,ou=users,dc=example,dc=com");
        }
    }
}
