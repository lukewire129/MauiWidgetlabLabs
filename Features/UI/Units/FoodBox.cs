namespace MauiWidgetlabLabs.Features.UI.Units;

public class FoodBox : TemplatedView
{
    SetupButton SetupButton;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();
        SetupButton = GetTemplateChild ("PART_Setup") as SetupButton;

      SetupButton.EditCommand = new Command ((e) =>
        {
            bool commandParm = (bool)e;
            if (commandParm == true)
            {
                new Animation (e=>                
                {
                    this.HeightRequest = e;   
                },100, 200)
                .Commit (this, "height", length: 500);
            }
            else
            {
                new Animation (e =>
                {
                    this.HeightRequest = e;
                }, 200, 100)
                .Commit (this, "height", length: 350);
            }
        });
    }
}
