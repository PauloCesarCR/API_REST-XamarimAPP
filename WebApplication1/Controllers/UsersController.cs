using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Data.Entity;

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
            int idAtual = _context.Users.Count() + 1;
            try
            {
             var id = $"b{idAtual-1}f{idAtual}a-b{idAtual + 1}f{idAtual +2}a-b{idAtual -2}f" +
                    $"{idAtual+4}a-b{idAtual- 4}f{idAtual-3}a-b{idAtual + 3}f{idAtual}a";

              var userAdd = new UserModel
                {
                    id = id,
                    firstName = user.firstName,
                    surName = user.surName,
                    age = user.age,
                    creationDate = DateTime.UtcNow
                };

                _context.Users.Add(userAdd);
                _context.SaveChanges();

                return CreatedAtAction("GetUserId", new UserModel { id = userAdd.id }, userAdd);
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