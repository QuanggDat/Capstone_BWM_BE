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
    public class Procedure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid id { get; set; }

        public string name { get; set; } = null!;
        public bool isDeleted { get; set; }

        public virtual List<ProcedureItem> ProcedureItems { get; set; } = new();
        public virtual List<ProcedureStep> ProcedureSteps { get; set; } = new();
    }
}
