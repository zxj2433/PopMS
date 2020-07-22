using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using Microsoft.EntityFrameworkCore;

namespace PopMS.ViewModel.ShipOrder.ship_pop_sumVMs
{
    public partial class ship_pop_sumBatchVM : BaseBatchVM<ship_pop_sum, ship_pop_sum_BatchEdit>
    {
        public ship_pop_sumBatchVM()
        {
            ListVM = new ship_pop_sumListVM();
            LinkedVM = new ship_pop_sum_BatchEdit();
        }
        public override bool DoBatchDelete()
        {
            var sps = DC.Set<ship_pop>().Include("ShipIn").Where(r => Ids.Select(x => Guid.Parse(x)).ToList().Contains(r.Ship_Pop_SumID.Value));
            foreach (var item in sps)
            {
                item.Ship_Pop_SumID = null;
                item.Status = ShipStatus.NEW;
                foreach (var inv in item.ShipIn)
                {
                    inv.Inv.UsedQty -= inv.OutQty;
                    item.AlcQty -= inv.OutQty;
                }
                DC.Set<inventoryout>().RemoveRange(item.ShipIn);
            }            
            return base.DoBatchDelete();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ship_pop_sum_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }
    }

}
