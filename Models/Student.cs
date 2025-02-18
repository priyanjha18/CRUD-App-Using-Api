using System.ComponentModel.DataAnnotations;

namespace CrudAppUsingApi.Models
{
    
        public class Student
        {
           
            public int id { get; set; }
            [Required]
            public string studentName { get; set; }
        [Required]
        public string studentGender { get; set; }
        [Required,Range(3,21)]
        public int age { get; set; }
        [Required,Range(1,12)]
        public int standard { get; set; }
        }

    }
