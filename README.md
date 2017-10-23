# the-godfather
Just another Discord bot. Written using DSharpPlus.

[Project website](https://ivan-ristovic.github.io/the-godfather/)

---

# Command list

Commands are separated into groups. For example, ``!user`` is a group of commands which allow manipulation of users and it has subcommands ``kick`` , ``ban`` etc. So if you want to call the ``kick`` command, you do it like ``!user kick ...``.

The default prefix for the bot is ``!``, however you can change that using ``!prefix`` command. Also you can trigger commands by mentioning the bot. For example:
``!greet`` is the same as ``@TheGodfather greet``


### Argument type explanation:
Each command receives arguments. For example, in ``!say Some text``, the command is ``say`` and argument is ``Some text``. The type of this argument is ``text``.
Commands receive **only** arguments of the specified type, so for example if command expects an ``int``, passing ``text`` to it will cause an error.

Commands use the following types:
* ``int`` : Integer (positive or negative number)
* ``double`` : Decimal point number (positive or negative), can also be integer
* ``string`` : Word consisting of Unicode characters WITHOUT spaces. If you want to include spaces, then surround it with ``"``
* ``bool`` : ``true`` or ``false``
* ``text`` : Some Unicode text, can include spaces
* ``user`` : Discord user, given by ``@mention`` or ``Username``
* ``channel`` : Discord channel, given by ``name`` or ``#name``
* ``role`` : An existing role, given with ``@mentionrole`` or ``Role name``
* ``emoji`` : Emoji, either Unicode or Discord representation


## Command table

*Note: Required permissions are permissions required for both bot and user to run the command (if not specified otherwise in table)*
<br><br>

## Main commands

| Command group (with synonyms) | Command name (with synonyms) | Required Permissions | Command arguments | Command Description | Example of use |
|---|---|---|---|---|---|
|   |   |   |   |   |   |
|   | ``embed`` | Attach files (user) | ``[string] URL`` | Embeds image given as URL and sends a embed frame. | ``!embed https://img.memecdn.com/feelium_o_1007518.jpg``  |
|   | ``greet``<br>``hello``<br>``hi``<br>``halo``<br>``hey``<br>``howdy``<br>``sup`` |  |  | Greets a user and starts a conversation | ``!greet`` |
|   | ``invite``<br>``getinvite`` | Create instant invite |  | Get an instant invite link for the current channel. | ``!invite`` |
|   | ``leave`` | Kick members (user) |   | Makes Godfather leave the server. | ``!leave`` |
|   | ``leet`` |   | ``[text] Text`` | Wr1t3s m3ss@g3 1n 1337sp34k. | ``!leet This is so cool`` |
|   | ``ping`` |   |   | Ping the bot. | ``!ping`` |
|   | ``poll``<br>``vote`` |   | ``[text] Question`` | Starts a poll in the channel. The bot will ask for poll options, which you give separated with ``;``, for example: ``option1;option2;option3`` | ``!poll`` |
|   | ``prefix``<br>``setprefix`` | Administrator (user) | ``(optional) [string] New prefix`` | If invoked without arguments, gives current prefix for this channel, otherwise sets the prefix to ``New prefix``. If for example ``New prefix`` is ``;``, all commands in that channel from that point must be invoked using ``;``, for example ``;greet``. | ``!prefix``<br><br>``!prefix .`` |
|   | ``remind`` |  | ``[int] Time to wait before repeat (in seconds)``<br><br>``[text] What to repeat`` | Repeat given text after given time. | ``!repeat 3600 I was told to remind you to do something`` |
|   | ``report`` |   | ``[text] Report message`` | Send message to owner (hopefully about a bug, I can see it being abused) | ``!report Your bot sucks!`` |
|   | ``say``  |   | ``[text] What to say`` | Make Godfather say something! | ``!say Luke, I am your father!`` |
|   | ``zugify`` |   | ``[text] Text`` | Requested by Zugi. It is so stupid it isn't worth describing... | ``!zugify Some text`` |
|   |   |   |   |   |   |
| ``insult``<br>``burn`` |   |   | ``(optional) [user] User (def: sender)`` | Insult ``User``. | ``!insult``<br><br>``!insult @Someone`` |
| ``insult``<br>``burn`` | ``add``<br>``+``<br>``new`` | Owner Only | ``[text] Insult`` | Add a new insult to global insult list. You can use ``%user%`` in your insult text as a replacement for the user mention who will be insulted. | ``!insult add Your age is greater than your IQ, %user%!`` |
| ``insult``<br>``burn`` | ``clear``<br>``clearall`` | Owner Only |  | Delete all insults. | ``!insult clear`` |
| ``insult``<br>``burn`` | ``delete``<br>``-``<br>``remove``<br>``del``<br>``rm`` | Owner Only | ``[int] Index`` | Remove insult with a given index from list. Use ``!insults list`` to view indexes. | ``!insult delete 5`` |
| ``insult``<br>``burn`` | ``list``<br> |  | ``(optional) [int] Page (def: 1)`` | List insults on page ``Page``. | ``!insult list 3`` |
| ``insult``<br>``burn`` | ``save``<br> | Owner Only |  | Save all insults. | ``!insult save`` |
|   |   |   |   |   |   |
| ``meme``<br>``memes``<br>``mm`` |   |   | ``(optional) [text] Meme name`` | Send a meme with name ``Meme name``. If name isn't given, sends random one. | ``!meme``<br><br>``!meme fap`` |
| ``meme``<br>``memes``<br>``mm`` | ``add``<br>``+``<br>``new`` | Owner Only | ``[text] Name``<br><br>``[string] URL`` | Add a new meme to global meme list. | ``!meme add Name http://url.png`` |
| ``meme``<br>``memes``<br>``mm`` | ``delete``<br>``-``<br>``remove``<br>``del``<br>``rm`` | Owner Only | ``[text] Name`` | Remove meme with a given name from the list. Use ``!meme list`` to view all memes. | ``!meme delete fap`` |
| ``meme``<br>``memes``<br>``mm`` | ``list``<br> |  | ``(optional) [int] Page (def: 1)`` | List memes on page ``Page``. | ``!meme list 3`` |
| ``meme``<br>``memes``<br>``mm`` | ``save``<br> | Owner Only |  | Save all memes. | ``!meme save`` |

## Administration

| Command group (with synonyms) | Command name (with synonyms) | Required Permissions | Command arguments | Command Description | Example of use |
|---|---|---|---|---|---|
|   |   |   |   |   |   |
| ``channel``<br>``channels``<br>``c``<br>``chn`` | ``createcategory``<br>``createc``<br>``+c``<br>``makec``<br>``newc``<br>``addc`` | Manage Channels | ``[text] Name`` | Create new channel category. | ``!channel createcategory My Category`` |
| ``channel``<br>``channels``<br>``c``<br>``chn`` | ``createtext``<br>``createt``<br>``+``<br>``+t``<br>``maket``<br>``newt``<br>``addt`` | Manage Channels | ``[string] Name`` | Create new text channel. *Note: Discord does not allow spaces in text channel name.* | ``!channel createtext spam`` |
| ``channel``<br>``channels``<br>``c``<br>``chn`` | ``createvoice``<br>``createv``<br>``+v``<br>``makev``<br>``newv``<br>``addv`` | Manage Channels | ``[text] Name`` | Create new voice channel. | ``!channel createvoice My Voice Channel`` |
| ``channel``<br>``channels``<br>``c``<br>``chn`` | ``delete``<br>``-``<br>``d``<br>``del``<br>``remove`` | Manage Channels | ``(optional) [channel] Channel/Category`` | Delete channel or category. If channel is not given as argument, deletes the current channel. | ``!channel delete``<br><br>``!channel delete #afkchannel`` |
| ``channel``<br>``channels``<br>``c``<br>``chn`` | ``info``<br>``i``<br>``information`` | Manage Channels | ``(optional) [channel] Channel/Category`` | Get channel information. | ``!channel info``<br><br>``!channel info #afkchannel`` |
| ``channel``<br>``channels``<br>``c``<br>``chn`` | ``rename``<br>``r``<br>``name``<br>``setname`` | Manage Channels | ``[string] Name``<br><br>``(optional) [channel] Channel/Category`` | Rename channel. If channel is not given as argument, renames the current channel. | ``!channel rename New Name``<br><br>``!channel rename "New Name" "Some Channel Name"`` |
| ``channel``<br>``channels``<br>``c``<br>``chn`` | ``settopic``<br>``t``<br>``sett``<br>``topic`` | Manage Channels | ``[string] Topic``<br><br>``(optional) [channel] Channel/Category`` | Set a new channel topic. If channel is not given as argument, modifies the current channel. | ``!channel settopic Welcome to my channel!``<br><br>``!channel settopic "My topic" "Some Channel Name"`` |
|   |   |   |   |   |   |   |
| ``guild``<br>``server``<br>``g`` | ``info``<br>``i``<br>``information`` |  |  | Get guild information. | ``!guild info`` |
| ``guild``<br>``server``<br>``g`` | ``listmembers``<br>``memberlist``<br>``lm``<br>``members`` | Manage Guild | ``(optional) [int] page (def: 1)`` | Get guild member list. | ``!guild memberlist``<br>``!guild memberlist 3`` |
| ``guild``<br>``server``<br>``g`` | ``log``<br>``auditlog``<br>``viewlog``<br>``getlog``<br>``getlogs``<br>``logs`` | View Audit Log | ``(optional) [int] page (def: 1)`` | Get guild audit logs. | ``!guild log``<br>``!guild log 3`` |
| ``guild``<br>``server``<br>``g`` | ``prune``<br>``p``<br>``clean`` | Administrator (user)<br><br>KickMembers (bot) | ``(optional) [int] page (def: 7)`` | Kick members who weren't active in given ammount of days (1-7). | ``!guild prune``<br>``!guild prune 5`` |
| ``guild``<br>``server``<br>``g`` | ``rename``<br>``r``<br>``name``<br>``setname`` | Manage guild | ``[text] Name`` | Rename guild. | ``!guild rename New guild name`` |
| ``guild``<br>``server``<br>``g`` | ``getwelcomechannel``<br>``getwelcomec``<br>``getwc``<br>``getwelcome``<br>``welcomechannel``<br>``wc`` | Manage guild (user) |  | Get current welcome message channel for this guild. | ``!guild getwc`` |
| ``guild``<br>``server``<br>``g`` | ``getleavechannel``<br>``getleavec``<br>``getlc``<br>``getleave``<br>``leavechannel``<br>``lc`` | Manage guild (user) |  | Get current leave message channel for this guild. | ``!guild getlc`` |
| ``guild``<br>``server``<br>``g`` | ``setwelcomechannel``<br>``setwelcomec``<br>``setwc``<br>``setwelcome`` | Manage guild (user) | ``(optional) [channel] Channel`` | Set current welcome message channel for this guild. If not specified, the current channel is set. | ``!guild setwc``<br>``!guild setwc #welcome`` |
| ``guild``<br>``server``<br>``g`` | ``setleavechannel``<br>``setleavec``<br>``setwc``<br>``setleave`` | Manage guild (user) | ``(optional) [channel] Channel`` | Set current leave message channel for this guild. If not specified, the current channel is set. | ``!guild setlc``<br>``!guild setlc #general`` |
| ``guild``<br>``server``<br>``g`` | ``deletewelcomechannel``<br>``delwelcomec``<br>``delwc``<br>``deletewc``<br>``delwelcome``<br>``dwc`` | Manage guild (user) |  | Delete current welcome message channel for this guild. | ``!guild deletewc`` |
| ``guild``<br>``server``<br>``g`` | ``deleteleavechannel``<br>``delleavec``<br>``dellc``<br>``deletelc``<br>``delleave``<br>``dlc`` | Manage guild (user) |  | Delete current leave message channel for this guild. | ``!guild deletelc`` |
| ``g emoji``<br>``g emojis``<br>``g e`` |  |  |  | List guild emoji. | ``!guild emoji`` |
| ``g emoji``<br>``g emojis``<br>``g e`` | ``add``<br>``+``<br>``a``<br>``create`` | Manage emojis | ``[string] Name``<br><br>``[string] URL`` | Add a new guild emoji from URL. | ``!guild emoji add http://blabla.com/someemoji.img`` |
| ``g emoji``<br>``g emojis``<br>``g e`` | ``delete``<br>``-``<br>``del``<br>``d``<br>``remove`` | Manage emojis | ``[emoji] Emoji`` | Remove emoji from guild emoji list.<br>*Note: Bots can only remove emoji which they created!* | ``!guild emoji del :pepe:`` |
| ``g emoji``<br>``g emojis``<br>``g e`` | ``list``<br>``print``<br>``show``<br>``print``<br>``l``<br>``p`` |  |  | List guild emoji. | ``!guild emoji list`` |
| ``g emoji``<br>``g emojis``<br>``g e`` | ``modify``<br>``edit``<br>``mod``<br>``e``<br>``m`` | Manage emojis | ``[emoji] Emoji``<br>``[string] New name`` | Modify guild emoji. | ``!guild emoji edit :pepe: pepenewname`` |
|   |   |   |   |   |   |   |
| ``messages``<br>``m``<br>``msg``<br>``msgs`` | ``delete``<br>``-``<br>``d``<br>``del``<br>``prune`` | Administrator (user)<br><br>Manage messages (bot) | ``[int] Ammount (def: 5)`` | Delete ``Ammount`` messages from the current channel. | ``!messages delete 100`` |
| ``messages``<br>``m``<br>``msg``<br>``msgs`` | ``deletefrom``<br>``-user``<br>``du``<br>``deluser``<br>``dfu`` | Administrator (user)<br><br>Manage messages (bot) | ``[user] User``<br><br>``[int] Ammount (def: 5)`` | Delete ``Ammount`` messages from ``User`` in the current channel. | ``!messages deletefrom @Someone 100`` |
| ``messages``<br>``m``<br>``msg``<br>``msgs`` | ``listpinned``<br>``lp``<br>``listpins``<br>``listpin``<br>``pinned`` |  | ``[int] Ammount (def: 1)`` | List ``Ammount`` pinned messages. | ``!messages listpinned 5`` |
| ``messages``<br>``m``<br>``msg``<br>``msgs`` | ``pin``<br>``p`` | Manage Messages  |  | Pin last sent message (before ``pin`` command). | ``!messages pin`` |
| ``messages``<br>``m``<br>``msg``<br>``msgs`` | ``unpin``<br>``up`` | Manage Messages  | ``[int] Index (starting from 0)`` | Unpin pinned message with index ``Index`` in pinned message list. | ``!messages unpin 3`` |
| ``messages``<br>``m``<br>``msg``<br>``msgs`` | ``unpinall``<br>``upa`` | Manage Messages  |  | Unpin all pinned messages. | ``!messages unpinall`` |
|   |   |   |   |   |   |
| ``role``<br>``roles``<br>``r``<br>``rl`` |  |  |  | List all roles for this guild. | ``!roles`` |
| ``role``<br>``roles``<br>``r``<br>``rl`` | ``create``<br>``new``<br>``add``<br>``+`` | Manage Roles | ``[text] Name`` | Create new role with name ``Name`` | ``!role create My new role`` |
| ``role``<br>``roles``<br>``r``<br>``rl`` | ``delete``<br>``del``<br>``d``<br>``-``<br>``remove``<br>``rm`` | Manage Roles | ``[role] Role`` | Delete role ``Role``. | ``!role delete @role``<br><br>``!role delete Some Role`` |
| ``role``<br>``roles``<br>``r``<br>``rl`` | ``mentionall``<br>``@``<br>``ma`` | Mention everyone | ``[role] Role`` | Mention everyone from role ``Role``. | ``!role mentionall @role``<br><br>``!role mentionall Some Role`` |
| ``role``<br>``roles``<br>``r``<br>``rl`` | ``setcolor``<br>``clr``<br>``c``<br>``sc`` | Manage Roles | ``[string] Color (hex code)``<br><br>``[role] Role`` | Set ``Role`` color to ``Color``. | ``!role setcolor #800000 @role``<br><br>``!role setcolor #800000 Some Role`` |
| ``role``<br>``roles``<br>``r``<br>``rl`` | ``setname``<br>``rename``<br>``name``<br>``n`` | Manage Roles | ``[role] Role``<br><br>``[text] Name`` | Change ``Role`` name to ``Name``. | ``!role rename @somerole New Name``<br><br>``!role rename "Unmentionable role" Some new name`` |
| ``role``<br>``roles``<br>``r``<br>``rl`` | ``setmentionable``<br>``mentionable``<br>``m``<br>``setm`` | Manage Roles | ``[role] Role``<br><br>``[bool] Mentionable`` | Set ``Role`` to be mentionable or not. | ``!role mentionable @somerole false``<br><br>``!role mentionable "Unmentionable role" true`` |
| ``role``<br>``roles``<br>``r``<br>``rl`` | ``setvisible``<br>``separate``<br>``h``<br>``seth``<br>``hoist``<br>``sethoist`` | Manage Roles | ``[role] Role``<br><br>``[bool] Visible`` | Set ``Role`` to be visible (hoisted) or not. Visible roles appear separated in memberlist. | ``!role hoist @somerole false``<br><br>``!role hoist "Unmentionable role" true`` |
|   |   |   |   |   |   |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``addrole``<br>``+role``<br>``+r``<br>``ar`` | Manage Roles | ``[user] User``<br><br>``[role] Role`` | Give ``Role`` to ``User``. | ``!user addrole @SomeUser @admins``<br><br>``!user addrole @SomeUser "Unmentionable role"`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``avatar``<br>``a``<br>``pic`` |  | ``[user] User`` | Print ``User``'s avatar. | ``!user avatar @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``ban``<br>``b`` | Ban Members | ``[user] User`` | Ban ``User``. | ``!user ban @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``deafen``<br>``d``<br>``deaf`` | Deafen Members | ``[user] User`` | Toggle ``User``'s voice deaf status. | ``!user deafen @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``info``<br>``i``<br>``information`` |  | ``(optional) [user] User (def: sender)`` | Get information about ``User``. | ``!user info``<br><br>``!user info @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``kick``<br>``k`` | Kick Members | ``[user] User`` | Kick ``User``. | ``!user kick @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``listperms``<br>``permlist``<br>``perms``<br>``p`` |  | ``(optional) [user] User (def: sender)`` | List permissions for ``User``. | ``!user perms``<br><br>``!user perms @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``listroles``<br>``rolelist``<br>``roles``<br>``r`` |  | ``(optional) [user] User (def: sender)`` | List roles for ``User``. | ``!user roles``<br><br>``!user roles @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``mute``<br>``m`` | Mute Members | ``[user] User`` | Mute ``User``. | ``!user mute @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``removerole``<br>``remrole``<br>``rmrole``<br>``-role``<br>``-r``<br>``rr`` | Manage Roles | ``[user] User``<br><br>``[role] Role`` | Remove ``Role`` from ``User``. | ``!user remrole @SomeUser @admins``<br><br>``!user remrole @SomeUser "Unmentionable role"`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``removeallroles``<br>``remallroles``<br>``rmallroles``<br>``-ra``<br>``-rall``<br>``-allr`` | Manage Roles | ``[user] User`` | Remove all roles for ``User``. | ``!user rmallroles @SomeUser`` |
| ``user``<br>``users``<br>``u``<br>``usr`` | ``setname``<br>``nick``<br>``rename``<br>``name``<br>``newname`` | Manage Nicknames | ``[user] User``<br><br>``[text] New name`` | Change ``User``'s nickname to ``New name`` (for this server). | ``!user setname @SomeUser Some new name`` |

## Gambling

| Command group (with synonyms) | Command name (with synonyms) | Required Permissions | Command arguments | Command Description | Example of use |
|---|---|---|---|---|---|
|   |   |   |   |   |   |
| ``bank``<br>``$``<br>``$$``<br>``$$$`` |  |  |  | Prints the account balance for sender. | ``!bank`` |
| ``bank``<br>``$``<br>``$$``<br>``$$$`` | ``grant``<br>``give`` | Administrator | ``[user] User``<br><br>``[int] Ammount`` | Add ``Ammount`` credits to ``User``'s account. | ``!bank grant 100 @LuckyGuy`` |
| ``bank``<br>``$``<br>``$$``<br>``$$$`` | ``register``<br>``r``<br>``activate``<br>``signup`` |  |  | Opens an account for sender in WM bank. | ``!bank register`` |
| ``bank``<br>``$``<br>``$$``<br>``$$$`` | ``status``<br>``balance``<br>``s`` |  | ``(optional) [user] User (def: sender)`` | Prints the account balance for a user. | ``!bank balance``<br><br>``!bank balance @BillGates`` |
| ``bank``<br>``$``<br>``$$``<br>``$$$`` | ``top``<br>``leaderboard`` |  |  | Prints a list of richest users (globally). | ``!bank top`` |
| ``bank``<br>``$``<br>``$$``<br>``$$$`` | ``transfer``<br>``lend`` |  | ``[user] User``<br><br>``[int] Ammount`` | Give ``Ammount`` credits from your account to ``User``'s account. | ``!bank transfer @MyFriend 100`` |
|   |   |   |   |   |   |
| ``gamble``<br>``bet`` | ``coinflip``<br>``coin``<br>``flip`` |   | ``[int] Bid``<br><br>``[string] Heads/Tails`` | Bet on a coin flip outcome! Can be invoked without both arguments if you do not wish to bet. | ``!bet coinflip 5 heads`` |
| ``gamble``<br>``bet`` | ``roll``<br>``dice``<br>``die`` |   | ``[int] Bid``<br><br>``[int] Guess [1-6]`` | Bet on a dice roll outcome! Can be invoked without both arguments if you do not wish to bet. | ``!bet dice 50 6`` |
| ``gamble``<br>``bet`` | ``slot``<br>``slotmachine`` |   | ``[int] Bid (min: 5)`` | Bet on a slot machine outcome! | ``!bet slot 5`` |
|   |   |   |   |   |   |

## Games

| Command group (with synonyms) | Command name (with synonyms) | Required Permissions | Command arguments | Command Description | Example of use |
|---|---|---|---|---|---|
|   |   |   |   |   |   |
| ``cards``<br>``deck`` | ``draw``<br>``take`` |   | ``(optional) [int] Ammount (def: 1)`` | Draw ``Ammount`` of cards from the top of the deck. | ``!deck draw 5`` |
| ``cards``<br>``deck`` | ``reset``<br>``opennew``<br>``new`` |   |   | Open new deck of cards (unshuffled). | ``!deck new`` |
| ``cards``<br>``deck`` | ``shuffle``<br>``s``<br>``sh``<br>``mix`` |   |   | Shuffle current card deck. | ``!deck shuffle`` |
|   |   |   |   |   |   |
| ``games``<br>``game``<br>``gm`` | ``duel``<br>``fight``<br>``vs`` |   | ``[user] Opponent`` | Call ``Opponent`` to a death battle! | ``!game duel @TheRock`` |
| ``games``<br>``game``<br>``gm`` | ``hangman`` |   |   | Start a new hangman game! | ``!game hangman`` |
| ``games``<br>``game``<br>``gm`` | ``rps``<br>``rockpaperscissors`` |   |   | Make Godfather play rock-paper-scissors! | ``!game rps`` |
| ``games``<br>``game``<br>``gm`` | ``tictactoe``<br>``ttt`` |   |   | Challenge friends to a tictactoe game! First who replies with ``me`` or ``i`` will join your game. | ``!game ttt`` |
| ``games``<br>``game``<br>``gm`` | ``typing``<br>``type``<br>``typerace``<br>``typingrace`` |   |   | Start a typing race game. | ``!game typerace`` |
| ``game nunchi`` |  |   |   | Start a signup process for a new nunchi game! | ``!game nunchi`` |
| ``game nunchi`` | ``new``<br>``create`` |   |   | Identical result as to above command. | ``!game nunchi new`` |
| ``game nunchi`` | ``join``<br>``+``<br>``compete`` |   |   | Join a pending nunchi game. | ``!game nunchi join`` |
| ``game nunchi`` | ``rules``<br>``help`` |   |   | How to play? | ``!game nunchi rules`` |
| ``game quiz``<br>``game trivia`` | ``countries``<br>``flags`` |   |   | Start a new countries quiz. | ``!game quiz countries`` |
| ``game race`` |  |   |   | Start a signup process for a new race! | ``!game race`` |
| ``game race`` | ``new``<br>``create`` |   |   | Identical result as to above command. | ``!game race new`` |
| ``game race`` | ``join``<br>``+``<br>``compete`` |   |   | Join a pending race. | ``!game race join`` |


**(Command list is incomplete)**


---
