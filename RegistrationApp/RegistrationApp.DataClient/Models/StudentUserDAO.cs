using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationApp.DataClient.Models
{

    [DataContract]
    public class StudentUserDAO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string UserPassword { get; set; }

        [DataMember]
        public int StudentId { get; set; }
    }

}