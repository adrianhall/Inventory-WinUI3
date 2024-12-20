﻿using Inventory.Core.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Inventory.Core.Services;

public class NavigationService(IViewMapper viewMapper, Window window, ILogger<NavigationService> logger) : INavigationService
{
    private object? _lastParameterUsed;
    private Frame? _frame;

    #region INavigationService Implementation
    /// <summary>
    /// An event handler that can be subscribed to for navigation events.
    /// </summary>
    public event NavigatedEventHandler? Navigated;

    /// <summary>
    /// <c>true</c> when the navigation service can navigate back.
    /// </summary>
    public bool CanGoBack 
        => _frame?.CanGoBack ?? false;

    /// <summary>
    /// The <see cref="Frame"/> to use for navigation events.
    /// </summary>
    public Frame? NavigationFrame 
    { 
        get
        {
            if (_frame is null)
            {
                // Create a new frame from the window content.
                _frame = window.Content as Frame;
                RegisterFrameEvents();
            }
            return _frame;
        }

        set
        {
            UnregisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }
    }

    /// <summary>
    /// Navigates backwards in the navigation stack.
    /// </summary>
    /// <returns><c>true</c> if navigation occurred.</returns>
    public bool GoBack()
    {
        logger.LogTrace("GoBack()");
        if (CanGoBack)
        {
            object? viewModelBeforeNavigation = _frame?.GetPageViewModel();
            logger.LogDebug("viewModelBeforeNavigation: {viewModelBeforeNavigation}", viewModelBeforeNavigation?.GetType().Name);
            _frame?.GoBack();
            CallNavigationLifecycle(viewModelBeforeNavigation);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Navigates to the page associated with the specified view model type
    /// </summary>
    /// <param name="viewModelType">The view model type.</param>
    /// <param name="parameter">The parameter for the view model/page.</param>
    /// <param name="clearNavigation">If true, clears the navigation stack.</param>
    /// <returns><c>true</c> if navigation occurred.</returns>
    public bool Navigate(Type viewModelType, object? parameter = null, bool clearNavigation = false)
    {
        logger.LogTrace("Navigate({viewModelType}, {clearNavigation})", viewModelType.Name, clearNavigation);
        Type pageType = viewMapper.GetViewFromViewModel(viewModelType);
        logger.LogDebug("pageType: {pageType}", pageType.Name);
        if (_frame is not null && (_frame.Content?.GetType() != pageType || (parameter is not null && !parameter.Equals(_lastParameterUsed))))
        {
            _frame.Tag = clearNavigation;
            object? viewModelBeforeNavigation = _frame.GetPageViewModel();
            logger.LogDebug("viewModelBeforeNavigation: {viewModelBeforeNavigation}", viewModelBeforeNavigation?.GetType().Name);
            bool navigated = _frame.Navigate(pageType, parameter);
            logger.LogDebug("navigated: {navigated}", navigated);
            if (navigated)
            {
                _lastParameterUsed = parameter;
                CallNavigationLifecycle(viewModelBeforeNavigation);
            }
            return navigated;
        }
        return false;
    }
    #endregion

    /// <summary>
    /// Registers for frame navigation events so that propagation can occur.
    /// </summary>
    private void RegisterFrameEvents()
    {
        if (_frame is not null)
        {
            logger.LogDebug("Registering for frame events");
            _frame.Navigated += OnNavigated;
        }
    }

    /// <summary>
    /// Unregisters from frame navigation events.
    /// </summary>
    private void UnregisterFrameEvents()
    {
        if (_frame is not null)
        {
            logger.LogDebug("Unregistering from frame events");
            _frame.Navigated -= OnNavigated;
        }
    }

    /// <summary>
    /// The event handler for the frame's navigation event.
    /// </summary>
    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        logger.LogTrace("OnNavigated({sender})", sender.GetType().Name);
        if (sender is Frame frame)
        {
            logger.LogDebug("OnNavigated: sender is frame; tag = {tag}", frame.Tag);
            if (frame.Tag is bool clearNavigation && clearNavigation)
            {
                logger.LogDebug("Clearing navigation stack");
                _frame?.BackStack.Clear();
            }

            logger.LogDebug("Propagating navigation event");
            Navigated?.Invoke(sender, e);
        }
    }

    /// <summary>
    /// Calls the navigation lifecycle methods on the provided view model.
    /// </summary>
    /// <param name="viewModel">The view model to use.</param>
    /// <param name="action">The action to call.</param>
    private void CallNavigationLifecycle(object? previousViewModel)
    {
        logger.LogTrace("CallNavigationLifecycle()");

        if (previousViewModel is not null)
        {
            logger.LogDebug("CallNavigationLifecycle(): previousViewModel is {previousViewModel}", previousViewModel.GetType().Name);
            if (previousViewModel is INavigationLifecycle previousLifecycle)
            {
                logger.LogDebug("previousViewModel has INavigationLifecycle - calling OnNavigatedFrom()");
                previousLifecycle.OnNavigatedFrom();
            }
            else
            {
                logger.LogDebug("previousViewModel does not have INavigationLifecycle");
            }
        }

        object? currentViewModel = _frame?.GetPageViewModel();
        if (currentViewModel is not null)
        {
            logger.LogDebug("CallNavigationLifecycle(): currentViewModel is {currentViewModel}", currentViewModel.GetType().Name);
            if (currentViewModel is INavigationLifecycle currentLifecycle)
            {
                logger.LogDebug("currentViewModel has INavigationLifecycle - calling OnNavigatedTo()");
                currentLifecycle.OnNavigatedTo();
            }
            else
            {
                logger.LogDebug("currentViewModel does not have INavigationLifecycle");
            }
        }
    }
}
