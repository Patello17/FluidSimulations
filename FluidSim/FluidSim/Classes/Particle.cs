using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Particle
{
    private const float TEXTURE_SCALE = 0.0042f;
    private Texture2D texture;
    private DiscCollider collider;
    public DiscCollider Collider
    {
        get { return this.collider; }
        set { this.collider = value; }
    }
    private Vector2 pos;
    public Vector2 Position
    {
        get { return this.pos; }
        set { this.pos = value; }
    }
    private Vector2 vel;
    public Vector2 Velocity
    {
        get { return this.vel; }
        set { this.vel = value; }
    }
    

    public Particle(Vector2 _pos, Vector2 _vel, float _radius)
    {
        this.pos = _pos;
        this.vel = _vel;
        this.collider = new DiscCollider(_pos, _radius);
    }

    public void Update(GameTime gameTime)
    {
        this.pos += this.vel * gameTime.ElapsedGameTime.Milliseconds;
        this.collider.Position = this.pos;
    }
    
    public void LoadContent(ContentManager content)
    {
        this.texture = content.Load<Texture2D>("particle"); // 500 pxls x 500 pxls
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.texture, this.pos,
                         null, this.CalculateColorBasedOnVelocity(),
                         0f, Vector2.Zero,
                         this.collider.Radius * TEXTURE_SCALE,
                         SpriteEffects.None, 0f);
    }

    public DiscCollider GetCollider()
    {
        return this.collider;
    }

    public bool IsColliding(Particle other)
    {
        return this.collider.IsColliding(other.Collider);
    }

    public void Accelerate(Vector2 acceleration)
    {
        this.vel += acceleration;
    }

    public float CalculateDistanceToParticle(Particle other)
    {
        float delta_x = other.Position.X - this.pos.X;
        float delta_y = other.Position.Y - this.pos.Y;
        return (float)Math.Sqrt(delta_x * delta_x + delta_y * delta_y);
    }
    public Vector2 CalculateVectorToParticle(Particle other)
    {
        float delta_x = other.Position.X - this.pos.X;
        float delta_y = other.Position.Y - this.pos.Y;
        return new Vector2(delta_x, delta_y);
    }
    private Color CalculateColorBasedOnVelocity()
    {
        float magnitude = (float)Math.Sqrt(this.vel.X * this.vel.X + this.vel.Y * this.vel.Y);
        if (magnitude == 0)
        {
            return Color.White;
        }
        float colorScale = 0.5f;
        // Debug.WriteLine($"R: {magnitude * colorScale}, B: {255 - magnitude * colorScale}");
        // Faster ~ Redder; Slower ~ Bluer
        return new Color(Math.Clamp(magnitude * colorScale, 0, 255), 0, Math.Clamp(255 - magnitude * colorScale, 0, 255));
    }
}