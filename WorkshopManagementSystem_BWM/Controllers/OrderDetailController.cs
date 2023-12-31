﻿using Data.Models;
using Data.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sevices.Core.OrderDetailService;
using WorkshopManagementSystem_BWM.Extensions;

namespace WorkshopManagementSystem_BWM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpPost("CreateOrderDetail")]
        public IActionResult CreateOrderDetail(CreateOrderDetailModel model)
        {
            if (!ValidateCreateOrderDetail(model))
            {
                return BadRequest(ModelState);
            }
            var result =_orderDetailService.CreateOrderDetail(model, User.GetId());
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { Code = result.Code, ErrorMessage = result.ErrorMessage });
        }

        [HttpGet("GetByOrderIdWithPaging")]
        public IActionResult GetByOrderIdWithPaging(Guid orderId, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size, string? search = null)
        {
            var result = _orderDetailService.GetByOrderIdWithPaging(orderId, pageIndex, pageSize, search);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("UpdateOrderDetail")]
        public IActionResult Update([FromBody] UpdateOrderDetailModel model)
        {
            if (!ValidateUpdateOrderDetail(model))
            {
                return BadRequest(ModelState);
            }
            var result = _orderDetailService.Update(model, User.GetId());
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { Code = result.Code, ErrorMessage = result.ErrorMessage });
        }

        [HttpDelete("DeleteOrderDetail")]
        public IActionResult Delete(Guid id)
        {
            var result = _orderDetailService.Delete(id, User.GetId());
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(new ResponeResultModel { Code = result.Code, ErrorMessage = result.ErrorMessage });
        }

        [HttpGet("GetAllTaskByOrderDetailId")]
        public IActionResult GetAllByOrderDetailId(Guid orderDetailId)
        {
            var result = _orderDetailService.GetAllByOrderDetailId(orderDetailId);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("GetLogOnOrderDetailByOrderId")]
        public IActionResult GetLogOnOrderDetailByOrderId(Guid orderId, string? search, int pageIndex = ConstPaging.Index, int pageSize = ConstPaging.Size)
        {
            var result = _orderDetailService.GetLogOnOrderDetailByOrderId(orderId, search, pageIndex, pageSize);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        #region Validate
        private bool ValidateCreateOrderDetail(CreateOrderDetailModel model)
        {
            if (model.quantity<0)
            {
                ModelState.AddModelError(nameof(model.quantity),
                    $"{model.quantity} không được để trống !");
            }
            if (ModelState.ErrorCount > 0) return false;
            return true;
        }

        private bool ValidateUpdateOrderDetail(UpdateOrderDetailModel model)
        {
            if (model.quantity < 0)
            {
                ModelState.AddModelError(nameof(model.quantity),
                    $"{model.quantity} không được để trống !");
            }
            if (ModelState.ErrorCount > 0) return false;
            return true;
        }
        #endregion
    }
}
