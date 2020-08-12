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
        [Display(Name ="组别")]
        public ExcelPropety Group_Excel = ExcelPropety.CreateProperty<pop>(x => x.GroupID);
        [Display(Name = "外部编码")]
        public ExcelPropety OutID_Excel = ExcelPropety.CreateProperty<pop>(x => x.OutID);
        [Display(Name = "物料名称")]
        public ExcelPropety PopName_Excel = ExcelPropety.CreateProperty<pop>(x => x.PopName);
        [Display(Name = "序号")]
        public ExcelPropety index_Excel = ExcelPropety.CreateProperty<pop>(x => x.index);
        [Display(Name = "规格")]
        public ExcelPropety Pack_Excel = ExcelPropety.CreateProperty<pop>(x => x.Pack);
        [Display(Name = "单位")]
        public ExcelPropety Unit_Excel = ExcelPropety.CreateProperty<pop>(x => x.Unit);
        [Display(Name = "重量")]
        public ExcelPropety Weight_Excel = ExcelPropety.CreateProperty<pop>(x => x.Weight);

	    protected override void InitVM()
        {
            Group_Excel.DataType = ColumnDataType.ComboBox;
            Group_Excel.ListItems = DC.Set<pop_group>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

    public class popImportVM : BaseImportVM<popTemplateVM, pop>
    {

    }

}
