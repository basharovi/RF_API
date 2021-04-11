using BusinessDomain.Aggregates;
using Microsoft.AspNetCore.Mvc;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models.Api;
using System;
using System.Linq;

namespace Web.Controllers
{
    public class DropdownController : Controller
    {
        Db db;
        public DropdownController(IConfig config)
        {
            db = new Db(config);
        }
        [HttpPost]
        public JsonResult Grades()
        {
            var res = db.Get<GradeInfo>().Select(x => new { Id = x.GradeId, Name = x.GradeName });
            return Json(res);
        }
        [HttpPost]
        public JsonResult Courses(long GradeId)
        {
            var res = db.Get<CourseInfo>(x => x.GradeId == GradeId).Select(x => new { Id = x.CourseId, Name = x.CourseName }).ToList();
            return Json(res);
        }
        [HttpPost]
        public JsonResult Units(long CourseId)
        {
            var res = db.Get<UnitInfo>(x => x.CourseId == CourseId).Select(x => new { Id = x.UnitId, Name = x.UnitName }).ToList();
            return Json(res);
        }
        [HttpPost]
        public JsonResult QuestionSetType()
        {
            var res = db.Get<QuestionSetType>().Select(x => new { Id = x.QuestionSetTypeId, Name = x.QuestionSetTypeName }).ToList();
            return Json(res);
        }
        [HttpGet]
        public JsonResult GetCompanyByDistrictCode(string DistrictCode)
        {
            string sql = $@"select C.CompanyCode As ValueText, C.CompanyName AS DisplayText from Company C
                        INNER JOIN CompanyMapping CM ON CM.CompanyCode=C.CompanyCode
                        where CM.DistrictCode='{DistrictCode}' ORDER BY CompanyName ASC";
            var res = db.Get<SpinnerValue>(sql).ToList();
            return Json(res);
        }
        [HttpGet]
        public JsonResult GetParentsAndGuardianInfo(string BenTypeId, string CompanyCode, string TeaEstateCode, string EstateDivisionCode)
        {

            string sql = $@"EXEC GetParentsDropdownList '{BenTypeId}', '{CompanyCode}','{TeaEstateCode}','{EstateDivisionCode}'";
            var res = db.Get<SpinnerValue>(sql).ToList();
            return Json(res);
        }
        [HttpGet]
        public JsonResult GetCommitteeMemberInfo(string committeeId)
        {

            try
            {
                string sql = $@"EXEC GetCommitteeMemberInfo '{committeeId}'";
                var res = db.GetDataTable(sql);
                return Json(new { success = true, Data = res });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message });
            }
        }
        [HttpGet]
        public JsonResult GetGroupMemberInfo(string groupId)
        {

            try
            {
                string sql = $@"EXEC GetGroupMemberInfo '{groupId}'";
                var res = db.GetDataTable(sql);
                return Json(new { success = true, Data = res });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message });
            }
        }
    }
}
