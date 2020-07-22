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
    public partial class ship_popSearcher : BaseSearcher
    {
        [Display(Name = "状态")]
        public ShipStatus? Status { get; set; }
        public List<ComboSelectListItem> AllShip_Pop_Sums { get; set; }
        [Display(Name ="领取单")]
        public Guid? Ship_Pop_SumID { get; set; }

        protected override void InitVM()
        {
            AllShip_Pop_Sums = DC.Set<ship_pop_sum>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.OrderRemark);
        }

    }
}
