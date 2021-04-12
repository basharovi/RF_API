using BusinessDomain.Aggregates.Product;
using RapidFireLib.Lib.Api;
using RapidFireLib.Lib.Core;

namespace BusinessDomain.Handlers
{
    public class CustomHandler : IApiHandler
    {
        public object Handle(DbProcessMode modePrePost, DbProcessType processType, object model, Db db)
        {
            var modelName = model.GetType().Name;
            var recordId = model.GetType().GetProperty($"{modelName.Replace("Info", "")}Id");

            var sql = $"DELETE FROM {modelName} WHERE {recordId.Name} = {recordId.GetValue(model)}";
            db.ExecuteSQL(sql);

            return "Executed";
        }
    }
}
