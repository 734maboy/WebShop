using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebShop
{
	public class AuthOptions
	{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "web-shop_secretK1E2Y3";   // ключ для шифрации
    public const int LIFETIME = 10; // время жизни токена - 3 минуты
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
      return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
  }
}
