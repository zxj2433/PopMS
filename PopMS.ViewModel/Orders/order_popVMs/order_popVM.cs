using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace PopMS.ViewModel.Orders.order_popVMs
{
    public partial class order_popVM : BaseCRUDVM<order_pop>
    {
        public List<ComboSelectListItem> AllContractPops { get; set; }
        public List<ComboSelectListItem> AllLocations { get; set; }
        [Display(Name ="上架货位")]
        public Guid? Location { get; set; }
        [Display(Name = "实收数量")]
        public int RecQty { get; set; }

        public order_popVM()
        {
        }

        protected override void InitVM()
        {
            AllContractPops = DC.Set<contract_pop>()
                .Include("Pop")
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.Pop.DCID)
                .Select(r => new ComboSelectListItem
                {
                    Value = r.ID,
                    Text = r.Pop.PopName
                }).ToList();
            AllLocations = DC.Set<area_location>()
                .Include("Area")
                .DPWhere(LoginUserInfo?.DataPrivileges, x => x.Area.DCID)
                .OrderBy(r=>r.Location)
                .Select(r => new ComboSelectListItem
                {
                    Value = r.ID,
                    Text = r.Location,
                    Selected = false
                }).ToList();
            RecQty = Entity.OrderQty - Entity.RecQty;
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
        public bool DoRecPop()
        {
            if(RecQty>(Entity.OrderQty-Entity.RecQty))
            {
                MSD.AddModelError("QtyOver", "实收数量不能大于剩余可收货数量");
                return false;
            }
            if(Location==null||Location==Guid.Empty)
            {
                MSD.AddModelError("NullLocation", "请选择上架货位");
                return false;
            }
            inventory inv = new inventory
            {
                ID = Guid.NewGuid(),
                LocationID = Location.Value,
                Stock=RecQty,
                PutUser = LoginUserInfo.ITCode + " | " + LoginUserInfo.Name,
                PutTime = DateTime.Now
            };
            inventoryIn InvIn = new inventoryIn
            {
                CreateBy=LoginUserInfo.ITCode,
                CreateTime=DateTime.Now,
                InvID = inv.ID,
                OrderPopID = Entity.ID,
                InQty = RecQty
            };
            DC.AddEntity(inv);
            DC.AddEntity(InvIn);
            var OrderPop = DC.Set<order_pop>().Where(r => r.ID == Entity.ID).FirstOrDefault();
            OrderPop.RecQty += RecQty;
            DC.UpdateEntity(OrderPop);
            return DC.SaveChanges() > 0 ? true : false;
        }
    }
}
