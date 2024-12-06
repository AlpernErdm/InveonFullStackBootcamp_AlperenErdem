using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class RedisCacheRequestModel
    {
        [Required]
        public string ?Key { get; set; }

        [Required]
        public string ?Value { get; set; }
    }
}
