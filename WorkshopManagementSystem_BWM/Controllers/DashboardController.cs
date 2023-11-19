﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sevices.Core.DashboardService;
using Sevices.Core.UserService;

namespace WorkshopManagementSystem_BWM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("[action]")/*, Authorize(Roles = "Admin,Foreman")*/]
        public IActionResult UserDashboard()
        {
            var result = _dashboardService.UserDashboard();
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("[action]")/*, Authorize(Roles = "Admin,Foreman")*/]
        public IActionResult OrderDashboard()
        {
            var result = _dashboardService.OrderDashboard();
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("[action]")/*, Authorize(Roles = "Admin,Foreman")*/]
        public IActionResult OrderByMonthDashboard(int year)
        {
            var result = _dashboardService.OrderByMonthDashboard(year);
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("[action]")/*, Authorize(Roles = "Admin,Foreman")*/]
        public IActionResult LeaderTaskDashboard()
        {
            var result = _dashboardService.LeaderTaskDashboard();
            if (result.Succeed) return Ok(result.Data);
            return BadRequest(result.ErrorMessage);
        }
    }
}
