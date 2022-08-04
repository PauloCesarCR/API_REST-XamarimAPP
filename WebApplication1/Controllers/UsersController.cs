using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Repositorio;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class UsersController : Controller
    {
        private readonly IUsersRepository _repositorio;
        public UsersController(IUsersRepository repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _repositorio.GetAllUsers();

            if (users != null)
            {
            return Ok(users);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]string id)
        {
            var user = _repositorio.GetById(id);    

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostUsersRequest user)
        {
            if (_repositorio.Create(user))
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PostUsersRequest user,[FromRoute]string id)
        {
            
            if (_repositorio.Update(user, id))
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
          
                if (_repositorio.Delete(id))
                {
                return Ok("Usuario Deletado com Sucesso");
                }
                return BadRequest();

        }

    }
}
