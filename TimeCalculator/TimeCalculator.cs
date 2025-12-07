// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace TimeCalculator;

[Guid("a9911853-13b4-48af-8b5a-4657048ec829")]
public sealed partial class TimeCalculator : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;

    private readonly TimeCalculatorCommandsProvider _provider = new();

    public TimeCalculator(ManualResetEvent extensionDisposedEvent)
    {
        this._extensionDisposedEvent = extensionDisposedEvent;
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose() => this._extensionDisposedEvent.Set();
}
