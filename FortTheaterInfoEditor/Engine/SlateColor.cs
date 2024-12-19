using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;

[Editor(typeof(SlateColorEditor), typeof(UITypeEditor))]
public class SlateColor
{
    public LinearColor SpecifiedColor { get; set; }
    public SlateColorStylingMode ColorUseRule { get; set; }
}

public class LinearColor
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }
}

public enum SlateColorStylingMode
{
    UseColor_Specified = 0,
    UseColor_Specified_Link = 1,
    UseColor_Foreground = 2,
    UseColor_Foreground_Subdued = 3
}

public class SlateColorEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        if (value is SlateColor slateColor)
        {
            using (var editorForm = new SlateColorEditorForm(slateColor))
            {
                var result = editorForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return editorForm.SlateColor;
                }
            }
        }
        return value;
    }
}
public class SlateColorEditorForm : Form
{
    private ComboBox stylingModeComboBox;
    private Button colorButton;
    private ColorDialog colorDialog;
    private SlateColor slateColor;

    public SlateColor SlateColor => slateColor;

    public SlateColorEditorForm(SlateColor slateColor)
    {
        this.slateColor = slateColor;
        InitializeComponents();
        PopulateControls();
    }

    private void InitializeComponents()
    {
        stylingModeComboBox = new ComboBox
        {
            Dock = DockStyle.Top,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        stylingModeComboBox.Items.AddRange(Enum.GetNames(typeof(SlateColorStylingMode)));
        stylingModeComboBox.SelectedIndexChanged += StylingModeComboBox_SelectedIndexChanged;

        colorButton = new Button
        {
            Text = "Choose Color",
            Dock = DockStyle.Top
        };
        colorDialog = new ColorDialog();

        colorButton.Click += ColorButton_Click;

        Controls.Add(colorButton);
        Controls.Add(stylingModeComboBox);

        Text = "SlateColor Editor";
        Size = new Size(300, 150);
    }

    private void PopulateControls()
    {
        stylingModeComboBox.SelectedIndex = (int)slateColor.ColorUseRule;

        colorButton.BackColor = Color.FromArgb(
            (int)(slateColor.SpecifiedColor.R * 255),
            (int)(slateColor.SpecifiedColor.G * 255),
            (int)(slateColor.SpecifiedColor.B * 255)
        );
    }

    private void StylingModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        slateColor.ColorUseRule = (SlateColorStylingMode)stylingModeComboBox.SelectedIndex;
    }

    private void ColorButton_Click(object sender, EventArgs e)
    {
        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            colorButton.BackColor = colorDialog.Color;
            slateColor.SpecifiedColor = new LinearColor
            {
                R = colorDialog.Color.R / 255f,
                G = colorDialog.Color.G / 255f,
                B = colorDialog.Color.B / 255f,
                A = colorDialog.Color.A / 255f
            };
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
    }
}
