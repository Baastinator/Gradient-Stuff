using Gradient_Stuff.Vector;

namespace Gradient_Stuff.Hydraulic_Erosion
{
    public class Particle
    {
        public Vectorf Position { get; set; }
        public Vectorf Velocity { get; set; }
        public float WaterMass { get; set; }
        public float SedimentMass { get; set; }
        public float Mass => WaterMass + SedimentMass;

    }
}