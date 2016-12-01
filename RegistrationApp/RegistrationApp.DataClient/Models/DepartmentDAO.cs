using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationApp.DataClient.Models
{

    [DataContract]
    public class DepartmentDAO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

    }

}