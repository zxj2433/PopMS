using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using Microsoft.EntityFrameworkCore;

namespace PopMS.ViewModel.CTT.contract_popVMs
{
    public partial class contract_popTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Pop_Excel = ExcelPropety.CreateProperty<contract_pop>(x => x.PopID);
        [Display(Name = "单位规格")]
        public ExcelPropety UnitPack_Excel = ExcelPropety.CreateProperty<contract_pop>(x => x.UnitPack);
        [Display(Name = "箱规")]
        public ExcelPropety Cnt_Excel = ExcelPropety.CreateProperty<contract_pop>(x => x.Cnt);
        [Display(Name = "单价")]
        public ExcelPropety Price_Excel = ExcelPropety.CreateProperty<contract_pop>(x => x.Price);
        public ExcelPropety Contract_Excel = ExcelPropety.CreateProperty<contract_pop>(x => x.ContractID);

	    protected override void InitVM()
        {
            Pop_Excel.DataType = ColumnDataType.ComboBox;
            Pop_Excel.ListItems = DC.Set<pop>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PopName);
            Contract_Excel.DataType = ColumnDataType.ComboBox;
            Contract_Excel.ListItems = DC.Set<contract>().DPWhere(LoginUserInfo?.DataPrivileges,x=>x.DCID).Include("DC").GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.DC.Name + "-" + y.Name);

            FileDisplayName = "合同物料报价导入模板";
        }

    }

    public class contract_popImportVM : BaseImportVM<contract_popTemplateVM, contract_pop>
    {
        public override bool BatchSaveData()
        {
            
            return base.BatchSaveData();
        }

    }

}
