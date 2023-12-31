﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ItemMaterial
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        [ForeignKey("itemId")]
        public Guid itemId { get; set; }
        public Item Item { get; set; } = null!;

        [ForeignKey("materialId")]
        public Guid materialId { get; set; }
        public Material Material { get; set; } = null!;

        public int quantity { get; set; }
        public double price { get; set; }
        public double totalPrice { get; set; }
    }
}
