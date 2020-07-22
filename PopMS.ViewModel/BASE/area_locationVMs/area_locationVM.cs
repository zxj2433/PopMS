using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.area_locationVMs
{
    public partial class area_locationVM : BaseCRUDVM<area_location>
    {
        public List<ComboSelectListItem> AllAreas { get; set; }

        public area_locationVM()
        {
            SetInclude(x => x.Area);
        }

        protected override void InitVM()
        {
            AllAreas = DC.Set<area>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Area);
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
