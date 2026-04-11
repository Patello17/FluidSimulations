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
    private const float WALL_DAMPING = 0.8f;
    private const float GRAVITY = 0.002f;
    private List<Particle> particles;
    private BoxCollider boxCollider;

    // Define Initial State
    private int numParticle = 40;
    private Vector2 particleSpacing = new Vector2(40, 40); 
    private Vector2 initialPositionPadding = new Vector2(40, 100);
    private int particleRadius = 20;
    // private int numParticle = 100;
    // private Vector2 particleSpacing = new Vector2(4, 4); 
    // private Vector2 initialPositionPadding = new Vector2(40, 100);
    // private int particleRadius = 2;

    public ParticleManager(Vector2 managementZonePos, int width, int height)
    {
        // Particle particleA = new Particle(new Vector2(200, 200),
        //                                       new Vector2(0.5f, 0), 20);
        // Particle particleB = new Particle(new Vector2(800, 400),
        //                                       new Vector2(-2f, 0f), 20);
        // Particle particleC = new Particle(new Vector2(1000, 400),
        //                                       new Vector2(0, 0), 20);
        this.particles = new List<Particle>();
        // this.particles.Add(particleA);
        // this.particles.Add(particleB);
        // this.particles.Add(particleC);
        int cols = (int)((width - 2 * initialPositionPadding.X) / (2 * particleRadius * TEXTURE_SCALE + particleSpacing.X));
        for (int i = 0; i < numParticle; i++)
        {
            float row = (float)Math.Floor((double)i / cols);
            float col = i % cols;
            this.particles.Add(new Particle(new Vector2(particleSpacing.X * col + initialPositionPadding.X, 
                                                        particleSpacing.Y * row + initialPositionPadding.Y),
                                                        new Vector2(4f, 2f), particleRadius));
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
            particle.Position += boxCollider.CalculateCollisionCorrection(gameTime, particle);
            particle.Accelerate(new Vector2(0, GRAVITY * gameTime.ElapsedGameTime.Milliseconds));
            if (boxCollider.IsCollidingHorizontal(particle.Collider))
            {
                particle.Velocity = new Vector2(-particle.Velocity.X, particle.Velocity.Y) * WALL_DAMPING;
            }
            else if (boxCollider.IsCollidingVertical(particle.Collider))
            {
                particle.Velocity = new Vector2(particle.Velocity.X, -particle.Velocity.Y) * WALL_DAMPING;
            }
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