namespace MkBuildBot.Classes.Stats;

public class PartStat : IPartStat
{
    public string Name { get; set; }
    public double GroundSpeed { get; set; }
    public double AntiGravitySpeed { get; set; }
    public double WaterSpeed { get; set; }
    public double GlidingSpeed { get; set; }

    public double Acceleration { get; set; }
    public double Weight { get; set; }

    public double GroundHandling { get; set; }
    public double AntiGravityHandling { get; set; }
    public double WaterHandling { get; set; }
    public double GlidingHandling { get; set; }

    public double Traction { get; set; }
    public double MiniTurbo { get; set; }
    public double Invincibility { get; set; }

    public PartStat()
    {
        Name = "Not Found";
    }

    public PartStat(string name, double groundSpeed, double antiGravitySpeed, double waterSpeed, double glidingSpeed,
        double acceleration, double weight, double groundHandling, double antiGravityHandling, double waterHandling,
        double glidingHandling, double traction, double miniTurbo, double invincibility)
    {
        Name = name;
        GroundSpeed = groundSpeed;
        AntiGravitySpeed = antiGravitySpeed;
        WaterSpeed = waterSpeed;
        GlidingSpeed = glidingSpeed;

        Acceleration = acceleration;
        Weight = weight;

        GroundHandling = groundHandling;
        AntiGravityHandling = antiGravityHandling;
        WaterHandling = waterHandling;
        GlidingHandling = glidingHandling;

        Traction = traction;
        MiniTurbo = miniTurbo;
        Invincibility = invincibility;
    }
}