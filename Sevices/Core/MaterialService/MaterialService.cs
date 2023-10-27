﻿using AutoMapper;
using Data.DataAccess;
using Data.Entities;
using Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Sevices.Core.MaterialService
{
    public class MaterialService : IMaterialService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public MaterialService(AppDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        
        public ResultModel CreateMaterial(Guid createdById, CreateMaterialModel model)
        {
            var result = new ResultModel();
            result.Succeed = false;
            try
            {
                var checkMaterial = _dbContext.Material.SingleOrDefault(x => x.name == model.name && x.isDeleted == false);
                if (checkMaterial != null)
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Tên vật liệu này đã tồn tại !";
                }
                else 
                {
                    var checkCategory = _dbContext.MaterialCategory.Where(x => x.id == model.materialCategoryId).SingleOrDefault();
                    if (checkCategory == null)
                    {
                        result.Succeed = false;
                        result.ErrorMessage = "Không tìm thấy thông tin MaterialCategory !";
                    }
                    else
                    {
                        //Create Material 
                        var material = new Material
                        {
                            createById = createdById,
                            materialCategoryId = model.materialCategoryId,
                            name = model.name,
                            image = model.image,
                            color = model.color,
                            supplier = model.supplier,
                            thickness = model.thickness,
                            unit = model.unit,
                            sku = $"{model.name[0]}-{model.supplier}-{model.thickness}",
                            importDate = DateTime.Now,
                            importPlace = model.importPlace,
                            amount = model.amount,
                            price = model.price,
                            totalPrice = model.price * model.amount,
                            isDeleted = false
                        };

                        _dbContext.Material.Add(material);
                        _dbContext.SaveChanges();
                        result.Succeed = true;
                        result.Data = material.id;
                    }                 
                }                                  
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return result;
        }
        /*
         
        //Search base on name and color of the material
        public ResultModel Search(string search, int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(m => m.isDeleted != true && m.name.Contains(search) || m.color.Contains(search)).OrderByDescending(i => i.importDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                result.Data = new PagingModel()
                {
                    Data = _mapper.Map<List<MaterialModel>>(data),
                    Total = data.Count
                };
                result.Succeed = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel UpdateMaterial(UpdateMaterialModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                //Validation
                if (string.IsNullOrEmpty(model.name))
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Tên này không được để trống.";
                    return result;
                }
                if (string.IsNullOrEmpty(model.color))
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Màu này không được để trống.";
                    return result;
                }
                if (string.IsNullOrEmpty(model.supplier))
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Nguồn cung này không được để trống.";
                    return result;
                }
                if (model.amount < 0)
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Độ dày không được âm.";
                    return result;
                }
                if (string.IsNullOrEmpty(model.unit))
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Đơn vị này không được để trống.";
                    return result;
                }
                if (string.IsNullOrEmpty(model.importPlace))
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Nơi nhập này không được để trống.";
                    return result;
                }
                if (model.amount < 0)
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Số lượng không được âm.";
                    return result;
                }
                if (model.price < 0)
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Giá tiền không được âm.";
                    return result;
                }
                if (model.categoryId == Guid.Empty)
                {
                    result.Succeed = false;
                    result.ErrorMessage = "Không nhận được category";
                    return result;
                }

                //Update Material
                var data = _dbContext.Material.Where(m => m.id == model.id).FirstOrDefault();
                if (data != null)
                {
                    data.name = model.name;
                    data.image = model.image;
                    data.color = model.color;
                    data.supplier = model.supplier;
                    data.thickness = model.thickness;
                    data.unit = model.unit;
                    data.sku = model.sku;
                    data.importDate = model.importDate;
                    data.importPlace = model.importPlace;
                    data.amount = model.amount;
                    data.price = model.price;
                    data.totalPrice = model.totalPrice = model.price * model.amount;
                    data.categoryId = model.categoryId;
                    _dbContext.SaveChanges();
                    result.Succeed = true;
                    result.Data = _mapper.Map<Material, MaterialModel>(data);
                }
                else
                {
                    result.ErrorMessage = "Material" + ErrorMessage.ID_NOT_EXISTED;
                    result.Succeed = false;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel UpdateMaterialAmount(UpdateMaterialAmountModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(m => m.id == model.id).FirstOrDefault();
                if (data != null)
                { 
                    //Validation
                    if (model.amount < 0)
                    {
                        result.Succeed = false;
                        result.ErrorMessage = "Số lượng không được âm.";
                        return result;
                    }

                    //Update Amount
                    else {
                        data.amount = model.amount;
                        data.totalPrice = data.price * model.amount;
                        _dbContext.SaveChanges();
                        result.Succeed = true;
                        result.Data = _mapper.Map<Material, MaterialModel>(data);
                    }
                }
                else
                {
                    result.ErrorMessage = "Material" + ErrorMessage.ID_NOT_EXISTED;
                    result.Succeed = false;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel GetAllMaterial(int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(i => i.isDeleted != true).OrderByDescending(i => i.importDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                result.Data = new PagingModel()
                {
                    Data = _mapper.Map<List<MaterialModel>>(data),
                    Total = data.Count
                };
                result.Succeed = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel GetAllMaterialByCategoryId(Guid id, int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(i => i.isDeleted != true && i.categoryId==id).OrderByDescending(i => i.importDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                result.Data = new PagingModel()
                {
                    Data = _mapper.Map<List<MaterialModel>>(data),
                    Total = data.Count
                };
                result.Succeed = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel GetMaterialById(Guid id)
        {
            ResultModel resultModel = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(m => m.id == id && m.isDeleted != true);
                if (data != null)
                {

                    var view = _mapper.ProjectTo<MaterialModel>(data).FirstOrDefault();
                    resultModel.Data = view!;
                    resultModel.Succeed = true;
                }
                else
                {
                    resultModel.ErrorMessage = "Material" + ErrorMessage.ID_NOT_EXISTED;
                    resultModel.Succeed = false;
                }
            }
            catch (Exception ex)
            {
                resultModel.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return resultModel;
        }

        public ResultModel DeleteMaterial(Guid id)
        {
            ResultModel resultModel = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(m => m.id == id).FirstOrDefault();
                if (data != null)
                {
                    data.isDeleted = true;
                    _dbContext.SaveChanges();
                    var view = _mapper.Map<Material, MaterialModel>(data);
                    resultModel.Data = view;
                    resultModel.Succeed = true;
                }
                else
                {
                    resultModel.ErrorMessage = "Material" + ErrorMessage.ID_NOT_EXISTED;
                    resultModel.Succeed = false;
                }
            }
            catch (Exception ex)
            {
                resultModel.ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return resultModel;
        }

        public ResultModel SortMaterialbyPrice(int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(i => i.isDeleted != true).OrderByDescending(i=>i.price).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                result.Data = new PagingModel()
                {
                    Data = _mapper.Map<List<MaterialModel>>(data),
                    Total = data.Count
                };
                result.Succeed = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel SortMaterialbyThickness(int pageIndex, int pageSize)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Material.Where(i => i.isDeleted != true).OrderByDescending(i => i.thickness).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                result.Data = new PagingModel()
                {
                    Data = _mapper.Map<List<MaterialModel>>(data),
                    Total = data.Count
                };
                result.Succeed = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public Task<ResultModel> CreateMaterial(Guid id, CreateMaterialModel model)
        {
            throw new NotImplementedException();
        }

        public ResultModel SortMaterialByThickness(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResultModel SortMaterialByPrice(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResultModel UpdateMaterial(Guid id, UpdateMaterialModel model)
        {
            throw new NotImplementedException();
        }

        public ResultModel UpdateMaterialAmount(Guid id, UpdateMaterialAmountModel model)
        {
            throw new NotImplementedException();
        }
        */
    }
}
