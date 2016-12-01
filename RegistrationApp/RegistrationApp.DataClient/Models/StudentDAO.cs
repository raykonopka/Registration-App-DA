using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationApp.DataClient.Models
{
    [DataContract]
    public class StudentDAO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public int MajorId { get; set; }

    }

}