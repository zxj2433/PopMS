using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_popVMs
{
    public partial class ship_popVM : BaseCRUDVM<ship_pop>
    {
        public List<ComboSelectListItem> AllPops { get; set; }

        public ship_popVM()
        {
            SetInclude(x => x.Pop);
        }

        protected override void InitVM()
        {
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
        }

        public override void DoAdd()
        {
            Entity.UserID = LoginUserInfo.Id;
            int UsedQty = DC.Set<inventoryout>().Where(r => r.sp.PopID == Entity.PopID).Sum(r => r.sp.AlcQty);
            int Stock = DC.Set<inventoryIn>().Where(r => r.OrderPop.ContractPop.PopID == Entity.PopID).Sum(r => r.Inv.Stock);
            if((Stock-UsedQty)<Entity.OrderQty)
            {
                MSD.AddModelError("OverQty", "最大货订量不可超过" + (Stock - UsedQty).ToString()+"个");
                return;
            }
            Entity.EnableQty = Stock - UsedQty;
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
    }
}
