using CleanArchitecture.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    public class Role : IdentityRole<int>, IEntity
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }


}
