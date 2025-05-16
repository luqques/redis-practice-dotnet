using System.ComponentModel.DataAnnotations;

namespace Redis.Practice.Api.Models
{
    public class UserDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
