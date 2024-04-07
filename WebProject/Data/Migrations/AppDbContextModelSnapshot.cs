﻿// <auto-generated />
using System;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Data.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"), 1L, 1);

                    b.Property<string>("AdminNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("JoiningDate")
                        .HasColumnType("datetime");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AdminID");

                    b.HasIndex("UserID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Data.Models.Answer", b =>
                {
                    b.Property<int>("AnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerID"), 1L, 1);

                    b.Property<string>("File")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<string>("Writing")
                        .HasColumnType("text");

                    b.HasKey("AnswerID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("StudentID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Data.Models.Competition", b =>
                {
                    b.Property<int>("CompetitionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompetitionID"), 1L, 1);

                    b.Property<string>("CompetitionName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("CompetitionID");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("Data.Models.Events", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ParticipantCount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("EventID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Data.Models.Exam", b =>
                {
                    b.Property<int>("ExamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamID"), 1L, 1);

                    b.Property<int>("CompetitionID")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("ExamID");

                    b.HasIndex("CompetitionID");

                    b.HasIndex("StudentID");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Data.Models.Faculty", b =>
                {
                    b.Property<int>("FacultyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacultyID"), 1L, 1);

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FacultyNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("JoiningDate")
                        .HasColumnType("datetime");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FacultyID");

                    b.HasIndex("UserID");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Data.Models.FAQ", b =>
                {
                    b.Property<int>("FAQID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FAQID"), 1L, 1);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("FAQID");

                    b.ToTable("FAQs");
                });

            modelBuilder.Entity("Data.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionID"), 1L, 1);

                    b.Property<int>("CompetitionID")
                        .HasColumnType("int");

                    b.Property<string>("File")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Writing")
                        .HasColumnType("text");

                    b.HasKey("QuestionID");

                    b.HasIndex("CompetitionID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Data.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegistrationID"), 1L, 1);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("RegistrationID");

                    b.HasIndex("UserID");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("Data.Models.Response", b =>
                {
                    b.Property<int>("ResponseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResponseID"), 1L, 1);

                    b.Property<string>("Answers")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime");

                    b.Property<int>("SurveyID")
                        .HasColumnType("int");

                    b.HasKey("ResponseID");

                    b.HasIndex("SurveyID");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("Data.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"), 1L, 1);

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("StudentID");

                    b.HasIndex("UserID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Data.Models.StudentCompetition", b =>
                {
                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("CompetitionID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime");

                    b.HasKey("StudentID", "CompetitionID");

                    b.HasIndex("CompetitionID");

                    b.ToTable("StudentCompetitions");
                });

            modelBuilder.Entity("Data.Models.Survey", b =>
                {
                    b.Property<int>("SurveyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SurveyID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("TargetRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("SurveyID");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Models.Admin", b =>
                {
                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Admins")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.Answer", b =>
                {
                    b.HasOne("Data.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Student", "Student")
                        .WithMany("Answers")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Data.Models.Exam", b =>
                {
                    b.HasOne("Data.Models.Competition", "Competition")
                        .WithMany("Exams")
                        .HasForeignKey("CompetitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Student", "Student")
                        .WithMany("Exams")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Data.Models.Faculty", b =>
                {
                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Faculties")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.Question", b =>
                {
                    b.HasOne("Data.Models.Competition", "Competition")
                        .WithMany("Questions")
                        .HasForeignKey("CompetitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");
                });

            modelBuilder.Entity("Data.Models.Registration", b =>
                {
                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Registrations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.Response", b =>
                {
                    b.HasOne("Data.Models.Survey", "Survey")
                        .WithMany("Responses")
                        .HasForeignKey("SurveyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Data.Models.Student", b =>
                {
                    b.HasOne("Data.Models.User", "User")
                        .WithMany("Students")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Models.StudentCompetition", b =>
                {
                    b.HasOne("Data.Models.Competition", "Competition")
                        .WithMany("StudentCompetitions")
                        .HasForeignKey("CompetitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Models.Student", "Student")
                        .WithMany("StudentCompetitions")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Data.Models.Competition", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Questions");

                    b.Navigation("StudentCompetitions");
                });

            modelBuilder.Entity("Data.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Data.Models.Student", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Exams");

                    b.Navigation("StudentCompetitions");
                });

            modelBuilder.Entity("Data.Models.Survey", b =>
                {
                    b.Navigation("Responses");
                });

            modelBuilder.Entity("Data.Models.User", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Faculties");

                    b.Navigation("Registrations");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
