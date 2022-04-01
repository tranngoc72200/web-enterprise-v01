﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebEnterprise_mssql.Api.Data;

namespace WebEnterprise_mssql.Api.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20220326152023_add local sql server")]
    partial class addlocalsqlserver
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("DepartmentsDepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentsDepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Categories", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Comments", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("Postsid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommentId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("Postsid");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Departments", b =>
                {
                    b.Property<Guid>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.FilesPath", b =>
                {
                    b.Property<Guid>("FilesPathID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Postsid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("filePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FilesPathID");

                    b.HasIndex("Postsid");

                    b.ToTable("FilesPath");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Posts", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoriesCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CatogoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("LastModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("SubmissionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("createdDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("CategoriesCategoryId");

                    b.HasIndex("SubmissionsId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.RefreshToken", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("JwtId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Submissions", b =>
                {
                    b.Property<Guid>("SubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClosureDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionSubmission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinalClosureDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubmissionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubmissionId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Views", b =>
                {
                    b.Property<Guid>("ViewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("LastVistedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("Postsid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("postId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ViewId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("Postsid");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Votes", b =>
                {
                    b.Property<Guid>("voteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Postsid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("postId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userDownVote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userUpvote")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("voteId");

                    b.HasIndex("Postsid");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.ApplicationUser", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.Departments", "Departments")
                        .WithMany("ApplicationUser")
                        .HasForeignKey("DepartmentsDepartmentId");

                    b.Navigation("Departments");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Comments", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Comments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("WebEnterprise_mssql.Models.Posts", "Posts")
                        .WithMany("Comments")
                        .HasForeignKey("Postsid");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.FilesPath", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.Posts", "Posts")
                        .WithMany("filesPaths")
                        .HasForeignKey("Postsid");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Posts", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.Categories", "Categories")
                        .WithMany("Posts")
                        .HasForeignKey("CategoriesCategoryId");

                    b.HasOne("WebEnterprise_mssql.Models.Submissions", "Submissions")
                        .WithMany("Posts")
                        .HasForeignKey("SubmissionsId");

                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Categories");

                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.RefreshToken", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Views", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Views")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("WebEnterprise_mssql.Models.Posts", "Posts")
                        .WithMany("Views")
                        .HasForeignKey("Postsid");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Votes", b =>
                {
                    b.HasOne("WebEnterprise_mssql.Models.Posts", "Posts")
                        .WithMany()
                        .HasForeignKey("Postsid");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.ApplicationUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");

                    b.Navigation("Views");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Categories", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Departments", b =>
                {
                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Posts", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("filesPaths");

                    b.Navigation("Views");
                });

            modelBuilder.Entity("WebEnterprise_mssql.Models.Submissions", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
