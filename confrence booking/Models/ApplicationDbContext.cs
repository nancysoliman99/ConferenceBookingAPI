using Microsoft.EntityFrameworkCore;
using confrence_booking.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Submission> Submissions { get; set; }
    public DbSet<InvitationVideo> InvitationVideos { get; set; }
    public DbSet<MainTopic> MainTopics { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<GalleryItem> GalleryItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Main Topics (المواضيع الرئيسية)
        modelBuilder.Entity<MainTopic>().HasData(
            new MainTopic
            {
                Id = 1,
                TitleAr = "الذكاء الاصطناعي وتطبيقاته",
                TitleEn = "Artificial Intelligence Applications",
                DescriptionAr = "مناقشة أحدث تقنيات تعلم الآلة والمعالجة اللغوية.",
                DescriptionEn = "Discussion on latest ML and NLP advances."
            },
            new MainTopic
            {
                Id = 2,
                TitleAr = "أمن المعلومات والأمن السيبراني",
                TitleEn = "Cybersecurity & Information Security",
                DescriptionAr = "طرق حماية الأنظمة والتشفير الرقمي.",
                DescriptionEn = "Methods for system protection and cryptography."
            }
        );

        // 2. Speakers (المتحدثين)
        modelBuilder.Entity<Speaker>().HasData(
            new Speaker
            {
                Id = 1,
                NameAr = "د. أحمد علي",
                NameEn = "Dr. Ahmed Ali",
                TitleAr = "أستاذ علوم الحاسب",
                TitleEn = "Computer Science Professor",
                ImageUrl = "/uploads/speakers/speaker1.jpg"
            },
            new Speaker
            {
                Id = 2,
                NameAr = "د. سارة إبراهيم",
                NameEn = "Dr. Sarah Ibrahim",
                TitleAr = "خبيرة هندسة البرمجيات",
                TitleEn = "Software Engineering Expert",
                ImageUrl = "/uploads/speakers/speaker2.jpg"
            }
        );

        // 3. Workshops (ورش العمل)
        modelBuilder.Entity<Workshop>().HasData(
            new Workshop
            {
                Id = 1,
                TitleAr = "ورشة عمل: بناء Web API بواسطة .NET 8",
                TitleEn = "Workshop: Building Web APIs with .NET 8",
                Instructor = "Eng. Mohamed Hassan",
                Date = new DateTime(2026, 10, 15, 10, 0, 0)
            },
            new Workshop
            {
                Id = 2,
                TitleAr = "ورشة عمل: أمن البيانات والـ Cloud",
                TitleEn = "Workshop: Cloud Data Security",
                Instructor = "Dr. Mahmoud Khaled",
                Date = new DateTime(2026, 10, 16, 14, 0, 0)
            }
        );

        // 4. Invitation Videos (فيديوهات الدعوة)
        modelBuilder.Entity<InvitationVideo>().HasData(
            new InvitationVideo
            {
                Id = 1,
                TitleAr = "الفيديو الترويجي للمؤتمر",
                TitleEn = "Official Conference Promo Video",
                VideoUrl = "https://www.youtube.com/watch?v=example1"
            },
            new InvitationVideo
            {
                Id = 2,
                TitleAr = "دعوة رئيس المؤتمر للباحثين",
                TitleEn = "Conference Chair Invitation",
                VideoUrl = "https://www.youtube.com/watch?v=example2"
            }
        );

        // 5. Partners / Sponsors (الشركاء والرعاة)
        modelBuilder.Entity<Partner>().HasData(
            new Partner
            {
                Id = 1,
                Name = "جامعة المنوفية",
                LogoUrl = "/uploads/partners/menofia.png"
            },
            new Partner
            {
                Id = 2,
                Name = "معهد تكنولوجيا المعلومات (ITI)",
                LogoUrl = "/uploads/partners/iti.png"
            }
        );

        // 6. Gallery Items (معرض الصور)
        modelBuilder.Entity<GalleryItem>().HasData(
            new GalleryItem
            {
                Id = 1,
                TitleAr = "افتتاح المؤتمر السابق",
                TitleEn = "Previous Conference Opening",
                ImageUrl = "/uploads/gallery/opening.jpg"
            },
            new GalleryItem
            {
                Id = 2,
                TitleAr = "تكريم الباحثين الفائزين",
                TitleEn = "Honoring Winning Researchers",
                ImageUrl = "/uploads/gallery/awards.jpg"
            }
        );

        // 7. Submissions (أبحاث الباحثين) - التحديث الهيكلي الجديد
        modelBuilder.Entity<Submission>().HasData(
            new Submission
            {
                Id = 1,
                FirstName = "علي",
                LastName = "محمد",
                Phone = "01012345678",
                Email = "ali.mohamed@example.com",
                College = CollegeType.ComputersAndAI,
                Level = "Master Student",
                PaperTitle = "تطبيقات الذكاء الاصطناعي في الطب",
                AbstractText = "هذا الملخص يوضح كيفية استخدام الخوارزميات الذكية في التشخيص.",
                FullPaperFilePath = "/uploads/submissions/paper_1.pdf",
                SubmittedAt = new DateTime(2026, 9, 1, 12, 0, 0)
            },
            new Submission
            {
                Id = 2,
                FirstName = "سارة",
                LastName = "محمود",
                Phone = "01187654321",
                Email = "sara.mahmoud@example.com",
                College = CollegeType.Engineering,
                Level = "PhD Candidate",
                PaperTitle = "تطوير الأنظمة المدمجة للخدمات الطبية",
                AbstractText = "دراسة تحليلية لتطوير الأجهزة الطبية باستخدام الحوسبة السحابية.",
                FullPaperFilePath = "/uploads/submissions/paper_2.pdf",
                SubmittedAt = new DateTime(2026, 9, 2, 14, 30, 0)
            }
        );
    }
}