using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.CTT.contractVMs
{
    public partial class contractTemplateVM : BaseTemplateVM
    {
        [Display(Name = "合同编号")]
        public ExcelPropety ID_Excel = ExcelPropety.CreateProperty<contract>(x => x.ContractID);
        [Display(Name = "合同名")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<contract>(x => x.Name);
        [Display(Name = "供应商")]
        public ExcelPropety Vendor_Excel = ExcelPropety.CreateProperty<contract>(x => x.Vendor);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<contract>(x => x.Remark);
        [Display(Name = "开始日期")]
        public ExcelPropety StartDate_Excel = ExcelPropety.CreateProperty<contract>(x => x.StartDate);
        [Display(Name = "失效日期")]
        public ExcelPropety EndDate_Excel = ExcelPropety.CreateProperty<contract>(x => x.EndDate);
        [Display(Name = "合同金额")]
        public ExcelPropety Cost_Excel = ExcelPropety.CreateProperty<contract>(x => x.MaxCost);

        protected override void InitVM()
        {
            FileDisplayName = "合同导入模板";
        }

    }

    public class contractImportVM : BaseImportVM<contractTemplateVM, contract>
    {
        public override bool BatchSaveData()
        {
            SetEntityList();
            foreach (var item in EntityList)
            {
                item.DCID = DC.Set<user>().Where(r => r.ID == LoginUserInfo.Id).FirstOrDefault().DCID.Value;
            }
            return base.BatchSaveData();
        }
    }

}
