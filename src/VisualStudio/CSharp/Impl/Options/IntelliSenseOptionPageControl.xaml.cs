﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

using System.Windows;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.Editor.Shared.Options;
using Microsoft.VisualStudio.LanguageServices.Implementation.Options;

namespace Microsoft.VisualStudio.LanguageServices.CSharp.Options
{
    internal partial class IntelliSenseOptionPageControl : AbstractOptionPageControl
    {
        public IntelliSenseOptionPageControl(OptionStore optionStore) : base(optionStore)
        {
            InitializeComponent();

            BindToOption(Automatically_complete_statement_on_semicolon, FeatureOnOffOptions.AutomaticallyCompleteStatementOnSemicolon);

            BindToOption(Show_completion_item_filters, CompletionViewOptions.ShowCompletionItemFilters, LanguageNames.CSharp);
            BindToOption(Highlight_matching_portions_of_completion_list_items, CompletionViewOptions.HighlightMatchingPortionsOfCompletionListItems, LanguageNames.CSharp);

            BindToOption(Show_completion_list_after_a_character_is_typed, CompletionOptions.Metadata.TriggerOnTypingLetters, LanguageNames.CSharp);
            Show_completion_list_after_a_character_is_deleted.IsChecked = this.OptionStore.GetOption(CompletionOptions.Metadata.TriggerOnDeletion, LanguageNames.CSharp) == true;
            Show_completion_list_after_a_character_is_deleted.IsEnabled = Show_completion_list_after_a_character_is_typed.IsChecked == true;

            BindToOption(Never_include_snippets, CompletionOptions.Metadata.SnippetsBehavior, SnippetsRule.NeverInclude, LanguageNames.CSharp);
            BindToOption(Always_include_snippets, CompletionOptions.Metadata.SnippetsBehavior, SnippetsRule.AlwaysInclude, LanguageNames.CSharp);
            BindToOption(Include_snippets_when_question_Tab_is_typed_after_an_identifier, CompletionOptions.Metadata.SnippetsBehavior, SnippetsRule.IncludeAfterTypingIdentifierQuestionTab, LanguageNames.CSharp);

            BindToOption(Never_add_new_line_on_enter, CompletionOptions.Metadata.EnterKeyBehavior, EnterKeyRule.Never, LanguageNames.CSharp);
            BindToOption(Only_add_new_line_on_enter_with_whole_word, CompletionOptions.Metadata.EnterKeyBehavior, EnterKeyRule.AfterFullyTypedWord, LanguageNames.CSharp);
            BindToOption(Always_add_new_line_on_enter, CompletionOptions.Metadata.EnterKeyBehavior, EnterKeyRule.Always, LanguageNames.CSharp);

            BindToOption(Show_name_suggestions, CompletionOptions.Metadata.ShowNameSuggestions, LanguageNames.CSharp);
            BindToOption(Automatically_show_completion_list_in_argument_lists, CompletionOptions.Metadata.TriggerInArgumentLists, LanguageNames.CSharp);

            Show_items_from_unimported_namespaces.IsChecked = this.OptionStore.GetOption(CompletionOptions.Metadata.ShowItemsFromUnimportedNamespaces, LanguageNames.CSharp);
            Tab_twice_to_insert_arguments.IsChecked = this.OptionStore.GetOption(CompletionViewOptions.EnableArgumentCompletionSnippets, LanguageNames.CSharp);

        }

        private void Show_completion_list_after_a_character_is_typed_Checked(object sender, RoutedEventArgs e)
            => Show_completion_list_after_a_character_is_deleted.IsEnabled = Show_completion_list_after_a_character_is_typed.IsChecked == true;

        private void Show_completion_list_after_a_character_is_typed_Unchecked(object sender, RoutedEventArgs e)
        {
            Show_completion_list_after_a_character_is_deleted.IsEnabled = Show_completion_list_after_a_character_is_typed.IsChecked == true;
            Show_completion_list_after_a_character_is_deleted.IsChecked = false;
            Show_completion_list_after_a_character_is_deleted_Unchecked(sender, e);
        }

        private void Show_completion_list_after_a_character_is_deleted_Checked(object sender, RoutedEventArgs e)
            => this.OptionStore.SetOption(CompletionOptions.Metadata.TriggerOnDeletion, LanguageNames.CSharp, value: true);

        private void Show_completion_list_after_a_character_is_deleted_Unchecked(object sender, RoutedEventArgs e)
            => this.OptionStore.SetOption(CompletionOptions.Metadata.TriggerOnDeletion, LanguageNames.CSharp, value: false);

        private void Show_items_from_unimported_namespaces_CheckedChanged(object sender, RoutedEventArgs e)
        {
            Show_items_from_unimported_namespaces.IsThreeState = false;
            this.OptionStore.SetOption(CompletionOptions.Metadata.ShowItemsFromUnimportedNamespaces, LanguageNames.CSharp, value: Show_items_from_unimported_namespaces.IsChecked);
        }

        private void Tab_twice_to_insert_arguments_CheckedChanged(object sender, RoutedEventArgs e)
        {
            Tab_twice_to_insert_arguments.IsThreeState = false;
            this.OptionStore.SetOption(CompletionViewOptions.EnableArgumentCompletionSnippets, LanguageNames.CSharp, value: Tab_twice_to_insert_arguments.IsChecked);
        }
    }
}
