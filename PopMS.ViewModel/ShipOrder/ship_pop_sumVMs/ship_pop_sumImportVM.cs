using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.ShipOrder.ship_pop_sumVMs
{
    public partial class ship_pop_sumTemplateVM : BaseTemplateVM
    {
        [Display(Name = "日期")]
        public ExcelPropety OrderDate_Excel = ExcelPropety.CreateProperty<ship_pop_sum>(x => x.OrderDate);
        [Display(Name = "备注")]
        public ExcelPropety OrderRemark_Excel = ExcelPropety.CreateProperty<ship_pop_sum>(x => x.OrderRemark);

	    protected override void InitVM()
        {
        }

    }

    public class ship_pop_sumImportVM : BaseImportVM<ship_pop_sumTemplateVM, ship_pop_sum>
    {

    }

}
