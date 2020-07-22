using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.INV.inventoryVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class inventoryControllerTest
    {
        private inventoryController _controller;
        private string _seed;

        public inventoryControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<inventoryController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as inventoryListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(inventoryVM));

            inventoryVM vm = rv.Model as inventoryVM;
            inventory v = new inventory();
			
            v.LocationID = AddLocation();
            v.OrderPopID = AddOrderPop();
            v.Stock = 32;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<inventory>().FirstOrDefault();
				
                Assert.AreEqual(data.Stock, 32);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            inventory v = new inventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LocationID = AddLocation();
                v.OrderPopID = AddOrderPop();
                v.Stock = 32;
                context.Set<inventory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(inventoryVM));

            inventoryVM vm = rv.Model as inventoryVM;
            v = new inventory();
            v.ID = vm.Entity.ID;
       		
            v.Stock = 0;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LocationID", "");
            vm.FC.Add("Entity.OrderPopID", "");
            vm.FC.Add("Entity.Stock", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<inventory>().FirstOrDefault();
 				
                Assert.AreEqual(data.Stock, 0);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            inventory v = new inventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LocationID = AddLocation();
                v.OrderPopID = AddOrderPop();
                v.Stock = 32;
                context.Set<inventory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(inventoryVM));

            inventoryVM vm = rv.Model as inventoryVM;
            v = new inventory();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<inventory>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            inventory v = new inventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.LocationID = AddLocation();
                v.OrderPopID = AddOrderPop();
                v.Stock = 32;
                context.Set<inventory>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            inventory v1 = new inventory();
            inventory v2 = new inventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LocationID = AddLocation();
                v1.OrderPopID = AddOrderPop();
                v1.Stock = 32;
                v2.LocationID = v1.LocationID; 
                v2.OrderPopID = v1.OrderPopID; 
                v2.Stock = 0;
                context.Set<inventory>().Add(v1);
                context.Set<inventory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(inventoryBatchVM));

            inventoryBatchVM vm = rv.Model as inventoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<inventory>().Count(), 0);
            }
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

        private Guid AddLocation()
        {
            area_location v = new area_location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.AreaID = AddArea();
                v.Location = "a2Q2KGB2";
                context.Set<area_location>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
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

                v.index = 29;
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

                v.FileName = "eaX";
                v.FileExt = "rmY9";
                v.Length = 61;
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
                v.Name = "J90dYCQ";
                v.Prority = 35;
                v.Vendor = "Y8d13c";
                v.Remark = "PK4N";
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
                v.Cnt = 78;
                v.Price = 89;
                v.ContractID = AddContract();
                context.Set<contract_pop>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Int32 AddOrderPop()
        {
            order_pop v = new order_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.ID = 16;
                v.OrderID = AddOrder();
                v.ContractPopID = AddContractPop();
                v.OrderQty = 95;
                v.RecQty = 15;
                context.Set<order_pop>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
