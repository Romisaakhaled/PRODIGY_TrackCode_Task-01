using System;
namespace CRUD_Operations_Api.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
