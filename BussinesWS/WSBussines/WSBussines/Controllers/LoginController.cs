using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Odbc;
using WSBussines.Models;

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
        public User LoginClient(string username, string password)
        {
            string value = string.Format("Username: {0} - Password: {1}", username, password);
            User user = new User();
            user.id = "";
            user.username = "";
            user.password = "";
            user.usertype = "";
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
                                while (reader.Read())
                                {
                                    if (!string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("user_id")).ToString()))
                                    {
                                        user.id = reader.GetValue(reader.GetOrdinal("user_id")).ToString();
                                    }
                                    if (!string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("email")).ToString()))
                                    {
                                        user.username = reader.GetValue(reader.GetOrdinal("email")).ToString();
                                    }
                                    if (!string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("user_password")).ToString()))
                                    {
                                        user.password = reader.GetValue(reader.GetOrdinal("user_password")).ToString();
                                    }
                                    if (!string.IsNullOrEmpty(reader.GetValue(reader.GetOrdinal("user_type")).ToString()))
                                    {
                                        user.usertype = reader.GetValue(reader.GetOrdinal("user_type")).ToString();
                                    }
                                }
                            }
                            else
                            {
                                value += @"\n Loggin Failed";
                            }
                        }
                    }
                }
                Console.Write(value);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return user;
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
