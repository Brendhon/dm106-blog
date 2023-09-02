using System.IdentityModel.Tokens.Jwt;
namespace Blog.Services
{
  public class UserInfoService
  {
    public UserInfoModel GetUserInfo(IHeaderDictionary headers)
    {
      // Get the Google ID token from the request headers
      if (headers.TryGetValue("X-MS-TOKEN-GOOGLE-ID-TOKEN", out var googleIdToken))
      {

        // Decode the Google ID token
        JwtSecurityToken? token = new JwtSecurityToken(jwtEncodedString: googleIdToken);

        // Create a UserInfoModel object from the decoded token
        UserInfoModel userInfoModel = new UserInfoModel()
        {
          Email = token.Claims.First(c => c.Type == "email").Value,
          Name = token.Claims.First(c => c.Type == "name").Value
        };

        // Return the UserInfoModel object
        return userInfoModel;
      }
      else
      {
        // Return a UserInfoModel object with default values
        return new UserInfoModel()
        {
          Email = "none",
          Name = "none"
        };
      }
    }
  }
}