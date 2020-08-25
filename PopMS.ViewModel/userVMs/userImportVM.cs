using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;
using NPOI.HPSF;

namespace PopMS.ViewModel.userVMs
{
    public partial class userTemplateVM : BaseTemplateVM
    {
        [Display(Name = "部门")]
        public ExcelPropety Dept_Excel = ExcelPropety.CreateProperty<user>(x => x.DeptID);
        [Display(Name = "工号")]
        public ExcelPropety ITCode_Excel = ExcelPropety.CreateProperty<user>(x => x.ITCode);
        [Display(Name = "姓名")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<user>(x => x.Name);
        [Display(Name = "性别")]
        public ExcelPropety Sex_Excel = ExcelPropety.CreateProperty<user>(x => x.Sex);
        [Display(Name = "联系电话")]
        public ExcelPropety CellPhone_Excel = ExcelPropety.CreateProperty<user>(x => x.CellPhone);

	    protected override void InitVM()
        {
            Dept_Excel.DataType = ColumnDataType.ComboBox;
            Dept_Excel.ListItems = DC.Set<dept>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.DeptName);
        }

    }

    public class userImportVM : BaseImportVM<userTemplateVM, user>
    {
        public override bool BatchSaveData()
        {
            SetEntityList();
            var my = DC.Set<user>().Where(r => r.ID == LoginUserInfo.Id).FirstOrDefault();
            var role = DC.Set<FrameworkRole>().Where(r => r.RoleCode=="1001").FirstOrDefault();
            var Group = DC.Set<FrameworkUserGroup>().Where(r => r.UserId == LoginUserInfo.Id).ToList();
            foreach (var item in EntityList)
            {
                item.DCID = my.DCID;
                item.UserRoles = new List<FrameworkUserRole>();
                item.UserRoles.Add(new FrameworkUserRole
                {
                    ID = Guid.NewGuid(),
                    UserId = item.ID,
                    RoleId = role.ID
                });
                item.IsValid = true;
                item.Password = Utils.GetMD5String(item.ITCode);
                item.UserGroups = new List<FrameworkUserGroup>();
                item.UserGroups.AddRange(Group.Select(r => new FrameworkUserGroup
                {
                    ID=Guid.NewGuid(),
                    UserId = item.ID,
                    GroupId = r.GroupId
                }).ToList());
            }
            DC.Set<user>().AddRange(EntityList);
            return DC.SaveChanges() > 0 ? true : false;
        }

    }

}
