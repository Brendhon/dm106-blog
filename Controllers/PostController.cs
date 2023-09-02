using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Post.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
  // Init logger
  private readonly ILogger<PostController> _logger;

  // Init UserInfoService
  private readonly UserInfoService _userInfoService;

  // Constructor 
  public PostController(ILogger<PostController> logger, UserInfoService userInfoService)
  {
    // Assign logger
    _logger = logger;

    // Inject UserInfoService
    _userInfoService = userInfoService;
  }

  // GET api/post
  [HttpGet]
  public string GetAll()
  {
    // Log information
    _logger.LogInformation("Get all posts");

    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Log user info
    _logger.LogInformation("Email: " + userInfoModel.Email);
    _logger.LogInformation("Name: " + userInfoModel.Name);

    // Return all posts
    return "All posts";
  }
}