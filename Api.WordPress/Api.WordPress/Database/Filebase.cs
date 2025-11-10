using Library.WordPress.Models;
using Newtonsoft.Json;

namespace Api.WordPress.Database
{
    public class Filebase
    {
        private string _root;
        private string _blogRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp";
            _blogRoot = $"{_root}\\Blogs";
        }

        public int LastBlogKey
        {
            get
            {
                if (Blogs.Any())
                {
                    return Blogs.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        public Blog AddOrUpdate(Blog blog)
        {
            //set up a new Id if one doesn't already exist
            if(blog.Id <= 0)
            {
                blog.Id = LastBlogKey + 1;
            }

            //go to the right place
            string path = $"{_blogRoot}\\{blog.Id}.json";
            

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(blog));

            //return the item, which now has an id
            return blog;
        }
        
        public List<Blog> Blogs
        {
            get
            {
                var root = new DirectoryInfo(_blogRoot);
                var _blogs = new List<Blog>();
                foreach(var patientFile in root.GetFiles())
                {
                    var patient = JsonConvert
                        .DeserializeObject<Blog>
                        (File.ReadAllText(patientFile.FullName));
                    if(patient != null)
                    {
                        _blogs.Add(patient);
                    }

                }
                return _blogs;
            }
        }


        public bool Delete(string type, string id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            return true;
        }
    }


   
}