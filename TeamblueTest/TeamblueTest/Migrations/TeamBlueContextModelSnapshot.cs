﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamblueTest.DatabaseModels;

namespace TeamblueTest.Migrations
{
    [DbContext(typeof(TeamBlueContext))]
    partial class TeamBlueContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamblueTest.DatabaseModels.UniqueWord", b =>
                {
                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Word");

                    b.ToTable("UniqueWords");
                });

            modelBuilder.Entity("TeamblueTest.DatabaseModels.WatchListWord", b =>
                {
                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Word");

                    b.ToTable("WatchListWords");
                });
#pragma warning restore 612, 618
        }
    }
}