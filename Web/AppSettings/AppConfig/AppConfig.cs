using BusinessDomain.Contexts;
using BusinessDomain.Handlers;
using RapidFireLib.Lib.Core;
using System;
using System.Linq.Expressions;

namespace Web.Config
{
    public class AppConfig : IConfig
    {
        public void Configure(Configuration configuration)
        {
            //APP
            configuration.APP.BusinessModuleName = "BusinessDomain";
            configuration.APP.RootDomain = "app.starterkit.scibd.info";
            configuration.APP.SocialLoginEnable = false;
            configuration.APP.DefaultSSO = false;
            configuration.APP.EnableCSP = false;
            configuration.APP.AttachmentRoot = "files";
            configuration.APP.AttachmentStoreType = AttachmentStoreType.FileSystem;
            configuration.APP.AzureBlobAccessKey = "SharedAccessSignature=sv=2019-07-07&ss=btqf&srt=sco&st=2020-06-08T14%3A19%3A24Z&se=2021-06-09T14%3A19%3A00Z&sp=rwdlacu&sig=NZc8dl4qtDcZpSlaeDBsOb9whmqZxYCRQtMIInrgTEo%3D;BlobEndpoint=https://blobvmsrgprod.blob.core.windows.net/;";
            configuration.APP.AppTitle = "Starter KIT";
            configuration.APP.AppSlogan = "";
            configuration.APP.AppLogo = "EGE_ICO.webp";
            configuration.APP.LoginHomeImage = "bg-4.jpg";

            //-----SSO Credentials-----//

            /*----------Development-------------*/
            configuration.APP.SSOClientId = "16296a6f-f99b-495d-bfcb-f1daee488cf8";
            configuration.APP.SSOClientSecret = "W7z67Y1nwlz9_.Dg~_-3x_Bl.XPOpMjow1";
            configuration.APP.SSOAuthority = "https://login.microsoftonline.com/37ef3d19-1651-4452-b761-dc2414bf0416";


            /*----------Test-------------*/
            //configuration.APP.SSOClientId = "e86fceb8-31b0-4117-8bdf-402cdca5e7e4";
            //configuration.APP.SSOClientSecret = "MyUtH8JlMRdMZ6I.3n_XcuQ3I_~B~1EVkg";
            //configuration.APP.SSOAuthority = "https://login.microsoftonline.com/37ef3d19-1651-4452-b761-dc2414bf0416";


            //Authentication
            //configuration.Authentication.LoginFactory.Add(new StudentLoginFactory()); // Recom.
            configuration.Authentication.LoginType = RapidFireLib.Lib.Authintication.LoginType.LoginDB;
            configuration.Authentication.JwtKeys.SecretKey =
                @"MIGpAgEAAiEAuMmqfAzvVKJpIieaQkfC8BlZACwoOZssBCc/HIphNXcCAwEAAQIgGivly4ABfZkrDr1RKcYEI8Oyi9IoYes6eiO2fU1ALIECEQDe3gSNIlRk7Y8isu+Y
                qS1hAhEA1EJmx1b6rhjMxd4r2SG51wIQVylfgE7/0KU0CK8Qk5T+oQIQOI1cft3gukPnQwy3mAlRTwIQDeu1TMQl74QOdaI3YZ5voA==";
            configuration.Authentication.LoginBanner = @"../RapidFire/Scripts/insertData.sql";

            //DB
            configuration.DB.DefaultDbContext = new DefaultMSSQLContext(SAASType.NoSaas);
            configuration.DB.CheckTablePermission = false;
            configuration.DB.DynamicViewModelHandlers = new IDbHandler[] { new UpdateCommonFields() };
            configuration.DB.SAASScripts.NewDatabaseScriptPath = "../Web/Asset/Scripts/Tenant_Setup_Separate_DB.sql";
            configuration.DB.SAASScripts.NewSchemaScriptPath = "../Web/Asset/Scripts/Tenant_Setup_New_Schema_Same_DB.sql";
            configuration.DB.SAASScripts.DatabaseUpgradeScriptPath = "../Web/Asset/Scripts/Upgrades_ Scripts_ID3452_12-Jan-2019.sql";

            //Messaging 
            configuration.Messaging.FCM.RequestUri = "https://fcm.googleapis.com/fcm/send";
            configuration.Messaging.FCM.ServerKey =
                @"AAAAuYZIzbo:APA91bFDt1ekYu2n_HfpQNn1M69bdWJPSDL2o-84nLZELW3YKObVly-f9UzaFxYR_RCE2v7qRgtyrCTOM8G8V0IsRbonaxI-lzJ0tkdhHrJ36u-ETOtJdP6Tc1qSHOdUfpdiYczT1YnT";
            configuration.Messaging.FCM.SenderId = "796821867962";

            configuration.Messaging.SMS.RequestUri = "";
            configuration.Messaging.SMS.SecretKey = "";

            configuration.Messaging.Voice.RequestUri = "";
            configuration.Messaging.Voice.SecretKey = "";

            configuration.Messaging.Email.DefaultUser = "career.bangladesh@savethechildren.org";
            configuration.Messaging.Email.DefaultPassword = "Desk@hr2017";
            configuration.Messaging.Email.DefaultSignature = EmailSignatures.EmailSignature();

        }

