using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.deptVMs
{
    public partial class deptTemplateVM : BaseTemplateVM
    {
        [Display(Name = "部门")]
        public ExcelPropety DeptName_Excel = ExcelPropety.CreateProperty<dept>(x => x.DeptName);
        [Display(Name = "备注")]
        public ExcelPropety DeptRemark_Excel = ExcelPropety.CreateProperty<dept>(x => x.DeptRemark);
        public ExcelPropety Index_Excel = ExcelPropety.CreateProperty<dept>(x => x.Index);

	    protected override void InitVM()
        {
            FileDisplayName = "部门导入模板";
        }

    }

    public class deptImportVM : BaseImportVM<deptTemplateVM, dept>
    {

    }

}
