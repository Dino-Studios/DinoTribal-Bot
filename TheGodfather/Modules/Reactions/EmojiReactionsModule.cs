﻿#region USING_DIRECTIVES
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using TheGodfather.Common.Attributes;
using TheGodfather.Exceptions;
using TheGodfather.Extensions;
using TheGodfather.Common.Collections;
using TheGodfather.Modules.Reactions.Common;
using TheGodfather.Services.Database.Reactions;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using TheGodfather.Services.Database;
#endregion

namespace TheGodfather.Modules.Reactions
{
    [Group("emojireaction"), Module(ModuleType.Reactions)]
    [Description("Orders a bot to react with given emoji to a message containing a trigger word inside (guild specific). If invoked without subcommands, adds a new emoji reaction to a given trigger word list. Note: Trigger words can be regular expressions (use ``emojireaction addregex`` command).")]
    [Aliases("ereact", "er", "emojir", "emojireactions")]
    [UsageExamples("!emojireaction :smile: haha laughing")]
    [Cooldown(3, 5, CooldownBucketType.Guild)]
    [NotBlocked]
    public class EmojiReactionsModule : TheGodfatherModule
    {

        public EmojiReactionsModule(SharedData shared, DBService db) : base(shared, db) { }


        [GroupCommand, Priority(2)]
        public Task ExecuteGroupAsync(CommandContext ctx)
            => ListAsync(ctx);

        [GroupCommand, Priority(1)]
        [RequirePermissions(Permissions.ManageGuild)]
        public Task ExecuteGroupAsync(CommandContext ctx,
                                     [Description("Emoji to send.")] DiscordEmoji emoji,
                                     [RemainingText, Description("Trigger word list.")] params string[] triggers)
            => AddEmojiReactionAsync(ctx, emoji, false, triggers);

        [GroupCommand, Priority(0)]
        [RequirePermissions(Permissions.ManageGuild)]
        public Task ExecuteGroupAsync(CommandContext ctx,
                                     [Description("Trigger word (case-insensitive).")] string trigger,
                                     [Description("Emoji to send.")] DiscordEmoji emoji)
            => AddEmojiReactionAsync(ctx, emoji, false, trigger);


        #region COMMAND_EMOJI_REACTIONS_ADD
        [Command("add"), Priority(1)]
        [Module(ModuleType.Reactions)]
        [Description("Add emoji reaction to guild reaction list.")]
        [Aliases("+", "new", "a")]
        [UsageExamples("!emojireaction add :smile: haha",
                       "!emojireaction add haha :smile:")]
        [RequireUserPermissions(Permissions.ManageGuild)]
        public Task AddAsync(CommandContext ctx,
                            [Description("Emoji to send.")] DiscordEmoji emoji,
                            [RemainingText, Description("Trigger word list (case-insensitive).")] params string[] triggers)
            => AddEmojiReactionAsync(ctx, emoji, false, triggers);

        [Command("add"), Priority(0)]
        public Task AddAsync(CommandContext ctx,
                            [Description("Trigger word (case-insensitive).")] string trigger,
                            [Description("Emoji to send.")] DiscordEmoji emoji)
            => AddEmojiReactionAsync(ctx, emoji, false, trigger);
        #endregion

        #region COMMAND_EMOJI_REACTIONS_ADDREGEX
        [Command("addregex"), Priority(1)]
        [Module(ModuleType.Reactions)]
        [Description("Add emoji reaction triggered by a regex to guild reaction list.")]
        [Aliases("+r", "+regex", "+regexp", "+rgx", "newregex", "addrgx")]
        [UsageExamples("!emojireaction addregex :smile: (ha)+",
                       "!emojireaction addregex (ha)+ :smile:")]
        [RequireUserPermissions(Permissions.ManageGuild)]
        public Task AddRegexAsync(CommandContext ctx,
                                 [Description("Emoji to send.")] DiscordEmoji emoji,
                                 [RemainingText, Description("Trigger word list (case-insensitive).")] params string[] triggers)
            => AddEmojiReactionAsync(ctx, emoji, true, triggers);

        [Command("addregex"), Priority(0)]
        public Task AddRegexAsync(CommandContext ctx,
                                 [Description("Trigger word (case-insensitive).")] string trigger,
                                 [Description("Emoji to send.")] DiscordEmoji emoji)
            => AddEmojiReactionAsync(ctx, emoji, true, trigger);
        #endregion

