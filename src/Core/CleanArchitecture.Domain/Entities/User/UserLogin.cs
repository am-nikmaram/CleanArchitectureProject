﻿using CleanArchitecture.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities.User;

public class UserLogin:IdentityUserLogin<int>,IEntity
{
    public UserLogin()
    {
        LoggedOn=DateTime.Now;
    }

    public User User { get; set; }
    public DateTime LoggedOn { get; set; }
}