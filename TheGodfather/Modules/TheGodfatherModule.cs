﻿#region USING_DIRECTIVES
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TheGodfather.Common;
using TheGodfather.Services.Database;
#endregion

namespace TheGodfather.Modules
{
    public abstract class TheGodfatherModule : BaseCommandModule
    {
        private static readonly HttpClientHandler _handler = new HttpClientHandler { AllowAutoRedirect = false };
        protected static readonly HttpClient _http = new HttpClient(_handler, true);

        protected SharedData Shared { get; private set; }
        protected DBService Database { get; }
        protected DiscordColor ModuleColor {
            get { return this.moduleColor ?? DiscordColor.Green; }
            set { this.moduleColor = value; }
        }

        private DiscordColor? moduleColor;


        protected TheGodfatherModule(SharedData shared = null, DBService db = null)
        {
            this.Shared = shared;
            this.Database = db;
            this.ModuleColor = DiscordColor.Green;
        }


        protected Task InformAsync(CommandContext ctx, string message = null, string emoji = null, bool important = true)
            => InformAsync(ctx, (emoji == null ? StaticDiscordEmoji.CheckMarkSuccess : DiscordEmoji.FromName(ctx.Client, emoji)), message, important);

        protected Task InformAsync(CommandContext ctx, DiscordEmoji emoji, string message = null, bool important = true)
        {
            if (!important && this.Shared.GetGuildConfig(ctx.Guild.Id).ReactionResponse) {
                return ctx.Message.CreateReactionAsync(StaticDiscordEmoji.CheckMarkSuccess);
            } else {
                return ctx.RespondAsync(embed: new DiscordEmbedBuilder {
                    Description = $"{(emoji ?? StaticDiscordEmoji.CheckMarkSuccess)} {message ?? "Done!"}",
                    Color = this.ModuleColor
                });
            }
        }

        protected Task InformFailureAsync(CommandContext ctx, string message)
        {
            return ctx.RespondAsync(embed: new DiscordEmbedBuilder {
                Description = $"{StaticDiscordEmoji.BoardPieceX} {message}",
                Color = DiscordColor.IndianRed
            });
        }

        protected async Task<bool> IsValidImageUriAsync(Uri uri)
        {
            try {
                HttpResponseMessage response = await _http.GetAsync(uri).ConfigureAwait(false);
                if (response.Content.Headers.ContentType.MediaType.StartsWith("image/"))
                    return true;
            } catch {

            }

            return false;
        }
    }
}
