namespace Dinostamp.BlazorWebAssembly;

public class Frame
{
    public Frame(Game game)
    {
        Player = game.Player;
        Level = game.Level;
        Camera = game.Camera;
        SoundEffects = game.SoundEffects;
    }

    public Player Player { get; }
    public Level Level { get; }
    public Camera Camera { get; }
    public SoundEffects SoundEffects { get; }
}
