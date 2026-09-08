using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace confrence_booking.Migrations
{
    /// <inheritdoc />
    public partial class InitialCleanDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GalleryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvitationVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationVideos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTopics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    College = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaperTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AbstractText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullPaperFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GalleryItems",
                columns: new[] { "Id", "ImageUrl", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "/uploads/gallery/opening.jpg", "افتتاح المؤتمر السابق", "Previous Conference Opening" },
                    { 2, "/uploads/gallery/awards.jpg", "تكريم الباحثين الفائزين", "Honoring Winning Researchers" }
                });

            migrationBuilder.InsertData(
                table: "InvitationVideos",
                columns: new[] { "Id", "TitleAr", "TitleEn", "VideoUrl" },
                values: new object[,]
                {
                    { 1, "الفيديو الترويجي للمؤتمر", "Official Conference Promo Video", "https://www.youtube.com/watch?v=example1" },
                    { 2, "دعوة رئيس المؤتمر للباحثين", "Conference Chair Invitation", "https://www.youtube.com/watch?v=example2" }
                });

            migrationBuilder.InsertData(
                table: "MainTopics",
                columns: new[] { "Id", "DescriptionAr", "DescriptionEn", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "مناقشة أحدث تقنيات تعلم الآلة والمعالجة اللغوية.", "Discussion on latest ML and NLP advances.", "الذكاء الاصطناعي وتطبيقاته", "Artificial Intelligence Applications" },
                    { 2, "طرق حماية الأنظمة والتشفير الرقمي.", "Methods for system protection and cryptography.", "أمن المعلومات والأمن السيبراني", "Cybersecurity & Information Security" }
                });

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, "/uploads/partners/menofia.png", "جامعة المنوفية" },
                    { 2, "/uploads/partners/iti.png", "معهد تكنولوجيا المعلومات (ITI)" }
                });

            migrationBuilder.InsertData(
                table: "Speakers",
                columns: new[] { "Id", "ImageUrl", "NameAr", "NameEn", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "/uploads/speakers/speaker1.jpg", "د. أحمد علي", "Dr. Ahmed Ali", "أستاذ علوم الحاسب", "Computer Science Professor" },
                    { 2, "/uploads/speakers/speaker2.jpg", "د. سارة إبراهيم", "Dr. Sarah Ibrahim", "خبيرة هندسة البرمجيات", "Software Engineering Expert" }
                });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "Id", "AbstractText", "College", "Email", "FirstName", "FullPaperFilePath", "LastName", "Level", "PaperTitle", "Phone", "SubmittedAt" },
                values: new object[,]
                {
                    { 1, "هذا الملخص يوضح كيفية استخدام الخوارزميات الذكية في التشخيص.", 7, "ali.mohamed@example.com", "علي", "/uploads/submissions/paper_1.pdf", "محمد", "Master Student", "تطبيقات الذكاء الاصطناعي في الطب", "01012345678", new DateTime(2026, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "دراسة تحليلية لتطوير الأجهزة الطبية باستخدام الحوسبة السحابية.", 6, "sara.mahmoud@example.com", "سارة", "/uploads/submissions/paper_2.pdf", "محمود", "PhD Candidate", "تطوير الأنظمة المدمجة للخدمات الطبية", "01187654321", new DateTime(2026, 9, 2, 14, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Workshops",
                columns: new[] { "Id", "Date", "Instructor", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Eng. Mohamed Hassan", "ورشة عمل: بناء Web API بواسطة .NET 8", "Workshop: Building Web APIs with .NET 8" },
                    { 2, new DateTime(2026, 10, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), "Dr. Mahmoud Khaled", "ورشة عمل: أمن البيانات والـ Cloud", "Workshop: Cloud Data Security" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryItems");

            migrationBuilder.DropTable(
                name: "InvitationVideos");

            migrationBuilder.DropTable(
                name: "MainTopics");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Workshops");
        }
    }
}
