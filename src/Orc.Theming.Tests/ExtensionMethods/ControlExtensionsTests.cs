namespace Orc.Theming.Tests.Services;

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;

[TestFixture]
public class ControlExtensionsTests
{
    [Test]
    [Apartment(ApartmentState.STA)]
    public void Get_Required_Template_Should_Return_Template_If_It_Exist()
    {
        const string partName = "PART_Border";

        //Prepare
        var button = PrepareControlWithBorderPart(partName);

        //Act
        var border = button.GetRequiredTemplateChild<Border>(partName);

        Assert.That(border, Is.Not.Null);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void Get_Required_Template_Should_Fail_With_InvalidOperationException_If_Template_Does_Not_Exist()
    {
        const string partName = "PART_Border";
        string differentPartName = partName + "1";

        //Prepare
        var button = PrepareControlWithBorderPart(partName);

        //Act - Assert
        Assert.Throws<InvalidOperationException>(() => button.GetRequiredTemplateChild<Border>(differentPartName));
    }

    private Button PrepareControlWithBorderPart(string partName)
    {
        var button = new Button();

        var style = new Style();
        var templateButton = new ControlTemplate(typeof(Button));
        var elemFactory = new FrameworkElementFactory(typeof(Border))
        {
            Name = partName
        };

        templateButton.VisualTree = elemFactory;
        elemFactory.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));
        style.Setters.Add(new Setter
        {
            Property = Control.TemplateProperty, 
            Value = templateButton
        });

        button.Style = style;

        button.ApplyTemplate();

        return button;
    }
}
