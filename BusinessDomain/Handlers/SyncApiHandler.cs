using RapidFireLib.Lib.Api;
using RapidFireLib.Lib.Core;
using RapidFireLib.Lib.Extension;
using System.Collections;
using System.Collections.Generic;

namespace BusinessDomain.Handlers
{
    public class SyncHandler : IApiHandler
    {
        public object Handle(DbProcessMode modePrePost, DbProcessType ProcessType, object model, Db db)
        {
            if (modePrePost == DbProcessMode.Pre)
            {
                switch (ProcessType)
                {
                    case DbProcessType.INSERT:
                        StatusFieldUpdate(model);
                        break;
                    case DbProcessType.UPDATE:
                        StatusFieldUpdate(model);
                        break;
                    case DbProcessType.DELETE:
                        break;
                    case DbProcessType.SELECT:
                        break;
                    default:
                        break;
                }
            }

            return model;
        }
        private void StatusFieldUpdate(object model)
        {
            var modelType = model.GetType();
            if (model.HasProperty("DataStatus"))
                model.SetPropertyValue("DataStatus", 1);
            var genericTypeProperty = modelType.GetProperties();
            foreach (var item in genericTypeProperty)
            {
                if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition().Equals(typeof(List<>)))
                {
                    var genericPropertyValues = model.GetPropertyValue(item.Name);
                    foreach (var modelChild in genericPropertyValues as IList)
                    {
                        if (modelChild.HasProperty("DataStatus"))
                            modelChild.SetPropertyValue("DataStatus", 1);
                    }
                }

            }

        }
    }
}
