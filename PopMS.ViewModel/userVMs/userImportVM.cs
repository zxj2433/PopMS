using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.userVMs
{
    public partial class userTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Dept_Excel = ExcelPropety.CreateProperty<user>(x => x.DeptID);
        [Display(Name = "Account")]
        public ExcelPropety ITCode_Excel = ExcelPropety.CreateProperty<user>(x => x.ITCode);
        [Display(Name = "Password")]
        public ExcelPropety Password_Excel = ExcelPropety.CreateProperty<user>(x => x.Password);
        [Display(Name = "Email")]
        public ExcelPropety Email_Excel = ExcelPropety.CreateProperty<user>(x => x.Email);
        [Display(Name = "Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<user>(x => x.Name);
        [Display(Name = "Sex")]
        public ExcelPropety Sex_Excel = ExcelPropety.CreateProperty<user>(x => x.Sex);
        [Display(Name = "CellPhone")]
        public ExcelPropety CellPhone_Excel = ExcelPropety.CreateProperty<user>(x => x.CellPhone);
        [Display(Name = "IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<user>(x => x.IsValid);

	    protected override void InitVM()
        {
            Dept_Excel.DataType = ColumnDataType.ComboBox;
            Dept_Excel.ListItems = DC.Set<dept>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.DeptName);
        }

    }

    public class userImportVM : BaseImportVM<userTemplateVM, user>
    {

    }

}
