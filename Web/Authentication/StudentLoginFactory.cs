using BusinessDomain.Aggregates;
using Microsoft.AspNetCore.Http;
using RapidFireLib.Lib.Core;
using RapidFireLib.Lib.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Authentication
{
    public class StudentLoginFactory : ILoginFactory
    {
        public ExternalLogin Attach(string userId, string username, string password, string type)
        {
            //Student.studentName = Username;
            //Student.Password = password;
            var iConfig = (IConfig)new HttpContextAccessor().HttpContext.RequestServices.GetService(typeof(IConfig));
            var rf = new RapidFire(iConfig);
            var student = new Student();
            if (!string.IsNullOrEmpty(userId))
                student = rf.Db.Get<Student>(x => x.StudentId == userId)?.FirstOrDefault();
            else
                student = rf.Db.Get<Student>(x => x.Mobile == username && x.Password == password)?.FirstOrDefault();
            return new ExternalLogin()
            {
                UserId = student.StudentId,
                UserName = student.Mobile,
                FullName = student.StudentName
            };
        }
    }
}
