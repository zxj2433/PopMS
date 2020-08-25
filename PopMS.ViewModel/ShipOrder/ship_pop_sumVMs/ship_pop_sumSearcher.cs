using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_pop_sumVMs
{
    public partial class ship_pop_sumSearcher : BaseSearcher
    {
        [Display(Name = "日期")]
        public DateRange OrderDate { get; set; }
        [Display(Name ="状态")]
        public ShipStatus? Status { get; set; }
        public List<ComboSelectListItem> AllDCs { get; set; }

        [Display(Name = "仓库")]
        public Guid? DCID { get; set; }
        public List<ComboSelectListItem> AllPops { get; set; }
        [Display(Name = "物料")]
        public Guid? PopID { get; set; }
        protected override void InitVM()
        {
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.Name);
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
        }

    }
}
