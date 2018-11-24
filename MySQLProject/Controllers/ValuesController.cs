using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySQLProject.Models;

namespace MySQLProject.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        // GET api/values
        [Route("api/users")]
        [HttpGet]
        public List<User> Get()
        {
            WebApiContext context = HttpContext.RequestServices.GetService(typeof(MySQLProject.Models.WebApiContext)) as WebApiContext;
            return context.GetUsers();
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public User GetUser(int id)
        {
            WebApiContext context = HttpContext.RequestServices.GetService(typeof(MySQLProject.Models.WebApiContext)) as WebApiContext;
            return context.GetUser(id);
        }

        [Route("api/user/{id}")]
        [HttpDelete]
        public object DeleteUser(int id)
        {
            WebApiContext context = HttpContext.RequestServices.GetService(typeof(MySQLProject.Models.WebApiContext)) as WebApiContext;

            try
            {
                if (context.DeleteUser(id)) 
                {
                    return Ok(context.GetUsers());
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("api/user")]
        [HttpPost]
        public object CreateUser([FromBody]User value)
        {
            WebApiContext context = HttpContext.RequestServices.GetService(typeof(MySQLProject.Models.WebApiContext)) as WebApiContext;

            try
            {
                context.CreateUser(value);
                return Ok(context.GetUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
