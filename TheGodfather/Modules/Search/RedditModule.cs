﻿#region USING_DIRECTIVES
using System.Threading.Tasks;

using TheGodfather.Common.Attributes;
using TheGodfather.Exceptions;
using TheGodfather.Extensions;
using TheGodfather.Services;
using TheGodfather.Services.Database;
using TheGodfather.Services.Database.Feeds;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
#endregion

namespace TheGodfather.Modules.Search
{
    [Group("reddit"), Module(ModuleType.Searches)]
    [Description("Reddit commands.")]
    [Aliases("r")]
    [UsageExamples("!reddit aww")]
    [Cooldown(3, 5, CooldownBucketType.Channel)]
    [NotBlocked]
    public class RedditModule : TheGodfatherModule
    {

        public RedditModule(SharedData shared, DBService db)
                : base(shared, db) { }


        [GroupCommand]
        public async Task ExecuteGroupAsync(CommandContext ctx,
                                           [Description("Subreddit.")] string sub = "all")
        {
            if (string.IsNullOrWhiteSpace(sub))
                throw new InvalidCommandUsageException("Subreddit missing.");

            var url = RssService.GetFeedURLForSubreddit(sub, out string rsub);
            if (url == null)
                throw new CommandFailedException("That subreddit doesn't exist.");

            var res = RssService.GetFeedResults(url);
            if (res == null)
                throw new CommandFailedException($"Failed to get the data from that subreddit ({Formatter.Bold(rsub)}).");
            await RssService.SendFeedResultsAsync(ctx.Channel, res)
                .ConfigureAwait(false);
        }


        #region COMMAND_RSS_REDDIT_SUBSCRIBE
        [Command("subscribe"), Module(ModuleType.Searches)]
        [Description("Add new feed for a subreddit.")]
        [Aliases("add", "a", "+", "sub")]
        [UsageExamples("!reddit sub aww")]
        [RequireUserPermissions(Permissions.ManageGuild)]
        public async Task SubscribeAsync(CommandContext ctx,
                                        [Description("Subreddit.")] string sub)
        {
            var url = RssService.GetFeedURLForSubreddit(sub, out string rsub);
            if (url == null)
                throw new CommandFailedException("That subreddit doesn't exist.");

            if (!await Database.TryAddSubscriptionAsync(ctx.Channel.Id, url, rsub).ConfigureAwait(false))
                throw new CommandFailedException("You are already subscribed to this subreddit!");

            await InformAsync(ctx, $"Subscribed to {Formatter.Bold(rsub)} !")
                .ConfigureAwait(false);
        }
        #endregion

        #region COMMAND_RSS_REDDIT_UNSUBSCRIBE
        [Command("unsubscribe"), Priority(1)]
        [Module(ModuleType.Searches)]
        [Description("Remove a subreddit feed using subreddit name or subscription ID (use command ``feed list`` to see IDs).")]
        [Aliases("del", "d", "rm", "-", "unsub")]
        [UsageExamples("!reddit unsub aww",
                       "!reddit unsub 12")]
        [RequireUserPermissions(Permissions.ManageGuild)]
        public async Task UnsubscribeAsync(CommandContext ctx,
                                          [Description("Subreddit.")] string sub)
        {
            if (RssService.GetFeedURLForSubreddit(sub, out string rsub) == null)
                throw new CommandFailedException("That subreddit doesn't exist.");

            await Database.RemoveSubscriptionByNameAsync(ctx.Channel.Id, rsub)
                .ConfigureAwait(false);
            await InformAsync(ctx, $"Unsubscribed from {Formatter.Bold(rsub)} !")
                .ConfigureAwait(false);
        }

        [Command("unsubscribe"), Priority(0)]
        public async Task UnsubscribeAsync(CommandContext ctx,
                                          [Description("Subscription ID.")] int id)
        {
            await Database.RemoveSubscriptionByIdAsync(ctx.Channel.Id, id)
                .ConfigureAwait(false);
            await InformAsync(ctx)
                .ConfigureAwait(false);
        }
        #endregion
    }
}
