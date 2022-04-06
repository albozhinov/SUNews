namespace SUNews.Data.Context
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SUNews.Data.Models;

    public class SUNewsDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CommentReview> CommentReviews { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public DbSet<AuthorUser> AuthorUsers { get; set; }

        public DbSet<Like> Likes { get; set; }

        public SUNewsDbContext()
        {

        }

        public SUNewsDbContext(DbContextOptions<SUNewsDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<User>()
                                  .HasMany(u => u.Comments)
                                  .WithOne(c => c.User)
                                  .HasForeignKey(c => c.UserId)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Author>()
                                    .HasMany(au => au.Articles)
                                    .WithOne(a => a.Author)
                                    .HasForeignKey(a => a.AuthorId)
                                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Article>()
                                     .HasMany(a => a.Comments)
                                     .WithOne(c => c.Article)
                                     .HasForeignKey(c => c.ArticleId)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CommentReview>()
                                            .HasKey(cr => new { cr.CommentId, cr.UserId });

            builder.Entity<User>()
                                  .HasMany(u => u.Ratings)
                                  .WithOne(r => r.User)
                                  .HasForeignKey(r => r.UserId)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                                     .HasMany(c => c.Ratings)
                                     .WithOne(r => r.Comment)
                                     .HasForeignKey(r => r.CommentId);

            builder.Entity<ArticleCategory>()
                                             .HasKey(ac => new { ac.CategoryId, ac.ArticleId });

            builder.Entity<Article>()
                                     .HasMany(a => a.Categories)
                                     .WithOne(c => c.Article)
                                     .HasForeignKey(c => c.ArticleId);

            builder.Entity<Category>()
                                      .HasMany(c => c.Articles)
                                      .WithOne(a => a.Category)
                                      .HasForeignKey(a => a.CategoryId);

            builder.Entity<AuthorUser>()
                                        .HasKey(au => new {au.AuthorId, au.UserId });

            base.OnModelCreating(builder);
        }
    }
}
