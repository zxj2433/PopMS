using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using PopMS.Controllers;
using PopMS.ViewModel.INV.inv_recordVMs;
using PopMS.Model;
using PopMS.DataAccess;

namespace PopMS.Test
{
    [TestClass]
    public class inv_recordControllerTest
    {
        private inv_recordController _controller;
        private string _seed;

        public inv_recordControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<inv_recordController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as inv_recordListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(inv_recordVM));

            inv_recordVM vm = rv.Model as inv_recordVM;
            inv_record v = new inv_record();
			
            v.InvID = AddInv();
            v.Qty = 69;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<inv_record>().FirstOrDefault();
				
                Assert.AreEqual(data.Qty, 69);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            inv_record v = new inv_record();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.InvID = AddInv();
                v.Qty = 69;
                context.Set<inv_record>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(inv_recordVM));

            inv_recordVM vm = rv.Model as inv_recordVM;
            v = new inv_record();
            v.ID = vm.Entity.ID;
       		
            v.Qty = 38;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.InvID", "");
            vm.FC.Add("Entity.Qty", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<inv_record>().FirstOrDefault();
 				
                Assert.AreEqual(data.Qty, 38);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            inv_record v = new inv_record();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.InvID = AddInv();
                v.Qty = 69;
                context.Set<inv_record>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(inv_recordVM));

            inv_recordVM vm = rv.Model as inv_recordVM;
            v = new inv_record();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<inv_record>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            inv_record v = new inv_record();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.InvID = AddInv();
                v.Qty = 69;
                context.Set<inv_record>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            inv_record v1 = new inv_record();
            inv_record v2 = new inv_record();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.InvID = AddInv();
                v1.Qty = 69;
                v2.InvID = v1.InvID; 
                v2.Qty = 38;
                context.Set<inv_record>().Add(v1);
                context.Set<inv_record>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(inv_recordBatchVM));

            inv_recordBatchVM vm = rv.Model as inv_recordBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<inv_record>().Count(), 0);
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

        private Guid AddArea()
        {
            area v = new area();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.DCID = AddDC();
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
                v.Location = "nRSH";
                context.Set<area_location>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }

        private Guid AddInv()
        {
            inventory v = new inventory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {

                v.LocationID = AddLocation();
                v.Stock = 4;
                v.UsedQty = 70;
                context.Set<inventory>().Add(v);
                context.SaveChanges();
            }
            return v.ID;
        }


    }
}
