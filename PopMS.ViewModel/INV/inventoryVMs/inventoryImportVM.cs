using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inventoryVMs
{
    public partial class inventoryTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Location_Excel = ExcelPropety.CreateProperty<inventory>(x => x.LocationID);
        [Display(Name = "库存")]
        public ExcelPropety Stock_Excel = ExcelPropety.CreateProperty<inventory>(x => x.Stock);

	    protected override void InitVM()
        {
            Location_Excel.DataType = ColumnDataType.ComboBox;
            Location_Excel.ListItems = DC.Set<area_location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Location);
        }

    }

    public class inventoryImportVM : BaseImportVM<inventoryTemplateVM, inventory>
    {

    }

}
