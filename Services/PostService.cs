using Blog.Models;
using Blog.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services;

public class PostService
{
  // Init blog context
  private readonly BlogContext _context;
  
  public PostService(BlogContext blogContext)
  {
    _context = blogContext;
  }

  // Get all posts by publisher
  public IEnumerable<Post> GetAllByPublisher(string publisherEmail)
  {
    return _context.Posts // Access the Posts table
     .AsNoTracking() // This line improves performance but makes the entities read-only
     .Where(p => p.PublisherEmail.Equals(publisherEmail)) // Filter by publisher email
     .ToList(); // Execute the query and convert the result to a list
  }

  // Get post by id
  public Post? GetById(int id)
  {
    return _context.Posts // Access the Posts table
     .AsNoTracking() // This line improves performance but makes the entities read-only
     .Include(p => p.Reviews) // Include the Reviews table (this is needed to get the reviews of the post)
     .SingleOrDefault(p => p.Id == id); // Find the post with the given id
  }

  // Create post
  public Post? Create(Post newPost, UserInfoModel userInfoModel)
  {
    // Set the publisher email and name
    newPost.PublisherEmail = userInfoModel.Email; // Set the publisher email
    newPost.PublisherName = userInfoModel.Name; // Set the publisher name

    // Add the post to the Posts table
    _context.Posts.Add(newPost);

    // Save the changes to the database
    _context.SaveChanges();

    // Return the new post
    return newPost;
  }

  // Update post
  public Post? Update(int id, Post post, UserInfoModel userInfoModel)
  {
    // Find the post with the given id
    Post? postToUpdate = _context.Posts.SingleOrDefault(p => p.Id == id);

    // If the post is null, then return null
    if (postToUpdate is null) return postToUpdate;

    // If the publisher email of the post is not equal to the email of the user, then throw an OperationNotAllowedException
    if (!postToUpdate.PublisherEmail.Equals(userInfoModel.Email))
    {
      throw new OperationNotAllowedException(403, "You're not allowed to update this post");
    }

    // Update the post
    postToUpdate.PostContent = post.PostContent;

    // Update the post title
    postToUpdate.PostTitle = post.PostTitle;

    // Update the post
    _context.Entry(postToUpdate).State = EntityState.Modified; // Mark the post as modified
    _context.SaveChanges(); // Save the changes to the database

    // Return the updated post
    return postToUpdate;
  }

  // Delete post
  public Post? DeleteById(int id, UserInfoModel userInfoModel)
  {
    // Find the post with the given id
    Post? postToDelete = _context.Posts.Find(id);

    // If the post is not null, then delete it
    if (postToDelete is not null)
    {
      // If the publisher email of the post is equal to the email of the user, then delete the post
      if (postToDelete.PublisherEmail.Equals(userInfoModel.Email))
      {
        _context.Posts.Remove(postToDelete); // Remove the post
        _context.SaveChanges(); // Save the changes to the database
      }
      else // If the publisher email of the post is not equal to the email of the user, then throw an OperationNotAllowedException
      {
        throw new OperationNotAllowedException(403, "You're not allowed to delete this post");
      }
    }

    // Return the deleted post
    return postToDelete;
  }
}