using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using PopMS.Model;


namespace PopMS.ViewModel.userVMs
{
    public partial class userVM : BaseCRUDVM<user>
    {
        public List<ComboSelectListItem> AllDCs { get; set; }
        public List<ComboSelectListItem> AllDepts { get; set; }
        public List<ComboSelectListItem> AllUserRoless { get; set; }
        [Display(Name = "角色")]
        public List<Guid> SelectedUserRolesIDs { get; set; }
        public List<ComboSelectListItem> AllUserGroupss { get; set; }
        [Display(Name = "用户组")]
        public List<Guid> SelectedUserGroupsIDs { get; set; }

        public userVM()
        {
            SetInclude(x => x.Dept);
            SetInclude(x => x.UserRoles);
            SetInclude(x => x.UserGroups);
        }

        protected override void InitVM()
        {
            AllDCs = DC.Set<dc>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllDepts = DC.Set<dept>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.DeptName);
            AllUserRoless = DC.Set<FrameworkRole>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.RoleName);
            SelectedUserRolesIDs = Entity.UserRoles?.Select(x => x.RoleId).ToList();
            AllUserGroupss = DC.Set<FrameworkGroup>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.GroupName);
            SelectedUserGroupsIDs = Entity.UserGroups?.Select(x => x.GroupId).ToList();
        }

        public override void DoAdd()
        {
            Entity.Password = Utils.GetMD5String(Entity.ITCode);
            Entity.UserRoles = new List<FrameworkUserRole>();
            if (SelectedUserRolesIDs != null)
            {
                foreach (var id in SelectedUserRolesIDs)
                {
                    Entity.UserRoles.Add(new FrameworkUserRole { RoleId = id });
                }
            }

            Entity.UserGroups = new List<FrameworkUserGroup>();
            if (SelectedUserGroupsIDs != null)
            {
                foreach (var id in SelectedUserGroupsIDs)
                {
                    Entity.UserGroups.Add(new FrameworkUserGroup { GroupId = id });
                }
            }
           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            Entity.UserRoles = new List<FrameworkUserRole>();
            if(SelectedUserRolesIDs != null )
            {
                SelectedUserRolesIDs.ForEach(x => Entity.UserRoles.Add(new FrameworkUserRole { ID = Guid.NewGuid(), RoleId = x }));
            }

            Entity.UserGroups = new List<FrameworkUserGroup>();
            if(SelectedUserGroupsIDs != null )
            {
                SelectedUserGroupsIDs.ForEach(x => Entity.UserGroups.Add(new FrameworkUserGroup { ID = Guid.NewGuid(), GroupId = x }));
            }

            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
