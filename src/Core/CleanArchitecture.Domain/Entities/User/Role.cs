using CleanArchitecture.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.User
{
    public class Role : IdentityRole<int>, IEntity
    {
        public Role()
        {
            CreatedDate = DateTime.Now;
        }

        public string DisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<RoleClaim> Claims { get; set; }
        public ICollection<UserRole> Users { get; set; }


    }


}
