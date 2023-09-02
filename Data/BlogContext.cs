using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog.Data;

public class BlogContext : DbContext
{
  public BlogContext(DbContextOptions<BlogContext> options) : base(options)
  {
  }

  // DbSets from models - Posts and Reviews
  public DbSet<Post> Posts => Set<Post>();
  public DbSet<Review> Reviews => Set<Review>();
}