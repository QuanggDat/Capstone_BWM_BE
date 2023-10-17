﻿using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Models.ReportModel;

namespace Sevices.Core.ReportService
{
    public interface IReportService
    {
        Task<ResultModel> CreateReport(Guid reporterId, CreateReportModel model);
        Task<ResponseReportModel?> GetReportByReportId(Guid reportId);
        Task<ResultModel> ReportResponse(ReviewsReportModel model);
        Task<List<ResponseReportModel>> GetProgressReportsByManagerId(Guid managerId);
        Task<List<ResponseReportModel>> GetProblemReportsByManagerId(Guid managerId);
    }
}
