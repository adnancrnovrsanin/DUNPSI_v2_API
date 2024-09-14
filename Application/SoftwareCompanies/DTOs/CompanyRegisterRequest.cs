using Application.AppUsers.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Application.SoftwareCompanies.DTOs
{
    public class CompanyRegisterRequest
    {
        public UserRegisterRequest User { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Web { get; set; }
    }
}