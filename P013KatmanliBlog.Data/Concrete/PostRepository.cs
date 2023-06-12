using Microsoft.EntityFrameworkCore;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace P013KatmanliBlog.Data.Concrete
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Post> GetPostByIncludeAsync(int id)
        {

            return await _context.Posts.Include(x => x.User).Include(x => x.Category).FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<List<Post>> GetPostsByIncludeAsync()
        {
            return await _context.Posts.Include(x => x.User).Include(a => a.Category).ToListAsync();
        }

       

        public async Task<List<Post>> GetPostsByIncludeAsync(Expression<Func<Post, bool>> expression)
        {
            return await _context.Posts.Where(expression).Include(x => x.User).Include(a => a.Category).ToListAsync();
        }
    }
}
