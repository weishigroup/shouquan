using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication12.Models
{
    public class JwtSetting
    {
        public string Issuer { set; get; }
        public string Audience { set; get; }
        public string SignKey { set; get; }
    }
}
