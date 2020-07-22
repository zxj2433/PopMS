using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.BASE.area_locationVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class area_locationControllerTest
    {
        private area_locationController _controller;
        private string _seed;

        public area_locationControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<area_locationController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as area_locationListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(area_locationVM));

            area_locationVM vm = rv.Model as area_locationVM;
            area_location v = new area_location();
			
            v.AreaID = AddArea();
            v.Location = "Cw7Go";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<area_location>().FirstOrDefault();
				
                Assert.AreEqual(data.Location, "Cw7Go");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            area_location v = new area_location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.AreaID = AddArea();
                v.Location = "Cw7Go";
                context.Set<area_location>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(area_locationVM));

            area_locationVM vm = rv.Model as area_locationVM;
            v = new area_location();
            v.ID = vm.Entity.ID;
       		
            v.Location = "Bs8U";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.AreaID", "");
            vm.FC.Add("Entity.Location", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<area_location>().FirstOrDefault();
 				
                Assert.AreEqual(data.Location, "Bs8U");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            area_location v = new area_location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.AreaID = AddArea();
                v.Location = "Cw7Go";
                context.Set<area_location>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(area_locationVM));

            area_locationVM vm = rv.Model as area_locationVM;
            v = new area_location();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<area_location>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            area_location v = new area_location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.AreaID = AddArea();
                v.Location = "Cw7Go";
                context.Set<area_location>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            area_location v1 = new area_location();
            area_location v2 = new area_location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.AreaID = AddArea();
                v1.Location = "Cw7Go";
                v2.AreaID = v1.AreaID; 
                v2.Location = "Bs8U";
                context.Set<area_location>().Add(v1);
                context.Set<area_location>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(area_locationBatchVM));

            area_locationBatchVM vm = rv.Model as area_locationBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<area_location>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as area_locationListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddArea()
        {
            area v = new area();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                context.Set<area>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
