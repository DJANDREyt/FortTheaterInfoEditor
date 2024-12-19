using DarkModeForms;
using FortTheaterInfoEditor;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms.PropertyGridInternal;

public class DataTableRowHandleEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        if (provider == null || value == null)
            return value;

        IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

        if (editorService != null)
        {
            var DataTableRowHandlee = value as DataTableRowHandle;

            using (DataTableRowHandleEditForm editorForm = new DataTableRowHandleEditForm(DataTableRowHandlee))
            {
                if (editorService.ShowDialog(editorForm) == DialogResult.OK)
                {
                    value = editorForm.EditedDataTableRowHandle;
                }
            }
        }

        return value;
    }
}

public class DataTableRowHandleEditForm : Form
{
    private RichTextBox RowNameTextBox;
    private RichTextBox DataTableTextBox;
    private Button buttonOK;
    private Button buttonCancel;

    public DataTableRowHandle EditedDataTableRowHandle { get; private set; }

    public DataTableRowHandleEditForm(DataTableRowHandle handle)
    {
        EditedDataTableRowHandle = new DataTableRowHandle
        {
            DataTable = handle.DataTable,
            RowName = handle.RowName
        };

        InitializeComponents();
        LoadProperties();

        /*DarkModeCS darkmode = new DarkModeCS(this)
        {
            ColorMode = EditorWindow.Settings.DarkMode ? DarkModeCS.DisplayMode.DarkMode : DarkModeCS.DisplayMode.ClearMode,
        };*/

    }

    private void InitializeComponents()
    {
        DataTableTextBox = new RichTextBox { Dock = DockStyle.Top, Height = 40 };
        RowNameTextBox = new RichTextBox { Dock = DockStyle.Top, Height = 40 };

        buttonOK = new Button { Text = "OK", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };
        buttonCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Dock = DockStyle.Bottom };


        DataTableTextBox.TextChanged += (s, e) => OnDataTableTextChanged();
        RowNameTextBox.TextChanged += (s, e) => OnRowNameTextChanged();

        Controls.Add(DataTableTextBox);
        Controls.Add(RowNameTextBox);
        Controls.Add(buttonOK);
        Controls.Add(buttonCancel);

        Text = "DataTable Row Handle Editor";
        Size = new System.Drawing.Size(400, 300);
    }

    private void OnDataTableTextChanged()
    {
        EditedDataTableRowHandle.DataTable = DataTableTextBox.Text;
    }

    private void OnRowNameTextChanged()
    {
        EditedDataTableRowHandle.RowName = RowNameTextBox.Text;
    }

    private void LoadProperties()
    {
        DataTableTextBox.Text = EditedDataTableRowHandle.DataTable;
        RowNameTextBox.Text = EditedDataTableRowHandle.RowName;
    }
}

[Editor(typeof(DataTableRowHandleEditor), typeof(UITypeEditor))]
public class DataTableRowHandle
{
    public string DataTable { get; set; }
    public string RowName { get; set; }
}