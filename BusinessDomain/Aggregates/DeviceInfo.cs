using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessDomain.Aggregates
{
    public class DeviceInfo : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DeviceInfoId { get; set; }
        public int? UserId { get; set; }
        public string DeviceUniqueId { get; set; }
        public string DeviceId { get; set; }
    }
}
