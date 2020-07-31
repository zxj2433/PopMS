using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_popVMs
{
    public partial class ship_popBatchVM : BaseBatchVM<ship_pop, ship_pop_BatchEdit>
    {
        public ship_popBatchVM()
        {
            ListVM = new ship_popListVM();
            LinkedVM = new ship_pop_BatchEdit();
        }
        public bool ShipPop()
        {
            var pops = DC.Set<ship_pop>().Where(r => Ids.Select(x => Guid.Parse(x)).Contains(r.ID)).Where(r=>r.Status==ShipStatus.ING);
            foreach (var item in pops)
            {
                item.Status = ShipStatus.FINISH;
                item.ShipQty = item.AlcQty;
                item.ShipUser = LoginUserInfo.ITCode + " | " + LoginUserInfo.Name;
                item.ShipTime = DateTime.Now;
            }
            DC.Set<ship_pop>().UpdateRange(pops);
            return DC.SaveChanges() > 0 ? true : false;
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ship_pop_BatchEdit : BaseVM
    {
        public List<ComboSelectListItem> AllShip_Pop_Sums { get; set; }
        public Guid? Ship_Pop_SumID { get; set; }

        protected override void InitVM()
        {
            AllShip_Pop_Sums = DC.Set<ship_pop_sum>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.OrderRemark);
        }

    }

}
