using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.BASE.pop_groupVMs
{
    public partial class pop_groupTemplateVM : BaseTemplateVM
    {
        [Display(Name = "仓库")]
        public ExcelPropety DC_Excel = ExcelPropety.CreateProperty<pop_group>(x => x.DCID);
        [Display(Name = "组别")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<pop_group>(x => x.Name);
        public ExcelPropety Index_Excel = ExcelPropety.CreateProperty<pop_group>(x => x.Index);

	    protected override void InitVM()
        {
            DC_Excel.DataType = ColumnDataType.ComboBox;
            DC_Excel.ListItems = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            FileDisplayName = "物料类型导入模板";
        }

    }

    public class pop_groupImportVM : BaseImportVM<pop_groupTemplateVM, pop_group>
    {

    }

}
