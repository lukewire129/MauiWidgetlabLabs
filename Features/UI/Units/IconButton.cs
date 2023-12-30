using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace MauiWidgetlabLabs.Features.UI.Units;

public class IconButton : TemplatedView
{
    public static readonly BindableProperty PathDataProperty =
            BindableProperty.Create (nameof (PathData), typeof (Geometry), typeof (IconButton), null);

    [System.ComponentModel.TypeConverter (typeof (PathGeometryConverter))]
    public Geometry PathData
    {
        get => (Geometry)GetValue (PathDataProperty);
        set { SetValue (PathDataProperty, value); }
    }

    public static new readonly BindableProperty XProperty =
            BindableProperty.Create (nameof (X), typeof (double), typeof (IconButton), null);

    public new double X
    {
        get => (double)GetValue (XProperty);
        set { SetValue (XProperty, value); }
    }

    public static new readonly BindableProperty YProperty =
            BindableProperty.Create (nameof (Y), typeof (double), typeof (IconButton), null);

    public new double Y
    {
        get => (double)GetValue (YProperty);
        set { SetValue (YProperty, value); }
    }

    public static readonly BindableProperty PathColorProperty =
            BindableProperty.Create (nameof (PathColor), typeof (Brush), typeof (IconButton), null);

    [TypeConverter (typeof (BrushTypeConverter))]
    public Brush PathColor
    {
        get { return (Brush)GetValue (PathColorProperty); }
        set { SetValue (PathColorProperty, value); }
    }

    public new static readonly BindableProperty BackgroundProperty =
            BindableProperty.Create (nameof (Background), typeof (Color), typeof (IconButton), null);

    public new Color Background
    {
        get { return (Color)GetValue (BackgroundProperty); }
        set { SetValue (BackgroundProperty, value); }
    }
    public Microsoft.Maui.Controls.Shapes.Path shape;
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate ();
        shape = GetTemplateChild ("PART_Path") as Microsoft.Maui.Controls.Shapes.Path;
    }
}
