﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.EmbeddedLanguages.LanguageServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Classification.Classifiers
{
    internal class EmbeddedLanguagesClassifier : AbstractSyntaxClassifier
    {
        private readonly IEmbeddedLanguagesProvider _languagesProvider;

        public override ImmutableArray<int> SyntaxTokenKinds { get; }

        public EmbeddedLanguagesClassifier(IEmbeddedLanguagesProvider languagesProvider)
        {
            _languagesProvider = languagesProvider;
            SyntaxTokenKinds =
                languagesProvider.Languages.Where(p => p.Classifier != null)
                                           .SelectMany(p => p.Classifier.SyntaxTokenKinds)
                                           .Distinct()
                                           .ToImmutableArray();
        }

        public override void AddClassifications(SyntaxToken token, SemanticModel semanticModel, ClassificationOptions options, ArrayBuilder<ClassifiedSpan> result, CancellationToken cancellationToken)
        {
            foreach (var language in _languagesProvider.Languages)
            {
                var classifier = language.Classifier;
                if (classifier != null)
                {
                    var count = result.Count;
                    classifier.AddClassifications(token, semanticModel, options, result, cancellationToken);
                    if (result.Count != count)
                    {
                        // This classifier added values.  No need to check the other ones.
                        return;
                    }
                }
            }
        }
    }
}
