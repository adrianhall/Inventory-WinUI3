using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System.Reflection;

namespace Inventory.Core.Extensions;

/// <summary>
/// A set of extension methods for the <see cref="Frame"/> class.
/// </summary>
public static class FrameExtensions
{
    /// <summary>
    /// Finds the view model associated with the given frame content.
    /// </summary>
    /// <remarks>
    /// The view model is identified as either a property named "ViewModel" or the "DataContext" of the frame content,
    /// and only if the view model is an ObservableObject.
    /// </remarks>
    /// <param name="frame">The frame to use.</param>
    /// <returns>The view model or <c>null</c> if not found.</returns>
    public static object? GetPageViewModel(this Frame frame)
    {
        object? viewModel = frame.Content?.GetType().GetProperty("ViewModel", BindingFlags.Public | BindingFlags.Instance)?.GetValue(frame.Content);
        if (viewModel is not null && viewModel is ObservableObject)
        {
            return viewModel;
        }

        if (frame.Content is Page page)
        {
            viewModel = page.DataContext;
            if (viewModel is not null && viewModel is ObservableObject)
            {
                return viewModel;
            }
        }

        return null;
    }
}
