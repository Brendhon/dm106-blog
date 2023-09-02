using Blog.Models;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
  // Init logger
  private readonly ILogger<ReviewController> _logger;

  // Init UserInfoService
  private readonly UserInfoService _userInfoService;

  // Init review service
  private readonly ReviewService _reviewService;

  // Constructor
  public ReviewController(ILogger<ReviewController> logger, UserInfoService
  userInfoService, ReviewService reviewService)
  {
    _logger = logger;
    _userInfoService = userInfoService;
    _reviewService = reviewService;
  }

  // Get all reviews by post id
  [HttpGet]
  public ActionResult<Review> GetById(int? postId, int? reviewId)
  {
    // If the post id is not null, then return all reviews by post id
    if (postId is not null)
    {
      return Ok(_reviewService.GetAllByPostId(postId.Value)); // Return OK (200) and the reviews
    }

    // If the review id is not null, then return the review by id
    if (reviewId is not null)
    {
      // Get the review by id
      Review? review = _reviewService.GetById(reviewId.Value);

      // If the review is not null, then return the review
      if (review is not null)
      {
        return Ok(review); // Return OK (200) and the review
      }
      else
      {
        return NotFound(); // Return not found (404) if the review is null
      }
    }

    // Return bad request (400) if the post id and review id are null
    return BadRequest();
  }

  // Create review
  [HttpPost]
  public ActionResult Create(Review newReview)
  {
    // Log the new review
    _logger.LogInformation("New review... - PostId: " + newReview.PostId + " - Rating: " + newReview.Rating);

    // If the model state is not valid, then return bad request (400)
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Create the review
    Review? review = _reviewService.Create(newReview, userInfoModel);

    // If the review is not null, then return created (201) and the review
    // nameof(GetById) returns the name of the GetById method
    return CreatedAtAction(nameof(GetById), new { id = review!.Id }, review);
  }

  // Update review by id
  [HttpPut("{id}")]
  public ActionResult Update(int id, Review review)
  {
    // Log the update review
    _logger.LogInformation("Update review - id: " + id);

    // If the model state is not valid, then return bad request (400)
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState); // Return bad request (400)
    }

    // If the id is not equal to the review id, then return bad request (400)
    if (id != review.Id)
    {
      return BadRequest(); // Return bad request (400)
    }

    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Update the review
    Review? reviewUpdated = _reviewService.Update(id, review, userInfoModel);

    // If the review is not null, then return OK (200) and the review
    if (reviewUpdated is not null)
    {
      return Ok(reviewUpdated); // Return OK (200) and the review
    }

    // Return not found (404) if the review is null
    return NotFound();
  }

  // Delete review by id
  [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    // Log the delete review
    _logger.LogInformation("Delete review - id: " + id);

    // Get the user info from the request headers
    UserInfoModel userInfoModel = _userInfoService.GetUserInfo(Request.Headers);

    // Delete the review
    Review? reviewToBeDeleted = _reviewService.DeleteById(id, userInfoModel);

    // If the review is not null, then return the review
    if (reviewToBeDeleted is not null)
    {
      return Ok(reviewToBeDeleted); // Return OK (200) and the review
    }
    else
    {
      return NotFound(); // Return not found (404) if the review is null
    }
  }
}