        #region COMMAND_EMOJI_REACTIONS_CLEAR
        [Command("clear"), Module(ModuleType.Reactions)]
        [Description("Delete all reactions for the current guild.")]
        [Aliases("da", "c", "ca", "cl", "clearall")]
        [UsageExamples("!emojireactions clear")]
        [RequireUserPermissions(Permissions.Administrator)]
        [UsesInteractivity]
        public async Task ClearAsync(CommandContext ctx)
        {
            if (!await ctx.WaitForBoolReplyAsync("Are you sure you want to delete all emoji reactions for this guild?").ConfigureAwait(false))
                return;

            if (this.Shared.EmojiReactions.ContainsKey(ctx.Guild.Id))
                this.Shared.EmojiReactions.TryRemove(ctx.Guild.Id, out _);

            try {
                await this.Database.RemoveAllGuildEmojiReactionsAsync(ctx.Guild.Id)
                    .ConfigureAwait(false);
            } catch (Exception e) {
                this.Shared.LogProvider.LogException(LogLevel.Warning, e);
                throw new CommandFailedException("Failed to delete emoji reactions from the database.");
            }

            var logchn = this.Shared.GetLogChannelForGuild(ctx.Client, ctx.Guild);
            if (logchn != null) {
                var emb = new DiscordEmbedBuilder() {
                    Title = "All emoji reactions have been deleted",
                    Color = DiscordColor.Blue
                };
                emb.AddField("User responsible", ctx.User.Mention, inline: true);
                emb.AddField("Invoked in", ctx.Channel.Mention, inline: true);
                await logchn.SendMessageAsync(embed: emb.Build())
                    .ConfigureAwait(false);
            }

            await InformAsync(ctx, "Removed all emoji reactions!")
                .ConfigureAwait(false);
        }
        #endregion

        #region COMMAND_EMOJI_REACTIONS_DELETE
        [Command("delete"), Priority(2)]
        [Module(ModuleType.Reactions)]
        [Description("Remove emoji reactions for given trigger words.")]
        [Aliases("-", "remove", "del", "rm", "d")]
        [UsageExamples("!emojireaction delete haha sometrigger",
                       "!emojireaction delete 5",
                       "!emojireaction delete 5 4",
                       "!emojireaction delete :joy:")]
        [RequireUserPermissions(Permissions.ManageGuild)]
        public async Task DeleteAsync(CommandContext ctx,
                                     [Description("Emoji to remove reactions for.")] DiscordEmoji emoji)
        {
            if (!this.Shared.EmojiReactions.ContainsKey(ctx.Guild.Id))
                throw new CommandFailedException("This guild has no emoji reactions registered.");

            var ename = emoji.GetDiscordName();
            if (this.Shared.EmojiReactions[ctx.Guild.Id].RemoveWhere(er => er.Response == ename) == 0)
                throw new CommandFailedException("No such reactions found!");

            var errors = new StringBuilder();
            try {
                await this.Database.RemoveAllTriggersForEmojiReactionAsync(ctx.Guild.Id, ename)
                    .ConfigureAwait(false);
            } catch (Exception e) {
                this.Shared.LogProvider.LogException(LogLevel.Warning, e);
                errors.AppendLine($"Warning: Failed to remove reaction from the database.");
            }

            string errlist = errors.ToString();
            var logchn = this.Shared.GetLogChannelForGuild(ctx.Client, ctx.Guild);
            if (logchn != null) {
                var emb = new DiscordEmbedBuilder() {
                    Title = "Several emoji reactions have been deleted",
                    Color = DiscordColor.Blue
                };
                emb.AddField("User responsible", ctx.User.Mention, inline: true);
                emb.AddField("Invoked in", ctx.Channel.Mention, inline: true);
                emb.AddField("Removed reaction for emoji", emoji, inline: true);
                if (!string.IsNullOrWhiteSpace(errlist))
                    emb.AddField("With errors", errlist);
                await logchn.SendMessageAsync(embed: emb.Build())
                    .ConfigureAwait(false);
            }

            await InformAsync(ctx, $"Done!\n\n{errlist}")
                .ConfigureAwait(false);
        }

