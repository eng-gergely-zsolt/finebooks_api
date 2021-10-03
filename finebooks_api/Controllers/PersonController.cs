using System.Data;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace finebooks_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PersonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            DataTable dataTable = new();
            SqlDataReader sqlDataReader;

            string query = @"EXEC SelectAllUser";
            string sqlDataSource = _configuration.GetConnectionString("FinebooksConnection");
            
            using (SqlConnection dbConnection = new(sqlDataSource))
            {
                dbConnection.Open();
                using SqlCommand sqlCommand = new(query, dbConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);

                sqlDataReader.Close();
                dbConnection.Close();
            }
            
            return new JsonResult(dataTable);
        }

        [HttpGet("{pUsername}")]
        public JsonResult Get(string pUsername)
        {
            DataTable dataTable = new();
            SqlDataReader sqlDataReader;

            string query = @"EXEC SelectUserByUsername @username = " + pUsername;
            string sqlDataSource = _configuration.GetConnectionString("FinebooksConnection");

            using (SqlConnection dbConnection = new(sqlDataSource))
            {
                dbConnection.Open();
                using SqlCommand sqlCommand = new(query, dbConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);

                sqlDataReader.Close();
                dbConnection.Close();
            }

            return new JsonResult(dataTable);
        }
    }
}
