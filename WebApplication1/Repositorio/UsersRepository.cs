using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Repositorio
{
    public interface IUsersRepository
    {
        public bool Create(PostUsersRequest user);
        public UserModel GetById(string id);
        public IEnumerable<UserModel> GetAllUsers();
        public bool Update(PostUsersRequest user, string id);
        public bool Delete(string id);
    }


    public class UsersRepository : IUsersRepository
    {
        private readonly BancoContext _bancocontext;

        public UsersRepository(BancoContext bancocontext)
        {
            _bancocontext = bancocontext;
        }

        public bool Create(PostUsersRequest user)
        {
            try
            {
                var newUser = new UserModel()
                {
                    firstName = user.firstName,
                    surName = user.surName,
                    age = user.age,
                    creationDate = DateTime.UtcNow
                };
                _bancocontext.Users.Add(newUser);
                _bancocontext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserModel GetById(string id)
        {
                var user = _bancocontext.Users.Find(id);
                return user;
        }

        public IEnumerable<UserModel> GetAllUsers()
        { 
                var users = _bancocontext.Users;
                return users;    
        }

        public bool Update(PostUsersRequest user, string id)
        { 
            try
            {
       
                var userAtual = _bancocontext.Users.Find(id);
             
                    userAtual.firstName = user.firstName;
                    userAtual.surName = user.surName;
                    userAtual.age = user.age;
                    userAtual.creationDate = userAtual.creationDate;
                    
                    _bancocontext.SaveChanges();

                    return true;
            } catch
            {
                return false;

            }
        }

        public bool Delete(string id)
        {

            try
            {
                var userAtual = _bancocontext.Users.Find(id);

                if (userAtual == null)
                {
                    return false;
                }

                _bancocontext.Remove(userAtual);
                _bancocontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

    }
}


