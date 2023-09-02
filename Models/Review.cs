namespace Blog.Models;
public class Review
{
  // Constructor with default values
  public Review()
  {
    ReviewerName = "none";
    ReviewerEmail = "none";
  }

  // Review properties
  public int Id { get; set; }
  public required string Comment { get; set; }
  public int Rating { get; set; }
  public string ReviewerName { get; set; }
  public string ReviewerEmail { get; set; }
  public int PostId { get; set; }
}