        public void ConfigureSetting(AppSettings appSetting)
        {
            appSetting.AppSetting(new AppSetting());
        }

        public void ConfigureGlobalFilter<TEntity>(ref Expression<Func<TEntity, bool>> exp, RFCoreDbContext ctx) where TEntity : class
        {
            //switch (typeof(TEntity).Name)
            //{
            //    case "Region":
            //        exp = model =>
            //            ctx.Set<UserGlobalGeo>().Any(geo =>
            //                (geo.RegionId == EF.Property<int>(model, "RegionId") || geo.RegionId == 0) 
            //                && geo.UserId == ctx.UserId
            //             );
            //        break;
            //    case "Country":
            //    case "CountryOfOrigin":
            //        exp = model =>
            //            ctx.Set<UserGlobalGeo>().Any(geo =>
            //                (geo.RegionId == EF.Property<int>(model, "RegionId") || geo.RegionId == 0) &&
            //                (geo.CountryId == EF.Property<int>(model, "CountryId") || geo.CountryId == 0)                           
            //                && geo.UserId == ctx.UserId
            //             );
            //        break;
            //    case "Programs":
            //    case "Employee":
            //        exp = model =>
            //            ctx.Set<UserGlobalGeo>().Any(geo =>
            //                (geo.RegionId == EF.Property<int>(model, "RegionId") || geo.RegionId == 0) &&
            //                (geo.CountryId == EF.Property<int>(model, "CountryId") || geo.CountryId == 0) &&
            //                (geo.ProgramId == EF.Property<int>(model, "ProgramId") || geo.ProgramId == 0)                           
            //                && geo.UserId == ctx.UserId
            //             );
            //        break;
            //    case "FieldOffice":
            //        exp = model =>
            //            ctx.Set<UserGlobalGeo>().Any(geo =>
            //                (geo.RegionId == EF.Property<int>(model, "RegionId") || geo.RegionId == 0) &&
            //                (geo.CountryId == EF.Property<int>(model, "CountryId") || geo.CountryId == 0) &&
            //                (geo.FieldOfficeId == EF.Property<int>(model, "FieldOfficeId") || geo.FieldOfficeId == 0)
            //                && geo.UserId == ctx.UserId
            //             );
            //        break;
            //    case "VolunteerType":
            //    case "ReferenceCheckType":
            //    case "TrainingTopic":
            //    case "Review":
            //    case "NoHire":
            //    case "CSTrainingMember":
            //    case "ProgramRoles":
            //        break;
            //    case "EmployeeData":
            //        exp = model =>
            //            ctx.Set<UserGlobalGeo>().Any(geo =>
            //                (geo.CountryId == EF.Property<int>(model, "CountryId") || geo.CountryId == 0)
            //                && geo.UserId == ctx.UserId
            //             );
            //        break;
            //    case "ZoneBlock":
            //        exp = model =>
            //            ctx.Set<UserGlobalGeo>().Any(geo =>
            //                (geo.RegionId == EF.Property<int>(model, "RegionId") || geo.RegionId == 0) &&
            //                (geo.CountryId == EF.Property<int>(model, "CountryId") || geo.CountryId == 0) &&
            //                (geo.FieldOfficeId == EF.Property<int>(model, "FieldOfficeId") || geo.FieldOfficeId == 0)
            //                && geo.UserId == ctx.UserId
            //             );
            //        break;
            //    default:
            //        exp = model =>
            //            ctx.Set<UserGlobalGeo>().Any(geo =>
            //                (geo.RegionId == EF.Property<int>(model, "RegionId") || geo.RegionId == 0) &&
            //                (geo.CountryId == EF.Property<int>(model, "CountryId") || geo.CountryId == 0) &&
            //                (geo.ProgramId == EF.Property<int>(model, "ProgramId") || geo.ProgramId == 0) &&
            //                (geo.FieldOfficeId == EF.Property<int>(model, "FieldOfficeId") || geo.FieldOfficeId == 0)
            //                && geo.UserId == ctx.UserId
            //             );
            //        break;
            //}

        }

        public void RegisterCustomImplementation(RapidFireServices services)
        {
            //services.Email(new Test());
        }
    }

    static class EmailSignatures
    {
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
    }
}