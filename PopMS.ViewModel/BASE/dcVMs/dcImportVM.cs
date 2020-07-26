using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.dcVMs
{
    public partial class dcTemplateVM : BaseTemplateVM
    {
        [Display(Name = "仓库号")]
        public ExcelPropety DcNo_Excel = ExcelPropety.CreateProperty<dc>(x => x.DcNo);
        [Display(Name = "仓库名")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<dc>(x => x.Name);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<dc>(x => x.Remark);

	    protected override void InitVM()
        {
            FileDisplayName = "仓库导入模板";
        }

    }

    public class dcImportVM : BaseImportVM<dcTemplateVM, dc>
    {

    }

}
