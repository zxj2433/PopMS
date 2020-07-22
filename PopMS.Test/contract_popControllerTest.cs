using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.CTT.contract_popVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class contract_popControllerTest
    {
        private contract_popController _controller;
        private string _seed;

        public contract_popControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<contract_popController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as contract_popListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(contract_popVM));

            contract_popVM vm = rv.Model as contract_popVM;
            contract_pop v = new contract_pop();
			
            v.PopID = AddPop();
            v.Cnt = 16;
            v.Price = 13;
            v.ContractID = AddContract();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<contract_pop>().FirstOrDefault();
				
                Assert.AreEqual(data.Cnt, 16);
                Assert.AreEqual(data.Price, 13);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            contract_pop v = new contract_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.PopID = AddPop();
                v.Cnt = 16;
                v.Price = 13;
                v.ContractID = AddContract();
                context.Set<contract_pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(contract_popVM));

            contract_popVM vm = rv.Model as contract_popVM;
            v = new contract_pop();
            v.ID = vm.Entity.ID;
       		
            v.Cnt = 34;
            v.Price = 82;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.PopID", "");
            vm.FC.Add("Entity.Cnt", "");
            vm.FC.Add("Entity.Price", "");
            vm.FC.Add("Entity.ContractID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<contract_pop>().FirstOrDefault();
 				
                Assert.AreEqual(data.Cnt, 34);
                Assert.AreEqual(data.Price, 82);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            contract_pop v = new contract_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.PopID = AddPop();
                v.Cnt = 16;
                v.Price = 13;
                v.ContractID = AddContract();
                context.Set<contract_pop>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(contract_popVM));

            contract_popVM vm = rv.Model as contract_popVM;
            v = new contract_pop();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<contract_pop>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            contract_pop v = new contract_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.PopID = AddPop();
                v.Cnt = 16;
                v.Price = 13;
                v.ContractID = AddContract();
                context.Set<contract_pop>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            contract_pop v1 = new contract_pop();
            contract_pop v2 = new contract_pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.PopID = AddPop();
                v1.Cnt = 16;
                v1.Price = 13;
                v1.ContractID = AddContract();
                v2.PopID = v1.PopID; 
                v2.Cnt = 34;
                v2.Price = 82;
                v2.ContractID = v1.ContractID; 
                context.Set<contract_pop>().Add(v1);
                context.Set<contract_pop>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(contract_popBatchVM));

            contract_popBatchVM vm = rv.Model as contract_popBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<contract_pop>().Count(), 0);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as contract_popListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddPop()
        {
            pop v = new pop();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.index = 61;
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

                v.FileName = "Wck";
                v.FileExt = "GConBqILW";
                v.Length = 77;
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
                v.Name = "JhNsfA";
                v.Prority = 78;
                v.Vendor = "eUP3MYsfz";
                v.Remark = "hIc";
                v.ContractFileID = AddContractFile();
                context.Set<contract>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
