using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class StudentDto
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Range(15, 60)]
        public int Age { get; set; }

        [Range(1, 100)]
        public int Grade { get; set; }

    }
}
