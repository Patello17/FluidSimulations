using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class ParticleManager
{
    private const float GRAVITY = 0.002f;
    List<Particle> particles;
    BoxCollider boxCollider;

    public ParticleManager(Vector2 managementZonePos, int width, int height)
    {
        Particle particleA = new Particle(new Vector2(200, 400),
                                              new Vector2(0.5f, 0), 20);
        Particle particleB = new Particle(new Vector2(800, 400),
                                              new Vector2(-3f, 0), 20);
        this.particles = new List<Particle>();
        this.particles.Add(particleA);
        this.particles.Add(particleB);
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
            if (!boxCollider.IsColliding(particle.GetCollider()))
            {
                particle.Accelerate(new Vector2(0, GRAVITY) * gameTime.ElapsedGameTime.Milliseconds);
                particle.Update(gameTime);
            }
            else
            {
                particle.Velocity *= -1;
                particle.Update(gameTime);
            }
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