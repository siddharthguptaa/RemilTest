using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTestAssign.Models
{
    public class StudentModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        //[RegularExpression("/^[a-zA-Z][a-zA-Z\\s]+$/", ErrorMessage ="Please Enter Name in string.")]
        public string Name { get; set; }
        [Required]
        //[RegularExpression(@"/^[A-Za-z0-9'\.\-\s\,]+$/", ErrorMessage = "Please Enter Address in string.")]
        public string Address { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime DOJ { get; set; }
        [Required]
        //[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please Enter valid Mobile Number.")]
        public long Mobile { get; set; }
        [Required]
        //[RegularExpression("/^[A-Za-z]+$/", ErrorMessage ="Please select valid city")]
        public string City { get; set; }
        [JsonIgnore]
        public int CityId { get; set; }
    }
}