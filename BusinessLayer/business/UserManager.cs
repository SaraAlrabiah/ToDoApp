using ApplicationLayer.Application;
using DataAccess.DbContexts;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.business
{
    
        public class UserManager : IUserManager
    {
            private readonly AppDbContext _db;
            //private readonly IUserManager _user;

            public UserManager(AppDbContext DB)
            {
            this._db = DB;
      //      this._user = user;
            }

       

        public int UserExistence(User user)
        {

            var userExistence = _db.Users.Any(x => x.Username == user.Username);
            if (userExistence)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        // public string GetRole(User userModel)
        //  {
        //      string role = "";
        //      var userObj = _db.Users.Where(x => x.Username.ToLower() == userModel.Username.ToLower()
        //          && x.Password == userModel.Password).FirstOrDefault();

        //      if (userObj != null)
        //      {
        //          var roleObj = _db.Roles.Where(x => x.ID == userObj.RoleID).FirstOrDefault();
        //          if (roleObj != null)
        //          {
        //              //UserRoleDto userInfo = new UserRoleDto()
        //              //{
        //              //    UserName = userObj.Username,
        //              //    Password = userObj.Password,
        //              //    Role = roleObj.Name
        //              //};
        //              //role = userInfo.Role;
        //          }
        //          else
        //          {
        //              //UserRoleDto userInfo = new UserRoleDto()
        //              //{
        //              //    UserName = userObj.Username,
        //              //    Password = userObj.Password,
        //              //    Role = string.Empty
        //              //};
        //              //role = string.Empty;
        //          }


        //      }
        //      return role;
        //  }



        public int SignUp(User userModel)
            {

                var user = new User
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Password = userModel.Password,
                    Phone = userModel.Phone,
                    RoleID = 2,
                    Username = userModel.Username,

                };
                _db.
                    Users.Add(user);
            _db.SaveChanges();
              
                return 1;


            }
            //public string UserStatus(User userModel)
            //{
            //    string status = "";
            //    var userObj = _db.Users.Where(x => x.Username.ToLower() == userModel.Username.ToLower()
            //      && x.Password == userModel.Password).FirstOrDefault();
            //    if (userObj != null)
            //    {
            //        if (userObj.loginStatus == 0)
            //        {
            //            status = "new";

            //        }
            //        else
            //        {
            //            status = "normal";
            //        }
            //    }

            //    return status;
            //}
        }
    }

