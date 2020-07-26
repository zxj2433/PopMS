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
        protected override void InitVM()
        {
            AllContracts = DC.Set<contract>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.ContractID + "-" + x.Name);
        }

    }
}
