﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;


namespace Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string fullName { get; set; } = null!;
        public string? address { get; set; }
        public string? image { get; set; }
        public string? skill { get; set; } 
        public DateTime dob { get; set; }
        public bool gender { get; set; }
        public bool banStatus { get; set; }

        [ForeignKey("groupId")]
        public Guid groupId { get; set; }
        public Group group { get; set; }

        [ForeignKey("roleID")]
        public Guid? roleID { get; set; }  
        public virtual Role? Role { get; set; }  

        //public virtual List<ManagerTask> ManagerTasks { get; set; } = new();
        //public virtual List<ManagerTask> CreateBy { get; set; } = new();
        public virtual List<Order> Orders { get; set; } = new();
    }
}
