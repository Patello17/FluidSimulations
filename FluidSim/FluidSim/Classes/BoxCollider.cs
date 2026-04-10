using System;
using System.Diagnostics;
using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class BoxCollider
{
    // private const float COLLISION_CORRECTION = 10f;
    private Vector2 pos;
    public Vector2 Position // the top-left corner of the box
    {
        get {return this.pos; }
        set {this.pos = value; }
    }
    private Rectangle rectangle;

    public BoxCollider(Vector2 _pos, int _width, int _height)
    {
        this.Position = _pos;
        this.rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, _width, _height);
    }

    public void Update(GameTime gameTime)
    {
        
    }
    
    public bool IsCollidingVertical(DiscCollider disc)
    {
        return IsCollidingBottom(disc) || IsCollidingTop(disc);
    }

    public bool IsCollidingHorizontal(DiscCollider disc)
    {
        return IsCollidingLeft(disc) || IsCollidingRight(disc);
    }
    private bool IsCollidingLeft(DiscCollider disc)
    {
        return disc.Position.X - disc.Radius <= this.rectangle.Left;
    }

    private bool IsCollidingRight(DiscCollider disc)
    {
        return disc.Position.X + disc.Radius >= this.rectangle.Right;
    }
    private bool IsCollidingTop(DiscCollider disc)
    {
        return disc.Position.Y - disc.Radius <= this.rectangle.Top;
    }

    private bool IsCollidingBottom(DiscCollider disc)
    {
        return disc.Position.Y + disc.Radius >= this.rectangle.Bottom;
    }

    public Vector2 CalculateCollisionCorrection(GameTime gameTime, Particle particle)
    {
        DiscCollider disc = particle.GetCollider();
        Vector2 delta_r = particle.Velocity * gameTime.ElapsedGameTime.Milliseconds;
        float r_mag = (float)Math.Sqrt(delta_r.X * delta_r.X + delta_r.Y * delta_r.Y);
        // Debug.WriteLine($"DELTA_R: {delta_r}, MAG: {r_mag}");
        if (IsCollidingBottom(disc))
        {
            // Debug.WriteLine($"BOTTOM");
            float delta_y = this.rectangle.Bottom - (disc.Position.Y + disc.Radius);
            return new Vector2(-delta_r.X, delta_y);
        }
        else if (IsCollidingTop(disc))
        {
            // Debug.WriteLine($"TOP");
            float delta_y = (disc.Position.Y + disc.Radius) - this.rectangle.Top;
            return new Vector2(delta_r.X, delta_y);
        }
        else if (IsCollidingLeft(disc))
        {
            // Debug.WriteLine($"LEFT");
            float delta_x = (disc.Radius - disc.Position.X) - this.rectangle.Left;
            return new Vector2(delta_x, -delta_r.Y);
        }
        else if (IsCollidingRight(disc))
        {
            // Debug.WriteLine($"RIGHT");
            float delta_x = this.rectangle.Right - (disc.Position.X + disc.Radius);
            return new Vector2(delta_x, -delta_r.Y);
        }
        return Vector2.Zero;
    }
}