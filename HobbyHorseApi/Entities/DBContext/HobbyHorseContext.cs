using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace HobbyHorseApi.Entities.DBContext
{
    public class HobbyHorseContext : DbContext
    {
        public HobbyHorseContext(DbContextOptions<HobbyHorseContext> options) : base(options)
        {
        }
        public DbSet<AssignedSkill> AssignedSkills { get; set; }
        public DbSet<CheckPoint> CheckPoints { get; set; }
        public DbSet<CustomTrail> CustomTrails { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ParkTrail> ParkTrails { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<SkateProfile> SkateProfiles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillRecommendation> SkillRecommendations { get; set; }
        public DbSet<Trail> Trails { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Zone> Zones { get; set; }

        public DbSet<Day> Days { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignedSkill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(assignedSkill => assignedSkill.Skill).WithMany(skill => skill.AssignedSkills)
                .HasForeignKey(assignedSkill => assignedSkill.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(assignedSkill => assignedSkill.SkateProfile).WithMany(skateProfile => skateProfile.AssignedSkills)
                .HasForeignKey(assignedSkill => assignedSkill.SkateProfileId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<CheckPoint>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<CustomTrail>(entity =>
            {
                entity.HasBaseType<Trail>();
            });
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasMany(evnt => evnt.SkateProfiles).WithMany(skateProfile => skateProfile.Events);

                entity.HasMany(evnt => evnt.RecommendedSkateProfiles)
                .WithMany(recommendedSkateProfile => recommendedSkateProfile.RecommendedEvents);

                entity.HasMany(evnt => evnt.ScheduleRefrences);

            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasMany(location => location.Zones)
                .WithOne(zone => zone.Location).HasForeignKey(zone => zone.LocationId);
            });
            modelBuilder.Entity<Outing>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasMany(outing => outing.Days).WithOne()
                .OnDelete(DeleteBehavior.Cascade);

                //entity.HasOne(outing => outing.VotedDay);

            });
            modelBuilder.Entity<ParkTrail>(entity =>
            {
                entity.HasBaseType<Trail>();
            });
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(schedule => schedule.Zones).WithOne(zone => zone.Schedule)
                .HasForeignKey(zone => zone.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);

               // entity.HasMany(schedule => schedule.Days).WithMany(day => day.Schedules);
                entity.HasMany(schedule => schedule.Days).WithOne().OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<SkateProfile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(skateprofile => skateprofile.AssignedSkills).WithOne(assignedSkill => assignedSkill.SkateProfile)
                .HasForeignKey(assignedSkill => assignedSkill.SkateProfileId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(skateprofile => skateprofile.Events).WithMany(evnt => evnt.SkateProfiles);

                entity.HasMany(skateprofile => skateprofile.Schedules).WithOne(schedule => schedule.SkateProfile)
                .HasForeignKey(schedule => schedule.SkateProfileId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(skateprofile => skateprofile.RecommendedEvents)
                .WithMany(recommendedEvent => recommendedEvent.RecommendedSkateProfiles);


                //.UsingEntity<Dictionary<string, object>>(
                //    mapping => mapping.HasOne<SkateProfile>().WithMany().HasForeignKey("Id"),
                //    mapping => mapping.HasOne<Event>().WithMany().HasForeignKey("Id"),
                //    mapping =>
                //    {
                //        mapping.ToTable("SkateProfileEventRecommendationsMapping");
                //        mapping.HasKey("Id", "Id");
                //        mapping.Property<string>("EventId");
                //        mapping.Property<string>("SkateProfileId");
                //    });

            });
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<SkillRecommendation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(skillRecommendation => skillRecommendation.Skill).WithMany(skill => skill.SkillRecommendations)
                .HasForeignKey(skillRecommendation => skillRecommendation.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Trail>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(user => user.Id);
                entity.HasMany(user => user.SkateProfiles).WithOne(skateProfile => skateProfile.User)
                .HasForeignKey(skateProfile => skateProfile.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Zone>(entity =>
            {
                entity.HasKey(zone => zone.Id);

                entity.HasOne(zone => zone.Location).WithMany(location => location.Zones)
                .HasForeignKey(zone => zone.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(zone => zone.Schedule).WithMany(schedule => schedule.Zones)
                .HasForeignKey(zone => zone.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Day>(entity =>
            {
                entity.HasKey(e => e.Id);


               //entity.HasMany(day => day.Schedules).WithMany(schedule => schedule.Days);

            });
        }
    }
}
