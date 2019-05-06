using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Panacea.Controls
{
    public class TransitioningContentControl : ContentControl
    {
        internal const string PresentationGroup = "PresentationStates";
        internal const string NormalState = "Normal";
        internal const string PreviousContentPresentationSitePartName = "PreviousContentPresentationSite";
        internal const string CurrentContentPresentationSitePartName = "CurrentContentPresentationSite";
        public const string DefaultTransitionState = "DefaultTransition";

        public static readonly DependencyProperty IsTransitioningProperty =
            DependencyProperty.Register("IsTransitioning", typeof(bool), typeof(TransitioningContentControl),
                new PropertyMetadata(OnIsTransitioningPropertyChanged));

        public static readonly DependencyProperty TransitionProperty = DependencyProperty.Register("Transition",
            typeof(string), typeof(TransitioningContentControl),
            new PropertyMetadata(DefaultTransitionState, OnTransitionPropertyChanged));

        public static readonly DependencyProperty RestartTransitionOnContentChangeProperty =
            DependencyProperty.Register("RestartTransitionOnContentChange", typeof(bool),
                typeof(TransitioningContentControl),
                new PropertyMetadata(false, OnRestartTransitionOnContentChangePropertyChanged));

        public static readonly DependencyProperty CustomVisualStatesProperty =
            DependencyProperty.Register("CustomVisualStates", typeof(ObservableCollection<VisualState>),
                typeof(TransitioningContentControl), new PropertyMetadata(null));

        private bool _allowIsTransitioningWrite;
        private Storyboard _currentTransition;

        public TransitioningContentControl()
        {
            CustomVisualStates = new ObservableCollection<VisualState>();
            DefaultStyleKey = typeof(TransitioningContentControl);
        }

        private ContentPresenter CurrentContentPresentationSite { get; set; }
        private ContentPresenter PreviousContentPresentationSite { get; set; }

        public ObservableCollection<VisualState> CustomVisualStates
        {
            get { return (ObservableCollection<VisualState>)GetValue(CustomVisualStatesProperty); }
            set { SetValue(CustomVisualStatesProperty, value); }
        }

        public bool IsTransitioning
        {
            get { return (bool)GetValue(IsTransitioningProperty); }
            private set
            {
                _allowIsTransitioningWrite = true;
                SetValue(IsTransitioningProperty, value);
                _allowIsTransitioningWrite = false;
            }
        }

        public string Transition
        {
            get { return GetValue(TransitionProperty) as string; }
            set { SetValue(TransitionProperty, value); }
        }

        public bool RestartTransitionOnContentChange
        {
            get { return (bool)GetValue(RestartTransitionOnContentChangeProperty); }
            set { SetValue(RestartTransitionOnContentChangeProperty, value); }
        }

        private Storyboard CurrentTransition
        {
            get { return _currentTransition; }
            set
            {
                // decouple event
                if (_currentTransition != null)
                {
                    _currentTransition.Completed -= OnTransitionCompleted;
                }

                _currentTransition = value;

                if (_currentTransition != null)
                {
                    _currentTransition.Completed += OnTransitionCompleted;
                }
            }
        }

        public event RoutedEventHandler TransitionCompleted;

        private static void OnIsTransitioningPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (TransitioningContentControl)d;

            if (!source._allowIsTransitioningWrite)
            {
                source.IsTransitioning = (bool)e.OldValue;
                throw new InvalidOperationException();
            }
        }

        private static void OnTransitionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (TransitioningContentControl)d;
            var oldTransition = e.OldValue as string;
            var newTransition = e.NewValue as string;

            if (source.IsTransitioning)
            {
                source.AbortTransition();
            }

            // find new transition
            Storyboard newStoryboard = source.GetStoryboard(newTransition);

            // unable to find the transition.
            if (newStoryboard == null)
            {
                // could be during initialization of xaml that presentationgroups was not yet defined
                if (VisualStates.TryGetVisualStateGroup(source, PresentationGroup) == null)
                {
                    // will delay check
                    source.CurrentTransition = null;
                }
                else
                {
                    // revert to old value
                    source.SetValue(TransitionProperty, oldTransition);

                    throw new ArgumentException(
                        string.Format(CultureInfo.CurrentCulture, "Temporary removed exception message", newTransition));
                }
            }
            else
            {
                source.CurrentTransition = newStoryboard;
            }
        }

        private static void OnRestartTransitionOnContentChangePropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ((TransitioningContentControl)d).OnRestartTransitionOnContentChangeChanged((bool)e.OldValue,
                (bool)e.NewValue);
        }

        protected virtual void OnRestartTransitionOnContentChangeChanged(bool oldValue, bool newValue)
        {
        }

        public override void OnApplyTemplate()
        {
            if (IsTransitioning)
            {
                AbortTransition();
            }

            if (CustomVisualStates != null && CustomVisualStates.Any())
            {
                VisualStateGroup presentationGroup = VisualStates.TryGetVisualStateGroup(this, PresentationGroup);
                if (presentationGroup != null)
                {
                    foreach (VisualState state in CustomVisualStates)
                    {
                        presentationGroup.States.Add(state);
                    }
                }
            }

            base.OnApplyTemplate();

            PreviousContentPresentationSite =
                GetTemplateChild(PreviousContentPresentationSitePartName) as ContentPresenter;
            CurrentContentPresentationSite =
                GetTemplateChild(CurrentContentPresentationSitePartName) as ContentPresenter;

            if (CurrentContentPresentationSite != null)
            {
                CurrentContentPresentationSite.Content = Content;
            }

            // hookup currenttransition
            Storyboard transition = GetStoryboard(Transition);
            CurrentTransition = transition;
            if (transition == null)
            {
                string invalidTransition = Transition;
                // revert to default
                Transition = DefaultTransitionState;

                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, "Temporary removed exception message", invalidTransition));
            }
            VisualStateManager.GoToState(this, NormalState, false);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            StartTransition(oldContent, newContent);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "newContent",
            Justification = "Should be used in the future.")]
        private void StartTransition(object oldContent, object newContent)
        {
            // both presenters must be available, otherwise a transition is useless.
            if (CurrentContentPresentationSite != null && PreviousContentPresentationSite != null)
            {
                if (RestartTransitionOnContentChange)
                {
                    CurrentTransition.Completed -= OnTransitionCompleted;
                }

                CurrentContentPresentationSite.Content = newContent;

                PreviousContentPresentationSite.Content = oldContent;

                // and start a new transition
                if (!IsTransitioning || RestartTransitionOnContentChange)
                {
                    if (RestartTransitionOnContentChange)
                    {
                        CurrentTransition.Completed += OnTransitionCompleted;
                    }
                    IsTransitioning = true;
                    VisualStateManager.GoToState(this, NormalState, false);
                    VisualStateManager.GoToState(this, Transition, true);
                }
            }
        }

        private void OnTransitionCompleted(object sender, EventArgs e)
        {
            AbortTransition();

            RoutedEventHandler handler = TransitionCompleted;
            if (handler != null)
            {
                handler(this, new RoutedEventArgs());
            }
        }

        public void AbortTransition()
        {
            // go to normal state and release our hold on the old content.
            VisualStateManager.GoToState(this, NormalState, false);
            IsTransitioning = false;
            if (PreviousContentPresentationSite != null)
            {
                PreviousContentPresentationSite.Content = null;
            }
        }

        private Storyboard GetStoryboard(string newTransition)
        {
            VisualStateGroup presentationGroup = VisualStates.TryGetVisualStateGroup(this, PresentationGroup);
            Storyboard newStoryboard = null;
            if (presentationGroup != null)
            {
                newStoryboard = presentationGroup.States
                    .OfType<VisualState>()
                    .Where(state => state.Name == newTransition)
                    .Select(state => state.Storyboard)
                    .FirstOrDefault();
            }
            return newStoryboard;
        }
    }

    internal static class VisualStates
    {
        public const string GroupCommon = "CommonStates";
        public const string StateNormal = "Normal";
        public const string StateReadOnly = "ReadOnly";
        public const string StateMouseOver = "MouseOver";
        public const string StatePressed = "Pressed";
        public const string StateDisabled = "Disabled";
        public const string GroupFocus = "FocusStates";
        public const string StateUnfocused = "Unfocused";
        public const string StateFocused = "Focused";
        public const string GroupSelection = "SelectionStates";
        public const string StateSelected = "Selected";
        public const string StateUnselected = "Unselected";
        public const string StateSelectedInactive = "SelectedInactive";
        public const string GroupExpansion = "ExpansionStates";
        public const string StateExpanded = "Expanded";
        public const string StateCollapsed = "Collapsed";
        public const string GroupPopup = "PopupStates";
        public const string StatePopupOpened = "PopupOpened";
        public const string StatePopupClosed = "PopupClosed";
        public const string GroupValidation = "ValidationStates";
        public const string StateValid = "Valid";
        public const string StateInvalidFocused = "InvalidFocused";
        public const string StateInvalidUnfocused = "InvalidUnfocused";
        public const string GroupExpandDirection = "ExpandDirectionStates";
        public const string StateExpandDown = "ExpandDown";
        public const string StateExpandUp = "ExpandUp";
        public const string StateExpandLeft = "ExpandLeft";
        public const string StateExpandRight = "ExpandRight";
        public const string GroupHasItems = "HasItemsStates";
        public const string StateHasItems = "HasItems";
        public const string StateNoItems = "NoItems";
        public const string GroupIncrease = "IncreaseStates";
        public const string StateIncreaseEnabled = "IncreaseEnabled";
        public const string StateIncreaseDisabled = "IncreaseDisabled";
        public const string GroupDecrease = "DecreaseStates";
        public const string StateDecreaseEnabled = "DecreaseEnabled";
        public const string StateDecreaseDisabled = "DecreaseDisabled";
        public const string GroupInteractionMode = "InteractionModeStates";
        public const string StateEdit = "Edit";
        public const string StateDisplay = "Display";
        public const string GroupLocked = "LockedStates";
        public const string StateLocked = "Locked";
        public const string StateUnlocked = "Unlocked";
        public const string StateActive = "Active";
        public const string StateInactive = "Inactive";
        public const string GroupActive = "ActiveStates";
        public const string StateUnwatermarked = "Unwatermarked";
        public const string StateWatermarked = "Watermarked";
        public const string GroupWatermark = "WatermarkStates";
        public const string StateCalendarButtonUnfocused = "CalendarButtonUnfocused";
        public const string StateCalendarButtonFocused = "CalendarButtonFocused";
        public const string GroupCalendarButtonFocus = "CalendarButtonFocusStates";
        public const string StateBusy = "Busy";
        public const string StateIdle = "Idle";
        public const string GroupBusyStatus = "BusyStatusStates";
        public const string StateVisible = "Visible";
        public const string StateHidden = "Hidden";
        public const string GroupVisibility = "VisibilityStates";

        public static void GoToState(Control control, bool useTransitions, params string[] stateNames)
        {
            Debug.Assert(control != null, "control should not be null!");
            Debug.Assert(stateNames != null, "stateNames should not be null!");
            Debug.Assert(stateNames.Length > 0, "stateNames should not be empty!");

            foreach (string name in stateNames)
            {
                if (VisualStateManager.GoToState(control, name, useTransitions))
                {
                    break;
                }
            }
        }

        public static FrameworkElement GetImplementationRoot(DependencyObject dependencyObject)
        {
            Debug.Assert(dependencyObject != null, "DependencyObject should not be null.");
            return (1 == VisualTreeHelper.GetChildrenCount(dependencyObject))
                ? VisualTreeHelper.GetChild(dependencyObject, 0) as FrameworkElement
                : null;
        }

        public static VisualStateGroup TryGetVisualStateGroup(DependencyObject dependencyObject, string groupName)
        {
            FrameworkElement root = GetImplementationRoot(dependencyObject);
            if (root == null)
            {
                return null;
            }

            return VisualStateManager.GetVisualStateGroups(root)
                .OfType<VisualStateGroup>().FirstOrDefault(group => string.CompareOrdinal(groupName, @group.Name) == 0);
        }
    }
}
