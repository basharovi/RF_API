﻿using Api.Config;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;
using RapidFireLib.Lib.Extension;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using BusinessDomain.Handlers;
namespace API.Controllers
{

    [ApiController]
    public class SyncAPIController : ControllerBase
    {
        private string GetUUIDBasedSQL(ApiPacketRequest apr)
        {
            var dictionary = JsonConvert.DeserializeObject(apr.ApiPacket.Packet.ToString());
            var value = dictionary.GetPropertyValue("UUID");
            return value != null
                ? $"SELECT * from {apr.TableName} where UUID = '{value.ToString()}'"
                : "";
        }

        [HttpPost]
        [Route("api/Sync/SendPacket")]
        public ActionResult<ApiResponse> SyncData(ApiPacketRequest apr)
        {
            ApiResponse apiResponse = new ApiResponse();
            RapidFire rf = new RapidFire(new AppConfig());

            switch (apr.TableName)
            {
                case "ProductInfo":
                    apiResponse = rf.Api.ProcessSync(apr, null, null, new CustomHandler());

                    break;
                default:
                    var sqlString = GetUUIDBasedSQL(apr);
                    //apiResponse = rf.Api.ProcessSync(apr, null, string.IsNullOrEmpty(sqlString) ? null : sqlString, new SyncHandler());
                    apiResponse = rf.Api.ProcessSync(apr, null, string.IsNullOrEmpty(sqlString) ? null : sqlString, rf.Config.Item.DB.DynamicApiHandlers);
                    break;
            }

            apiResponse.ModelName = apr.TableName;
            return apiResponse;
        }


    }
}
