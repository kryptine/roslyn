﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Rename;

namespace Microsoft.CodeAnalysis.ExternalAccess.OmniSharp
{
    internal static class OmniSharpRenamer
    {
        public static Task<ConflictResolution> RenameSymbolAsync(
            Solution solution,
            ISymbol symbol,
            string newName,
            OmniSharpRenameOptions options,
            ImmutableHashSet<ISymbol>? nonConflictSymbols,
            CancellationToken cancellationToken)
            => Renamer.RenameSymbolAsync(solution, symbol, newName, options.ToRenameOptions(), nonConflictSymbols, cancellationToken);
    }
}
