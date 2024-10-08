﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystem.Model.Setup.PageSetting
{
    public class PageSettingModel
    {
        public PageSettingModel(int pageNo, int pageSize, int pageCount, int totalCount)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            PageCount = pageCount;
            TotalCount = totalCount;
        }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public bool IsEndOfPage { get { return PageNo == PageCount; } }
    }
}
