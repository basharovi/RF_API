﻿using BusinessDomain.Aggregates;
using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models;

namespace BusinessDomain.Contexts
{
    public class DefaultMSSQLContext : RFCoreDbContext
    {
        public DefaultMSSQLContext() : base("DefaultConnection") { }
        public DefaultMSSQLContext(SAASType sAASType = SAASType.NoSaas) : base("DefaultConnection", sAASType) { }
        public DbSet<DeviceInfo> DeviceInfo { get; set; }
        public DbSet<DataVerificationLog> DataVerificationLog { get; set; }


        #region Starter kit example
        public DbSet<GradeInfo> GradeInfo { get; set; }
        public DbSet<StudentInfo> StudentInfo { get; set; }
        #endregion


    }
}
