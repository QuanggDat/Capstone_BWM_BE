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
            CreateMap<UserCreateModel, User>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserCommentModel>().ReverseMap();

            // Order
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Order, CreateOrderModel>().ReverseMap();
            CreateMap<Order, QuoteMaterialOrderModel>().ReverseMap();

            // Order Detail
            CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();
            CreateMap<OrderDetailMaterial, OrderDetailMaterialModel>().ReverseMap();
            CreateMap< Item, ItemModel> ().ReverseMap();
            //Group
            CreateMap<Group, GroupModel>().ReverseMap();

            // Notification
            CreateMap<Notification, NotificationModel>().ReverseMap();
            CreateMap<Notification, NotificationCreateModel>().ReverseMap();

            // Supply
            CreateMap<Supply, SupplyModel>().ReverseMap();

            // Resource
            CreateMap<Resource, ResourceModel>().ReverseMap();

            // Comment
            CreateMap<Comment, CommentModel>().ReverseMap();

            // WorkerTask
            CreateMap<WorkerTask, WorkerTaskViewModel>().ReverseMap();
        }
    }
}
