﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Area  
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid id { get; set; }
        [Column(TypeName = "nvarchar(1000)")] 
        public string name { get; set; } = null!;
        public double price { get; set; }
        public bool isDeleted { get; set; }

        [ForeignKey("parentId")]
        public Guid? parentId { get; set; }
        public virtual Area? parent { get; set; } = null!;
        public virtual List<OrderDetail> OrderDetails { get; set; } = new();
        //public virtual List<Item> Items { get; set; } = new();
    }
}
