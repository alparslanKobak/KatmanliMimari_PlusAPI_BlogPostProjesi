using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Models
{
    public class PostDetailViewModel
    {
        public Post Post { get; set; }

        public List<Post>? RelatedPosts { get; set; }
    }
}
