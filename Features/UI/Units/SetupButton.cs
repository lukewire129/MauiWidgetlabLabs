using System.Runtime.CompilerServices;
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
    private Button EditButton;
    private Grid slidArea;
    private bool isEditMode = false;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();
        trashIcon = GetTemplateChild ("PART_TrashIcon") as IconButton;
        OpenButton = GetTemplateChild ("PART_OpenIcon") as IconButton;
        EditButton = GetTemplateChild ("PART_Edit") as Button;
        slidArea = GetTemplateChild ("PART_SlidArea") as Grid;
        if (OpenButton is not null)
        {
            var gesture = new TapGestureRecognizer ();
            OpenButton.GestureRecognizers.Add (gesture);

            gesture.Tapped += (s, e) =>
            {
                if (State == StateType.Normal)
                {
                    State = StateType.Setup;
                    slidArea.TranslationX = 150;
                    OpenButton.Rotation = -30;
                    slidArea.TranslateTo (0, 0, 350);
                    OpenButton.RotateTo (0);

                    return;
                }
                Animation animation2 = new Animation ()
                .WithConcurrent (new Animation ((e) =>
                {
                    slidArea.TranslationX = e;
                }, 0, 200))
                 .WithConcurrent (new Animation (e =>
                 {
                     OpenButton.Rotation = e;
                 }, 0, -30, Easing.Default, () =>
                 {
                     OpenButton.Rotation = 0;
                 }));
                animation2.Commit (this, "slide2", 16, 500, finished: (d, e) =>
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
                if(isEditMode == false)
                {
                    isEditMode = true;
                    OpenButton.FadeTo (0, 200);
                    trashIcon.FadeTo (0, 200);
                    slidArea.TranslateTo(70, 0,500);
                    OpenButton.TranslateTo (-200, 100, 350);
                    trashIcon.TranslateTo (0, 100, 350);
                    EditButton.TranslateTo (-20, 60, 500);
                    EditButton.Text = "Done";
                }
                else
                {
                    isEditMode = false;
                    OpenButton.TranslationX = 0;
                    OpenButton.FadeTo (1, 200);
                    State = StateType.Normal;
                    OpenButton.TranslateTo (0, 0, 350);
                    trashIcon.Opacity = 1;
                    trashIcon.TranslationX = 0;
                    trashIcon.TranslationY = 0;
                    EditButton.TranslationX = 0;
                    EditButton.TranslationY = 0;
                    EditButton.Text = "Edit";
                }
                EditCommand?.Execute (isEditMode);
            };
        }
    }
}
