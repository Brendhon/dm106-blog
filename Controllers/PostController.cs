using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
  // Init logger
  private readonly ILogger<PostController> _logger;

  // Init UserInfoService
  private readonly UserInfoService _userInfoService;

  // Init post service
  private readonly PostService _postService;

  // Constructor 
  public PostController(ILogger<PostController> logger, UserInfoService userInfoService, PostService postService)
  {
    // Assign logger
    _logger = logger;

    // Inject UserInfoService
    _userInfoService = userInfoService;

    // Inject post service
    _postService = postService;
  }

  // Get all posts by publisher
  [HttpGet]
  public IEnumerable<Post> GetAllByPublisher()
  {
    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Return all posts by publisher
    return _postService.GetAllByPublisher(userInfoModel.Email);
  }

  // Get post by id
  [HttpGet("{id}")]
  public ActionResult<Post> GetById(int id)
  {
    // Get the post by id
    Post? post = _postService.GetById(id);

    // If the post is not null, then return the post
    if (post is not null)
    {
      // Return OK (200) and the post
      return Ok(post);
    }
    else
    {
      // Return not found (404) if the post is null
      return NotFound(); 
    }
  }

  // Create post
  [HttpPost]
  public ActionResult Create(Post newPost)
  {

    // If the model state is not valid, then return bad request (400)
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Create the post
    Post? post = _postService.Create(newPost, userInfoModel);

     // If the post is not null, then return created (201) and the post
    return CreatedAtAction(nameof(GetById), new { id = post!.Id }, post);
  }

  // Update post by id
  [HttpPut("{id}")]
  public ActionResult Update(int id, Post post)
  {
    // If the model state is not valid, then return bad request (400)
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState); // Return bad request (400)
    }
    if (id != post.Id)
    { 
      return BadRequest(); // Return bad request (400)
    }

    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Update the post
    Post? postUpdated = _postService.Update(id, post, userInfoModel);

    // If the post is not null, then return the post
    if (postUpdated is not null)
    {
      return Ok(postUpdated); // Return OK (200) and the post
    }
    return NotFound(); // Return not found (404) if the post is null
  }

  // Delete post by id
  [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Delete the post
    Post? postToBeDeleted = _postService.DeleteById(id, userInfoModel);

    // If the post is not null, then return the post
    if (postToBeDeleted is not null)
    {
      return Ok(postToBeDeleted); // Return OK (200) and the post
    }
    else
    {
      return NotFound(); // Return not found (404) if the post is null
    }
  }
}