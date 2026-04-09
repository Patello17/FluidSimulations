using System;
using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class DiscCollider
{
    private Vector2 pos;
    public Vector2 Position
    {
        get {return this.pos; }
        set {this.pos = value; }
    }
    private float radius;
    public float Radius
    {
        get { return this.radius; }
        set { this.radius = value; }
    }

    public DiscCollider(Vector2 _pos, float _radius)
    {
        this.Position = _pos;
        this.Radius = _radius;
    }

    public void Update(GameTime gameTime)
    {
        
    }
    
    public bool IsColliding(DiscCollider other)
    {
        float delta_x = this.Position.X - other.Position.X;
        float delta_y = this.Position.Y - other.Position.Y;
        float dist_btw_center = (float)Math.Sqrt(delta_x * delta_x + delta_y * delta_y);
        return  dist_btw_center <= this.Radius + other.Radius;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // spriteBatch.Draw(this.texture, this.pos,
        //                  null, Color.White,
        //                  0f, Vector2.Zero,
        //                  0.1f, SpriteEffects.None, 0f);
    }
}