        [Command("delete"), Priority(1)]
        public async Task DeleteAsync(CommandContext ctx,
                                     [Description("IDs of the reactions to remove.")] params int[] ids)
        {
            if (!this.Shared.EmojiReactions.ContainsKey(ctx.Guild.Id))
                throw new CommandFailedException("This guild has no emoji reactions registered.");

            var errors = new StringBuilder();
            foreach (var id in ids) {
                if (!this.Shared.EmojiReactions[ctx.Guild.Id].Any(tr => tr.Id == id)) {
                    errors.AppendLine($"Note: Reaction with ID {id} does not exist in this guild.");
                    continue;
                }
            }

            try {
                await this.Database.RemoveEmojiReactionsAsync(ctx.Guild.Id, ids)
                    .ConfigureAwait(false);
            } catch (Exception e) {
                this.Shared.LogProvider.LogException(LogLevel.Warning, e);
                errors.AppendLine($"Warning: Failed to remove some reactions from the database.");
            }

            int removed = this.Shared.EmojiReactions[ctx.Guild.Id].RemoveWhere(tr => ids.Contains(tr.Id));

            string errlist = errors.ToString();
            if (removed > 0) {
                var logchn = this.Shared.GetLogChannelForGuild(ctx.Client, ctx.Guild);
                if (logchn != null) {
                    var emb = new DiscordEmbedBuilder() {
                        Title = "Several emoji reactions have been deleted",
                        Color = DiscordColor.Blue
                    };
                    emb.AddField("User responsible", ctx.User.Mention, inline: true);
                    emb.AddField("Invoked in", ctx.Channel.Mention, inline: true);
                    emb.AddField("Removed successfully", $"{removed} reactions", inline: true);
                    emb.AddField("IDs attempted to be removed", string.Join(", ", ids));
                    if (!string.IsNullOrWhiteSpace(errlist))
                        emb.AddField("With errors", errlist);
                    await logchn.SendMessageAsync(embed: emb.Build())
                        .ConfigureAwait(false);
                }
            }

            await InformAsync(ctx, $"Successfully removed {removed} emoji reactions!\n\n{errlist}")
                .ConfigureAwait(false);
        }

        [Command("delete"), Priority(0)]
        public async Task DeleteAsync(CommandContext ctx,
                                     [RemainingText, Description("Trigger words to remove.")] params string[] triggers)
        {
            if (triggers == null)
                throw new InvalidCommandUsageException("Missing trigger words!");

            if (!this.Shared.EmojiReactions.ContainsKey(ctx.Guild.Id))
                throw new CommandFailedException("This guild has no emoji reactions registered.");

            var errors = new StringBuilder();
            foreach (var trigger in triggers.Select(t => t.ToLowerInvariant())) {
                if (!trigger.IsValidRegex()) {
                    errors.AppendLine($"Error: Trigger {Formatter.Bold(trigger)} is not a valid regular expression.");
                    continue;
                }

                var found = this.Shared.EmojiReactions[ctx.Guild.Id].Where(er => er.ContainsTriggerPattern(trigger));
                if (!found.Any()) {
                    errors.AppendLine($"Warning: Trigger {Formatter.Bold(trigger)} does not exist in this guild.");
                    continue;
                }

                bool success = true;
                foreach (var er in found)
                    success |= er.RemoveTrigger(trigger);
                if (!success) {
                    errors.AppendLine($"Warning: Failed to remove some emoji reactions for trigger {Formatter.Bold(trigger)}.");
                    continue;
                }
            }

            try {
                await this.Database.RemoveEmojiReactionTriggersAsync(ctx.Guild.Id, triggers)
                    .ConfigureAwait(false);
            } catch (Exception e) {
                this.Shared.LogProvider.LogException(LogLevel.Warning, e);
                errors.AppendLine($"Warning: Failed to remove some triggers from the database.");
            }

            int removed = this.Shared.EmojiReactions[ctx.Guild.Id].RemoveWhere(er => er.TriggerRegexes.Count == 0);

            string errlist = errors.ToString();
            if (removed > 0) {
                var logchn = this.Shared.GetLogChannelForGuild(ctx.Client, ctx.Guild);
                if (logchn != null) {
                    var emb = new DiscordEmbedBuilder() {
                        Title = "Several emoji reactions have been deleted",
                        Color = DiscordColor.Blue
                    };
                    emb.AddField("User responsible", ctx.User.Mention, inline: true);
                    emb.AddField("Invoked in", ctx.Channel.Mention, inline: true);
                    emb.AddField("Removed successfully", $"{removed} reactions", inline: true);
                    emb.AddField("Triggers attempted to be removed", string.Join("\n", triggers));
                    if (!string.IsNullOrWhiteSpace(errlist))
                        emb.AddField("With errors", errlist);
                    await logchn.SendMessageAsync(embed: emb.Build())
                        .ConfigureAwait(false);
                }
            }

            await InformAsync(ctx, $"Successfully removed {removed} emoji reactions!\n\n{errlist}")
                .ConfigureAwait(false);
        }
        #endregion

