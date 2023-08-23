using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHorseApi.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    SkateExperience = table.Column<string>(type: "longtext", nullable: false),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Gender = table.Column<string>(type: "longtext", nullable: false),
                    MinimumAge = table.Column<int>(type: "int", nullable: false),
                    MaximumAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true),
                    Lat = table.Column<double>(type: "double", nullable: false),
                    Long = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    PushNotificationToken = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "longtext", nullable: false),
                    ShortDescription = table.Column<string>(type: "longtext", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ScheduleRefrence",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ScheduleId = table.Column<string>(type: "longtext", nullable: true),
                    SkateProfileId = table.Column<string>(type: "longtext", nullable: false),
                    EventOwner = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    YesVote = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EventId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleRefrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleRefrence_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false),
                    PracticeStyle = table.Column<string>(type: "longtext", nullable: true),
                    PracticeStyle2 = table.Column<string>(type: "longtext", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<string>(type: "varchar(255)", nullable: true),
                    OpeningHour = table.Column<int>(type: "int", nullable: true),
                    ClosingHour = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trails_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SkillRecommendations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    SkillId = table.Column<string>(type: "varchar(255)", nullable: false),
                    SkatePracticeStyle = table.Column<string>(type: "longtext", nullable: false),
                    SkateExperience = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillRecommendations_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SkateProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    SkateType = table.Column<string>(type: "longtext", nullable: false),
                    SkatePracticeStyle = table.Column<string>(type: "longtext", nullable: false),
                    SkateExperience = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkateProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkateProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CheckPoints",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CustomTrailId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LocationId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckPoints_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CheckPoints_Trails_CustomTrailId",
                        column: x => x.CustomTrailId,
                        principalTable: "Trails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssignedSkills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    SkateProfileId = table.Column<string>(type: "varchar(255)", nullable: false),
                    SkillId = table.Column<string>(type: "varchar(255)", nullable: false),
                    MasteringLevel = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedSkills_SkateProfiles_SkateProfileId",
                        column: x => x.SkateProfileId,
                        principalTable: "SkateProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventSkateProfile",
                columns: table => new
                {
                    EventsId = table.Column<string>(type: "varchar(255)", nullable: false),
                    SkateProfilesId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSkateProfile", x => new { x.EventsId, x.SkateProfilesId });
                    table.ForeignKey(
                        name: "FK_EventSkateProfile_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSkateProfile_SkateProfiles_SkateProfilesId",
                        column: x => x.SkateProfilesId,
                        principalTable: "SkateProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EventSkateProfile1",
                columns: table => new
                {
                    RecommendedEventsId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RecommendedSkateProfilesId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSkateProfile1", x => new { x.RecommendedEventsId, x.RecommendedSkateProfilesId });
                    table.ForeignKey(
                        name: "FK_EventSkateProfile1_Events_RecommendedEventsId",
                        column: x => x.RecommendedEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSkateProfile1_SkateProfiles_RecommendedSkateProfilesId",
                        column: x => x.RecommendedSkateProfilesId,
                        principalTable: "SkateProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    StartTime = table.Column<double>(type: "double", nullable: false),
                    EndTime = table.Column<double>(type: "double", nullable: false),
                    SkateProfileId = table.Column<string>(type: "varchar(255)", nullable: false),
                    MinimumAge = table.Column<int>(type: "int", nullable: true),
                    MaximumAge = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "longtext", nullable: false),
                    MaxNumberOfPeople = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_SkateProfiles_SkateProfileId",
                        column: x => x.SkateProfileId,
                        principalTable: "SkateProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Range = table.Column<double>(type: "double", nullable: false),
                    ScheduleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LocationId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zones_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    DayOfMonth = table.Column<int>(type: "int", nullable: false),
                    OutingId = table.Column<string>(type: "varchar(255)", nullable: true),
                    ScheduleId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Outing",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    EventId = table.Column<string>(type: "varchar(255)", nullable: false),
                    StartTime = table.Column<double>(type: "double", nullable: false),
                    EndTime = table.Column<double>(type: "double", nullable: false),
                    VotedDayId = table.Column<string>(type: "varchar(255)", nullable: true),
                    VotedStartTime = table.Column<double>(type: "double", nullable: false),
                    SkatePracticeStyle = table.Column<string>(type: "longtext", nullable: false),
                    TrailId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Booked = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outing_Days_VotedDayId",
                        column: x => x.VotedDayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Outing_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Outing_Trails_TrailId",
                        column: x => x.TrailId,
                        principalTable: "Trails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSkills_SkateProfileId",
                table: "AssignedSkills",
                column: "SkateProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedSkills_SkillId",
                table: "AssignedSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoints_CustomTrailId",
                table: "CheckPoints",
                column: "CustomTrailId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckPoints_LocationId",
                table: "CheckPoints",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_OutingId",
                table: "Days",
                column: "OutingId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_ScheduleId",
                table: "Days",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSkateProfile_SkateProfilesId",
                table: "EventSkateProfile",
                column: "SkateProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSkateProfile1_RecommendedSkateProfilesId",
                table: "EventSkateProfile1",
                column: "RecommendedSkateProfilesId");

            migrationBuilder.CreateIndex(
                name: "IX_Outing_EventId",
                table: "Outing",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Outing_TrailId",
                table: "Outing",
                column: "TrailId");

            migrationBuilder.CreateIndex(
                name: "IX_Outing_VotedDayId",
                table: "Outing",
                column: "VotedDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleRefrence_EventId",
                table: "ScheduleRefrence",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SkateProfileId",
                table: "Schedules",
                column: "SkateProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SkateProfiles_UserId",
                table: "SkateProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillRecommendations_SkillId",
                table: "SkillRecommendations",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Trails_LocationId",
                table: "Trails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_LocationId",
                table: "Zones",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_ScheduleId",
                table: "Zones",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Outing_OutingId",
                table: "Days",
                column: "OutingId",
                principalTable: "Outing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_SkateProfiles_SkateProfileId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Trails_Locations_LocationId",
                table: "Trails");

            migrationBuilder.DropForeignKey(
                name: "FK_Outing_Trails_TrailId",
                table: "Outing");

            migrationBuilder.DropForeignKey(
                name: "FK_Days_Outing_OutingId",
                table: "Days");

            migrationBuilder.DropTable(
                name: "AssignedSkills");

            migrationBuilder.DropTable(
                name: "CheckPoints");

            migrationBuilder.DropTable(
                name: "EventSkateProfile");

            migrationBuilder.DropTable(
                name: "EventSkateProfile1");

            migrationBuilder.DropTable(
                name: "ScheduleRefrence");

            migrationBuilder.DropTable(
                name: "SkillRecommendations");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SkateProfiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Trails");

            migrationBuilder.DropTable(
                name: "Outing");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
