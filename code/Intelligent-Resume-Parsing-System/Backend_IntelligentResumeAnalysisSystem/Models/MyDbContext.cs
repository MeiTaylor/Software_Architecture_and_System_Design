using Microsoft.EntityFrameworkCore;

namespace Backend_IntelligentResumeAnalysisSystem.Models
{
    // 注意：这里应该继承自 Microsoft.EntityFrameworkCore.MyDbContext
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantProfile> ApplicantProfiles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<JobMatch> JobMatches { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        //public DbSet<Project> Projects { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置Applicant表
            modelBuilder.Entity<Applicant>(entity =>
            {
                // 配置主键
                entity.HasKey(e => e.ID);

                // 配置属性
                entity.Property(e => e.Name)
                    .IsRequired() // 设置名称为必填
                    .HasMaxLength(50); // 设置最大长度为50

                entity.HasMany(e => e.WorkExperiences)
                    .WithOne() // 同上
                    .HasForeignKey(we => we.ApplicantID);

                /*entity.HasMany(e => e.Projects)
                    .WithOne() // 同上
                    .HasForeignKey(p => p.ApplicantID);*/

            });

            modelBuilder.Entity<ApplicantProfile>(entity => 
            {
                entity.HasKey(e => e.ID);

                modelBuilder.Entity<ApplicantProfile>()
                .HasOne(ap => ap.Applicant)
                .WithOne(a => a.ApplicantProfile)
                .HasForeignKey<ApplicantProfile>(ap => ap.ApplicantID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // 配置ApplicantProfile与PersonalCharacteristics的关系
            modelBuilder.Entity<ApplicantProfile>()
                .HasOne(ap => ap.PersonalCharacteristics)
                .WithOne()
                .HasForeignKey<PersonalCharacteristics>(pc => pc.ApplicantProfileID);

            // 配置ApplicantProfile与SkillsAndExperiences的关系
            modelBuilder.Entity<ApplicantProfile>()
                .HasOne(ap => ap.SkillsAndExperiences)
                .WithOne()
                .HasForeignKey<SkillsAndExperiences>(se => se.ApplicantProfileID);

            // 配置ApplicantProfile与AchievementsAndHighlights的关系
            modelBuilder.Entity<ApplicantProfile>()
                .HasOne(ap => ap.AchievementsAndHighlights)
                .WithOne()
                .HasForeignKey<AchievementsAndHighlights>(ah => ah.ApplicantProfileID);

            modelBuilder.Entity<Company>(entity => 
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.ID);

                // 因为Education类中没有Applicant导航属性，所以HasOne部分的参数为空
                entity.HasOne<Applicant>() // 注意这里没有提供 lambda 表达式
                      .WithMany() // 同样，没有提供对应Applicant中的Educations集合的引用
                      .HasForeignKey(e => e.ApplicantID)
                      .OnDelete(DeleteBehavior.Cascade); // 设置级联删除
            });

            modelBuilder.Entity<JobMatch>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasOne<ApplicantProfile>()
                .WithMany()
                .HasForeignKey(e => e.ApplicantProfileID) .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<JobPosition>(entity =>
            {
                entity.HasKey(jp =>jp.ID);
                // 配置与Company的一对多关系
                entity.HasOne(jp => jp.Company) // JobPosition包含一个Company导航属性
                      .WithMany(c => c.JobPositions) // Company包含一个JobPositions集合导航属性
                      .HasForeignKey(jp => jp.CompanyID) // CompanyID是JobPosition上的外键属性
                      .OnDelete(DeleteBehavior.Cascade); // 如果删除Company，相关的JobPosition也会被删除
                                                         // 可选：配置属性的最大长度等
                entity.Property(jp => jp.Title)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(jp => jp.Description)
                      .HasMaxLength(1000);
            });

            modelBuilder.Entity<Project>(entity => 
            { 
                entity.HasKey(p => p.ID);
                entity.HasOne<Applicant>().WithMany()
                .HasForeignKey(p => p.ApplicantID).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Resume>(entity =>
            {
                entity.HasKey(r => r.ID);
                modelBuilder.Entity<Resume>()
                .HasOne(r => r.Applicant) // 设置一对一关系
                .WithOne(a => a.Resume)
                .HasForeignKey<Resume>(r => r.ApplicantID).OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Resume>()
                .HasOne(r => r.JobPosition) // 设置一对一关系
                .WithMany(jp => jp.Resumes)
                .HasForeignKey(r => r.JobPositionID).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);
                modelBuilder.Entity<User>().HasOne(u => u.Company)
                .WithMany(c => c.Users).HasForeignKey(u => u.CompanyID).OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(u => u.Account).IsUnique();
            });

            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasOne<Applicant>().WithMany()
                .HasForeignKey(w => w.ApplicantID).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
