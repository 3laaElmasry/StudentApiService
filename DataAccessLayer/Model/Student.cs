using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Range(15,60)]
        public int Age { get; set; }

        [Range(1, 100)]
        public int Grade { get; set; }


    }
}
