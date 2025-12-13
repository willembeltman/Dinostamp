
namespace Dinostamp.Razor;

public class Game
{
    public Player Player { get; set; } = new Player();
    public Level CurrentLevel { get; set; } = new Level();
    public Camera Camera { get; set; } = new Camera();
    public SoundEffects SoundEffects { get; set; } = new SoundEffects();
    public void UpdateInputs(Inputs inputs)
    {

    }
    public Frame GetFrame()
    {
        var frame = new Frame(this);
        SoundEffects = new SoundEffects();
        return frame;
    }
}

public class Frame
{
    public Frame(Game game)
    {
        Player = game.Player;
        Level = game.CurrentLevel;
        Camera = game.Camera;
        SoundEffects = game.SoundEffects;
    }

    public Player Player { get; }
    public Level Level { get; }
    public Camera Camera { get; }
    public SoundEffects SoundEffects { get; }
}
public class Inputs
{
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Jump { get; set; }
    public bool Space { get; set; }
}
public class Camera
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Shake { get; set; }
    public float ShakeTime { get; set; }
}
public class SoundEffects
{
    public bool Jump { get; set; }
    public bool Star { get; set; }
    public bool Damage { get; set; }
}
public class Level
{
    public List<Platform> Platforms { get; set; } = new List<Platform>();
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
}
public class Enemy(int damage, Platform platform, Color color) : Player
{
    public bool Poisonous => Damage > 0;
    public int Damage { get; set; } = damage;
    public Platform Platform { get; set; } = platform;
    public Color Color { get; set; } = color;
}
public class Player : Rectangle
{
    public int VelocityX { get; set; }
    public int VelocityY { get; set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public bool FacingRight { get; set; }
    public bool FacadeLeft
    {
        get => !FacingRight;
        set => FacingRight = !value;
    }
    public bool Jumping { get; set; }
    public bool Starred { get; set; }
}
public class Platform(Color color) : Rectangle
{
    public Color Color { get; set; } = color;
}
public class Rectangle
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}
public class Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }
}