using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using WorkshopExpert.Core.ApplicationServices;
using WorkshopExpert.Core.DomainServices;
using WorkshopExpert.Core.Entities;
using WorkshopExpert.Infrastructure.BLL.ApplicationServices;

namespace WorkshopExpert.Infrastructure.DAL
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _curUserService;
        public ApplicationDbContext() : base("WorkshopExpertDbConnection", throwIfV1Schema: false) { }

        //Unity will call this constructor by default
        public ApplicationDbContext(ICurrentUserService curService)
            : this()
        {
            _curUserService = curService;
        }

        public static ApplicationDbContext Create() { return new ApplicationDbContext(); }

        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<WorkshopType> WorkshopTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Analysis> Analyses { get; set; }
        public DbSet<Development> Development { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<SummaryChecklist> SummaryChecklists { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Used by AppHarbor to automatically update the database
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

        }

        //Override method to automagically update CreateDate, CreatedBy, ModifiedDate, ModifiedDate every save
        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<IAuditable>())
            {
                if (auditableEntity.State == EntityState.Added ||
                    auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.ModifiedDate = DateTime.Now;
                    auditableEntity.Entity.ModifiedBy = _curUserService.UserID;

                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreateDate = DateTime.Now;
                        auditableEntity.Entity.CreatedBy = _curUserService.UserID;
                    }
                    else
                    {
                        // we also want to make sure that code is not inadvertly
                        // modifying created date and created by columns 
                        auditableEntity.Property(p => p.CreateDate).IsModified = false;
                        auditableEntity.Property(p => p.CreatedBy).IsModified = false;
                    }
                }
            }

            return base.SaveChanges();

        }

    }
}