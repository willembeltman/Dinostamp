namespace Dinostamp.BlazorWebAssembly;

public class Level1 : Level
{
    public Level1(int groundY)
    {
        Platforms = GetPlatforms(groundY);
        Enemies = GetEnemies(Platforms);
    }
    public static List<Platform> GetPlatforms(int groundY)
    {
        return new()
        {
            // ==== GROUND ====
            new Platform { X = 0,     Y = groundY,     Width = 1000, Height = groundY, Type = PlatformType.Ground, Color = "#754" },
            new Platform { X = 2500,  Y = groundY,     Width = 1000, Height = groundY, Type = PlatformType.Ground, Color = "#754" },
            new Platform { X = 5000,  Y = groundY - 20,Width = 1000, Height = groundY, Type = PlatformType.Ground, Color = "#754" },
            new Platform { X = 7000,  Y = groundY + 20,Width = 1000, Height = groundY, Type = PlatformType.Ground, Color = "#754" },
            new Platform { X = 12000, Y = groundY,     Width = 1000, Height = groundY, Type = PlatformType.Ground, Color = "#754" },

            // ==== NORMAL ====
            new Platform { X = 500,  Y = groundY - 120, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 800,  Y = groundY - 200, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 1100, Y = groundY - 120, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 1400, Y = groundY - 200, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 1700, Y = groundY - 120, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 2000, Y = groundY - 160, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 2300, Y = groundY - 100, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },

            new Platform { X = 2600, Y = groundY - 140, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 2900, Y = groundY - 180, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 3200, Y = groundY - 120, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 3500, Y = groundY - 160, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 3800, Y = groundY - 100, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 4100, Y = groundY - 200, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 4400, Y = groundY - 150, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 4700, Y = groundY - 100, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 5000, Y = groundY - 180, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 5300, Y = groundY - 120, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 5600, Y = groundY - 160, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 5900, Y = groundY - 100, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 6200, Y = groundY - 200, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 6500, Y = groundY - 140, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 6800, Y = groundY - 120, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 7100, Y = groundY - 180, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 7400, Y = groundY - 150, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 7700, Y = groundY - 100, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 8000, Y = groundY - 200, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 8300, Y = groundY - 160, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 8600, Y = groundY - 120, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 8900, Y = groundY - 180, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 9200, Y = groundY - 140, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 9500, Y = groundY - 100, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 9800, Y = groundY - 160, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 10100,Y = groundY - 200, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 10400,Y = groundY - 120, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 10700,Y = groundY - 150, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 11000,Y = groundY - 100, Width = 80,  Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 11300,Y = groundY - 180, Width = 100, Height = 50, Type = PlatformType.Normal, Color = "#999" },
            new Platform { X = 11600,Y = groundY - 140, Width = 120, Height = 50, Type = PlatformType.Normal, Color = "#999" },
        };
    }
    public static List<Enemy> GetEnemies(List<Platform> platforms)
    {
        var ground0 = platforms.First(p => p.Type == PlatformType.Ground);

        return new()
        {
            new Enemy
            {
                X = ground0.X + 200,
                Y = ground0.Y - 40,
                Width = 40,
                Height = 40,
                VX = 2,
                VY = 0,
                Facing = 1,
                Health = 1,
                Speed = 2,
                Jumping = false,
                Starred = false,
                Color = "#c33",
                Poisonous = false,
                Platform = ground0
            }
        };
    }
}