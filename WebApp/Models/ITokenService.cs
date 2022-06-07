using WebApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
  public interface ITokenService
    {
        string BuildToken( UserInfo user);
        bool IsTokenValid( string token);
    }
}
