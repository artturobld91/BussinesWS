using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Odbc;

namespace WSBussines.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/Login/LoginClient/{username}/{password}")]
        [HttpGet]
        // GET: api/Login/5
        public string LoginClient(string username, string password)
        {
            string value = string.Format("Username: {0} - Password: {1}", username, password);

            try
            {
                using (OdbcConnection connection = new OdbcConnection("DSN=BussinesDB;UID=postgres;PWD=password"))
                {
                    connection.Open();
                    using (OdbcCommand command = connection.CreateCommand())
                    { 
                        command.CommandText = string.Format("Select * from public.Users where email = '{0}' and user_password = '{1}'", username, password);
                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                value += @"\n Login Successful";
                            }
                            else
                            {
                                value += @"\n Loggin Failed";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return value;
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
