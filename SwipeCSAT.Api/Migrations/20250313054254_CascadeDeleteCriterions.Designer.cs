﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SwipeCSAT.Api;

#nullable disable

namespace SwipeCSAT.Api.Migrations
{
    [DbContext(typeof(SwipeCsatDbContext))]
    [Migration("20250313054254_CascadeDeleteCriterions")]
    partial class CascadeDeleteCriterions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SwipeCSAT.Api.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.PrimitiveCollection<List<string>>("Criterions")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SwipeCSAT.Api.Entities.CriterionRatingEntity", b =>
                {
                    b.Property<string>("CriterionName")
                        .HasColumnType("text");

                    b.Property<Guid?>("ProductEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("CriterionName");

                    b.HasIndex("ProductEntityId");

                    b.HasIndex("ProductId");

                    b.ToTable("CriterionRatings");
                });

            modelBuilder.Entity("SwipeCSAT.Api.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SwipeCSAT.Api.Entities.CriterionRatingEntity", b =>
                {
                    b.HasOne("SwipeCSAT.Api.Entities.ProductEntity", null)
                        .WithMany("Criterions")
                        .HasForeignKey("ProductEntityId");

                    b.HasOne("SwipeCSAT.Api.Entities.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SwipeCSAT.Api.Entities.ProductEntity", b =>
                {
                    b.HasOne("SwipeCSAT.Api.Entities.CategoryEntity", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("SwipeCSAT.Api.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SwipeCSAT.Api.Entities.ProductEntity", b =>
                {
                    b.Navigation("Criterions");
                });
#pragma warning restore 612, 618
        }
    }
}
