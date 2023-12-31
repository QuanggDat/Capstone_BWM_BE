﻿using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SupplyModel
    {
        public Guid id { get; set; }
        public Guid reportId { get; set; }
        public Guid materialId { get; set; }

        public string materialName { get; set; } = null!;
        public string materialSupplier { get; set; } = null!;
        public double materialThickness { get; set; }
        public string materialSku { get; set; } = null!;
        public string materialColor { get; set; } = null!;
        public string materialUnit { get; set; } = null!;

        public int amount { get; set; }
        public double price { get; set; }
        public double totalPrice { get; set; }

        public ESupplyStatus status { get; set; }
    }
}
