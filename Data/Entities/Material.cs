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
    public class Material
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid id { get; set; }

        [ForeignKey("createById")]
        public Guid? createById { get; set; }
        public virtual User CreateBy { get; set; } = null!;

        [ForeignKey("materialCategoryId")]
        public Guid materialCategoryId { get; set; }
        public MaterialCategory MaterialCategory { get; set; } = null!;

        public string name { get; set; } = null!;
        public string? image { get; set; } 
        public string color { get; set; } = null!;
        public string supplier { get; set; } = null!;
        public double thickness { get; set; } 
        public string unit { get; set; } = null!;
        public string sku { get; set; } = null!;
        public DateTime importDate { get; set; }
        public string importPlace { get; set; } = null!;
        public int amount { get; set; }
        public double price { get; set; }
        public double totalPrice { get; set; }
        public bool isDeleted { get; set; }

        public virtual List<ItemMaterial> ItemMaterials { get; set; } = new();
    }
}
