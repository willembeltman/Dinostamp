namespace Dinostamp.BlazorWebAssembly;

#nullable disable

public class Game
{
    private int GroundY;
    private readonly float Gravity = 1.1f;
    private Dictionary<string, bool> Keys = new();

    public Player Player { get; set; }
    public Level Level { get; set; }
    public Camera Camera { get; set; } = new();
    public SoundEffects SoundEffects { get; set; } = new();

    public Frame GetFrame(int width, int height)
    {
        GameLoop(width, height);
        var frame = new Frame(this);
        SoundEffects = new SoundEffects();
        return frame;
    }
    public void KeyDown(string key)
    {
        Keys[key.ToLower()] = true;
    }
    public void KeyUp(string key)
    {
        Keys[key.ToLower()] = false;
    }

    private void GameLoop(int width, int heigth)
    {
        if (Level == null)
        {
            GroundY = heigth - 80;
            Player = CreatePlayer(GroundY);
            Level = new Level1(GroundY);
            Keys["a"] = false;
            Keys["arrowleft"] = false;
            Keys["d"] = false;
            Keys["arrowright"] = false;
            Keys[" "] = false;
            Keys["z"] = false;
            Keys["arrowup"] = false;
            Keys["p"] = false;
        }

        // Check player died
        if (Player.Y > heigth)
        {
            Player.X = 100;
            Player.Y = GroundY - 80;
            Player.VY = 0;
        }

        // Update shake
        if (Camera.ShakeTime > 0)
        {
            Camera.ShakeTime--;
            Camera.CameraShake *= 0.85f;
            if (Camera.CameraShake < 1) Camera.CameraShake = 0;
        }
        else
        {
            Camera.CameraShake = 0;
        }

        // Horizontaal
        if (Keys["a"] || Keys["arrowleft"])
        {
            Player.VX = -Player.Speed;
            Player.Facing = -1;
        }
        else if (Keys["d"] || Keys["arrowright"])
        {
            Player.VX = Player.Speed;
            Player.Facing = 1;
        }
        else
        {
            Player.VX = 0;
        }
        // Jump
        if ((Keys[" "] || Keys["z"] || Keys["arrowup"]) && !Player.Jumping)
        {
            Player.VY = -22;
            Player.Jumping = true;
        }
        if (Keys["p"])
        {
            Player.Starred = !Player.Starred;
        }

        // Apply
        Player.X += Player.VX;
        Player.Y += Player.VY;
        Player.VY += Gravity;

        // Collision (AABB, verbeterd)
        var onGround = false;
        foreach (var p in Level.Platforms)
        {
            if (
                Player.X + Player.Width / 2 > p.X &&
                Player.X - Player.Width / 2 < p.X + p.Width &&
                Player.Y + 2 > p.Y &&
                Player.Y + 2 < p.Y + p.Height &&
                Player.VY >= 0
            )
            {
                if (Player.Starred)
                {
                    // Blijf springen
                    Player.Y = p.Y - Player.Height;
                }
                else
                {
                    Player.Y = p.Y;
                }
                Player.VY = 0;
                onGround = true;
            }
        }

        if (onGround)
        {
            if (Player.Jumping)
            {
                PlayBoom();
                ShakeScreen();
            }
            Player.Jumping = false;
        }
        else
        {
            Player.Jumping = true;
        }

        // Camera volgen
        Camera.X += ((Player.X - 300) - Camera.X) * 0.1f;
        if (Camera.X < 0) Camera.X = 0;

        foreach (var e in Level.Enemies)
        {
            e.X += e.VX * e.Facing;
            if (e.X < e.Platform.X)
            {
                e.X = e.Platform.X;
                e.Facing = 1;
            }
            else if (e.X + e.Width > e.Platform.X + e.Platform.Width)
            {
                e.X = e.Platform.X + e.Platform.Width - e.Width;
                e.Facing = -1;
            }



            // AABB collision
            if (
                Player.X + Player.Width / 2 > e.X &&
                Player.X - Player.Width / 2 < e.X + e.Width &&
                Player.Y + Player.Height / 2 > e.Y &&
                Player.Y - Player.Height / 2 < e.Y + e.Height
            )
            {
                Player.InCollision = true;
                // Enemy is "sterker" tenzij hij giftig is
                if (!e.Poisonous)
                {
                    // Duw speler weg afhankelijk van enemy richting
                    var push = 12;
                    if (Player.X < e.X)
                    {
                        Player.X -= push;
                    }
                    else
                    {
                        Player.X += push;
                    }
                    // Eventueel shake/geluid
                    ShakeScreen();
                }
                else
                {
                    // Hier kun je giftig effect toevoegen
                }
            }
            else
            {

                Player.InCollision = false;
            }
        }
    }

    private void PlayBoom()
    {
        SoundEffects.Boom = true;
    }
    internal void ShakeScreen()
    {
        Camera.CameraShake = 24;
        Camera.ShakeTime = 12;
    }

    public static Player CreatePlayer(int groundY)
    {
        return new Player
        {
            X = 100,
            Y = groundY - 80,
            Width = 64,
            Height = 80,
            VX = 0,
            VY = 0,
            Speed = 6,
            Health = 3,
            Jumping = false,
            Starred = false,
            Facing = 1,
            Color = "#6c3"
        };
    }
}
