using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.WebAPIUsing.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Post>? Posts { get; set; }
    }
}
