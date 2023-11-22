namespace MkBuildBot.Classes.Stats;

public interface IPartStat
{
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
}