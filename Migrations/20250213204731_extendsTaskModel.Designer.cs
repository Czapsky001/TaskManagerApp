﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagerApp.DatabaseConnector;

#nullable disable

namespace TaskManagerApp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250213204731_extendsTaskModel")]
    partial class extendsTaskModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManagerApp.Model.TaskModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentTaskId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("TaskStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentTaskId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskManagerApp.Model.TaskModel", b =>
                {
                    b.HasOne("TaskManagerApp.Model.TaskModel", "ParentTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("ParentTaskId");

                    b.Navigation("ParentTask");
                });

            modelBuilder.Entity("TaskManagerApp.Model.TaskModel", b =>
                {
                    b.Navigation("SubTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
