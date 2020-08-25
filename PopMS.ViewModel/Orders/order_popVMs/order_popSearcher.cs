using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.Orders.order_popVMs
{
    public partial class order_popSearcher : BaseSearcher
    {
        [Display(Name ="订货日期")]
        public DateRange OrderDate { get; set; }
        public List<ComboSelectListItem> AllContracts { get; set; }
        [Display(Name = "合同")]
        public Guid? ContractID { get; set; }
        [Display(Name = "状态")]
        public RecStatus? Status { get; set; }
        public List<ComboSelectListItem> AllDCs { get; set; }

        [Display(Name = "仓库")]
        public Guid? DCID { get; set; }
        public List<ComboSelectListItem> AllPops { get; set; }
        [Display(Name = "物料")]
        public Guid? PopID { get; set; }
        protected override void InitVM()
        {
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
            AllContracts = DC.Set<contract>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.ContractID + "-" + x.Name);
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.Name);
        }

    }
}
