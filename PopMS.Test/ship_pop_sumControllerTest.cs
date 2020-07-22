using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.ShipOrder.ship_pop_sumVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class ship_pop_sumControllerTest
    {
        private ship_pop_sumController _controller;
        private string _seed;

        public ship_pop_sumControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<ship_pop_sumController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as ship_pop_sumListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(ship_pop_sumVM));

            ship_pop_sumVM vm = rv.Model as ship_pop_sumVM;
            ship_pop_sum v = new ship_pop_sum();
			
            v.OrderRemark = "6od";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ship_pop_sum>().FirstOrDefault();
				
                Assert.AreEqual(data.OrderRemark, "6od");
            }

        }

        [TestMethod]
        public void EditTest()
        {
            ship_pop_sum v = new ship_pop_sum();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.OrderRemark = "6od";
                context.Set<ship_pop_sum>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ship_pop_sumVM));

            ship_pop_sumVM vm = rv.Model as ship_pop_sumVM;
            v = new ship_pop_sum();
            v.ID = vm.Entity.ID;
       		
            v.OrderRemark = "xq2r";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.OrderRemark", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ship_pop_sum>().FirstOrDefault();
 				
                Assert.AreEqual(data.OrderRemark, "xq2r");
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            ship_pop_sum v = new ship_pop_sum();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.OrderRemark = "6od";
                context.Set<ship_pop_sum>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ship_pop_sumVM));

            ship_pop_sumVM vm = rv.Model as ship_pop_sumVM;
            v = new ship_pop_sum();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<ship_pop_sum>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            ship_pop_sum v = new ship_pop_sum();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.OrderRemark = "6od";
                context.Set<ship_pop_sum>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ship_pop_sum v1 = new ship_pop_sum();
            ship_pop_sum v2 = new ship_pop_sum();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.OrderRemark = "6od";
                v2.OrderRemark = "xq2r";
                context.Set<ship_pop_sum>().Add(v1);
                context.Set<ship_pop_sum>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(ship_pop_sumBatchVM));

            ship_pop_sumBatchVM vm = rv.Model as ship_pop_sumBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<ship_pop_sum>().Count(), 0);
            }
        }


    }
}
