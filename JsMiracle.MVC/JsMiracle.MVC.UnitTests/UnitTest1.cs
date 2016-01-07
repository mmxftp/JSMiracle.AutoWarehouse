using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsMiracle.Entities.TabelEntities;
using System.Linq;
using System.Linq.Dynamic;
using JsMiracle.Entities.Enum;

namespace JsMiracle.MVC.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {

                var db = new ORGModels();

                //var filter = string.Format(" (YHID = \"{0}\" || YHM.Contains(\"{0}\")) && ZT = {1} "
                //    , "aa", (int)UserStateEnum.Normal);

                var filter = string.Format("  CJRQ > '{0}' && ZT = 1 "
                    , "2015-11-19", (int)UserStateEnum.Normal);

                var data = db.IMS_UP_YH_S.Where(filter);

                Assert.AreEqual<int>(3, data.Count(), "fail");
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
    }
}
