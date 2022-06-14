using DataAccess.Model;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Application
{
    public interface IUserManager
    {


        public UserInfo GetRole(User userModel);
        public string UserStatus(User userModel);
        public string SignUp(User userModel);

        public int UserExistence(User user);

    }

}
