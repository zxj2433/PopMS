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
            SetInclude(x => x.ContractPop);
            SetInclude(x => x.ContractPop.Contract);
        }

        protected override void InitVM()
        {
            AllContractPops = DC.Set<contract_pop>()
                .Include("Contract")
                .DPWhere(LoginUserInfo?.DataPrivileges,x=>x.Contract.DCID)
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
            var MaxCost = DC.Set<contract_pop>()
                .Include("Contract")
                .Where(r => r.ID == Entity.ContractPopID).FirstOrDefault().Contract.MaxCost;
            //var MaxCost = DC.Set<contract>().Where(r => r.ID == OrderPop.ContractPop.ContractID).FirstOrDefault().MaxCost;
            var CurQty = DC.Set<order_pop>()
                .Include("ContractPop")
                .Include("ContractPop.Contract")
                .Where(r => r.ContractPopID == Entity.ContractPopID).Sum(x=>x.OrderQty);
            var Price= DC.Set<contract_pop>()
                .Include("Contract")
                .Where(r => r.ID == Entity.ContractPopID).FirstOrDefault().Price;
            if (MaxCost > 0 && (CurQty+Entity.OrderQty)*Price>MaxCost)
            {
                MSD.AddModelError("OverCost", "合同订货金额超出最大限制");
                return;
            }
            Entity.Price = Price;
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
            if(Location==null||Location==Guid.Empty)
            {
                MSD.AddModelError("NullLocation", "请选择上架货位");
                return false;
            }
            if(RecQty>(Entity.OrderQty-Entity.RecQty))
            {
                MSD.AddModelError("QtyOver", "实收数量不能大于剩余可收货数量");
                return false;
            }
            var loc = DC.Set<area_location>().AsNoTracking().Where(r => r.ID == Location.Value).FirstOrDefault();
            if(!loc.isMix.Value)
            {
                var invs = DC.Set<inventory>().Where(r => r.LocationID == Location.Value);
                var InInvs = DC.Set<inventoryIn>().Include("OrderPop.ContractPop").Where(r => invs.Select(x => x.ID).Contains(r.InvID));
                List<Guid> pops = InInvs.Select(r => r.OrderPop.ContractPop.PopID).ToList();
                pops.Add(Entity.ContractPop.PopID);
                if (pops.Distinct().Count() > 1)
                {
                    MSD.AddModelError("LocNotMix", "货位不可混放，但当前货位已经有其他货品了");
                    return false;
                }
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
            OrderPop.Status = OrderPop.RecQty + RecQty == OrderPop.OrderQty ? RecStatus.FINISH : RecStatus.ING;
            OrderPop.RecQty += RecQty;
            OrderPop.RecTime = DateTime.Now;
            OrderPop.RecUser = LoginUserInfo.ITCode + " | " + LoginUserInfo.Name;
            DC.UpdateEntity(OrderPop);
            return DC.SaveChanges() > 0 ? true : false;
        }
    }
}
