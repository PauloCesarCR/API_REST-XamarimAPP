using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models

{
    public class UserModel
    {
        public int Id { get; set; }


        public string firstName { get; set; }


        public string surName { get; set; }

        [Required]
        public int age { get; set; }

        [Required]
        public DateTime creationDate { get; set; }
    }
}
