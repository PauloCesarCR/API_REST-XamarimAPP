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
            return Ok(users);

        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("id inválido");
            }
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

        if (string.IsNullOrEmpty(user.firstName))
         {
           throw new Exception("O Nome é obrigatório");
        }
         
        if (_repositorio.Create(user))
         {
           return Ok(user);
         }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PostUsersRequest user,[FromRoute]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("id inválido");
            }
            
            if (_repositorio.Update(user, id))
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("id inválido");
            }

            if (_repositorio.Delete(id))
            {
                return Ok("Usuario Deletado com Sucesso");
            }
            return BadRequest();
        }

    }
}
