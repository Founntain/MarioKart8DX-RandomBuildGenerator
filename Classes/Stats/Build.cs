namespace Mk8RPBot.Classes.Stats;

public class Build
{
    public PartStat Character { get; set; }
    public PartStat Body { get; set; }
    public PartStat Tire { get; set; }
    public PartStat Glider { get; set; }

    public double GroundSpeed()
    {
        return Character.GroundSpeed + Body.GroundSpeed + Tire.GroundSpeed + Glider.GroundSpeed;
    }
    
    public double WaterSpeed()
    {
        return Character.WaterSpeed + Body.WaterSpeed + Tire.WaterSpeed + Glider.WaterSpeed;
    }
    
    public double GlidingSpeed()
    {
        return Character.GlidingSpeed + Body.GlidingSpeed + Tire.GlidingSpeed + Glider.GlidingSpeed;
    }
    
    public double AntiGravitySpeed()
    {
        return Character.AntiGravitySpeed + Body.AntiGravitySpeed + Tire.AntiGravitySpeed + Glider.AntiGravitySpeed;
    }
    
    public double Acceleration()
    {
        return Character.Acceleration + Body.Acceleration + Tire.Acceleration + Glider.Acceleration;
    }
    
    public double Weight()
    {
        return Character.Weight + Body.Weight + Tire.Weight + Glider.Weight;
    }
    
    public double GroundHandling()
    {
        return Character.GroundHandling + Body.GroundHandling + Tire.GroundHandling + Glider.GroundHandling;
    }
    
    public double WaterHandling()
    {
        return Character.WaterHandling + Body.WaterHandling + Tire.WaterHandling + Glider.WaterHandling;
    }
    
    public double GlidingHandling()
    {
        return Character.GlidingHandling + Body.GlidingHandling + Tire.GlidingHandling + Glider.GlidingHandling;
    }
    
    public double AntiGravityHandling()
    {
        return Character.AntiGravityHandling + Body.AntiGravityHandling + Tire.AntiGravityHandling + Glider.AntiGravityHandling;
    }
    
    public double Traction()
    {
        return Character.Traction + Body.Traction + Tire.Traction + Glider.Traction;
    }
    
    public double MiniTurbo()
    {
        return Character.MiniTurbo + Body.MiniTurbo + Tire.MiniTurbo + Glider.MiniTurbo;
    }
    
    public double Invincibility()
    {
        return Character.Invincibility + Body.Invincibility + Tire.Invincibility + Glider.Invincibility;
    }
}