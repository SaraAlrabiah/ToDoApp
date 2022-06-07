using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Application
{
    public interface IUserManager
    {


        public string GetRole(User userModel);
        public string UserStatus(User userModel);
        public int SignUp(User userModel);

        public int UserExistence(User user);

    }

}
