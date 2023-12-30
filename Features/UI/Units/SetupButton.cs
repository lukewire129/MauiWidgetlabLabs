using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiWidgetlabLabs.Features.UI.Units;

public enum StateType
{
    Normal,
    Setup
}

public class SetupButton : TemplatedView
{
    public static readonly BindableProperty EditCommandProperty =
            BindableProperty.Create (nameof (EditCommand), typeof (ICommand), typeof (SetupButton), null);

    public ICommand EditCommand
    {
        get => (ICommand)GetValue (EditCommandProperty);
        set => SetValue (EditCommandProperty, value);
    }

    public static readonly BindableProperty StateProperty =
            BindableProperty.Create (nameof (State), typeof (StateType), typeof (SetupButton), StateType.Normal);

    public StateType State
    {
        get => (StateType)GetValue (StateProperty);
        set => SetValue (StateProperty, value);
    }
    private IconButton trashIcon;
    private IconButton OpenButton;
    private Grid SlideArea;
    private Button EditButton;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();
        trashIcon = GetTemplateChild ("PART_TrashIcon") as IconButton;
        SlideArea = GetTemplateChild ("PART_Area") as Grid;
        OpenButton = GetTemplateChild ("PART_OpenIcon") as IconButton;
        EditButton = GetTemplateChild ("PART_Edit") as Button;
        if (OpenButton is not null)
        {
            var gesture = new TapGestureRecognizer ();
            OpenButton.GestureRecognizers.Add (gesture);

            gesture.Tapped += (s, e) =>
            {
                if(State == StateType.Normal)
                {
                    State = StateType.Setup;

                    Animation animation = new Animation (e =>
                    {
                        SlideArea.TranslationX = e;
                    }, this.Width * 2, 0)
                    .WithConcurrent(new Animation(e=>
                    {
                        OpenButton.Rotation = e;
                    },-30,0,Easing.Default));
                    animation.Commit (this,"slide", easing: Easing.SpringOut);
                    return;
                }
                Animation animation2 = new Animation ((e) =>
                {
                    SlideArea.TranslationX = e;
                }, 0, this.Width * 2)
                 .WithConcurrent (new Animation (e =>
                 {
                     OpenButton.Rotation = e;
                 }, 0,-30, Easing.Default, ()=>
                 {
                     OpenButton.Rotation = 0;
                 }));
                animation2.Commit (this, "slide2",16, 500, finished: (d, e) =>
                {
                    State = StateType.Normal;
                });
            };
        }

        if (EditButton is not null)
        {
            var gesture = new TapGestureRecognizer ();
            EditButton.GestureRecognizers.Add (gesture);
            gesture.Tapped += (s, e) =>
            {
                Animation animation = new Animation ((e) =>
                {
                    OpenButton.Opacity = e;
                }, 1, 0, finished: () =>
                {
                    OpenButton.IsVisible = false;
                    OpenButton.Opacity = 1;
                })
                .WithConcurrent (new Animation ((e) =>
                {
                    trashIcon.WidthRequest = e;
                }, trashIcon.Width, 0, finished: () =>
                {
                    trashIcon.IsVisible = false;
                    trashIcon.Opacity = 1;
                }));
                animation.Commit (this, "aa");
                EditCommand?.Execute (null);
                SlideArea.TranslateTo (50, 100);
            };
        }
    }
}
