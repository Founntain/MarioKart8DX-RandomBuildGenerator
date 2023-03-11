namespace MkBuildBot.Classes.Stats;

public class PartStat : IPartStat
{
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

    public string Name { get; set; }
}