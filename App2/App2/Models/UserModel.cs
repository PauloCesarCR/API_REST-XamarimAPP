using System;


namespace WebApplication1.Models

{
    public class UserModel
    {
        public string id { get; set; } = Guid.NewGuid().ToString();

        public string firstName { get; set; }

        public string surName { get; set; } 

        public int age { get; set; }

        public DateTime creationDate { get; set; }
    }
}
