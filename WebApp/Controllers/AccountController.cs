using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configration;
       // private readonly ITokenService _tokenService;

        private string generatedToken = null;

        string BaseUrl = "https://localhost:7026/api/values/";//API

        //public AccountController(IConfiguration config, ITokenService tokenService)
        //{
        //    _configration = config;
        //    _tokenService = tokenService;

        //}
        public AccountController(IConfiguration config)
        {
            _configration = config;

        }
        public IActionResult LoginView()
        {
            return View();
        }


        //[AllowAnonymous]
        //[Route("login")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel)
        {


            if (ModelState.IsValid)
            {
                string role = null;
              //  var validUser = new UserInfo();
                var user = new UserModel
                {
                    UserName = userModel.UserName,
                    Password = userModel.Password

                };

                IActionResult response1 = Unauthorized();
                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri(BaseUrl);

                    var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress + "GetCurrentUserRole");
                    if (user != null)
                    {
                        request.Content = new StringContent(
                            JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage responseMessage = await client.SendAsync(request);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        role = await responseMessage.Content.ReadAsStringAsync();
                        return RedirectToAction("LoginView");
                        //if (role != "")
                        //{

                        //    var username = user.UserName;
                        //    validUser = new UserInfo()
                        //    {
                        //        Role = role,
                        //        UserName = userModel.UserName,
                        //        Password = userModel.Password
                        //    };

                        //    generatedToken = _tokenService.BuildToken(_configration["Jwt:Key"].ToString(), _configration["Jwt:Issuer"].ToString(),
                        //    validUser);

                        //    if (generatedToken != null)
                        //    {

                        //        HttpContext.Session.SetString("Token", generatedToken);
                        //        HttpContext.Session.SetString("username", username);
                        //        var usernameis = HttpContext.Session.GetInt32(username);

                        //        return RedirectToAction("dashboard");
                        //    }
                        //}
                        //else
                        //{

                        //    ModelState.AddModelError("", "You Email or password not correct");
                        //    return View("LoginView");
                        //}
                    }
                }

            }

            return View("LoginView");
        }


        //// [Authorize(Roles = "Employee, Client")]
        //[Route("dashboard")]
        //[HttpGet]
        //public IActionResult dashboard()
        //{
        //    string token = HttpContext.Session.GetString("Token");
        //    string username = HttpContext.Session.GetString("username");

        //    if (token == null)
        //    {
        //        return (RedirectToAction("Login"));
        //    }

        //    if (!_tokenService.IsTokenValid(_configration["Jwt:Key"].ToString(),
        //        _configration["Jwt:Issuer"].ToString(), token))
        //    {
        //        return (RedirectToAction("Login"));
        //    }
        //    //  ViewBag.BaseMvcUrl = BaseMvcUrl;
        //    return View();

        //}



        public IActionResult SignUpView()
        {
            return View();
        }
        [AllowAnonymous]
      //  [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel userModel)
        {

            if (ModelState.IsValid)
            {
                string result = null;
                var user = new SignUpModel
                {
                    UserName = userModel.UserName,
                    Password = userModel.Password,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    PhoneNumber = userModel.PhoneNumber,



                };
                var validUser = new SignUpModel();
                IActionResult response1 = Unauthorized();
                using (var client = new HttpClient())
                {


                    client.BaseAddress = new Uri(BaseUrl);

                    var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress + "UserSignUp");
                    if (user != null)
                    {
                        request.Content = new StringContent(
                            JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage responseMessage = await client.SendAsync(request);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        result = await responseMessage.Content.ReadAsStringAsync();
                        if (result != "0")
                        {

                            var username = user.UserName;
                            //validUser = new UserModel()
                            //{
                            //    Role = "Client",
                            //    UserName = userModel.UserName,
                            //    Password = userModel.Password
                            //};

                            //generatedToken = _tokenService.BuildToken(_configration["Jwt:Key"].ToString(), _configration["Jwt:Issuer"].ToString(),
                            //validUser);

                            //if (generatedToken != null)
                            //{

                            //    HttpContext.Session.SetString("Token", generatedToken);
                            //    HttpContext.Session.SetString("username", username);
                            //    var usernameis = HttpContext.Session.GetInt32(username);

                            //    return RedirectToAction("dashboard");
                            //}
                        }
                        else
                        {
                            ModelState.AddModelError("", "This Email already used");
                            return View("SignUpView");
                        }
                    }
                }



            }
            return View("SignUpView");
        }



        public IActionResult Error()
        {

            return View();
        }

        //[Authorize]
        //[Route("Logout")]
        //[HttpGet]
        //public IActionResult Logout()
        //{

        //    HttpContext.Session.Clear();
        //    return View("LoginView");
        //}
    }
}
