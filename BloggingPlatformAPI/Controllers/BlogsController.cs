using Microsoft.AspNetCore.Mvc;
using BloggingPlatformAPI.Entities;
using System.Linq;
using BloggingPlatformAPI.Context;

namespace BloggingPlatformAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public BlogsController(BlogDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var blogs = _context.Blogs
                .AsEnumerable()   
                .Select(blog => new
                {
                    blog.Id,
                    blog.Title,
                    blog.Content,
                    blog.Category,
                    Tags = blog.Tags != null ? blog.Tags.Split(';') : new string[] { },
                    blog.CreatedAt,
                    blog.UpdatedAt
                }).ToList();

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _context.Blogs
                .AsEnumerable()  
                .Where(b => b.Id == id)
                .Select(b => new
                {
                    b.Id,
                    b.Title,
                    b.Content,
                    b.Category,
                    Tags = b.Tags != null ? b.Tags.Split(';') : new string[] { }, 
                    b.CreatedAt,
                    b.UpdatedAt
                }).FirstOrDefault();

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult PostBlog([FromBody] Blog blog)
        {
            blog.Tags = string.Join(";", blog.Tags?.Split(',') ?? Array.Empty<string>());

            _context.Blogs.Add(blog);
            _context.SaveChanges();

            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }

        [HttpPut("{id}")]
        public IActionResult PutBlog(int id, [FromBody] Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest("Blog ID in the URL does not match the ID in the body.");
            }

            blog.Tags = string.Join(";", blog.Tags?.Split(',') ?? Array.Empty<string>());

            _context.Entry(blog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!_context.Blogs.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
