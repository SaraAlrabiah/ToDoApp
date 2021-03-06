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
      private readonly ITokenService _tokenService;

        private string generatedToken = null;
       


        string BaseUrl = "https://localhost:7026/api/values/";//API

 
        public AccountController(IConfiguration config , ITokenService tokenService)
        {
            _configration = config;
            _tokenService = tokenService;

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
                var role = new UserModel();
               var validUser = new UserInfo();
                var user = new UserModel
                {
                    UserName = userModel.UserName,
                    Password = userModel.Password

                };

                IActionResult response1 = Unauthorized();
                using (var client = new HttpClient())
                {

                    string issuer = _configration["BaseUrl"];

                    client.BaseAddress = new Uri(issuer);

                    var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress + "GetCurrentUserRole");
                    if (user != null)
                    {
                        request.Content = new StringContent(
                            JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage responseMessage = await client.SendAsync(request );

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var clientResponse = responseMessage.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list
                        validUser = JsonConvert.DeserializeObject<UserInfo
                        >(clientResponse);

                        if (validUser.Role != "")
                        {

                            var username = user.UserName;
                            validUser = new UserInfo()
                            {
                                Role = validUser.Role,
                                UserName = userModel.UserName,
                                Password = userModel.Password
                            };

                            generatedToken = _tokenService.BuildToken( 
                            validUser);

                            if (generatedToken != null)
                            {

                                HttpContext.Session.SetString("Token", generatedToken);
                                HttpContext.Session.SetString("username", username);
                                var usernameis = HttpContext.Session.GetInt32(username);

                                return RedirectToAction("MainPage");
                            }
                        }
                        else
                        {

                            ModelState.AddModelError("", "You Email or password not correct");
                            return View("LoginView");
                        }
                    }
                }

            }

            return View("LoginView");
        }


        [HttpGet]
        public IActionResult MainPage()
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                string username = HttpContext.Session.GetString("username");

                if (token == null)
                {
                    return (RedirectToAction("Login"));
                }

                if (!_tokenService.IsTokenValid( token))
                {
                    return (RedirectToAction("Login"));
                }
              
            }
            catch (Exception)
            {
                throw;
            }
            return View();

        }

        [HttpGet]
        public IActionResult WelcomePage()
        {
           
            return View();

        }


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
                string result ;
                var user = new SignUpModel
                {
                    UserName = userModel.UserName,
                    Password = userModel.Password,
                 
                    Phone = userModel.Phone,



                };
                var validUser = new UserInfo();
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
                            validUser = new UserInfo()
                            {
                                Role = "Client",
                                UserName = userModel.UserName,
                                Password = userModel.Password
                            };

                            generatedToken = _tokenService.BuildToken(validUser);

                            if (generatedToken != null)
                            {

                                HttpContext.Session.SetString("Token", generatedToken);
                                HttpContext.Session.SetString("username", username);
                                var usernameis = HttpContext.Session.GetInt32(username);

                                return RedirectToAction("MainPage");
                            }
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

        [Authorize]
       // [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("WelcomePage");
        }
    }
}
