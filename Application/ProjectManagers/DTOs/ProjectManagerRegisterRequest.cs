using Application.AppUsers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectManagers.DTOs
{
    public class ProjectManagerRegisterRequest
    {
        public UserRegisterRequest User { get; set; }
        public string CertificateUrl { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
