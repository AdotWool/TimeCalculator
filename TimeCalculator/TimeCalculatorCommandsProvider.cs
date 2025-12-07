// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace TimeCalculator;

public partial class TimeCalculatorCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public TimeCalculatorCommandsProvider()
    {
        DisplayName = "Time Calculator";
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        _commands = [
            new CommandItem(new TimeCalculatorPage()) { Title = DisplayName },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
