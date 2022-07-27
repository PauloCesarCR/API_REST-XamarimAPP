using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Repositorio
{
    public class UsersRepositorio : IUsersRepositorio
    {

        private readonly BancoContext _bancocontext;

        public UsersRepositorio(BancoContext bancocontext)
        {
            _bancocontext = bancocontext;
        }

        public UserModel Adicionar(UserModel user)
        {
            _bancocontext.Users.Add(user);
            _bancocontext.SaveChanges();
            return user;
        }
    }
}
