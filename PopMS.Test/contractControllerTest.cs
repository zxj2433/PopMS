using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.CTT.contractVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class contractControllerTest
    {
        private contractController _controller;
        private string _seed;

        public contractControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<contractController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as contractListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(contractVM));

            contractVM vm = rv.Model as contractVM;
            contract v = new contract();
			
            v.DCID = AddDC();
            v.Name = "E3p";
            v.Prority = 64;
            v.Vendor = "yGhaxrb";
            v.Remark = "t37pj9sxs";
            v.ContractFileID = AddContractFile();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<contract>().FirstOrDefault();
				
                Assert.AreEqual(data.Name, "E3p");
                Assert.AreEqual(data.Prority, 64);
                Assert.AreEqual(data.Vendor, "yGhaxrb");
                Assert.AreEqual(data.Remark, "t37pj9sxs");
            }

        }

        [TestMethod]
        public void EditTest()
        {
            contract v = new contract();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DCID = AddDC();
                v.Name = "E3p";
                v.Prority = 64;
                v.Vendor = "yGhaxrb";
                v.Remark = "t37pj9sxs";
                v.ContractFileID = AddContractFile();
                context.Set<contract>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(contractVM));

            contractVM vm = rv.Model as contractVM;
            v = new contract();
            v.ID = vm.Entity.ID;
       		
            v.Name = "7pgJ";
            v.Prority = 47;
            v.Vendor = "PZP";
            v.Remark = "KCcxOh7";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DCID", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Prority", "");
            vm.FC.Add("Entity.Vendor", "");
            vm.FC.Add("Entity.Remark", "");
            vm.FC.Add("Entity.ContractFileID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<contract>().FirstOrDefault();
 				
                Assert.AreEqual(data.Name, "7pgJ");
                Assert.AreEqual(data.Prority, 47);
                Assert.AreEqual(data.Vendor, "PZP");
                Assert.AreEqual(data.Remark, "KCcxOh7");
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            contract v = new contract();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DCID = AddDC();
                v.Name = "E3p";
                v.Prority = 64;
                v.Vendor = "yGhaxrb";
                v.Remark = "t37pj9sxs";
                v.ContractFileID = AddContractFile();
                context.Set<contract>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(contractVM));

            contractVM vm = rv.Model as contractVM;
            v = new contract();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<contract>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            contract v = new contract();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.DCID = AddDC();
                v.Name = "E3p";
                v.Prority = 64;
                v.Vendor = "yGhaxrb";
                v.Remark = "t37pj9sxs";
                v.ContractFileID = AddContractFile();
                context.Set<contract>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            contract v1 = new contract();
            contract v2 = new contract();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DCID = AddDC();
                v1.Name = "E3p";
                v1.Prority = 64;
                v1.Vendor = "yGhaxrb";
                v1.Remark = "t37pj9sxs";
                v1.ContractFileID = AddContractFile();
                v2.DCID = v1.DCID; 
                v2.Name = "7pgJ";
                v2.Prority = 47;
                v2.Vendor = "PZP";
                v2.Remark = "KCcxOh7";
                v2.ContractFileID = v1.ContractFileID; 
                context.Set<contract>().Add(v1);
                context.Set<contract>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(contractBatchVM));

            contractBatchVM vm = rv.Model as contractBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<contract>().Count(), 0);
            }
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

                v.FileName = "w2h46";
                v.FileExt = "OSYZUYz";
                v.Length = 91;
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
