using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.User
{
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            this.GeneratedCode = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string GeneratedCode { get; set; }
        #region added by AI
        #endregion
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserToken> Tokens { get; set; }
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }



    }
}
