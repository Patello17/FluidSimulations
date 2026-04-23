using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class ParticleManager
{
    private const float TEXTURE_SCALE = 0.0042f;
    private float SMOOTHING_RADIUS = 100f;
    private const float WALL_DAMPING = 0.8f; // 0.8f is good
    private const float R_EQ = 1f;
    private const float REPULSIVE_TERM = 1f;
    private const float ATTRACTIVE_TERM = 1f;
    private const float LJF_SCALING = 1000;
    private const float GRAVITY = 0.002f;
    private List<Particle> particles;
    private BoxCollider boxCollider;

    // Define Initial State
    // private int numParticle = 20;
    // private Vector2 particleSpacing = new Vector2(40, 40); 
    // private Vector2 initialPositionPadding = new Vector2(40, 100);
    // private int particleRadius = 20;
    private int numParticle = 1000;
    private Vector2 particleSpacing = new Vector2(40, 40);
    private Vector2 initialPositionPadding = new Vector2(40, 100);
    private int particleRadius = 5;

    public ParticleManager(Vector2 managementZonePos, int width, int height)
    {
        Particle particleA = new Particle(new Vector2(200, 200),
                                              new Vector2(0.2f, 0.2f), 20);
        Particle particleB = new Particle(new Vector2(400, 200),
                                              new Vector2(0f, 0f), 20);
        Particle particleC = new Particle(new Vector2(1000, 400),
                                              new Vector2(0, 0), 20);
        this.particles = new List<Particle>();
        this.particles.Add(particleA);
        // this.particles.Add(particleB);
        // this.particles.Add(particleC);

        int cols = (int)((width - 2 * initialPositionPadding.X) / (2 * particleRadius * TEXTURE_SCALE + particleSpacing.X));
        for (int i = 0; i < numParticle; i++)
        {
            float row = (float)Math.Floor((double)i / cols);
            float col = i % cols;
            this.particles.Add(new Particle(new Vector2(particleSpacing.X * col + initialPositionPadding.X, 
                                                        particleSpacing.Y * row + initialPositionPadding.Y),
                                                        new Vector2(0f, 0f), particleRadius));
        }
        this.boxCollider = new BoxCollider(managementZonePos, width, height);
    }

    public void LoadParticles(ContentManager content)
    {
        foreach (Particle particle in this.particles)
        {
            particle.LoadContent(content);
        }
    }

    public void Update(GameTime gameTime)
    {
        // ApplyGravity(gameTime);

        foreach (Particle particle in this.particles)
        {
            List<Particle> nearby_particles = new List<Particle>();
            foreach(Particle nearby_particle in this.particles)
            {
                float dist = particle.CalculateDistanceToParticle(nearby_particle);
                if (dist < SMOOTHING_RADIUS && particle != nearby_particle)
                {
                    // if (particle.GetCollider().Radius == 20) { Debug.WriteLine(dist); }
                    nearby_particles.Add(nearby_particle);
                }
            }
            // particle.Accelerate(new Vector2(0, GRAVITY * gameTime.ElapsedGameTime.Milliseconds));
            foreach (Particle other in nearby_particles)
            {
                Vector2 r = particle.CalculateVectorToParticle(other);
                float mag_r = particle.CalculateDistanceToParticle(other);
                int n_att = 12; // 12; attractive potential
                int n_rep = 6; // 6; repulsive potential
                float inv_r = 1.0f / mag_r;
                float sr = R_EQ * inv_r;
                float sr_rep = (float)Math.Pow(sr, n_rep);
                float sr_att = (float)Math.Pow(sr, n_att);
                float force = inv_r * (n_rep * sr_rep - n_att * sr_att) * LJF_SCALING;

                Vector2 dir = r / mag_r;
                Vector2 f = dir * force;
                particle.Accelerate(f);
                other.Accelerate(-f);
                // Debug.WriteLine(other.Velocity);

            }
            if (boxCollider.IsCollidingHorizontal(particle.Collider))
            {
                particle.Velocity = new Vector2(-particle.Velocity.X, particle.Velocity.Y) * WALL_DAMPING;
            }
            else if (boxCollider.IsCollidingVertical(particle.Collider))
            {
                particle.Velocity = new Vector2(particle.Velocity.X, -particle.Velocity.Y) * WALL_DAMPING;
            }
            // particle.Accelerate(new Vector2(0, GRAVITY * gameTime.ElapsedGameTime.Milliseconds));
            particle.Position += boxCollider.CalculateCollisionCorrection(gameTime, particle);
            particle.Update(gameTime);
        }
    }
    
    public void ApplyGravity(GameTime gameTime)
    {
        foreach (Particle particle in this.particles)
        {
            particle.Velocity = particle.Velocity + new Vector2(0, GRAVITY) * gameTime.ElapsedGameTime.Milliseconds;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (Particle particle in this.particles)
        {
            particle.Draw(spriteBatch);
        }
    }
}