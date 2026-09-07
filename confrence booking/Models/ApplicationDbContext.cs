using confrence_booking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
}

