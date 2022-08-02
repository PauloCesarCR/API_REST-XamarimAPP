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

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] UserID user)
        {
            var userFound = _repositorio.Get(user.id);

            return Ok(userFound);
        }

        [HttpPost]
        public IActionResult Post([FromBody]PostUsersRequest user)
        {
            if (_repositorio.Create(user))
            {
                return Ok();
            }
            return BadRequest();
        }

 
        public IActionResult Put([FromBody] PutUserRequest user)
        {
            if (_repositorio.Update(user))
            {
                return Ok();
                
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _repositorio.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();

            }

        }

    }
}
