using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities.Users
{
    public class PostUsersRequest
    {
        public string firstName { get; set; }

        public string? surName { get; set; }

        public int age { get; set; }
    }
}
