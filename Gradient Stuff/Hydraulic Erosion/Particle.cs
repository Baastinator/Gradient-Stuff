using System;
using System.Xml;
using Gradient_Stuff.Vector;

namespace Gradient_Stuff.Hydraulic_Erosion
{
    public class Particle
    {
        public Vectorf Position { get; set; }
        public Vectorf Velocity { get; set; }
        public Vectorf Force { get; set; }
        public float WaterMass { get; set; }
        public float SedimentMass { get; set; }
        public float Mass => WaterMass + SedimentMass;
        public float SedimentPercentage => SedimentMass / (SedimentMass + WaterMass);
        public Vectorf Momentum => Velocity * Mass;
        public Vectorf Acceleration => Force / Mass;

        private const float dt = 10;

        public Particle(Vectorf iniPos, float iniMass)
        {
            if (iniMass <= 0) throw new Exception("Mass must be above 0");
            Position = iniPos;
            Velocity = Vectorf.Zero;
            Force = Vectorf.Zero;
            WaterMass = iniMass;
            SedimentMass = 0;
        }

        public void addGravityForce(Vectorf force, float slope)
        {
            Force += force*MathF.Atan(slope);
        }

        public void step()
        {
            Position += Velocity * dt;
            Velocity += Acceleration * dt;

            Force = Vectorf.Zero;
        }
    }
}