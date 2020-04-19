using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models
{
    public class InstallmentStudentClass
    {
        public List<Class_Students> _ClassStudents { get; set; }
        public int installmentCount { get; set; }
    }
}