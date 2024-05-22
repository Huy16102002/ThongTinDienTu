using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTLG06WNC.Migrations
{
    /// <inheritdoc />
    public partial class addResopneColtblFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    iCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTitle = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__342A082C87DBF986", x => x.iCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "FileCategory",
                columns: table => new
                {
                    iFileCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTitle = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FileCate__7B6F2EC200F8AA71", x => x.iFileCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    iRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sRolename = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__D69F8CBE3AA3D277", x => x.iRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    iContentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sTitle = table.Column<string>(type: "ntext", nullable: true),
                    dCreatedate = table.Column<DateTime>(type: "datetime", nullable: true),
                    sMainbody = table.Column<string>(type: "ntext", nullable: true),
                    sSource = table.Column<string>(type: "ntext", nullable: true),
                    iCategoryID = table.Column<int>(type: "int", nullable: true),
                    sImage = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Content__7FB67E4EBC14F5C3", x => x.iContentID);
                    table.ForeignKey(
                        name: "FK_CategoryContent",
                        column: x => x.iCategoryID,
                        principalTable: "Category",
                        principalColumn: "iCategoryID");
                });

            migrationBuilder.CreateTable(
                name: "ResourceFile",
                columns: table => new
                {
                    iResourceFileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sFilename = table.Column<string>(type: "ntext", nullable: true),
                    dUploaddate = table.Column<DateTime>(type: "datetime", nullable: true),
                    sDescription = table.Column<string>(type: "ntext", nullable: false),
                    iCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resource__9F0924CD60D1C77E", x => x.iResourceFileID);
                    table.ForeignKey(
                        name: "FK_FileCategoryResourceFile",
                        column: x => x.iCategoryID,
                        principalTable: "FileCategory",
                        principalColumn: "iFileCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    iAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sName = table.Column<string>(type: "ntext", nullable: true),
                    sEmail = table.Column<string>(type: "ntext", nullable: true),
                    sPassword = table.Column<string>(type: "ntext", nullable: true),
                    sPhone = table.Column<string>(type: "ntext", nullable: true),
                    sAvatar = table.Column<string>(type: "ntext", nullable: true),
                    dBirthofdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    iRoleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__B681F016745C5B38", x => x.iAccountID);
                    table.ForeignKey(
                        name: "FK_RolesAccount",
                        column: x => x.iRoleID,
                        principalTable: "Roles",
                        principalColumn: "iRoleID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    iFeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sContent = table.Column<string>(type: "ntext", nullable: true),
                    iAccountID = table.Column<int>(type: "int", nullable: true),
                    iFeedbackdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    sResponse = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__4A6AA8F7BD98F518", x => x.iFeedbackID);
                    table.ForeignKey(
                        name: "FK_FeedbackAccount",
                        column: x => x.iAccountID,
                        principalTable: "Account",
                        principalColumn: "iAccountID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_iRoleID",
                table: "Account",
                column: "iRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Content_iCategoryID",
                table: "Content",
                column: "iCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_iAccountID",
                table: "Feedback",
                column: "iAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFile_iCategoryID",
                table: "ResourceFile",
                column: "iCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "ResourceFile");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "FileCategory");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
