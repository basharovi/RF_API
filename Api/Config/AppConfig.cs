using BusinessDomain.Contexts;
using RapidFireLib.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Api.Config
{
    public class AppConfig : IConfig
    {
        public void Configure(Configuration configuration)
        {
            //APP
            configuration.APP.AppLogo = "";
            configuration.APP.AppTitle = "";
            configuration.APP.AppSlogan = "";
            configuration.APP.LoginHomeImage = "";
            configuration.APP.ReturnServerRecordId = true;
            configuration.APP.SocialLoginEnable = false;
            configuration.Authentication.LoginType = RapidFireLib.Lib.Authintication.LoginType.LoginDB;
            configuration.Authentication.JwtKeys.SecretKey =
                @"MIGpAgEAAiEAuMmqfAzvVKJpIieaQkfC8BlZACwoOZssBCc/HIphNXcCAwEAAQIgGivly4ABfZkrDr1RKcYEI8Oyi9IoYes6eiO2fU1ALIECEQDe3gSNIlRk7Y8isu+Y
                qS1hAhEA1EJmx1b6rhjMxd4r2SG51wIQVylfgE7/0KU0CK8Qk5T+oQIQOI1cft3gukPnQwy3mAlRTwIQDeu1TMQl74QOdaI3YZ5voA==";
            configuration.Authentication.LoginBanner = @"../RapidFire/Scripts/insertData.sql";
            configuration.APP.BusinessModuleName = "BusinessDomain";
            //DB
            configuration.DB.DefaultDbContext = new DefaultMSSQLContext();
            configuration.DB.CheckTablePermission = false;
            configuration.DB.SAASScripts.NewDatabaseScriptPath = "";
            configuration.DB.SAASScripts.NewSchemaScriptPath = "";
            configuration.DB.SAASScripts.DatabaseUpgradeScriptPath = "";
            //Messaging 
            configuration.Messaging.FCM.RequestUri = "https://fcm.googleapis.com/fcm/send";
            configuration.Messaging.FCM.ServerKey =
                @"AAAAuYZIzbo:APA91bFDt1ekYu2n_HfpQNn1M69bdWJPSDL2o-84nLZELW3YKObVly-f9UzaFxYR_RCE2v7qRgtyrCTOM8G8V0IsRbonaxI-lzJ0tkdhHrJ36u-ETOtJdP6Tc1qSHOdUfpdiYczT1YnT";
            configuration.Messaging.FCM.SenderId = "796821867962";

            configuration.Messaging.SMS.RequestUri = "";
            configuration.Messaging.SMS.SecretKey = "";

            configuration.Messaging.Voice.RequestUri = "";
            configuration.Messaging.Voice.SecretKey = "";

            configuration.APP.SSOAuthority = "https://login.microsoftonline.com/f156dbd0-0022-4d35-a88d-e5d06c88a312";
            configuration.APP.SSOClientId = "9ab5616e-3c23-4adf-9b53-f5ea8166ddd6";
            configuration.APP.SSOClientSecret = "6z6nmc55.tYKK:MtcjTJo:t=Lt0_dlI=";

            configuration.Messaging.Email.DefaultUser = "";
            configuration.Messaging.Email.DefaultPassword = "";
            configuration.Messaging.Email.DefaultSignature = EmailSignature();
        }

        public static string EmailSignature()
        {
            string signature = "";
            signature += "<br /><span style=\"font-family:Arial;font-size:12px;\"><b>" + "ONDESK SERVICE" +
                "</b>|<span style=\"color: red;\"> Save the Children in Bangladesh</span> | " +
                "<br />House CWN (A) 35, Road 43, Gulshan 2, Dhaka 1212, Bangladesh <br />";
            signature += " https://bangladesh.savethechildren.net/<br />";
            signature += @"Tel: +88-02-882 8081, Ext. 1065 | Fax: +88-02-881 2523 <br /></span>";
            return signature;
        }
        public void ConfigureSetting(AppSettings appSetting)
        {
            appSetting.AppSetting(new AppSetting());
        }

        public void ConfigureGlobalFilter<TEntity>(ref Expression<Func<TEntity, bool>> exp, RFCoreDbContext ctx) where TEntity : class
        {
            //throw new NotImplementedException();
        }

        public void RegisterCustomImplementation(RapidFireServices services)
        {
            
        }
    }
    class AppSetting : IAppSetting
    {
        public void ConfigureSetting(List<Setting> setting)
        {

        }
    }
}