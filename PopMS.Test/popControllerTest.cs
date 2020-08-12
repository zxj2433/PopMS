using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.BASE.popVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class popControllerTest
    {
        private popController _controller;
        private string _seed;

        public popControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<popController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as popListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(popVM));

            popVM vm = rv.Model as popVM;
            pop v = new pop();
			
            v.GroupID = AddGroup();
            v.PopIndex = 55;
            v.PopName = "lUMJoD";
            v.Weight = 54;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<pop>().FirstOrDefault();
				
                Assert.AreEqual(data.PopIndex, 55);
                Assert.AreEqual(data.PopName, "lUMJoD");
                Assert.AreEqual(data.Weight, 54);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            pop v = new pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.GroupID = AddGroup();
                v.PopIndex = 55;
                v.PopName = "lUMJoD";
                v.Weight = 54;
                context.Set<pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(popVM));

            popVM vm = rv.Model as popVM;
            v = new pop();
            v.ID = vm.Entity.ID;
       		
            v.PopIndex = 14;
            v.PopName = "vYtO";
            v.Weight = 71;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.GroupID", "");
            vm.FC.Add("Entity.PopIndex", "");
            vm.FC.Add("Entity.PopName", "");
            vm.FC.Add("Entity.Weight", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<pop>().FirstOrDefault();
 				
                Assert.AreEqual(data.PopIndex, 14);
                Assert.AreEqual(data.PopName, "vYtO");
                Assert.AreEqual(data.Weight, 71);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            pop v = new pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.GroupID = AddGroup();
                v.PopIndex = 55;
                v.PopName = "lUMJoD";
                v.Weight = 54;
                context.Set<pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(popVM));

            popVM vm = rv.Model as popVM;
            v = new pop();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<pop>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            pop v = new pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.GroupID = AddGroup();
                v.PopIndex = 55;
                v.PopName = "lUMJoD";
                v.Weight = 54;
                context.Set<pop>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            pop v1 = new pop();
            pop v2 = new pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.GroupID = AddGroup();
                v1.PopIndex = 55;
                v1.PopName = "lUMJoD";
                v1.Weight = 54;
                v2.GroupID = v1.GroupID; 
                v2.PopIndex = 14;
                v2.PopName = "vYtO";
                v2.Weight = 71;
                context.Set<pop>().Add(v1);
                context.Set<pop>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(popBatchVM));

            popBatchVM vm = rv.Model as popBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<pop>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as popListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddGroup()
        {
            pop_group v = new pop_group();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.Name = "4BaFMZwO";
                context.Set<pop_group>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
