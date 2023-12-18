﻿using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }   
        public string? Surname { get; set; } 
        public int? Age { get; set; }
    }
}
