using Api.Config;
using BusinessDomain.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RapidFireLib.Lib.Authintication;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;
using RapidFireLib.View.Models.Identity;
using System.Linq;
using RapidFireLib.Lib.Extension;

namespace API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        RapidFire rf = new RapidFire(new AppConfig());

        [Route("/api/login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(Login loginUser)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            //apiResponse.ApiPacket.Packet = null;
            var loginResult = (AccessControl.User)rf.AccessControl.LoginApi(loginUser, true, LoginType.LoginDB);
            if (loginResult == null)
                return BadRequest("invalid credential");

            apiResponse.Tag = "User";
            apiResponse.ApiPacket.Packet = GetLoginResult(loginResult, loginUser);


            apiResponse.Success = true;
            return Ok(apiResponse);

        }
        [Route("api/login/SendPacket")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(ApiPacketRequest apr)
        {
            ApiResponse<object> apiResponse = new ApiResponse<object>();
            //apiResponse.ApiPacket.Packet = null;

            Login loginUser = JsonConvert.DeserializeObject<Login>(apr.ApiPacket.Packet.ToString());

            var loginResult = (AccessControl.User)rf.AccessControl.LoginApi(loginUser, true, LoginType.LoginDB);
            apiResponse.Message = loginResult.Message;
            if (apiResponse.Message != "Login Successful")
            {
                apiResponse.Success = false;
                return Ok(apiResponse);
            }

            apiResponse.Tag = "User";
            apiResponse.ApiPacket.Packet = GetLoginResult(loginResult, loginUser);
            apiResponse.Success = true;
            return Ok(apiResponse);

        }
        private AccessControl.User GetLoginResult(AccessControl.User loginResult, Login loginUser)
        {
            var deviceInfos = rf.Db.Get<DeviceInfo>(x => x.UserId == loginResult.UserId && x.DeviceUniqueId == loginUser.DeviceUniqueId).FirstOrDefault();

            if (deviceInfos == null)
            {
                DeviceInfo deviceInfo = new DeviceInfo()
                {
                    DeviceUniqueId = loginUser.DeviceUniqueId,
                    UserId = loginResult.UserId
                };
                var obj = rf.Db.Save(deviceInfo);
                rf.Db.Commit();
                deviceInfo.DeviceId = ((DeviceInfo)(obj.Result.Model)).DeviceInfoId.ToString().PadLeft(5, '0');

                rf.Db.Save(deviceInfo);
                rf.Db.Commit();
                loginResult.DeviceId = deviceInfo.DeviceId;
            }
            else
            {
                if (string.IsNullOrEmpty(deviceInfos.DeviceId))
                {

                    deviceInfos.DeviceId = (deviceInfos).DeviceInfoId.ToString().PadLeft(5, '0');
                    rf.Db.Save(deviceInfos);
                    rf.Db.Commit();
                }
                loginResult.DeviceId = deviceInfos.DeviceId;
            }
            var userInfo = rf.Db.Get<ApplicationUser>(x => x.UserId == loginResult.UserId).FirstOrDefault();
            loginResult.GeoType = userInfo.GeoType;
            loginResult.FullName = userInfo.FullName;
            loginResult.Designation = userInfo.Designation;// userInfo.Designation;
            loginResult.Organization = userInfo.Organization;// userInfo.Organization;
            //loginResult.StaffID = userInfo.StaffId; //userInfo.StaffID;
            loginResult.PhoneNumber = userInfo.PhoneNumber;
            loginResult.CreateDate = userInfo.CreateDate.ToString();
            loginResult.EditDate = userInfo.EditDate.ToString();
            loginResult.Password = "";
            return loginResult;
        }

    }

}