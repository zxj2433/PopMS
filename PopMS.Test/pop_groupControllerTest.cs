using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.BASE.pop_groupVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class pop_groupControllerTest
    {
        private pop_groupController _controller;
        private string _seed;

        public pop_groupControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<pop_groupController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as pop_groupListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(pop_groupVM));

            pop_groupVM vm = rv.Model as pop_groupVM;
            pop_group v = new pop_group();
			
            v.DCID = AddDC();
            v.Index = 21;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<pop_group>().FirstOrDefault();
				
                Assert.AreEqual(data.Index, 21);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            pop_group v = new pop_group();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DCID = AddDC();
                v.Index = 21;
                context.Set<pop_group>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(pop_groupVM));

            pop_groupVM vm = rv.Model as pop_groupVM;
            v = new pop_group();
            v.ID = vm.Entity.ID;
       		
            v.Index = 27;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DCID", "");
            vm.FC.Add("Entity.Index", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<pop_group>().FirstOrDefault();
 				
                Assert.AreEqual(data.Index, 27);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            pop_group v = new pop_group();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DCID = AddDC();
                v.Index = 21;
                context.Set<pop_group>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(pop_groupVM));

            pop_groupVM vm = rv.Model as pop_groupVM;
            v = new pop_group();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<pop_group>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            pop_group v = new pop_group();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.DCID = AddDC();
                v.Index = 21;
                context.Set<pop_group>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            pop_group v1 = new pop_group();
            pop_group v2 = new pop_group();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DCID = AddDC();
                v1.Index = 21;
                v2.DCID = v1.DCID; 
                v2.Index = 27;
                context.Set<pop_group>().Add(v1);
                context.Set<pop_group>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(pop_groupBatchVM));

            pop_groupBatchVM vm = rv.Model as pop_groupBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<pop_group>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as pop_groupListVM);
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


    }
}
