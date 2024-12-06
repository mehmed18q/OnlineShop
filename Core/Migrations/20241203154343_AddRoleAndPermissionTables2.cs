using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleAndPermissionTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles",
                newSchema: "Base");

            migrationBuilder.RenameTable(
                name: "RolePermission",
                newName: "RolePermissions",
                newSchema: "Base");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles",
                newSchema: "Base");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permissions",
                newSchema: "Base");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                schema: "Base",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                schema: "Base",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_RoleId",
                schema: "Base",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "Base",
                table: "RolePermissions",
                newName: "IX_RolePermissions_PermissionId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Base",
                table: "Roles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Base",
                table: "Permissions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Flag",
                schema: "Base",
                table: "Permissions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "Base",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                schema: "Base",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "Base",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                schema: "Base",
                table: "Permissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                schema: "Base",
                table: "RolePermissions",
                column: "PermissionId",
                principalSchema: "Base",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "Base",
                table: "RolePermissions",
                column: "RoleId",
                principalSchema: "Base",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "Base",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "Base",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Base",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "Base",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                schema: "Base",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "Base",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "Base",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Base",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "Base",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "Base",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                schema: "Base",
                table: "RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                schema: "Base",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Base",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Base",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                schema: "Base",
                newName: "RolePermission");

            migrationBuilder.RenameTable(
                name: "Permissions",
                schema: "Base",
                newName: "Permission");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermission",
                newName: "IX_RolePermission_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermission",
                newName: "IX_RolePermission_PermissionId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Flag",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                table: "RolePermission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UserId",
                table: "UserRole",
                column: "UserId",
                principalSchema: "Base",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
