using BusinessDomain.Aggregates.Product;
using RapidFireLib.Lib.Api;
using RapidFireLib.Lib.Core;

namespace BusinessDomain.Handlers
{
    public class CustomHandler : IApiHandler
    {
        public object Handle(DbProcessMode modePrePost, DbProcessType processType, object model, Db db)
        {
            var product = (ProductInfo)model;
            db.ExecuteSQL($"EXEC DeleteProduct '{product.ProductId}'");
            //throw new System.NotImplementedException();
            return null;
        }
    }
}
