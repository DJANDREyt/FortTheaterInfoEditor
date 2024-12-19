using System.Numerics;


public class SlateBrush
{
    public Vector2D ImageSize { get; set; }
    public SlateBrushDrawType DrawAs { get; set; }
    public Margin Margin { get; set; }
   // public LinearColor Tint { get; set; }
    public SlateColor TintColor { get; set; }
    public SlateBrushTileType Tiling { get; set; }
    public SlateBrushMirrorType Mirroring { get; set; }
    public SlateBrushImageType ImageType { get; set; }
    public string ResourceObject { get; set; }
    public string ResourceName { get; set; }
    public bool IsDynamicallyLoaded { get; set; }
   // public bool HasUObject { get; set; 
    public Box2D UVRegion { get; set; }
}

public class Margin
{
    public float Left { get; set; }
    public float Top { get; set; }
    public float Right { get; set; }
    public float Bottom { get; set; }
}

public class Box2D
{
    public Vector2D Min { get; set; }
    public Vector2D Max { get; set; }
}

public enum SlateBrushDrawType { Box, Border, Image, NoDrawType }
public enum SlateBrushTileType { NoTile, Horizontal, Vertical, Both }
public enum SlateBrushMirrorType { NoMirror, Horizontal, Vertical, Both }
public enum SlateBrushImageType { NoImage, FullColor, LinearColor }
