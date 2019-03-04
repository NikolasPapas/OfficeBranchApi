using Microsoft.EntityFrameworkCore;
using OfficeBranchApi.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using OfficeBranchApi.Models;

namespace OfficeBranchApi.Condext
{
    public class DbContextRepo : DbContext
    {

        public DbContextRepo(DbContextOptions<DbContextRepo> options) : base(options) {}
       

        
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentType> EquipmentType { get; set; }
        public virtual DbSet<PositionToEquipment> PositionToEquipment { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<OfficeBranch> OfficeBranch { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<EquipmentType>().ToTable("EquipmentType");
            modelBuilder.Entity<PositionToEquipment>().ToTable("PositionToEquipment");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<OfficeBranch>().ToTable("OfficeBranch");

            modelBuilder.Entity<PositionToEquipment>()
               .HasKey(p => new { p.PositionId, p.EquipmentId });


               
            

            //modelBuilder.Entity<Position>()
            //   .HasOne(p => p.Employee)
            //   .WithOne(c => c.Position)
            //   .OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<Position>()
            //    .HasOne(c => c.OfficeBranch)
            //    .WithMany(c => c.Position)
            //    .HasForeignKey(p => p.OfficeBranchId)
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Equipment>()
            //  .HasOne(p => p.EquipmentType)
            //  .WithMany()
            //  .HasForeignKey(p => p.EquipmentTypeId)
            //  .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
