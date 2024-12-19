using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

public class McpLootEntry
{
    public string ItemType { get; set; }
    public string ItemGuid { get; set; }
    public int Quantity { get; set; }
    public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();
    public string ItemProfile { get; set; }
}

[Editor(typeof(McpLootResultUITypeEditor), typeof(UITypeEditor))]
public class McpLootResult
{
    public string TierGroupName { get; set; }
    public List<McpLootEntry> Items { get; set; } = new List<McpLootEntry>();
}

public class McpLootResultEditForm : Form
{
    private TextBox txtTierGroupName;
    private DataGridView dgvItems;
    private Button btnAddItem;
    private Button btnEditItem;
    private Button btnDeleteItem;
    private Button btnOK;
    private Button btnCancel;

    public McpLootResult EditedLootResult { get; private set; }

    public McpLootResultEditForm(McpLootResult lootResult)
    {
        EditedLootResult = lootResult;
        this.Text = "Edit McpLootResult";
        this.Size = new Size(600, 400);
        this.StartPosition = FormStartPosition.CenterParent;

        var tableLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10)
        };

        var tierGroupPanel = new FlowLayoutPanel { Dock = DockStyle.Top, FlowDirection = FlowDirection.LeftToRight };
        tierGroupPanel.Controls.Add(new Label { Text = "Tier Group Name:", Width = 120, TextAlign = ContentAlignment.MiddleRight });
        txtTierGroupName = new TextBox { Width = 400, Text = lootResult.TierGroupName };
        tierGroupPanel.Controls.Add(txtTierGroupName);

        dgvItems = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            ReadOnly = true,
            AllowUserToAddRows = false
        };

        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Item Type", DataPropertyName = "ItemType", Width = 150 });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 100 });
        //dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Item Profile", DataPropertyName = "ItemProfile", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

        dgvItems.DataSource = lootResult.Items.ToList();

        var buttonPanel = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, Dock = DockStyle.Top, Padding = new Padding(5) };
        btnAddItem = new Button { Text = "Add Item" };
        btnEditItem = new Button { Text = "Edit Item" };
        btnDeleteItem = new Button { Text = "Delete Item" };
        btnOK = new Button { Text = "OK", DialogResult = DialogResult.OK };
        btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };

        buttonPanel.Controls.Add(btnAddItem);
        buttonPanel.Controls.Add(btnEditItem);
        buttonPanel.Controls.Add(btnDeleteItem);
        buttonPanel.Controls.Add(btnOK);
        buttonPanel.Controls.Add(btnCancel);

        btnAddItem.Click += (s, e) => AddItem();
        btnEditItem.Click += (s, e) => EditItem();
        btnDeleteItem.Click += (s, e) => DeleteItem();
        btnOK.Click += (s, e) => SaveChanges();

        tableLayout.Controls.Add(tierGroupPanel, 0, 0);
        tableLayout.Controls.Add(dgvItems, 0, 1);
        tableLayout.Controls.Add(buttonPanel, 0, 2);

        this.Controls.Add(tableLayout);
    }

    private void AddItem()
    {
        var newEntry = new McpLootEntry();
        using (var editorForm = new McpLootEntryEditForm(newEntry))
        {
            if (editorForm.ShowDialog() == DialogResult.OK)
            {
                EditedLootResult.Items.Add(editorForm.EditedLootEntry);
                dgvItems.DataSource = EditedLootResult.Items.ToList();
            }
        }
    }

    private void EditItem()
    {
        if (dgvItems.CurrentRow?.DataBoundItem is McpLootEntry selectedEntry)
        {
            using (var editorForm = new McpLootEntryEditForm(selectedEntry))
            {
                if (editorForm.ShowDialog() == DialogResult.OK)
                {
                    dgvItems.DataSource = EditedLootResult.Items.ToList();
                }
            }
        }
    }

    private void DeleteItem()
    {
        if (dgvItems.CurrentRow?.DataBoundItem is McpLootEntry selectedEntry)
        {
            EditedLootResult.Items.Remove(selectedEntry);
            dgvItems.DataSource = EditedLootResult.Items.ToList();
        }
    }

    private void SaveChanges()
    {
        EditedLootResult.TierGroupName = txtTierGroupName.Text;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}

public class McpLootEntryEditForm : Form
{
    private TextBox txtItemType;
    private TextBox txtItemGuid;
    private NumericUpDown numQuantity;
    private TextBox txtItemProfile;
    private Button btnOK;
    private Button btnCancel;

    public McpLootEntry EditedLootEntry { get; private set; }

    public McpLootEntryEditForm(McpLootEntry entry)
    {
        EditedLootEntry = entry;

        this.Text = "Edit McpLootEntry";
        this.Size = new Size(400, 300);
        this.StartPosition = FormStartPosition.CenterParent;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10), RowCount = 6, ColumnCount = 2 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        txtItemType = new TextBox { Text = entry.ItemType };
        txtItemGuid = new TextBox { Text = entry.ItemGuid };
        numQuantity = new NumericUpDown { Value = entry.Quantity, Minimum = 1, Maximum = 999999 };
        txtItemProfile = new TextBox { Text = entry.ItemProfile };

        btnOK = new Button { Text = "OK", DialogResult = DialogResult.OK };
        btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };

        layout.Controls.Add(new Label { Text = "Item Type:" }, 0, 0);
        layout.Controls.Add(txtItemType, 1, 0);

        layout.Controls.Add(new Label { Text = "Item GUID:" }, 0, 1);
        layout.Controls.Add(txtItemGuid, 1, 1);

        layout.Controls.Add(new Label { Text = "Quantity:" }, 0, 2);
        layout.Controls.Add(numQuantity, 1, 2);

        layout.Controls.Add(new Label { Text = "Item Profile:" }, 0, 3);
        layout.Controls.Add(txtItemProfile, 1, 3);

        var buttonPanel = new FlowLayoutPanel { Dock = DockStyle.Bottom, FlowDirection = FlowDirection.RightToLeft };
        buttonPanel.Controls.Add(btnOK);
        buttonPanel.Controls.Add(btnCancel);

        this.Controls.Add(layout);
        this.Controls.Add(buttonPanel);

        numQuantity.Size = new Size(70, 20);
        txtItemGuid.Size = new Size(200, 20);
        txtItemType.Size = new Size(200, 20);

        btnOK.Click += (s, e) =>
        {
            EditedLootEntry.ItemType = txtItemType.Text;
            EditedLootEntry.ItemGuid = txtItemGuid.Text;
            EditedLootEntry.Quantity = (int)numQuantity.Value;
            EditedLootEntry.ItemProfile = txtItemProfile.Text;
            this.DialogResult = DialogResult.OK;
        };
    }
}

public class McpLootResultUITypeEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
    {
        IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

        if (editorService != null && value is McpLootResult lootResult)
        {
            using (var editorForm = new McpLootResultEditForm(lootResult))
            {
                if (editorService.ShowDialog(editorForm) == DialogResult.OK)
                {
                    value = editorForm.EditedLootResult;
                }
            }
        }

        return value;
    }
}