        #region COMMAND_EMOJI_REACTIONS_LIST
        [Command("list"), Module(ModuleType.Reactions)]
        [Description("Show all emoji reactions for this guild.")]
        [Aliases("ls", "l", "view")]
        [UsageExamples("!emojireaction list")]
        public async Task ListAsync(CommandContext ctx)
        {
            if (!this.Shared.EmojiReactions.ContainsKey(ctx.Guild.Id) || !this.Shared.EmojiReactions[ctx.Guild.Id].Any())
                throw new CommandFailedException("No emoji reactions registered for this guild.");

            // TODO remove non-existing

            await ctx.SendCollectionInPagesAsync(
                "Emoji reactions for this guild",
                this.Shared.EmojiReactions[ctx.Guild.Id].OrderBy(er => er.OrderedTriggerStrings.First()),
                er => $"{er.Id} : {DiscordEmoji.FromName(ctx.Client, er.Response)} | Triggers: {string.Join(", ", er.TriggerStrings)}",
                DiscordColor.Blue
            ).ConfigureAwait(false);
        }
        #endregion


        #region HELPER_FUNCTIONS
        public async Task AddEmojiReactionAsync(CommandContext ctx, DiscordEmoji emoji, bool is_regex, params string[] triggers)
        {
            if (triggers == null)
                throw new InvalidCommandUsageException("Missing trigger words!");

            var errors = new StringBuilder();
            foreach (var trigger in triggers.Select(t => t.ToLowerInvariant())) {
                if (trigger.Length > 120) {
                    errors.AppendLine($"Error: Trigger {Formatter.Bold(trigger)} is too long (120 chars max).");
                    continue;
                }

                if (!this.Shared.EmojiReactions.ContainsKey(ctx.Guild.Id))
                    this.Shared.EmojiReactions.TryAdd(ctx.Guild.Id, new ConcurrentHashSet<EmojiReaction>());

                if (!trigger.IsValidRegex()) {
                    errors.AppendLine($"Error: Trigger {Formatter.Bold(trigger)} is not a valid regular expression.");
                    continue;
                }

                var ename = emoji.GetDiscordName();

                if (this.Shared.EmojiReactions[ctx.Guild.Id].Where(er => er.ContainsTriggerPattern(trigger)).Any(er => er.Response == ename)) {
                    errors.AppendLine($"Error: Trigger {Formatter.Bold(trigger)} already exists for this emoji.");
                    continue;
                }

                int id = 0;
                try {
                    id = await this.Database.AddEmojiReactionAsync(ctx.Guild.Id, trigger, ename, regex: is_regex)
                        .ConfigureAwait(false);
                } catch (Exception e) {
                    this.Shared.LogProvider.LogException(LogLevel.Warning, e);
                    errors.AppendLine($"Warning: Failed to add trigger {Formatter.Bold(trigger)} to the database.");
                }

                var reaction = this.Shared.EmojiReactions[ctx.Guild.Id].FirstOrDefault(tr => tr.Response == ename);
                if (reaction != null) {
                    if (!reaction.AddTrigger(trigger, regex: is_regex))
                        throw new CommandFailedException($"Failed to add trigger {Formatter.Bold(trigger)}.");
                } else {
                    if (!this.Shared.EmojiReactions[ctx.Guild.Id].Add(new EmojiReaction(id, trigger, ename, regex: is_regex)))
                        throw new CommandFailedException($"Failed to add trigger {Formatter.Bold(trigger)}.");
                }
            }

            string errlist = errors.ToString();
            var logchn = this.Shared.GetLogChannelForGuild(ctx.Client, ctx.Guild);
            if (logchn != null) {
                var emb = new DiscordEmbedBuilder() {
                    Title = "New emoji reactions added",
                    Color = DiscordColor.Blue
                };
                emb.AddField("User responsible", ctx.User.Mention, inline: true);
                emb.AddField("Invoked in", ctx.Channel.Mention, inline: true);
                emb.AddField("Reaction", emoji, inline: true);
                emb.AddField("Triggers", string.Join("\n", triggers));
                if (!string.IsNullOrWhiteSpace(errlist))
                    emb.AddField("With errors", errlist);
                await logchn.SendMessageAsync(embed: emb.Build())
                    .ConfigureAwait(false);
            }

            await InformAsync(ctx, $"Done!\n\n{errlist}")
                .ConfigureAwait(false);
        }
        #endregion
    }
}
