using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationApp.DataClient.Models
{

    [DataContract]
    public class BookmarkedSessionDAO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StudentUserId { get; set; }

        [DataMember]
        public int SessionId { get; set; }

    }
}