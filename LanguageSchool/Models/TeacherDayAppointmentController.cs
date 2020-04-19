using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models
{
    public class TeacherDayAppointmentController
    {
        public List<Teacher> _Teachers { get; set; }
        public List<WeekDay> _WeekDays { get; set; }
    }
}