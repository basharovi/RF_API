using BusinessDomain.Aggregates;
using Microsoft.AspNetCore.Http;
using RapidFireLib.Lib.Core;
using RapidFireLib.Lib.Extension;
using RapidFireLib.View.Models.Identity;
using System;

namespace BusinessDomain.Handlers
{
    public class UpdateCommonFields : IDbHandler
    {
        object IDbHandler.Handle(DbProcessMode modePrePost, DbProcessType processType, object model)
        {
            if (model.GetType().BaseType == typeof(BaseModel))
            {
                RapidFire rf = new HttpContextAccessor().HttpContext == null ? null : new RapidFire((IConfig)new HttpContextAccessor().HttpContext.RequestServices.GetService(typeof(IConfig)));
                if (processType.Equals(DbProcessType.INSERT))
                {
                    model.SetPropertyValue("DataCollectionDate", DateTime.UtcNow);
                    model.SetPropertyValue("DataCollectionTime", DateTime.UtcNow.ToString("hh:mm tt"));
                    model.SetPropertyValue("UserId",   rf.AccessControl.AppHostUser.User.UserId);
                    model.SetPropertyValue("Latitude", "0");
                    model.SetPropertyValue("Longitude", "0");
                    model.SetPropertyValue("DataStatus", "0");
                    model.SetPropertyValue("IsVerified", "0");
                }
                else if (processType.Equals(DbProcessType.UPDATE))
                {
                    model.SetPropertyValue("ModifiedBy", rf.AccessControl.AppHostUser.User.UserId);
                    model.SetPropertyValue("ModificationDate", DateTime.UtcNow);
                    if(rf==null) model.SetPropertyValue("DataStatus", "0");
                }
            }
            return model;
        }
    }
    public class UpdateFetchCommonFields : IDbHandler
    {
        object IDbHandler.Handle(DbProcessMode modePrePost, DbProcessType processType, object model)
        {
            if (model.GetType().BaseType == typeof(BaseModel))
            {
                if (processType.Equals(DbProcessType.INSERT))
                {
                    model.SetPropertyValue("DataStatus", "1");
                }
            }
            return model;
        }
    }
}
