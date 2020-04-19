using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageSchool.Models
{
    public class ResultViewModel
    {
        public List<SocialOpportunity> _SocialOpportunities { get; set; }
        public List<BusTransportation> _BusTransportations { get; set; }
        public List<Course> _Courses { get; set; }
    }
}