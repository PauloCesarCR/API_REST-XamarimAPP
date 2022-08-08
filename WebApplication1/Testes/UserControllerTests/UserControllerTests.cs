using Moq;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Models.Entities.Users;
using WebApplication1.Repositorio;
using Xunit;

namespace WebApplication1.Testes.UserControllerTests
{
    public class UserControllerTests
    {
        private UsersController UsersController;

        public UserControllerTests()
        {
            UsersController = new UsersController(new Mock<IUsersRepository>().Object);
        }

        [Fact]
        public void GetAllUsersTest()
        {
            var exception = Assert.Throws<Exception>(() => UsersController.GetAllUsers());

            Assert.Equal("Nenhum usuario encontrado", exception.Message);

        }
        [Fact]
        public void GetByIdTests()
        {
                var exception = Assert.Throws<Exception>(() => UsersController.GetById(""));

                Assert.Equal("id inválido", exception.Message);
         }

        [Fact]
        public void PostTests()
        {
            var exception = Assert.Throws<Exception>(() => UsersController.Post(new PostUsersRequest()));

            Assert.Equal("O nome é obrigatório", exception.Message);
        }

        [Fact]
        public void PutTests()
        {
            var exception = Assert.Throws<Exception>(() => UsersController.Put(new PostUsersRequest(), ""));

            Assert.Equal("id inválido", exception.Message);
        }
        [Fact]
        public void DeleteTests()
        {
            var exception = Assert.Throws<Exception>(() => UsersController.Delete(""));

            Assert.Equal("id inválido", exception.Message);
        }
    }
}

