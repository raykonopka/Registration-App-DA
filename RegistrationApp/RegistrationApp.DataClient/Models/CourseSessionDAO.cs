using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationApp.DataClient.Models
{

    [DataContract]
    public class CourseSessionDAO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CourseId { get; set; }

        [DataMember]
        public string Professor { get; set; }

        [DataMember]
        public TimeSpan StartTime { get; set; }

        [DataMember]
        public TimeSpan EndTime { get; set; }

        [DataMember]
        public string DaysInSession { get; set; }

        [DataMember]
        public int Capacity { get; set; }
    }

}