using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_popVMs
{
    public partial class ship_popVM : BaseCRUDVM<ship_pop>
    {
        public List<ComboSelectListItem> AllPops { get; set; }

        public ship_popVM()
        {
            SetInclude(x => x.Pop);
        }

        protected override void InitVM()
        {
            AllPops = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
        }

        public override void DoAdd()
        {
            Entity.UserID = LoginUserInfo.Id;

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
