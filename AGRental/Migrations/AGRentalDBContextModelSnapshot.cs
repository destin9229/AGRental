﻿// <auto-generated />
using AGRental.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AGRental.Migrations
{
    [DbContext(typeof(AGRentalDBContext))]
    partial class AGRentalDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AGRental.Models.Properties", b =>
                {
                    b.Property<int>("Property_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address");

                    b.Property<string>("city");

                    b.Property<bool>("is_taken");

                    b.Property<int>("price");

                    b.Property<string>("property_name");

                    b.HasKey("Property_ID");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("AGRental.Models.User", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<int>("UserTypeID");

                    b.Property<string>("Username");

                    b.HasKey("User_ID");

                    b.ToTable("Users");

                    b.HasData(
                        new { User_ID = 1, Email = "", FirstName = "Destin", LastName = "Thomas", Password = "Qwer1234", UserTypeID = 1, Username = "admin" }
                    );
                });

            modelBuilder.Entity("AGRental.Models.UserProperties", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("PropertyID");

                    b.HasKey("UserID", "PropertyID");

                    b.ToTable("UserProperties");
                });

            modelBuilder.Entity("AGRental.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserTypeName");

                    b.HasKey("UserTypeID");

                    b.ToTable("UserType");

                    b.HasData(
                        new { UserTypeID = 1, UserTypeName = "admin" },
                        new { UserTypeID = 2, UserTypeName = "user" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
