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
        [Display(Name ="申领日期")]
        public DateRange ShipDate { get; set; }

        public List<ComboSelectListItem> AllPops { get; set; }
        [Display(Name = "物料")]
        public Guid? PopID { get; set; }
        public List<ComboSelectListItem> AllDCs { get; set; }
        [Display(Name = "仓库")]
        public Guid? DCID { get; set; }

        protected override void InitVM()
        {
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
            AllShip_Pop_Sums = DC.Set<ship_pop_sum>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y =>y.OrderDate.ToString("yyyy-MM-dd")+"|" +y.OrderRemark);
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.Name);
        }

    }
}
