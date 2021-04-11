using Api.Config;
using BusinessDomain.Handlers;
using Microsoft.AspNetCore.Mvc;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;

namespace API.Controllers
{
    [ApiController]
    public class FetchAPIController : ControllerBase
    {
        [HttpPost]
        [Route("api/Fetch/GetPacket")]
        public ApiResponse<object> GetPacketByUserId(ApiPacketRequest apr)
        {
            RapidFire rf = new RapidFire(new AppConfig());
            ApiResponse<object> apiResponse = new ApiResponse<object>();

            switch (apr.TableName)
            {
                //case "UserGeo":
                //    apiResponse = rf.Api.ProcessFetch(apr, null, new FetchGeolocationHandler());
                //    break;
                default:
                    apiResponse = rf.Api.ProcessFetch(apr, null, null);
                    break;
            }
            apiResponse.Command = apr.Command;

            return apiResponse;
        }
    }
}
