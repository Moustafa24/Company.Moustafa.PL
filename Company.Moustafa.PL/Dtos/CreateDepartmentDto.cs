﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Moustafa.PL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Code is Required!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date is Required!")]
        public DateTime CreateAt { get; set; }
    }
}

