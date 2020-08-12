using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using Microsoft.EntityFrameworkCore;

namespace PopMS.ViewModel.CTT.contract_popVMs
{
    public partial class contract_popVM : BaseCRUDVM<contract_pop>
    {
        public List<ComboSelectListItem> AllPops { get; set; }
        public List<ComboSelectListItem> AllContracts { get; set; }

        public contract_popVM()
        {
            SetInclude(x => x.Pop);
            SetInclude(x => x.Contract);
        }

        protected override void InitVM()
        {
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
            AllContracts = DC.Set<contract>()
                .DPWhere(LoginUserInfo?.DataPrivileges, x => x.DCID).Include("DC")
                .GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.DC.Name + "-" + y.Name);
        }

        public override void DoAdd()
        {           
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
