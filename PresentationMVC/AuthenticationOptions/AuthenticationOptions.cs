using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PresentationMVC.AuthificationOptions
{
    public class AuthenticationOptions
    {
        public const string ISSUER = "CleanArchit";
        public const string AUDIENCE = "CustomAuthenticationClin";
        public const string KEY = "key32";
        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
