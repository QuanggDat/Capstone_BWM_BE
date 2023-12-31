﻿using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices.Core.CategoryService
{
    public interface IMaterialCategoryService
    {       
        ResultModel Create(CreateMaterialCategoryModel model);
        ResultModel GetAll(string? search, int pageIndex, int pageSize);
        ResultModel GetById(Guid id);
        ResultModel Update(UpdateMaterialCategoryModel model);
        ResultModel Delete(Guid id);
    }
}
