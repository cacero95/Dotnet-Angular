using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class  AppUser
    {
        [Key] // tell to entityframework that this is a primary key
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}