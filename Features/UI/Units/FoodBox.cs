using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiWidgetlabLabs.Features.UI.Units;

public class FoodBox : TemplatedView
{
    SetupButton SetupButton;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();
        SetupButton = GetTemplateChild ("PART_Setup") as SetupButton;

      SetupButton.EditCommand = new Command (() =>
        {
            this.HeightRequest = 200;
        });
    }
}
