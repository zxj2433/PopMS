using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_pop_sumVMs
{
    public partial class ship_pop_sumVM : BaseCRUDVM<ship_pop_sum>
    {
        public string[] ShipPopIDs { get; set; }
        public ship_pop_sumVM()
        {
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {           
            base.DoAdd();
            var ShipPops = DC.Set<ship_pop>().Where(r => ShipPopIDs.Select(r => Guid.Parse(r)).Contains(r.ID)).ToList();
           
            foreach (var item in ShipPops)
            {
                item.Status = ShipStatus.ING;
                item.Ship_Pop_SumID = Entity.ID;
                var Invs = DC.Set<inventoryIn>().Where(r => r.OrderPop.ContractPop.PopID == item.PopID).Select(r=>r.Inv).ToList();
                Invs = Invs.OrderBy(r => r.PutTime).ToList();
                foreach (var inv in Invs)
                {
                    if(item.OrderQty>item.AlcQty)
                    {
                        int AlcQty = Math.Min(inv.Stock - inv.UsedQty, item.OrderQty.Value - item.AlcQty);
                        inv.UsedQty += AlcQty;
                        item.AlcQty += AlcQty;
                        inventoryOut InvOut = new inventoryOut
                        {
                            CreateBy=LoginUserInfo.ITCode,
                            CreateTime=DateTime.Now,
                            InvID = inv.ID,
                            spID = item.ID,
                            OutQty = AlcQty
                        };
                        DC.UpdateEntity(inv);
                        DC.Set<inventoryOut>().Add(InvOut);
                    }
                    else
                    {
                        break;
                    }
                }
                DC.UpdateEntity(item);
            }
            if(DC.SaveChanges()<=0)
            {
                base.DoDelete();
            }
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
