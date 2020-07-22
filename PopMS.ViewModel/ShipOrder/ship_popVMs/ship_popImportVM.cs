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
    public partial class ship_popTemplateVM : BaseTemplateVM
    {
        [Display(Name = "领用物料")]
        public ExcelPropety Pop_Excel = ExcelPropety.CreateProperty<ship_pop>(x => x.PopID);
        [Display(Name = "领用数量")]
        public ExcelPropety OrderQty_Excel = ExcelPropety.CreateProperty<ship_pop>(x => x.OrderQty);

	    protected override void InitVM()
        {
            Pop_Excel.DataType = ColumnDataType.ComboBox;
            Pop_Excel.ListItems = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
        }

    }

    public class ship_popImportVM : BaseImportVM<ship_popTemplateVM, ship_pop>
    {

    }

}
