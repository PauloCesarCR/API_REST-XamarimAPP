using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Data.Entity;
using System;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class UsersController : Controller
    {
        private readonly BancoContext _context;

        public UsersController(BancoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            return _context.Users;
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUserId([FromRoute]string id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserModel> PostUser(PostUsersRequest user)
        {
            try
            {        
              var userAdd = new UserModel
                {
                    firstName = user.firstName,
                    surName = user.surName,
                    age = user.age,
                    creationDate = DateTime.UtcNow
                };

                _context.Users.Add(userAdd);
                _context.SaveChanges();

                return CreatedAtAction("GetUserId", new UserModel { id = userAdd.id}, userAdd);
            }
            catch 
            {
                return BadRequest();
            }
  
        }

        [HttpPut("{id}")]
        public ActionResult PutUser(PostUsersRequest user,string id)
        {

            var userAtual = _context.Users.Find(id);

            if (userAtual == null)
            {
                return BadRequest();
            }

            userAtual.firstName = user.firstName;
            userAtual.surName = user.surName;
            userAtual.age = user.age;
            userAtual.creationDate = userAtual.creationDate;

            _context.SaveChanges();
            return Ok(userAtual);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
        {
            var userAtual = _context.Users.Find(id);

            if (userAtual == null)
            {
                return BadRequest();

            }
            _context.Users.Remove(userAtual);
            _context.SaveChanges();
            return Ok("Usuario Deletado com Sucesso");
        }
    }
}