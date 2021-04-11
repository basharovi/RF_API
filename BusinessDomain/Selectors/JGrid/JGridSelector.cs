using BusinessDomain.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Management.SqlParser.Metadata;
using RapidFireLib.Extensions;
using RapidFireLib.Lib.Core;
using RapidFireLib.Lib.Extension;
using RapidFireLib.UX.JGrid;
using RapidFireLib.View.UserInfo;

namespace BusinessDomain.Selectors.JGrid.Student
{
    public class JGridSelector : IJGridQuerySelector
    {
        public string Select(JGridRequest packet)
        {
            string sql = "";
            BaseFilter filter = packet.ExtractFilter<BaseFilter>();
            packet.ModelName = GetSwitch(packet.ModelName);
            if (packet.PageNo == 0) packet.PageNo = 1;
            if (packet.PageSize == 0) packet.PageSize = int.MaxValue;
            var userInfo = (IUserInfo)new HttpContextAccessor().HttpContext.RequestServices.GetService(typeof(IUserInfo));
            switch (packet.ModelName)
            {
                #region--Example
                case "StudentInfoView":
                    sql = $"EXEC JgStudentInfo '{userInfo.User.UserId}','{packet.PageSize}','{packet.PageNo}','{filter.FromDate}','{filter.ToDate}'";
                    break;
                    #endregion
            }
            return sql;
        }

        public string GetSwitch(string s)
        {
            string ret = s;
            if (s.Contains("MCheckList"))
            {
                if (s.Contains("QView")) ret = "QBQ";
                else if (s.Contains("StepsView")) ret = "QBSteps";
                else ret = "QB";
            }
            return ret;
        }
    }
}
