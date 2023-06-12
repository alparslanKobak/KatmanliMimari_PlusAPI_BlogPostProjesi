using P013KatmanliBlog.Core.Entities;
using System.Linq.Expressions;

namespace P013KatmanliBlog.Data.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPostByIncludeAsync(int id);

        Task<List<Post>> GetPostsByIncludeAsync(); // Tüm Postları User bilgisi ve Kategorisi ile getirecek olan metod

        Task<List<Post>> GetPostsByIncludeAsync(Expression<Func<Post, bool>> expression); // Yukarıdaki metodun lambda filtrelisi
    }
}
