﻿// <auto-generated />
using System;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorApp1.Migrations.ToDoData
{
    [DbContext(typeof(ToDoDataContext))]
    partial class ToDoDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("BlazorApp1.Models.Cpr", b =>
                {
                    b.Property<int>("Cprid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CprNr")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("User")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Cprid")
                        .HasName("PK__Cpr__C0242C1E46EA26CE");

                    b.ToTable("Cpr", (string)null);
                });

            modelBuilder.Entity("BlazorApp1.Models.TodoList", b =>
                {
                    b.Property<int>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("todoId");

                    b.Property<string>("Item")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT")
                        .HasColumnName("item");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TodoId")
                        .HasName("PK__TodoList__E5C578A103984B01");

                    b.HasIndex("UserId");

                    b.ToTable("TodoList", (string)null);
                });

            modelBuilder.Entity("BlazorApp1.Models.TodoList", b =>
                {
                    b.HasOne("BlazorApp1.Models.Cpr", "User")
                        .WithMany("TodoLists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__TodoList__UserId__267ABA7A");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlazorApp1.Models.Cpr", b =>
                {
                    b.Navigation("TodoLists");
                });
#pragma warning restore 612, 618
        }
    }
}
