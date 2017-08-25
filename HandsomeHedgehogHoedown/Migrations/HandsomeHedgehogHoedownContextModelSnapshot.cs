using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HandsomeHedgehogHoedown.Models;

namespace HandsomeHedgehogHoedown.Migrations
{
    [DbContext(typeof(HandsomeHedgehogHoedownContext))]
    partial class HandsomeHedgehogHoedownContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.Computer", b =>
                {
                    b.Property<int>("ComputerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DecommissionedDate");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("PurchaseDate");

                    b.HasKey("ComputerId");

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateStart");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.EmployeeComputer", b =>
                {
                    b.Property<int>("EmployeeComputerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComputerId");

                    b.Property<int>("EmployeeId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("EmployeeComputerId");

                    b.HasIndex("ComputerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeComputer");
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.EmployeeTraining", b =>
                {
                    b.Property<int>("EmployeeTrainingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<int>("TrainingProgramId");

                    b.HasKey("EmployeeTrainingId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TrainingProgramId");

                    b.ToTable("EmployeeTraining");
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.TrainingProgram", b =>
                {
                    b.Property<int>("TrainingProgramId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("MaxCapacity");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.HasKey("TrainingProgramId");

                    b.ToTable("TrainingProgram");
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.Employee", b =>
                {
                    b.HasOne("HandsomeHedgehogHoedown.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.EmployeeComputer", b =>
                {
                    b.HasOne("HandsomeHedgehogHoedown.Models.Computer", "Computer")
                        .WithMany("EmployeeComputers")
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HandsomeHedgehogHoedown.Models.Employee", "Employee")
                        .WithMany("EmployeeComputers")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HandsomeHedgehogHoedown.Models.EmployeeTraining", b =>
                {
                    b.HasOne("HandsomeHedgehogHoedown.Models.Employee", "Employee")
                        .WithMany("EmployeeTrainings")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HandsomeHedgehogHoedown.Models.TrainingProgram", "TrainingProgram")
                        .WithMany("EmployeeTrainings")
                        .HasForeignKey("TrainingProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
