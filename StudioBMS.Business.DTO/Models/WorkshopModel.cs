using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using StudioBMS.Business.DTO.Models.Interfaces;

namespace StudioBMS.Business.DTO.Models
{
    public class WorkshopModel : IModel
    {
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IList<TimeTableModel> TimeTables { get; set; }
        public Guid Id { get; set; }
        public string TitleWithCity => $"{Title} ({City})";

        public string HtmlTimetableString => TimeTables?.Aggregate("",(e, i) => e + $"{CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[(byte)i.WeekDay]} {i.Start:t}-{i.End:t}<br />");
    }
}