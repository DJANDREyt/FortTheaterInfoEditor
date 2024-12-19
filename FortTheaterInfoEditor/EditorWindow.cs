using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using DarkModeForms;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Versions;
using Newtonsoft.Json;
using CUE4Parse.UE4.Assets.Exports.Texture;
using CUE4Parse_Conversion.Textures;
using SkiaSharp;
using CUE4Parse.Encryption.Aes;
using CUE4Parse.UE4.Objects.Core.Misc;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Converters;

namespace FortTheaterInfoEditor
{
    public partial class EditorWindow : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_SHOW = 5;

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        private static readonly string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            + @"\FortTheaterInfoEditor\Settings.json";

        public static Settings Settings { get; set; } = new Settings();

        public static FortActiveTheaterInfo TheaterInfo { get; set; }

        private DarkModeCS DarkModeManager = null;

        private SKBitmap SelectedTexture = null;

        private DefaultFileProvider FileProvider = null;

        private ImageViewerPanel ImagePanel = null;

        private bool IsModified = false;

        public EditorWindow()
        {
            InitializeComponent();
            AllocConsole();

            IntPtr consoleHandle = GetConsoleWindow();
            if (consoleHandle != IntPtr.Zero)
            {
                ShowWindow(consoleHandle, SW_SHOW);
            }

            LoadSettings();

            foreach (EGame version in Enum.GetValues(typeof(EGame)))
            {
                ToolStripMenuItem versionMenuItem = new ToolStripMenuItem(version.ToString())
                {
                    Tag = version
                };
                versionMenuItem.Click += VersionMenuItem_Click;

                uEVersionToolStripMenuItem.DropDownItems.Add(versionMenuItem);
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(Settings.Culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Culture);

            DarkModeManager = new DarkModeCS(this)
            {
                ColorMode = Settings.DarkMode ? DarkModeCS.DisplayMode.DarkMode : DarkModeCS.DisplayMode.ClearMode
            };

            darkModeToolStripMenuItem.Checked = Settings.DarkMode;
            loadLastLoadedJsonOnLoadToolStripMenuItem.Checked = Settings.LoadLastLoadedJsonOnLoad;

            propertyGrid1.SelectedGridItemChanged += async (sender, e) => await OnSelectedGridItemChanged(sender, e);

            propertyGrid1.PropertyValueChanged += (sender, e) => OnPropertyValueChanged(sender, e);

            ImagePanel = new ImageViewerPanel
            {
                Name = "imagePanel",
                Dock = DockStyle.Fill,
                BackColor = Color.Black
            };

            ImageViewerPanel.Controls.Add(ImagePanel);
        }

        private void LoadSettings()
        {
            if (!File.Exists(SettingsPath))
            {
                string DefaultSettings = JsonConvert.SerializeObject(Settings, Formatting.Indented);
                File.WriteAllText(SettingsPath, DefaultSettings);
            }
            else
            {
                try
                {
                    string SettingsFileContent = File.ReadAllText(SettingsPath);
                    Settings = JsonConvert.DeserializeObject<Settings>(SettingsFileContent);
                }
                catch (Exception ex)
                {
                    AddLog($"Error loading settings: {ex.Message}");
                }

            }
        }

        private void SaveSettings()
        {
            string SettingsContent = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            File.WriteAllText(SettingsPath, SettingsContent);
        }


        public void AddLog(string message)
        {
            logTextBox.AppendText($"{DateTime.Now}: {message}\n");
        }

        private void loadJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Open World Info JSON File",
                Filter = "JSON Files (*.json)|*.json",
                InitialDirectory = Settings.LastOpenedLoadDirectory
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Settings.LastOpenedLoadDirectory = Path.GetDirectoryName(openFileDialog.FileName);

                    Settings.LastLoadedJson = openFileDialog.FileName;
                    string jsonContent = File.ReadAllText(openFileDialog.FileName);

                    TheaterInfo = JsonConvert.DeserializeObject<FortActiveTheaterInfo>(jsonContent);

                    AddLog("Json Loaded!");

                    PopulateTreeView();
                }
                catch (Exception ex)
                {
                    AddLog($"Error loading JSON: {ex.Message}");
                }
            }
        }

        private void PopulateTreeView()
        {
            treeView.Nodes.Clear();

            TreeNode TheatersNode = new TreeNode($"Theaters({TheaterInfo.Theaters.Count})")
            {
                Tag = TheaterInfo.Theaters
            };
            foreach (var theater in TheaterInfo.Theaters)
            {

                TheatersNode.Nodes.Add(PopulateTheaterView(theater));

            }
            TreeNode MissionsNode = new TreeNode($"Missions({TheaterInfo.Missions.Count})")
            {
                Tag = TheaterInfo.Missions
            };
            foreach (var mission in TheaterInfo.Missions)
            {

                MissionsNode.Nodes.Add(PopulateMissionsView(mission));

            }

            treeView.Nodes.Add(TheatersNode);
            treeView.Nodes.Add(MissionsNode);
            //treeView.ExpandAll();
        }
        private TreeNode PopulateMissionsView(FortAvailableTheaterMissions TheaterMissions)
        {
            string TheaterName = Utils.GetTheaterNameById(TheaterMissions.TheaterId);
            TreeNode TheaterMissionsNode = new TreeNode($"{TheaterName}")
            {
                Tag = TheaterMissions
            };

            TreeNode AvailableMissionsNode = new TreeNode($"AvailableMissions({TheaterMissions.AvailableMissions.Count})")
            {
                Tag = TheaterMissions.AvailableMissions
            };

            foreach(var AvailableMission in TheaterMissions.AvailableMissions)
            {
                AvailableMissionsNode.Nodes.Add(PopulateAvailableMissionDataView(AvailableMission));
            }

            TheaterMissionsNode.Nodes.Add(AvailableMissionsNode);
            return TheaterMissionsNode;
        }

        private TreeNode PopulateAvailableMissionDataView(FortAvailableMissionData AvailableMissionData)
        {
            string NodeName = $"MissionGuid ${AvailableMissionData.MissionGuid}";
            if (!string.IsNullOrEmpty(AvailableMissionData.MissionGenerator))
            {
                string[] SplitString = AvailableMissionData.MissionGenerator.Split(".");
                if (SplitString.Length > 1)
                {
                    string GeneratorName = SplitString[1];
                    NodeName = GeneratorName.Replace("_C", "");
                }


            }
            TreeNode AvaiableMissioNode = new TreeNode($"MissionGenerator '{NodeName}'")
            {
                Tag = AvailableMissionData
            };

            return AvaiableMissioNode;
        }

        private TreeNode PopulateTheaterView(FortTheaterMapData theater)
        {
            TreeNode theaterNode = new TreeNode(theater.DisplayName)
            {
                Tag = theater
            };

            TreeNode tilesNode = new TreeNode($"Tiles({theater.Tiles.Count})")
            {
                Tag = theater.Tiles
            };
            foreach (var tile in theater.Tiles)
            {
                tilesNode.Nodes.Add(PopulateTileView(tile));
            }

            TreeNode regionsNode = new TreeNode($"Regions({theater.Regions.Count})");
            foreach (var region in theater.Regions)
            {
                regionsNode.Nodes.Add(PopulateRegionView(region));
            }

            theaterNode.Nodes.Add(PopulateRuntimeInfoView(theater.RuntimeInfo));
            theaterNode.Nodes.Add(tilesNode);
            theaterNode.Nodes.Add(regionsNode);

            return theaterNode;
        }

        private TreeNode PopulateRuntimeInfoView(FortTheaterRuntimeData RuntimeInfo)
        {
            TreeNode RuntimeInfoNode = new TreeNode("RuntimeInfo")
            {
                Tag = RuntimeInfo
            };

            TreeNode TheaterVisibilityRequirementsNode = new TreeNode("TheaterVisibilityRequirements")
            {
                Tag = RuntimeInfo.TheaterVisibilityRequirements
            };

            TreeNode RequirementsNode = new TreeNode("Requirements")
            {
                Tag = RuntimeInfo.Requirements
            };


            TreeNode TheaterColorInfoNode = new TreeNode("TheaterColorInfo")
            {
                Tag = RuntimeInfo.TheaterColorInfo
            };

            TreeNode MissionAlertRequirementsNode = new TreeNode("MissionAlertRequirements")
            {
                Tag = RuntimeInfo.MissionAlertRequirements
            };




            RuntimeInfoNode.Nodes.Add(TheaterVisibilityRequirementsNode);
            RuntimeInfoNode.Nodes.Add(RequirementsNode);
            RuntimeInfoNode.Nodes.Add(PopulateTheaterImagesView(RuntimeInfo.TheaterImages));
            RuntimeInfoNode.Nodes.Add(TheaterColorInfoNode);
            RuntimeInfoNode.Nodes.Add(MissionAlertRequirementsNode);
            RuntimeInfoNode.Nodes.Add(PopulateMissionAlertCategoryRequirementsView(RuntimeInfo.MissionAlertCategoryRequirements));

            return RuntimeInfoNode;
        }

        private TreeNode PopulateTheaterImagesView(FortMultiSizeBrush MSBrush)
        {
            TreeNode TheaterImagesNode = new TreeNode("TheaterImages")
            {
                Tag = MSBrush
            };

            TreeNode Brush_XXSNode = new TreeNode("Brush_XXS")
            {
                Tag = MSBrush.Brush_XXS
            };
            TreeNode Brush_XSNode = new TreeNode("Brush_XS")
            {
                Tag = MSBrush.Brush_XS
            };
            TreeNode Brush_SNode = new TreeNode("Brush_S")
            {
                Tag = MSBrush.Brush_S
            };
            TreeNode Brush_MNode = new TreeNode("Brush_M")
            {
                Tag = MSBrush.Brush_M
            };
            TreeNode Brush_LNode = new TreeNode("Brush_L")
            {
                Tag = MSBrush.Brush_L
            };
            TreeNode Brush_XLNode = new TreeNode("Brush_XL")
            {
                Tag = MSBrush.Brush_XL
            };

            TheaterImagesNode.Nodes.Add(Brush_XXSNode);
            TheaterImagesNode.Nodes.Add(Brush_XSNode);
            TheaterImagesNode.Nodes.Add(Brush_SNode);
            TheaterImagesNode.Nodes.Add(Brush_MNode);
            TheaterImagesNode.Nodes.Add(Brush_LNode);
            TheaterImagesNode.Nodes.Add(Brush_XLNode);


            return TheaterImagesNode;
        }

        private TreeNode PopulateMissionAlertCategoryRequirementsView(List<FortMissionAlertRuntimeData> MissionAlertCategoryRequirements)
        {
            TreeNode MissionAlertCategoryRequirementsNode = new TreeNode($"MissionAlertCategoryRequirements({MissionAlertCategoryRequirements.Count})")
            {
                Tag = MissionAlertCategoryRequirements
            };

            foreach(var requirement in MissionAlertCategoryRequirements)
            {
                TreeNode requirementNode = new TreeNode($"MissionAlertCategory {requirement.MissionAlertCategory.ToString()}")
                {
                    Tag = requirement
                };

                MissionAlertCategoryRequirementsNode.Nodes.Add(requirementNode);
            }


            return MissionAlertCategoryRequirementsNode;
        }

        private TreeNode PopulateTileView(FortTheaterMapTileData tile)
        {
            string ZoneThemeName_C = tile.ZoneTheme.Split('.')[1];
            string ZoneThemeName = ZoneThemeName_C.Replace("_C", "");
            TreeNode tileNode = new TreeNode($"Tile {ZoneThemeName}")
            {
                Tag = tile
            };

            TreeNode RequirementsNode = new TreeNode("Requirements")
            {
                Tag = tile.Requirements
            };

            TreeNode LinkedQuestsNode = new TreeNode($"LinkedQuests({tile.LinkedQuests.Count})")
            {
                Tag = tile.LinkedQuests
            };

            foreach (var quest in tile.LinkedQuests)
            {
                string QuestName_C = quest.QuestDefinition.Split('.')[1];
                string QuestName = QuestName_C.Replace("_C", "");
                TreeNode QuestNode = new TreeNode(QuestName)
                {
                    Tag = quest
                };
                LinkedQuestsNode.Nodes.Add(QuestNode);
            }

            TreeNode MissionWeightOverridesNode = new TreeNode($"MissionWeightOverrides({tile.MissionWeightOverrides.Count})")
            {
                Tag = tile.MissionWeightOverrides
            };

            foreach (var WeightOverride in tile.MissionWeightOverrides)
            {
                MissionWeightOverridesNode.Nodes.Add(PopulateMissionWeightView(WeightOverride));
            }

            TreeNode DifficultyWeightOverridesNode = new TreeNode($"DifficultyWeightOverrides({tile.DifficultyWeightOverrides.Count})")
            {
                Tag = tile.DifficultyWeightOverrides
            };

            foreach (var WeightOverride in tile.DifficultyWeightOverrides)
            {
                MissionWeightOverridesNode.Nodes.Add(PopulateDifficultyWeightView(WeightOverride));
            }
            //not used ig


            tileNode.Nodes.Add(RequirementsNode);
            tileNode.Nodes.Add(LinkedQuestsNode);
            tileNode.Nodes.Add(MissionWeightOverridesNode);
            tileNode.Nodes.Add(DifficultyWeightOverridesNode);

            return tileNode;
        }

        private TreeNode PopulateRegionView(FortTheaterMapRegionData region)
        {
            TreeNode RegionNode = new TreeNode(region.DisplayName)
            {
                Tag = region
            };

            TreeNode RequirementsNode = new TreeNode("Requirements")
            {
                Tag = region.Requirements
            };

            RegionNode.Nodes.Add(PopulateMissionDataView(region.MissionData));
            RegionNode.Nodes.Add(RequirementsNode);

            

            return RegionNode;
        }

        private TreeNode PopulateMissionWeightView(FortTheaterMissionWeight missionWeight)
        {
            string NodeName = $"MissionWeight";

            if (!string.IsNullOrEmpty(missionWeight.MissionGenerator))
            {
                string[] SplitString = missionWeight.MissionGenerator.Split(".");
                if (SplitString.Length > 1)
                {
                    string GeneratorName = SplitString[1];
                    NodeName = GeneratorName.Replace("_C", "");
                }


            }
            TreeNode WeightNode = new TreeNode(NodeName)
            {
                Tag = missionWeight
            };

            return WeightNode;
        }

        private TreeNode PopulateDifficultyWeightView(FortTheaterDifficultyWeight difficultyWeight)
        {
            TreeNode difficultyWeightNode = new TreeNode("DifficultyWeight")
            {
                Tag = difficultyWeight
            };

            return difficultyWeightNode;
        }

        private TreeNode PopulateMissionDataView(FortTheaterMapMissionData MissionData)
        {
            TreeNode MissionDataNode = new TreeNode("MissionData")
            {
                Tag = MissionData
            };

            TreeNode MissionWeightsNode = new TreeNode($"MissionWeights({MissionData.MissionWeights.Count})")
            {
                Tag = MissionData.MissionWeights
            };
            foreach (var MissionWeight in MissionData.MissionWeights)
            {
                MissionWeightsNode.Nodes.Add(PopulateMissionWeightView(MissionWeight));
            }

            TreeNode DifficultyWeightsNode = new TreeNode($"DifficultyWeights({MissionData.DifficultyWeights.Count})")
            {
                Tag = MissionData.DifficultyWeights
            };
            foreach (var DifficultyWeight in MissionData.DifficultyWeights)
            {
                DifficultyWeightsNode.Nodes.Add(PopulateDifficultyWeightView(DifficultyWeight));
            }

            MissionDataNode.Nodes.Add(MissionWeightsNode);
            MissionDataNode.Nodes.Add(DifficultyWeightsNode);
            return MissionDataNode;
        }


        private void saveJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TheaterInfo == null)
            {
                MessageBox.Show("No data to save. Load or create JSON data first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save JSON File",
                Filter = "JSON Files (*.json)|*.json",
                DefaultExt = "json",
                InitialDirectory = Settings.LastOpenedSaveDirectory
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Settings.LastOpenedSaveDirectory = Path.GetDirectoryName(saveFileDialog.FileName);

                    var jsonSerializationSettings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                        {
                            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                        },
                        //NullValueHandling = NullValueHandling.Ignore,
                        //DefaultValueHandling = DefaultValueHandling.Ignore
                    };

                    jsonSerializationSettings.Converters.Add(new StringEnumConverter());

                    string jsonContent = JsonConvert.SerializeObject(TheaterInfo, jsonSerializationSettings);

                    File.WriteAllText(saveFileDialog.FileName, jsonContent);

                    IsModified = false;
                    AddLog("JSON saved successfully!");
                }
                catch (Exception ex)
                {
                    AddLog($"Error saving JSON: {ex.Message}");
                }
            }
        }

        private void EditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsModified)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Would you like to save them before exiting?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    saveJsonToolStripMenuItem_Click(null, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }


            SaveSettings();
            FreeConsole();
        }

        private bool IsValidPaksDirectory()
        {
            foreach (string pak in Directory.EnumerateFiles(Settings.PaksDirectory))
            {
                if (pak.EndsWith(".pak")) return true;
            }

            return false;
        }

        private void LoadPaks()
        {
            if(Settings.UEVersion == EGame.GAME_UE4_0)
            {
                AddLog("Please select UE Version before loading paks!");
                return;
            }
            if (IsValidPaksDirectory())
            {
                try
                {
                    if (FileProvider != null) FileProvider.Dispose();
                    FileProvider = new DefaultFileProvider(Settings.PaksDirectory, SearchOption.TopDirectoryOnly, true, new VersionContainer(Settings.UEVersion));
                    FileProvider.Initialize();
                    if (Settings.AESKey != "0x0")
                        FileProvider.SubmitKey(new FGuid(), new FAesKey(Settings.AESKey));
                    else
                    {
                        AddLog("Please input an aes key, you can get one for your version here: https://github.com/dippyshere/fortnite-aes-archive/tree/master/archive");
                        return;
                    }
                    AddLog($"Loaded paks from folder: {Settings.PaksDirectory} using ue version: {Settings.UEVersion.ToString()}");

                    var list = FileProvider.Files.ToList();
                    AddLog($"files: {list.Count}");
                }
                catch (Exception ex)
                {
                    AddLog($"Error loading paks: {ex.Message}");
                }

            }
            else
            {
                AddLog("ERROR: Failed to Load Paks, please select a valid paks directory in Settings -> Paks Directory");
            }
            
        }

        private void EditorWindow_Load(object sender, EventArgs e)
        {
            LoadPaks();

            if (Settings.LoadLastLoadedJsonOnLoad && Settings.LastLoadedJson != "none")
            {
                if (!File.Exists(Settings.LastLoadedJson)) return;
                string jsonContent = File.ReadAllText(Settings.LastLoadedJson);

                TheaterInfo = JsonConvert.DeserializeObject<FortActiveTheaterInfo>(jsonContent);

                PopulateTreeView();

                AddLog("Loaded Last loaded json!");
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                propertyGrid1.SelectedObject = e.Node.Tag;
            }
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView.ExpandAll();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView.CollapseAll();
        }

        private void darkModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DarkMode = darkModeToolStripMenuItem.Checked;
            DarkModeManager.ApplyTheme(darkModeToolStripMenuItem.Checked);
        }

        private void OnPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (e.OldValue != e.ChangedItem?.Value)
            {
                IsModified = true;
            }
        }
        private async Task OnSelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            ImagePanel.ClearImage();
            if (e.NewSelection.Value is string selectedValue && selectedValue.StartsWith("Texture2D"))
            {
                string texturePath = selectedValue;

                AddLog($"Selected a texture: {texturePath}");


                try
                {
                    string[] SplitString = texturePath.Split('\'');

                    if (SplitString.Length >= 2)
                    {
                        string PartWeNeed = SplitString[1];
                        string ModifiedForParser = PartWeNeed.Replace("/Game/", "FortniteGame/Content/");
                        AddLog($"Modified path: {ModifiedForParser}");

                        var texture = await Task.Run(() =>
                        {
                            UTexture2D loadedTexture = FileProvider.LoadObject<UTexture2D>(ModifiedForParser);
                            return loadedTexture;
                        });
                        AddLog($"Texture package name: {texture.Name}");
                        SelectedTexture = texture.Decode(ETexturePlatform.DesktopMobile);
                        AddLog("Decoded texture!");

                        ImagePanel.SetImage(SelectedTexture);

                        ImagePanel.SetPadding(20);
                        ImagePanel.SetBackgroundColor(new SKColor(50, 50, 50));
                        ImagePanel.ShowBorder(true, Color.White);

                    }
                }
                catch (Exception ex)
                {
                    AddLog($"Error when trying to load a texture: {ex.Message}");
                }

                //Texture2D'/Game/UI/Icons/Icon-TheaterDifficulty-_normal_.Icon-TheaterDifficulty-_normal_'

            }
        }

        private void paksFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select Paks folder";
                folderDialog.SelectedPath = Settings.PaksDirectory;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.PaksDirectory = folderDialog.SelectedPath;
                    AddLog($"New Paks Directory: {Settings.PaksDirectory}");
                }
            }
        }

        private void uEVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void VersionMenuItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
                if (clickedItem != null)
                {
                    Settings.UEVersion = (EGame)clickedItem.Tag;

                    AddLog($"New UE Version: {Settings.UEVersion.ToString()}");
                    LoadPaks();
                }
            }

        }

        private void loadLastLoadedJsonOnLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.LoadLastLoadedJsonOnLoad = loadLastLoadedJsonOnLoadToolStripMenuItem.Checked;
        }

        private void aESKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aes = Interaction.InputBox("Enter AES Key, you can get one here:\nhttps://github.com/dippyshere/fortnite-aes-archive/tree/master/archive", "AES Key Input", "0x0");

            if (!string.IsNullOrEmpty(aes))
            {
                Settings.AESKey = aes;
                LoadPaks();
            }
        }
    }
}
