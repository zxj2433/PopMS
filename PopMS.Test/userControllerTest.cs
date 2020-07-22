using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.userVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class userControllerTest
    {
        private userController _controller;
        private string _seed;

        public userControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<userController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as userListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(userVM));

            userVM vm = rv.Model as userVM;
            user v = new user();
			
            v.DCID = AddDC();
            v.DeptID = AddDept();
            v.ITCode = "XML";
            v.Password = "rHrvv";
            v.Name = "LxL";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<user>().FirstOrDefault();
				
                Assert.AreEqual(data.ITCode, "XML");
                Assert.AreEqual(data.Password, "rHrvv");
                Assert.AreEqual(data.Name, "LxL");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            user v = new user();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DCID = AddDC();
                v.DeptID = AddDept();
                v.ITCode = "XML";
                v.Password = "rHrvv";
                v.Name = "LxL";
                context.Set<user>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(userVM));

            userVM vm = rv.Model as userVM;
            v = new user();
            v.ID = vm.Entity.ID;
       		
            v.ITCode = "pUAF";
            v.Password = "7jjyz";
            v.Name = "xCEMR";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DCID", "");
            vm.FC.Add("Entity.DeptID", "");
            vm.FC.Add("Entity.ITCode", "");
            vm.FC.Add("Entity.Password", "");
            vm.FC.Add("Entity.Name", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<user>().FirstOrDefault();
 				
                Assert.AreEqual(data.ITCode, "pUAF");
                Assert.AreEqual(data.Password, "7jjyz");
                Assert.AreEqual(data.Name, "xCEMR");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            user v = new user();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DCID = AddDC();
                v.DeptID = AddDept();
                v.ITCode = "XML";
                v.Password = "rHrvv";
                v.Name = "LxL";
                context.Set<user>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(userVM));

            userVM vm = rv.Model as userVM;
            v = new user();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<user>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            user v = new user();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.DCID = AddDC();
                v.DeptID = AddDept();
                v.ITCode = "XML";
                v.Password = "rHrvv";
                v.Name = "LxL";
                context.Set<user>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            user v1 = new user();
            user v2 = new user();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DCID = AddDC();
                v1.DeptID = AddDept();
                v1.ITCode = "XML";
                v1.Password = "rHrvv";
                v1.Name = "LxL";
                v2.DCID = v1.DCID; 
                v2.DeptID = v1.DeptID; 
                v2.ITCode = "pUAF";
                v2.Password = "7jjyz";
                v2.Name = "xCEMR";
                context.Set<user>().Add(v1);
                context.Set<user>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(userBatchVM));

            userBatchVM vm = rv.Model as userBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<user>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as userListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDC()
        {
            dc v = new dc();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                context.Set<dc>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddDept()
        {
            dept v = new dept();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Index = 47;
                context.Set<dept>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
