using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationApp.DataClient.Models
{

    [DataContract]
    public class CourseDAO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int DepartmentId { get; set; }

        [DataMember]
        public int Credits { get; set; }
    }

}