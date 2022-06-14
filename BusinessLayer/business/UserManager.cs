using ApplicationLayer.Application;
using AutoMapper;
using DataAccess.DbContexts;
using DataAccess.Model;
using DataAccess.Models;
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
        private IMapper _mapper;

        public UserManager(AppDbContext DB , IMapper mapper)
            {
            this._db = DB;
      //      this._user = user;
      _mapper = mapper;
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
        public UserInfo GetRole(User userModel)
        {
            UserInfo userInfo = new UserInfo();
            var userObj = _db.Users.Where(x => x.Username.ToLower() == userModel.Username.ToLower()
                && x.Password == userModel.Password).FirstOrDefault();

            if (userObj != null)
            {
                var roleObj = _db.Roles.Where(x => x.ID == userObj.RoleID).FirstOrDefault();
                if (roleObj != null)
                {
                    userInfo = new UserInfo()
                    {
                        UserName = userObj.Username,
                        Password = userObj.Password,
                        Role = roleObj.Name
                    };
                   
                }
                else
                {
               userInfo = new UserInfo()
                    {
                        UserName = userObj.Username,
                        Password = userObj.Password,
                        Role = string.Empty
                    };
                }


            }
            return userInfo;
        }



        public string SignUp(User userModel)
            {
            var result = ""; 
            if(userModel.Username != null && userModel.Password != null)
            {
                              var user = new User
                {
                 
                    FirstName = "",
                    LastName = "",
                    Password = userModel.Password,
                    Phone = userModel.Phone,
                    RoleID = 2,
                    Username = userModel.Username,
                    loginStatus = 1,
                    Status = 1,
                

                };
                var newUser = _mapper.Map<User>(user);

                // hash password
           //     user.Password = BCrypt.HashPassword(user.Password);

                // save user
                _db.Users.Add(user);
                _db.SaveChanges();
             //   _db. Users.Add(user);
              
                result = "1";
                
            }
            else
            {
               result = "0";
            }
          //  _db.Save();
          //  _db.SaveChanges();
            return result;

            }
        public  string UserStatus(User userModel)
        {
            string status = "";
            var   userObj = _db.Users.Where(x => x.Username.ToLower() == userModel.Username.ToLower()
              && x.Password == userModel.Password).FirstOrDefault();
            if (userObj != null)
            {
                if (userObj.loginStatus == 0)
                {
                    status = "new";

                }
                else
                {
                    status = "normal";
                }
            }

            return status;
        }

        
    }
    }

