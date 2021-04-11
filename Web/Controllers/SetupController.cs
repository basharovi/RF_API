using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessDomain.Aggregates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidFireLib.Lib.Core;
using RapidFireLib.View.UserInfo;
using RapidFireLib.Lib.Extension;
using RapidFireLib.Services;
using RapidFireLib;

namespace Web.Controllers
{
    public class SetupController : Controller
    {
        private readonly RapidFire rf;
        Db db;
        private BlobStorageService blobStorageService = new BlobStorageService();
        public SetupController(IConfig config)
        {
            rf = new RapidFire(config);
            db = new Db(config);
        }
        public string getModelStateErrorMessage()
        {
            string msg = "";
            foreach (string key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    msg += error.ErrorMessage + "\r\n";
                }
            }
            return msg;
        }

        #region FileSave
        private object SaveAttachment(object modelData, string modelName, IFormFile file)
        {
            if (file != null)
            {
                var fs = new FileStorage();
                string fileExt = Path.GetExtension(file.FileName);
                var filePath = rf.Config.Item.APP.AttachmentRoot;
                var newFileName = file.Name + "Path" + "_" + Guid.NewGuid() + fileExt;
                var strPath = fs.Combine(fs.Root, "", filePath + "\\" + modelName, newFileName);
                var dirPath = fs.Combine(fs.Root, "", filePath + "\\" + modelName);
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                modelData.SetPropertyValue(file.Name + "Path", filePath + "/" + modelName + "/" + newFileName);
                if ((System.IO.File.Exists(strPath))) System.IO.File.Delete(strPath);
                using (var fileStream = new FileStream(strPath, FileMode.Create)) file.CopyTo(fileStream);
            }
            return modelData;
        }

        private object SaveToBlobStorege(object modelData, string modelName, IFormFile file)
        {
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                var newFileName = modelName + "_" + file.Name + "Path" + "_" + Guid.NewGuid() + extension;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var filePath = blobStorageService.UploadFileToBlob(newFileName, fileBytes, MimeMapping.GetMimeType(extension));
                    modelData.SetPropertyValue(file.Name + "Path", "View/DownloadFile/" + newFileName);
                }
            }
            return modelData;
        }
        private object SetBlob(object modelData, IFormFile file)
        {
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    modelData.SetPropertyValue(file.Name + "Path", fileBytes);
                }
            }
            return modelData;
        }
        private dynamic uploadFiles(dynamic modelData, IFormFileCollection files)
        {
            if (files.Count() > 0)
            {
                var storeType = rf.Config.Item.APP.AttachmentStoreType;
                foreach (var item in files)
                {
                    switch (storeType)
                    {
                        case AttachmentStoreType.Db:
                            modelData = SetBlob(modelData, item);
                            break;
                        case AttachmentStoreType.FileSystem:
                            modelData = SaveAttachment(modelData, modelData.GetType().Name, item);
                            break;
                        case AttachmentStoreType.BlobStorage:
                            modelData = SaveToBlobStorege(modelData, modelData.GetType().Name, item);
                            break;
                        case AttachmentStoreType.BothDbAndFileSystem:
                            modelData = SetBlob(modelData, item);
                            modelData = SaveAttachment(modelData, modelData.GetType().Name, item);
                            break;
                    }
                }
            }
            return modelData;
        }
        #endregion

      
        #region Grade Info
        [HttpPost]
        public IActionResult SaveGradeInfo(GradeInfo obj)
        {
            try
            {
                if (!ModelState.IsValid) return Json(new { success = false, Data = getModelStateErrorMessage() });
                obj.GradeName = obj.GradeName.Trim();
                var exist = rf.Db.Get<GradeInfo>().Any(x => x.GradeId != obj.GradeId && x.GradeName.ToLower() == obj.GradeName.ToLower());
                if (exist) return Json(new { success = false, Data = "Grade/Category already exist." });
                db.Save(obj);
                var Result = db.Commit();
                return Json(new { success = Result.Success, Data = Result.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message });
            }
        }
        public IActionResult GetGradeInfo()
        {
            string sql = $"select GradeId,GradeName,GradeNameBn from GradeInfo";
            var data = db.GetDataTable(sql);
            return Json(new { success = true, Data = data });
        }

        #endregion

      
    }

}