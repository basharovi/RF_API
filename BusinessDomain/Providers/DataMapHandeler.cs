using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Business;
using RapidFireLib.Models.Identity;
using static RapidFireLib.Business.Approvals.DataApproval;

namespace BusinessDomain.Providers
{
    public class DesignationHandeler : IExternalDataMap<Designation>
    {
        public Designation OnExternalDataMap(Designation model, Db db)
        {
            return db.Get<Designation>("").FirstOrDefault();
        }
    }

    public class xHandeler : IExternalDataMap<Designation>
    {
        public Designation OnExternalDataMap(Designation model, Db db)
        {
            throw new NotImplementedException();
        }
    }
}
