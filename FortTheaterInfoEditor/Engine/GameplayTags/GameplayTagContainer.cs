using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

[Editor(typeof(GameplayTagContainerEditor), typeof(UITypeEditor))]
public class GameplayTagContainer
{
    public List<GameplayTag> GameplayTags { get; set; } = new List<GameplayTag>();
    public List<GameplayTag> ParentTags { get; set; } = new List<GameplayTag>();//to avoid json serializer to serialize this even if empty i would have to create a dummy property and blah blah
}