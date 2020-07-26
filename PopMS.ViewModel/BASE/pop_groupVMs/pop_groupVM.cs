using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.pop_groupVMs
{
    public partial class pop_groupVM : BaseCRUDVM<pop_group>
    {
        public List<ComboSelectListItem> AllDCs { get; set; }

        public pop_groupVM()
        {
            SetInclude(x => x.DC);
        }

        protected override void InitVM()
        {
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
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
