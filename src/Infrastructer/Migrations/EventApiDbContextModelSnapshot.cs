﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(EventApiDbContext))]
    partial class EventApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EventDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                            EndDate = new DateTimeOffset(new DateTime(2025, 2, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            EventDescription = "Aramıza katılan yeni arkadaşlar, hoş geldiniz!",
                            EventName = "Tanışma Toplantısı",
                            Location = "Ofis",
                            OrganizerId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                            StartDate = new DateTimeOffset(new DateTime(2025, 2, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Timezone = "UTC"
                        },
                        new
                        {
                            Id = new Guid("5d7e2f23-49d2-4e7e-9517-3a14c67e36a9"),
                            EndDate = new DateTimeOffset(new DateTime(2025, 2, 8, 16, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            EventDescription = "Yeni projeler için planlama ve görev dağılımı yapılacaktır.",
                            EventName = "Proje Planlama Toplantısı",
                            Location = "Ofis",
                            OrganizerId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                            StartDate = new DateTimeOffset(new DateTime(2025, 2, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Timezone = "UTC"
                        });
                });

            modelBuilder.Entity("Core.Entities.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("OrganizerId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Invitations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2a5b59a3-d486-4b8b-b0e4-3fb27cf8b85b"),
                            EventId = new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                            IsAccepted = true,
                            Message = "Sen de davetlisin Hasan!",
                            OrganizerId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                            ReceiverId = new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db")
                        },
                        new
                        {
                            Id = new Guid("b95c33b8-0b68-4a5c-8255-8c4a48224862"),
                            EventId = new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                            IsAccepted = true,
                            Message = "Sen de davetlisin Eda!",
                            OrganizerId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                            ReceiverId = new Guid("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12")
                        },
                        new
                        {
                            Id = new Guid("f20d308d-fdd5-4b1b-b71d-b0a0c26c1280"),
                            EventId = new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                            IsAccepted = true,
                            Message = "Sen de davetlisin Gokhan!",
                            OrganizerId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                            ReceiverId = new Guid("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")
                        },
                        new
                        {
                            Id = new Guid("cfcf8770-728d-482c-90d8-fd40cba5551c"),
                            EventId = new Guid("5d7e2f23-49d2-4e7e-9517-3a14c67e36a9"),
                            IsAccepted = true,
                            Message = "Sen de davetlisin Gokhan!",
                            OrganizerId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                            ReceiverId = new Guid("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")
                        });
                });

            modelBuilder.Entity("Core.Entities.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2e44eccb-cf69-4af7-b581-b472a2d23d05"),
                            EventId = new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                            UserId = new Guid("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12")
                        },
                        new
                        {
                            Id = new Guid("e3e8de9d-acaa-4657-a84c-7ff76c5d1fae"),
                            EventId = new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                            UserId = new Guid("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")
                        },
                        new
                        {
                            Id = new Guid("1576c6bb-d7d2-4e65-b9cb-951381c2658a"),
                            EventId = new Guid("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                            UserId = new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db")
                        },
                        new
                        {
                            Id = new Guid("82f31305-2b32-4d6d-b53c-7b63ef1cd9f7"),
                            EventId = new Guid("5d7e2f23-49d2-4e7e-9517-3a14c67e36a9"),
                            UserId = new Guid("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5")
                        });
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                            Email = "secil@example.com",
                            FullName = "Seçil Seleci",
                            Password = "",
                            UserName = "secilSeleci"
                        },
                        new
                        {
                            Id = new Guid("d8a490c9-ef65-4c6b-9d0a-4d55f54307db"),
                            Email = "hasan@example.com",
                            FullName = "Hasan Yüksel",
                            Password = "",
                            UserName = "hasanYuksel"
                        },
                        new
                        {
                            Id = new Guid("b12f1fc3-a9e7-4b53-90a7-0b2e1e7d3a12"),
                            Email = "eda@example.com",
                            FullName = "Eda Mayalı",
                            Password = "",
                            UserName = "edaMayali"
                        },
                        new
                        {
                            Id = new Guid("9c24e2f7-52b1-4f78-8dce-3ae146b7f9d5"),
                            Email = "gokhan@example.com",
                            FullName = "Gökhan Bilir",
                            Password = "",
                            UserName = "gokhanBilir"
                        });
                });

            modelBuilder.Entity("Core.Entities.Event", b =>
                {
                    b.HasOne("Core.Entities.User", "Organizer")
                        .WithMany("OrganizedEvents")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("Core.Entities.Invitation", b =>
                {
                    b.HasOne("Core.Entities.Event", "Event")
                        .WithMany("Invitations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "Organizer")
                        .WithMany("SentInvitations")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "Receiver")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Organizer");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("Core.Entities.Participant", b =>
                {
                    b.HasOne("Core.Entities.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("ParticipatedEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.Event", b =>
                {
                    b.Navigation("Invitations");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("OrganizedEvents");

                    b.Navigation("ParticipatedEvents");

                    b.Navigation("ReceivedInvitations");

                    b.Navigation("SentInvitations");
                });
#pragma warning restore 612, 618
        }
    }
}
