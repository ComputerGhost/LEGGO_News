﻿using APIClient.DTOs;
using Calendar.Matrix;
using System.Collections.Generic;

namespace Public.ViewModels
{
    public struct ScheduleViewModel
    {
        public IEnumerable<CalendarSummary> Calendars { get; set; }
        public MonthMatrix Matrix {get;set;}
    }
}