using Library.WordPress.Models;
using Microsoft.Data.SqlClient;

namespace Api.WordPress.Database
{
    public class SqlServerContext
    {
        private string _connectionString => $"Server={_serverName};Database={_databaseName};Trusted_Connection=True;TrustServerCertificate=True;";
        private string _serverName;
        private string _databaseName;
        private SqlServerContext()
        {
            _serverName = "CMILLS";
            _databaseName = "WordPress";
        }

        //CREATE
        public Blog Create (Blog blog)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "[blog].[Create]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@title", blog.Title));
                    cmd.Parameters.Add(new SqlParameter("@content", blog.Content));
                    var idParam = new SqlParameter { ParameterName = "@id", Value = blog.Id, Direction = System.Data.ParameterDirection.InputOutput };
                    cmd.Parameters.Add(idParam);

                    conn.Open();
                    object id = -1;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        blog.Id = int.Parse(idParam.Value.ToString());
                    } catch(Exception ex)
                    {

                    }
                }
            }

            return blog;
        }

        //READ
        public List<Blog> Read()
        {
            var returnList = new List<Blog>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = "SELECT ID, TITLE, CONTENT FROM [QUERY].BLOG";
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var blog = new Blog
                        {
                            Id = int.Parse(reader["ID"].ToString()),
                            Title = reader["TITLE"].ToString(),
                            Content = reader["CONTENT"].ToString()
                        };

                        returnList.Add(blog);
                    }
                }
            }
            return returnList;
        }

        //UPDATE

        //DELETE

        private static SqlServerContext? _instance;
        private static object _instanceLock = new object();
        public static SqlServerContext Current
        {
            get
            {
                lock (_instanceLock) { 
                    if (_instance == null)
                    {
                        _instance = new SqlServerContext();
                    }
                }
                return _instance;
            }
        }
    }
}
