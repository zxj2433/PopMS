using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.popVMs
{
    public partial class popTemplateVM : BaseTemplateVM
    {
        [Display(Name = "物料编号")]
        public ExcelPropety PopNo_Excel = ExcelPropety.CreateProperty<pop>(x => x.PopNo);
        [Display(Name = "物料名称")]
        public ExcelPropety PopName_Excel = ExcelPropety.CreateProperty<pop>(x => x.PopName);
        [Display(Name = "序号")]
        public ExcelPropety index_Excel = ExcelPropety.CreateProperty<pop>(x => x.index);

	    protected override void InitVM()
        {
        }

    }

    public class popImportVM : BaseImportVM<popTemplateVM, pop>
    {

    }

}
