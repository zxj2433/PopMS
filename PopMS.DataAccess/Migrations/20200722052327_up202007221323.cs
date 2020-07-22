using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PopMS.DataAccess.Migrations
{
    public partial class up202007221323 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModuleName = table.Column<string>(maxLength: 50, nullable: true),
                    ActionName = table.Column<string>(maxLength: 50, nullable: true),
                    ITCode = table.Column<string>(maxLength: 50, nullable: true),
                    ActionUrl = table.Column<string>(maxLength: 250, nullable: true),
                    ActionTime = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IP = table.Column<string>(maxLength: 50, nullable: true),
                    LogType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "dcs",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    DcNo = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dcs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    FileName = table.Column<string>(nullable: false),
                    FileExt = table.Column<string>(maxLength: 10, nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    UploadTime = table.Column<DateTime>(nullable: false),
                    IsTemprory = table.Column<bool>(nullable: false),
                    SaveFileMode = table.Column<int>(nullable: true),
                    GroupName = table.Column<string>(maxLength: 50, nullable: true),
                    FileData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkDomains",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    DomainName = table.Column<string>(maxLength: 50, nullable: false),
                    DomainAddress = table.Column<string>(maxLength: 50, nullable: false),
                    DomainPort = table.Column<int>(nullable: true),
                    EntryUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkDomains", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    GroupCode = table.Column<string>(maxLength: 100, nullable: false),
                    GroupName = table.Column<string>(maxLength: 50, nullable: false),
                    GroupRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    RoleCode = table.Column<string>(maxLength: 100, nullable: false),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    RoleRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    RefreshToken = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ship_Pop_Sums",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderRemark = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ship_Pop_Sums", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    DCID = table.Column<Guid>(nullable: false),
                    Area = table.Column<string>(maxLength: 50, nullable: true),
                    AreaRemark = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_areas_dcs_DCID",
                        column: x => x.DCID,
                        principalTable: "dcs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "depts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    DCID = table.Column<Guid>(nullable: false),
                    DeptName = table.Column<string>(maxLength: 50, nullable: true),
                    DeptRemark = table.Column<string>(maxLength: 50, nullable: true),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_depts_dcs_DCID",
                        column: x => x.DCID,
                        principalTable: "dcs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pops",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    DCID = table.Column<Guid>(nullable: false),
                    PopNo = table.Column<string>(maxLength: 50, nullable: true),
                    PopName = table.Column<string>(maxLength: 100, nullable: true),
                    index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pops", x => x.ID);
                    table.ForeignKey(
                        name: "FK_pops_dcs_DCID",
                        column: x => x.DCID,
                        principalTable: "dcs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DCID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Vendor = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ContractFileID = table.Column<Guid>(nullable: true),
                    UserCode = table.Column<string>(maxLength: 50, nullable: true),
                    ImportTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_contracts_FileAttachments_ContractFileID",
                        column: x => x.ContractFileID,
                        principalTable: "FileAttachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_contracts_dcs_DCID",
                        column: x => x.DCID,
                        principalTable: "dcs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkMenus",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    PageName = table.Column<string>(maxLength: 50, nullable: false),
                    ActionName = table.Column<string>(nullable: true),
                    ModuleName = table.Column<string>(nullable: true),
                    FolderOnly = table.Column<bool>(nullable: false),
                    IsInherit = table.Column<bool>(nullable: false),
                    ClassName = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    DomainId = table.Column<Guid>(nullable: true),
                    ShowOnMenu = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    IsInside = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    ICon = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkMenus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FrameworkMenus_FrameworkDomains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "FrameworkDomains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FrameworkMenus_FrameworkMenus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FrameworkMenus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "area_Locations",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    AreaID = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(maxLength: 20, nullable: false),
                    isMix = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_area_Locations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_area_Locations_areas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "areas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    ITCode = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Sex = table.Column<int>(nullable: true),
                    CellPhone = table.Column<string>(nullable: true),
                    HomePhone = table.Column<string>(maxLength: 30, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    PhotoId = table.Column<Guid>(nullable: true),
                    IsValid = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DeptID = table.Column<Guid>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FrameworkUsers_depts_DeptID",
                        column: x => x.DeptID,
                        principalTable: "depts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FrameworkUsers_FileAttachments_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "FileAttachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contract_Pops",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    PopID = table.Column<Guid>(nullable: false),
                    UnitPack = table.Column<string>(maxLength: 50, nullable: false),
                    Cnt = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ContractID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_Pops", x => x.ID);
                    table.ForeignKey(
                        name: "FK_contract_Pops_contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contract_Pops_pops_PopID",
                        column: x => x.PopID,
                        principalTable: "pops",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunctionPrivileges",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    RoleId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    MenuItemId = table.Column<Guid>(nullable: false),
                    Allowed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionPrivileges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FunctionPrivileges_FrameworkMenus_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "FrameworkMenus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LocationID = table.Column<Guid>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    UserCode = table.Column<string>(maxLength: 50, nullable: true),
                    PutTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_inventories_area_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "area_Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataPrivileges",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    GroupId = table.Column<Guid>(nullable: true),
                    TableName = table.Column<string>(maxLength: 50, nullable: false),
                    RelateId = table.Column<string>(nullable: true),
                    DomainId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPrivileges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DataPrivileges_FrameworkDomains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "FrameworkDomains",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataPrivileges_FrameworkGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "FrameworkGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataPrivileges_FrameworkUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "FrameworkUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkUserGroup",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkUserGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FrameworkUserGroup_FrameworkGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "FrameworkGroups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FrameworkUserGroup_FrameworkUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "FrameworkUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkUserRole",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkUserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FrameworkUserRole_FrameworkRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "FrameworkRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FrameworkUserRole_FrameworkUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "FrameworkUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchConditions",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Condition = table.Column<string>(nullable: true),
                    VMName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchConditions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SearchConditions_FrameworkUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "FrameworkUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ship_Pops",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UserID = table.Column<Guid>(nullable: false),
                    PopID = table.Column<Guid>(nullable: false),
                    OrderQty = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ShipUser = table.Column<string>(maxLength: 50, nullable: true),
                    ShipTime = table.Column<DateTime>(nullable: false),
                    Ship_Pop_SumID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ship_Pops", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ship_Pops_pops_PopID",
                        column: x => x.PopID,
                        principalTable: "pops",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ship_Pops_ship_Pop_Sums_Ship_Pop_SumID",
                        column: x => x.Ship_Pop_SumID,
                        principalTable: "ship_Pop_Sums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ship_Pops_FrameworkUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "FrameworkUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "order_Pops",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPopID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    OrderQty = table.Column<int>(nullable: false),
                    RecQty = table.Column<int>(nullable: false),
                    RecUser = table.Column<string>(maxLength: 50, nullable: true),
                    RecTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_Pops", x => x.ID);
                    table.ForeignKey(
                        name: "FK_order_Pops_contract_Pops_ContractPopID",
                        column: x => x.ContractPopID,
                        principalTable: "contract_Pops",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventoryouts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    InvID = table.Column<Guid>(nullable: false),
                    spsumID = table.Column<Guid>(nullable: false),
                    OutQty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryouts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_inventoryouts_inventories_InvID",
                        column: x => x.InvID,
                        principalTable: "inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventoryouts_ship_Pop_Sums_spsumID",
                        column: x => x.spsumID,
                        principalTable: "ship_Pop_Sums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventoryIn",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    OrderPopID = table.Column<int>(nullable: false),
                    InvID = table.Column<Guid>(nullable: false),
                    InQty = table.Column<int>(nullable: false),
                    contract_popID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryIn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_inventoryIn_inventories_InvID",
                        column: x => x.InvID,
                        principalTable: "inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventoryIn_order_Pops_OrderPopID",
                        column: x => x.OrderPopID,
                        principalTable: "order_Pops",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inventoryIn_contract_Pops_contract_popID",
                        column: x => x.contract_popID,
                        principalTable: "contract_Pops",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_area_Locations_AreaID",
                table: "area_Locations",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_areas_DCID",
                table: "areas",
                column: "DCID");

            migrationBuilder.CreateIndex(
                name: "IX_contract_Pops_ContractID",
                table: "contract_Pops",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_contract_Pops_PopID",
                table: "contract_Pops",
                column: "PopID");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_ContractFileID",
                table: "contracts",
                column: "ContractFileID");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_DCID",
                table: "contracts",
                column: "DCID");

            migrationBuilder.CreateIndex(
                name: "IX_DataPrivileges_DomainId",
                table: "DataPrivileges",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPrivileges_GroupId",
                table: "DataPrivileges",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPrivileges_UserId",
                table: "DataPrivileges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_depts_DCID",
                table: "depts",
                column: "DCID");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkMenus_DomainId",
                table: "FrameworkMenus",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkMenus_ParentId",
                table: "FrameworkMenus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUserGroup_GroupId",
                table: "FrameworkUserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUserGroup_UserId",
                table: "FrameworkUserGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUserRole_RoleId",
                table: "FrameworkUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUserRole_UserId",
                table: "FrameworkUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUsers_DeptID",
                table: "FrameworkUsers",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUsers_ITCode",
                table: "FrameworkUsers",
                column: "ITCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUsers_PhotoId",
                table: "FrameworkUsers",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionPrivileges_MenuItemId",
                table: "FunctionPrivileges",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_LocationID",
                table: "inventories",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryIn_InvID",
                table: "inventoryIn",
                column: "InvID");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryIn_OrderPopID",
                table: "inventoryIn",
                column: "OrderPopID");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryIn_contract_popID",
                table: "inventoryIn",
                column: "contract_popID");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryouts_InvID",
                table: "inventoryouts",
                column: "InvID");

            migrationBuilder.CreateIndex(
                name: "IX_inventoryouts_spsumID",
                table: "inventoryouts",
                column: "spsumID");

            migrationBuilder.CreateIndex(
                name: "IX_order_Pops_ContractPopID",
                table: "order_Pops",
                column: "ContractPopID");

            migrationBuilder.CreateIndex(
                name: "IX_pops_DCID",
                table: "pops",
                column: "DCID");

            migrationBuilder.CreateIndex(
                name: "IX_SearchConditions_UserId",
                table: "SearchConditions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ship_Pops_PopID",
                table: "ship_Pops",
                column: "PopID");

            migrationBuilder.CreateIndex(
                name: "IX_ship_Pops_Ship_Pop_SumID",
                table: "ship_Pops",
                column: "Ship_Pop_SumID");

            migrationBuilder.CreateIndex(
                name: "IX_ship_Pops_UserID",
                table: "ship_Pops",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "DataPrivileges");

            migrationBuilder.DropTable(
                name: "FrameworkUserGroup");

            migrationBuilder.DropTable(
                name: "FrameworkUserRole");

            migrationBuilder.DropTable(
                name: "FunctionPrivileges");

            migrationBuilder.DropTable(
                name: "inventoryIn");

            migrationBuilder.DropTable(
                name: "inventoryouts");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "SearchConditions");

            migrationBuilder.DropTable(
                name: "ship_Pops");

            migrationBuilder.DropTable(
                name: "FrameworkGroups");

            migrationBuilder.DropTable(
                name: "FrameworkRoles");

            migrationBuilder.DropTable(
                name: "FrameworkMenus");

            migrationBuilder.DropTable(
                name: "order_Pops");

            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "ship_Pop_Sums");

            migrationBuilder.DropTable(
                name: "FrameworkUsers");

            migrationBuilder.DropTable(
                name: "FrameworkDomains");

            migrationBuilder.DropTable(
                name: "contract_Pops");

            migrationBuilder.DropTable(
                name: "area_Locations");

            migrationBuilder.DropTable(
                name: "depts");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "pops");

            migrationBuilder.DropTable(
                name: "areas");

            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "dcs");
        }
    }
}
