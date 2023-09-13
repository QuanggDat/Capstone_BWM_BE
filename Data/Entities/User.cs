﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Column(TypeName = "varchar(1000)")]
        public string firstName { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string? lastName { get; set; }
        public string address { get; set; }
        public string? image { get; set; }
        public DateTime dob { get; set; }
        public bool gender { get; set; }
        public bool banStatus { get; set; }
        public Guid? roleID { get; set; }
        [ForeignKey("roleID")]
        public virtual Role? Role { get; set; }
    }
}
