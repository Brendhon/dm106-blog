using Blog.Models;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services;

public class ReviewService
{
  // Init blog context
  private readonly BlogContext _context;

  public ReviewService(BlogContext blogContext)
  {
    _context = blogContext;
  }

  // Get all reviews by post id
  public IEnumerable<Review> GetAllByPostId(int postId)
  {
    return _context.Reviews // Access the Reviews table
     .AsNoTracking() // This line improves performance but makes the entities read-only
     .Where(r => r.PostId == postId) // Filter by post id
     .ToList(); // Execute the query and convert the result to a list
  }

  // Get review by id
  public Review? GetById(int postId)
  {
    return _context.Reviews // Access the Reviews table
     .AsNoTracking() // This line improves performance but makes the entities read-only
     .SingleOrDefault(r => r.Id == postId); // Find the review with the given id
  }

  // Create review
  public Review? Create(Review newReview, UserInfoModel userInfoModel)
  {
    // Find the post with the given id
    Post? postToReview = _context.Posts.Find(newReview.PostId);

    // If the post is null, then return null
    if (postToReview is null) return null;

    // If the publisher email of the post is not equal to the email of the user, then throw an OperationNotAllowedException
    if (postToReview.PublisherEmail.Equals(userInfoModel.Email))
    {
      throw new OperationNotAllowedException(403, "You're not allowed to comment your post");
    }

    // Set the reviewer email and name
    newReview.ReviewerEmail = userInfoModel.Email; // Set the reviewer email
    newReview.ReviewerName = userInfoModel.Name; // Set the reviewer name

    // Add the review to the Reviews table
    _context.Reviews.Add(newReview); // Add the review to the Reviews table
    _context.SaveChanges(); // Save the changes to the database

    // Return the new review
    return newReview;
  }

  // Update review
  public Review? Update(int id, Review review, UserInfoModel userInfoModel)
  {
    // Find the review with the given id
    Review? reviewToUpdate = _context.Reviews.SingleOrDefault(p => p.Id == id);

    // If the review is null, then return null
    if (reviewToUpdate is null) return reviewToUpdate;

    // If the reviewer email of the review is not equal to the email of the user, then throw an OperationNotAllowedException
    if (!reviewToUpdate.ReviewerEmail.Equals(userInfoModel.Email))
    {
      throw new OperationNotAllowedException(403, "You're not allowed to update this review");
    }

    // Update the review
    reviewToUpdate.Comment = review.Comment;

    // Update the review rating
    reviewToUpdate.Rating = review.Rating;

    // Update the review
    _context.Entry(reviewToUpdate).State = EntityState.Modified; // Mark the review as modified
    _context.SaveChanges(); // Save the changes to the database

    // Return the updated review
    return reviewToUpdate;
  }

  // Delete review by id
  public Review? DeleteById(int id, UserInfoModel userInfoModel)
  {
    // Find the review with the given id
    Review? reviewToDelete = _context.Reviews.Find(id);

    // If the review is not null, then delete it
    if (reviewToDelete is not null)
    {

      // If the reviewer email of the review is equal to the email of the user, then delete the review
      if (reviewToDelete.ReviewerEmail.Equals(userInfoModel.Email))
      {
        _context.Reviews.Remove(reviewToDelete); // Remove the review
        _context.SaveChanges(); // Save the changes to the database
      }
      else // If the reviewer email of the review is not equal to the email of the user, then throw an OperationNotAllowedException
      {
        throw new OperationNotAllowedException(403, "You're not allowed to delete this review");
      }
    }

    // Return the deleted review
    return reviewToDelete;
  }
}