using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Naheulbook.Data.Models;

namespace Naheulbook.Data.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("jobs");

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.PlayerDescription)
                .IsRequired()
                .HasColumnName("playerDescription");

            builder.Property(e => e.PlayerSummary)
                .IsRequired()
                .HasColumnName("playerSummary");

            builder.Property(e => e.Information)
                .HasColumnName("informations");

            builder.Property(e => e.IsMagic)
                .HasColumnName("ismagic")
                .HasDefaultValueSql("false");

            builder.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255);

            builder.Property(e => e.Flags)
                .HasColumnName("flags")
                .HasColumnType("json");

            builder.Property(e => e.Data)
                .HasColumnName("data")
                .HasColumnType("json");
        }
    }

    public class JobBonusConfiguration : IEntityTypeConfiguration<JobBonus>
    {
        public void Configure(EntityTypeBuilder<JobBonus> builder)
        {
            builder.ToTable("job_bonuses");

            builder.HasIndex(e => e.JobId)
                .HasDatabaseName("IX_job_bonuses_jobId");

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description");

            builder.Property(e => e.JobId)
                .HasColumnName("jobId");

            builder.Property(e => e.Flags)
                .HasColumnName("flags")
                .HasColumnType("json");

            builder.HasOne(e => e.Job)
                .WithMany(e => e.Bonuses)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_job_bonuses_jobId_jobs_id");
        }
    }

    public class JobRequirementConfiguration : IEntityTypeConfiguration<JobRequirement>
    {
        public void Configure(EntityTypeBuilder<JobRequirement> builder)
        {
            builder.ToTable("job_requirements");

            builder.HasIndex(e => e.JobId)
                .HasDatabaseName("IX_job_requirements_jobId");

            builder.HasIndex(e => e.StatName)
                .HasDatabaseName("IX_job_requirement_stat");

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.JobId)
                .HasColumnName("jobId");

            builder.Property(e => e.MaxValue)
                .HasColumnName("maxvalue");

            builder.Property(e => e.MinValue)
                .HasColumnName("minvalue");

            builder.Property(e => e.StatName)
                .IsRequired()
                .HasColumnName("stat")
                .HasMaxLength(64);

            builder.HasOne(e => e.Job)
                .WithMany(e => e.Requirements)
                .HasForeignKey(e => e.JobId)
                .HasConstraintName("FK_job_requirements_jobId_jobs_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Stat)
                .WithMany(e => e.JobRequirements)
                .HasForeignKey(e => e.StatName)
                .HasConstraintName("FK_job_requirement_stat_stat")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class JobRestrictionConfiguration : IEntityTypeConfiguration<JobRestriction>
    {
        public void Configure(EntityTypeBuilder<JobRestriction> builder)
        {
            builder.ToTable("job_restrictions");

            builder.HasIndex(e => e.JobId)
                .HasDatabaseName("IX_job_restrictions_jobId");

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.JobId)
                .HasColumnName("jobId");

            builder.Property(e => e.Text)
                .IsRequired()
                .HasColumnName("text");

            builder.Property(e => e.Flags)
                .HasColumnName("flags")
                .HasColumnType("json");

            builder.HasOne(e => e.Job)
                .WithMany(e => e.Restrictions)
                .HasForeignKey(e => e.JobId)
                .HasConstraintName("FK_job_restrictions_jobId_jobs_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class JobSkillConfiguration : IEntityTypeConfiguration<JobSkill>
    {
        public void Configure(EntityTypeBuilder<JobSkill> builder)
        {
            builder.HasKey(e => new {e.JobId, e.SkillId});

            builder.ToTable("job_skills");

            builder.HasIndex(e => e.SkillId)
                .HasDatabaseName("IX_job_skills_skillId");

            builder.HasIndex(e => e.JobId)
                .HasDatabaseName("IX_job_skills_jobId");

            builder.Property(e => e.JobId)
                .HasColumnName("jobId");

            builder.Property(e => e.SkillId)
                .HasColumnName("skillId");

            builder.Property(e => e.Default)
                .HasColumnName("default");

            builder.HasOne(e => e.Job)
                .WithMany(j => j.Skills)
                .HasForeignKey(e => e.JobId)
                .HasConstraintName("FK_job_skills_jobId_jobs_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Skill)
                .WithMany(s => s.JobSkills)
                .HasForeignKey(e => e.SkillId)
                .HasConstraintName("FK_job_skills_skillId_skills_id")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}