using Microsoft.AspNetCore.Mvc;
using Serilog;
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
            Log.Information("Acess a api/Users -> Get");

            try
            {
                var users = _repositorio.GetAllUsers();
                return Ok(users);
            }
            catch 
            {
                Log.Error("Error, not users found");
                return BadRequest("Error, not users found");
            }


        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]string id)
        {
          if (string.IsNullOrEmpty(id))
          {
          throw new Exception("id inválido");
          }

            Log.Information($"Acess a api/Users/id -> Get: {id}");

            var user = _repositorio.GetById(id);    

            if (user != null)
            {
                return Ok(user);
            }
            Log.Error("Not user found");
            return NotFound("Not user found");
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostUsersRequest user)
        {
         Log.Information($"Acess a api/Users -> Post: {user}");

            if (string.IsNullOrEmpty(user.firstName))
            {
                throw new Exception("O nome é obrigatório");
            }

            if (_repositorio.Create(user))
         {
           return Ok(user);
         }
            Log.Error("Erro ao cadastrar usuario");
            return BadRequest("Erro ao cadastrar usuario");
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PostUsersRequest user,[FromRoute]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("id inválido");
            }

            Log.Information($"Acess a api/Users/id -> Put: {user}");

            if (_repositorio.Update(user, id))
            {
                return Ok(user);
            }

            Log.Error("Erro ao atualizar o usuario");
            return BadRequest("Erro ao atualizar o Usuario");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("id inválido");
            }

            Log.Information($"Acess a api/Users/id -> Delete user do id: {id}");

            if (_repositorio.Delete(id))
            {
                return Ok("Usuario Deletado com Sucesso");
            }
            Log.Error("Erro ao deletar o Usuario");
            return BadRequest("Erro ao deletar o Usuario");
        }

    }
}
