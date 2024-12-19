using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

public class GameplayTagContainerEditor : UITypeEditor
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
            var tagContainer = value as GameplayTagContainer;

            using (GameplayTagEditorForm editorForm = new GameplayTagEditorForm(tagContainer))
            {
                if (editorService.ShowDialog(editorForm) == DialogResult.OK)
                {
                    value = editorForm.EditedTagContainer;
                }
            }
        }

        return value;
    }
}

public class GameplayTagEditorForm : Form
{
    private ListBox listBoxTags;
    private Button buttonAdd;
    private Button buttonRemove;
    private Button buttonOK;
    private Button buttonCancel;

    public GameplayTagContainer EditedTagContainer { get; private set; }

    public GameplayTagEditorForm(GameplayTagContainer tagContainer)
    {
        EditedTagContainer = new GameplayTagContainer
        {
            GameplayTags = new List<GameplayTag>(tagContainer.GameplayTags),
            ParentTags = new List<GameplayTag>(tagContainer.ParentTags)
        };

        InitializeComponents();
        LoadTags();
    }

    private void InitializeComponents()
    {
        listBoxTags = new ListBox { Dock = DockStyle.Top, Height = 150 };

        buttonAdd = new Button { Text = "Add", Dock = DockStyle.Left, Width = 75 };
        buttonRemove = new Button { Text = "Remove", Dock = DockStyle.Left, Width = 75 };
        buttonOK = new Button { Text = "OK", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };
        buttonCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Dock = DockStyle.Bottom };

        buttonAdd.Click += (s, e) => AddTag();
        buttonRemove.Click += (s, e) => RemoveSelectedTag();

        Controls.Add(listBoxTags);
        Controls.Add(buttonAdd);
        Controls.Add(buttonRemove);
        Controls.Add(buttonOK);
        Controls.Add(buttonCancel);

        Text = "Gameplay Tags Editor";
        Size = new System.Drawing.Size(400, 300);
    }

    private void LoadTags()
    {
        listBoxTags.Items.Clear();
        foreach (var tag in EditedTagContainer.GameplayTags)
        {
            listBoxTags.Items.Add(tag.TagName);
        }
    }

    private void AddTag()
    {
        var input = Microsoft.VisualBasic.Interaction.InputBox("Enter a new tag name:", "Add Tag", "");
        if (!string.IsNullOrWhiteSpace(input))
        {
            EditedTagContainer.GameplayTags.Add(new GameplayTag { TagName = input });
            LoadTags();
        }
    }

    private void RemoveSelectedTag()
    {
        if (listBoxTags.SelectedIndex >= 0)
        {
            EditedTagContainer.GameplayTags.RemoveAt(listBoxTags.SelectedIndex);
            LoadTags();
        }
    }
}