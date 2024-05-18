using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.service
{
    public interface IUserService
    {
        public bool RegisterUser(string username, string password);
        public bool IsUsernameExists(string username);
        public bool IsLoginValid(string username, string password);
    }
}
