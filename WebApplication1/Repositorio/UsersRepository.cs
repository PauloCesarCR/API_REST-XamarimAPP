using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;

namespace WebApplication1.Repositorio
{
    public interface IUsersRepository
    {
        public bool Create(PostUsersRequest user);
        public UserModel Get(int id);
        public bool Update(PutUserRequest user);
        public bool Delete(int id);

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

        public UserModel Get(int id)
        {
            try
            {
             var user = _bancocontext.Users.Find(id);
                    return user;    
          
            }
            catch
            {
                return new UserModel();
            }
        }

        public bool Update(PutUserRequest user)
        {  
            try
            {

                var userAtual = _bancocontext.Users.Find(user.id);
             
                    userAtual.firstName = user.firstName;
                    userAtual.surName = user.surName;
                    userAtual.age = user.age;
                    userAtual.creationDate =userAtual.creationDate;
                    
                    _bancocontext.SaveChanges();

                return true;
            } catch
            {
                return false; 

            }
        }

        public bool Delete(int id)
        {
            try
            {
                var userAtual = _bancocontext.Users.Find(id);
       
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


