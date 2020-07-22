using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.Orders.order_popVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class order_popControllerTest
    {
        private order_popController _controller;
        private string _seed;

        public order_popControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<order_popController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as order_popListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(order_popVM));

            order_popVM vm = rv.Model as order_popVM;
            order_pop v = new order_pop();
			
            v.ID = 87;
            v.OrderID = AddOrder();
            v.ContractPopID = AddContractPop();
            v.OrderQty = 15;
            v.RecQty = 1;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<order_pop>().FirstOrDefault();
				
                Assert.AreEqual(data.ID, 87);
                Assert.AreEqual(data.OrderQty, 15);
                Assert.AreEqual(data.RecQty, 1);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            order_pop v = new order_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 87;
                v.OrderID = AddOrder();
                v.ContractPopID = AddContractPop();
                v.OrderQty = 15;
                v.RecQty = 1;
                context.Set<order_pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(order_popVM));

            order_popVM vm = rv.Model as order_popVM;
            v = new order_pop();
            v.ID = vm.Entity.ID;
       		
            v.OrderQty = 65;
            v.RecQty = 73;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.OrderID", "");
            vm.FC.Add("Entity.ContractPopID", "");
            vm.FC.Add("Entity.OrderQty", "");
            vm.FC.Add("Entity.RecQty", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<order_pop>().FirstOrDefault();
 				
                Assert.AreEqual(data.OrderQty, 65);
                Assert.AreEqual(data.RecQty, 73);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            order_pop v = new order_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 87;
                v.OrderID = AddOrder();
                v.ContractPopID = AddContractPop();
                v.OrderQty = 15;
                v.RecQty = 1;
                context.Set<order_pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(order_popVM));

            order_popVM vm = rv.Model as order_popVM;
            v = new order_pop();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<order_pop>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            order_pop v = new order_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 87;
                v.OrderID = AddOrder();
                v.ContractPopID = AddContractPop();
                v.OrderQty = 15;
                v.RecQty = 1;
                context.Set<order_pop>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            order_pop v1 = new order_pop();
            order_pop v2 = new order_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 87;
                v1.OrderID = AddOrder();
                v1.ContractPopID = AddContractPop();
                v1.OrderQty = 15;
                v1.RecQty = 1;
                v2.OrderID = v1.OrderID; 
                v2.ContractPopID = v1.ContractPopID; 
                v2.OrderQty = 65;
                v2.RecQty = 73;
                context.Set<order_pop>().Add(v1);
                context.Set<order_pop>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(order_popBatchVM));

            order_popBatchVM vm = rv.Model as order_popBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<order_pop>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as order_popListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddOrder()
        {
            order v = new order();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                context.Set<order>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddPop()
        {
            pop v = new pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.index = 48;
                context.Set<pop>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
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

        private Guid AddContractFile()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.FileName = "gHbS2";
                v.FileExt = "LWmnVYS";
                v.Length = 76;
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddContract()
        {
            contract v = new contract();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.DCID = AddDC();
                v.Name = "lSHvv";
                v.Prority = 2;
                v.Vendor = "yd81kz";
                v.Remark = "FlIg";
                v.ContractFileID = AddContractFile();
                context.Set<contract>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddContractPop()
        {
            contract_pop v = new contract_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.PopID = AddPop();
                v.Cnt = 12;
                v.Price = 49;
                v.ContractID = AddContract();
                context.Set<contract_pop>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
