using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using Microsoft.EntityFrameworkCore;

namespace PopMS.ViewModel.Orders.order_popVMs
{
    public partial class order_popTemplateVM : BaseTemplateVM
    {
        [Display(Name ="合同物料")]
        public ExcelPropety ContractPop_Excel = ExcelPropety.CreateProperty<order_pop>(x => x.ContractPopID);
        [Display(Name = "订货数量")]
        public ExcelPropety OrderQty_Excel = ExcelPropety.CreateProperty<order_pop>(x => x.OrderQty);
        [Display(Name ="备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<order_pop>(x => x.Remark);

	    protected override void InitVM()
        {
            ContractPop_Excel.DataType = ColumnDataType.ComboBox;
            ContractPop_Excel.ListItems = DC.Set<contract_pop>().Include("Pop").GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Pop.PopName);
        }

    }

    public class order_popImportVM : BaseImportVM<order_popTemplateVM, order_pop>
    {
        public override bool BatchSaveData()
        {
            SetEntityList();
            foreach (var item in EntityList)
            {
                item.Price = DC.Set<contract_pop>().AsNoTracking().Where(r => r.ID == item.ContractPopID).FirstOrDefault().Price;
            }
            return base.BatchSaveData();
        }
    }

}
