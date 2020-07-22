using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.ShipOrder.ship_popVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class ship_popControllerTest
    {
        private ship_popController _controller;
        private string _seed;

        public ship_popControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<ship_popController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as ship_popListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(ship_popVM));

            ship_popVM vm = rv.Model as ship_popVM;
            ship_pop v = new ship_pop();
			
            v.UserID = AddUser();
            v.PopID = AddPop();
            v.OrderQty = 38;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ship_pop>().FirstOrDefault();
				
                Assert.AreEqual(data.OrderQty, 38);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            ship_pop v = new ship_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.UserID = AddUser();
                v.PopID = AddPop();
                v.OrderQty = 38;
                context.Set<ship_pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ship_popVM));

            ship_popVM vm = rv.Model as ship_popVM;
            v = new ship_pop();
            v.ID = vm.Entity.ID;
       		
            v.OrderQty = 97;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.UserID", "");
            vm.FC.Add("Entity.PopID", "");
            vm.FC.Add("Entity.OrderQty", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ship_pop>().FirstOrDefault();
 				
                Assert.AreEqual(data.OrderQty, 97);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            ship_pop v = new ship_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.UserID = AddUser();
                v.PopID = AddPop();
                v.OrderQty = 38;
                context.Set<ship_pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ship_popVM));

            ship_popVM vm = rv.Model as ship_popVM;
            v = new ship_pop();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<ship_pop>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            ship_pop v = new ship_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.UserID = AddUser();
                v.PopID = AddPop();
                v.OrderQty = 38;
                context.Set<ship_pop>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ship_pop v1 = new ship_pop();
            ship_pop v2 = new ship_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.UserID = AddUser();
                v1.PopID = AddPop();
                v1.OrderQty = 38;
                v2.UserID = v1.UserID; 
                v2.PopID = v1.PopID; 
                v2.OrderQty = 97;
                context.Set<ship_pop>().Add(v1);
                context.Set<ship_pop>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(ship_popBatchVM));

            ship_popBatchVM vm = rv.Model as ship_popBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<ship_pop>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as ship_popListVM);
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

                v.Index = 7;
                context.Set<dept>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddUser()
        {
            user v = new user();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.DCID = AddDC();
                v.DeptID = AddDept();
                v.ITCode = "3eT";
                v.Password = "4vlU";
                v.Name = "pJX";
                context.Set<user>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddPop()
        {
            pop v = new pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.index = 36;
                context.Set<pop>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
