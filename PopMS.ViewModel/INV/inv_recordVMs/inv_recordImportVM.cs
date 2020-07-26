using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.INV.inv_recordVMs
{
    public partial class inv_recordTemplateVM : BaseTemplateVM
    {
        [Display(Name = "类型")]
        public ExcelPropety Type_Excel = ExcelPropety.CreateProperty<inv_record>(x => x.Type);
        [Display(Name = "数量")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<inv_record>(x => x.Qty);
        [Display(Name = "操作人")]
        public ExcelPropety UserName_Excel = ExcelPropety.CreateProperty<inv_record>(x => x.UserName);
        [Display(Name = "操作时间")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<inv_record>(x => x.UpdateTime);

	    protected override void InitVM()
        {
        }

    }

    public class inv_recordImportVM : BaseImportVM<inv_recordTemplateVM, inv_record>
    {

    }

}
