﻿using AutoMapper;
using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevices.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //User
            CreateMap<UserCreateModel, User>();
            CreateMap<User, UserModel>();

            //Item
            CreateMap<Item, ItemModel>();
            //CreateMap<ItemCategory, ItemCategoryModel>();

            //Material
            CreateMap<Material, MaterialModel>();
            CreateMap<MaterialCategory, MaterialCategoryModel>();

            // Order
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Order, CreateOrderModel>().ReverseMap();

            // Order Detail
            CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();
        }
    }
}
