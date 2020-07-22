using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.Orders.orderVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class orderControllerTest
    {
        private orderController _controller;
        private string _seed;

        public orderControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<orderController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as orderListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(orderVM));

            orderVM vm = rv.Model as orderVM;
            order v = new order();
			
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<order>().FirstOrDefault();
				
            }

        }

        [TestMethod]
        public void EditTest()
        {
            order v = new order();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                context.Set<order>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(orderVM));

            orderVM vm = rv.Model as orderVM;
            v = new order();
            v.ID = vm.Entity.ID;
       		
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<order>().FirstOrDefault();
 				
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            order v = new order();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                context.Set<order>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(orderVM));

            orderVM vm = rv.Model as orderVM;
            v = new order();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<order>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            order v = new order();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                context.Set<order>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            order v1 = new order();
            order v2 = new order();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                context.Set<order>().Add(v1);
                context.Set<order>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(orderBatchVM));

            orderBatchVM vm = rv.Model as orderBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<order>().Count(), 0);
            }
        }


    }
}
