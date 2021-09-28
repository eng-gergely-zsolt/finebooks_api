using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using finebooks_api.Models;
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
            string query = @"SELECT * FROM dbo.Department";
            DataTable table = new();

            string sqlDataSource = _configuration.GetConnectionString("FinebooksConn");

            SqlDataReader myReader;
            using (SqlConnection myCon = new(sqlDataSource))
            {
                myCon.Open();
                using SqlCommand myCommand = new(query, myCon);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();
            }

            

            return new JsonResult(table);
        }
    }
}
