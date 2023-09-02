namespace Blog.Models;
public class Post
{
  // Constructor with default values
  public Post()
  {
    Reviews = new HashSet<Review>();
    PublisherName = "none";
    PublisherEmail = "none";
  }
  
  // Post properties
  public int Id { get; set; }
  public required string PostTitle { get; set; }
  public required string PostContent { get; set; }
  public string PublisherName { get; set; }
  public string PublisherEmail { get; set; }
  public virtual ICollection<Review> Reviews { get; set; }
}