using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.DTOs
{
    public class RoleForAddDto : IDto
    {
        public string Name { get; set; }
    }
}
