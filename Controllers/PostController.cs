using Microsoft.AspNetCore.Mvc;

namespace Post.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
  // Init logger
  private readonly ILogger<PostController> _logger;

  // Constructor 
  public PostController(ILogger<PostController> logger)
  {
    // Assign logger
    _logger = logger;
  }

  // GET api/post
  [HttpGet]
  public string GetAll()
  {
    // Log information
    _logger.LogInformation("Get all posts");

    // Return all posts
    return "All posts";
  }
}