using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.areaVMs
{
    public partial class areaTemplateVM : BaseTemplateVM
    {
        [Display(Name = "区域")]
        public ExcelPropety Area_Excel = ExcelPropety.CreateProperty<area>(x => x.Area);
        [Display(Name = "备注")]
        public ExcelPropety AreaRemark_Excel = ExcelPropety.CreateProperty<area>(x => x.AreaRemark);

	    protected override void InitVM()
        {
            FileDisplayName = "区域导入模板";
        }

    }

    public class areaImportVM : BaseImportVM<areaTemplateVM, area>
    {

    }

}
