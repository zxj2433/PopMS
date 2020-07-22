using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.area_locationVMs
{
    public partial class area_locationTemplateVM : BaseTemplateVM
    {
        [Display(Name = "区域")]
        public ExcelPropety Area_Excel = ExcelPropety.CreateProperty<area_location>(x => x.AreaID);
        [Display(Name = "货位")]
        public ExcelPropety Location_Excel = ExcelPropety.CreateProperty<area_location>(x => x.Location);
        [Display(Name = "可混放")]
        public ExcelPropety isMix_Excel = ExcelPropety.CreateProperty<area_location>(x => x.isMix);

	    protected override void InitVM()
        {
            Area_Excel.DataType = ColumnDataType.ComboBox;
            Area_Excel.ListItems = DC.Set<area>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Area);
        }

    }

    public class area_locationImportVM : BaseImportVM<area_locationTemplateVM, area_location>
    {

    }

}
