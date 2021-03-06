
using Content.Server.Database;
using Content.Server.Preferences.Managers;
using Content.Shared.Administration;
using Robust.Server.Player;
using Robust.Shared.Console;
using Robust.Shared.IoC;
using Robust.Shared.Localization;
using Robust.Shared.Maths;

namespace Content.Server.Administration.Commands
{
    [AdminCommand(AdminFlags.Admin)]
    internal class SetAdminOOC : IConsoleCommand
    {
        public string Command => "setadminooc";
        public string Description => Loc.GetString("set-admin-ooc-command-description", ("command", Command));
        public string Help => Loc.GetString("set-admin-ooc-command-help-text", ("command", Command));

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            if (!(shell.Player is IPlayerSession))
            {
                shell.WriteError(Loc.GetString("shell-only-players-can-run-this-command"));
                return;
            }

            if (args.Length < 1)
                return;

            var colorArg = string.Join(" ", args).Trim();
            if (string.IsNullOrEmpty(colorArg))
                return;

            var color = Color.TryFromHex(colorArg);
            if (!color.HasValue)
            {
                shell.WriteError(Loc.GetString("shell-invalid-color-hex"));
                return;
            }

            var userId = shell.Player.UserId;
            // Save the DB
            var dbMan = IoCManager.Resolve<IServerDbManager>();
            dbMan.SaveAdminOOCColorAsync(userId, color.Value);
            // Update the cached preference
            var prefManager = IoCManager.Resolve<IServerPreferencesManager>();
            var prefs = prefManager.GetPreferences(userId);
            prefs.AdminOOCColor = color.Value;
        }
    }
}
