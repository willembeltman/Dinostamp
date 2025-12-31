namespace Dinostamp.BlazorWebAssembly;

#nullable disable

public class Player
{
    public float X { get; set; }
    public float Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public float VX { get; set; }
    public float VY { get; set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public int Facing { get; set; }
    public bool Jumping { get; set; }
    public bool Starred { get; set; }
    public string Color { get; set; }
    public bool InCollision { get; set; }
}
