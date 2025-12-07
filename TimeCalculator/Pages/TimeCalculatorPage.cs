// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace TimeCalculator;

internal sealed partial class TimeCalculatorPage : DynamicListPage
{
    public TimeCalculatorPage()
    {
        Icon = new IconInfo("\U0001F550"); // Would like to replace with a different icon.
        Title = "Time Calculator";
        Name = "Open";
    }

    public override void UpdateSearchText(string _, string newSearch) => RaiseItemsChanged();


    public override IListItem[] GetItems()
    {
        // Use SearchText to pull user input.
        // Regex created mostly with Microsoft Copilot
        string result = CalculateTime.Calculate(SearchText).result;

        return [
            new ListItem(new CopyTextCommand(result)) { Title = result, Icon = new IconInfo("\U0001F554")},
            //new ListItem(new OpenUrlCommand("https://learn.microsoft.com/windows/powertoys/command-palette/adding-commands")) { Title = "Open the Command Palette documentation"},
            ];
    }
}
