using Application.AppUsers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Developers.DTOs
{
    public class DeveloperRegisterRequest
    {
        public UserRegisterRequest User { get; set; }
        public string Position { get; set; }
        public int NumberOfActiveTasks { get; set; }
    }
}
