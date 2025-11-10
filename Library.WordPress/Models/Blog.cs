using Library.WordPress.DTO;
using Library.WordPress.Services;

namespace Library.WordPress.Models
{
    public class Blog
    {
        public string? Title { get; set; }
        public string? Content {  get; set; }
        public int Id { get; set; }
        public string LegacyData1 { get; set; }
        public string LegacyData2 { get; set; }
        public string LegacyData3 { get; set; }
        public string LegacyData4 { get; set; }
        public string LegacyData5 { get; set; }
        public string LegacyData6 { get; set; }
        public string Display
        {
            get
            {
                return ToString();
            }
        }
        public override string ToString()
        {
            return $"{Id}. {Title} - {Content}";
        }

        public Blog(BlogDTO blogDTO)
        {
            Id = blogDTO.Id;
            Title = blogDTO.Title;
            Content = blogDTO.Content;


        }
        public Blog()
        {
            LegacyData1 = string.Empty;
            LegacyData2 = string.Empty;
            LegacyData3 = string.Empty;
            LegacyData4 = string.Empty;
            LegacyData5 = string.Empty;
            LegacyData6 = string.Empty;
        }
        public Blog(int id)
        {
            var blogCopy = BlogServiceProxy.Current.Blogs.FirstOrDefault(b => (b?.Id ?? 0) == id);

            if (blogCopy != null)
            {
                Id = blogCopy.Id;
                Title = blogCopy.Title;
                Content = blogCopy.Content;
            }

            LegacyData1 = string.Empty;
            LegacyData2 = string.Empty;
            LegacyData3 = string.Empty;
            LegacyData4 = string.Empty;
            LegacyData5 = string.Empty;
            LegacyData6 = string.Empty;
        }
    }
}
