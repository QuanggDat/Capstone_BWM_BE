﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class MaterialCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid id { get; set; }   
        public string name { get; set; } = null!;
        public bool isDeleted { get; set; }
        public virtual List<Material> Materials { get; set; } = new();
    }
}
