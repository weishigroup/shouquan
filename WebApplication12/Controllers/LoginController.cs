using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication12.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication12.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> LoginAsync(string name)
        {
            var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, "weiyunchuan@163.com"),
    new Claim("FullName", "weiyunchuan"),
    new Claim(ClaimTypes.Role, "Administrator"),
};

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //cookie 的绝对过期时间，会覆盖ExpireTimeSpan的设置。

                IsPersistent = true,
                //表示 cookie 是否是持久化的以便它在不同的 request 之间传送。设置了ExpireTimeSpan或ExpiresUtc是必须的。

                //IssuedUtc = <DateTimeOffset>,
                //  凭证认证的时间。

                //RedirectUri = <string>
                //http 跳转的时候的路径。
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return "登录成功";
        }
        [AllowAnonymous]
        public string GetToken()
        {
            var _jwtSetting = (JwtSetting)HttpContext.RequestServices.GetService(typeof(JwtSetting));
            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]{
                    new Claim(ClaimTypes.Name,"wesley"),
                    //new Claim(ClaimTypes.Role,"admin"),
                    new Claim("Admin","true"),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SignKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSetting.Issuer,
                _jwtSetting.Audience,
                claims,
                null,
                DateTime.Now.AddMinutes(120),
                credentials
            );

            var tokenstr= new JwtSecurityTokenHandler().WriteToken(token);
            return tokenstr;
             
        }

        [HttpGet]
        public string GetHello()
        {
            return "hello world";
        }
    }
}
