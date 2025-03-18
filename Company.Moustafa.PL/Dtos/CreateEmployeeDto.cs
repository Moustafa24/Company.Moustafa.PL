using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Moustafa.PL.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage ="Name is Required !")]
        public string EmpName { get; set; }
        [Range(22,60 , ErrorMessage ="Age must be between 22 And 60 ")]
        [Required]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress , ErrorMessage ="Email is not valid !!")  ]
        public string Email { get; set; }
        [RegularExpression( @"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$" , ErrorMessage ="Address Must be Like 123-Streate-City-Country")] 
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date of Create")]
        public DateTime CreateAt { get; set; }

        public int ? DepartmentId { get; set; }
    }

}
