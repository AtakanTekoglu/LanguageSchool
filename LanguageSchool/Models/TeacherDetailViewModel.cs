using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models
{
    public class TeacherDetailViewModel
    {
        public List<TeacherAppointment> TeacherAppointments { get; set; }
        public List<Language> Languages { get; set; }
    }
}