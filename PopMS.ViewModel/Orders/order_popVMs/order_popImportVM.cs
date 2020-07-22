using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.Orders.order_popVMs
{
    public partial class order_popTemplateVM : BaseTemplateVM
    {
        public ExcelPropety ContractPop_Excel = ExcelPropety.CreateProperty<order_pop>(x => x.ContractPopID);
        [Display(Name = "订货数量")]
        public ExcelPropety OrderQty_Excel = ExcelPropety.CreateProperty<order_pop>(x => x.OrderQty);

	    protected override void InitVM()
        {
            ContractPop_Excel.DataType = ColumnDataType.ComboBox;
            ContractPop_Excel.ListItems = DC.Set<contract_pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.UnitPack);
        }

    }

    public class order_popImportVM : BaseImportVM<order_popTemplateVM, order_pop>
    {

    }

}
