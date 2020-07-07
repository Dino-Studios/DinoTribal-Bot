﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TheGodfather.Database;

namespace TheGodfather.Migrations
{
    [DbContext(typeof(TheGodfatherDbContext))]
    partial class TheGodfatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("gf")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TheGodfather.Database.Models.BlockedChannel", b =>
                {
                    b.Property<long>("ChannelIdDb")
                        .HasColumnName("cid")
                        .HasColumnType("bigint");

                    b.Property<string>("Reason")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("reason")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64)
                        .HasDefaultValue(null);

                    b.HasKey("ChannelIdDb");

                    b.ToTable("blocked_channels");
                });

            modelBuilder.Entity("TheGodfather.Database.Models.BlockedUser", b =>
                {
                    b.Property<long>("UserIdDb")
                        .HasColumnName("uid")
                        .HasColumnType("bigint");

                    b.Property<string>("Reason")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("reason")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64)
                        .HasDefaultValue(null);

                    b.HasKey("UserIdDb");

                    b.ToTable("blocked_users");
                });

            modelBuilder.Entity("TheGodfather.Database.Models.BotStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Activity")
                        .HasColumnName("activity_type")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("status")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("bot_statuses");
                });

            modelBuilder.Entity("TheGodfather.Database.Models.GuildConfig", b =>
                {
                    b.Property<long>("GuildIdDb")
                        .HasColumnName("gid")
                        .HasColumnType("bigint");

                    b.Property<short>("AntiInstantLeaveCooldown")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antiinstantleave_cooldown")
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)3);

                    b.Property<bool>("AntiInstantLeaveEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antiinstantleave_enabled")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<byte>("AntifloodAction")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antiflood_action")
                        .HasColumnType("smallint")
                        .HasDefaultValue((byte)4);

                    b.Property<short>("AntifloodCooldown")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antiflood_cooldown")
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)10);

                    b.Property<bool>("AntifloodEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antiflood_enabled")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<short>("AntifloodSensitivity")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antiflood_sensitivity")
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)5);

                    b.Property<byte>("AntispamAction")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antispam_action")
                        .HasColumnType("smallint")
                        .HasDefaultValue((byte)0);

                    b.Property<bool>("AntispamEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antispam_enabled")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<short>("AntispamSensitivity")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("antispam_sensitivity")
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)5);

                    b.Property<string>("Currency")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("currency")
                        .HasColumnType("character varying(32)")
                        .HasMaxLength(32)
                        .HasDefaultValue(null);

                    b.Property<long?>("LeaveChannelIdDb")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("leave_cid")
                        .HasColumnType("bigint")
                        .HasDefaultValue(null);

                    b.Property<string>("LeaveMessage")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("leave_msg")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue(null);

                    b.Property<bool>("LinkfilterBootersEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("linkfilter_booters")
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("LinkfilterDiscordInvitesEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("linkfilter_invites")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("LinkfilterDisturbingWebsitesEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("linkfilter_disturbing")
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("LinkfilterEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("linkfilter_enabled")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("LinkfilterIpLoggersEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("linkfilter_loggers")
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("LinkfilterUrlShortenersEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("linkfilter_shorteners")
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Locale")
                        .HasColumnName("locale")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.Property<long?>("LogChannelIdDb")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("log_cid")
                        .HasColumnType("bigint")
                        .HasDefaultValue(null);

                    b.Property<long?>("MuteRoleIdDb")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("mute_rid")
                        .HasColumnType("bigint")
                        .HasDefaultValue(null);

                    b.Property<string>("Prefix")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("prefix")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8)
                        .HasDefaultValue(null);

                    b.Property<byte>("RatelimitAction")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ratelimit_action")
                        .HasColumnType("smallint")
                        .HasDefaultValue((byte)1);

                    b.Property<bool>("RatelimitEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ratelimit_enabled")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<short>("RatelimitSensitivity")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ratelimit_sensitivity")
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)5);

                    b.Property<bool>("ReactionResponse")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("silent_response_enabled")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("SuggestionsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("suggestions_enabled")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<long?>("WelcomeChannelIdDb")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("welcome_cid")
                        .HasColumnType("bigint")
                        .HasDefaultValue(null);

                    b.Property<string>("WelcomeMessage")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("welcome_msg")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128)
                        .HasDefaultValue(null);

                    b.HasKey("GuildIdDb");

                    b.ToTable("guild_cfg");
                });
#pragma warning restore 612, 618
        }
    }
}
