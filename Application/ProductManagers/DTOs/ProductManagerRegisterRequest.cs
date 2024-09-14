using Application.AppUsers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductManagers.DTOs
{
    public class ProductManagerRegisterRequest
    {
        public UserRegisterRequest User { get; set; }
    }
}
