using System;
using System.Diagnostics;
using System.Dynamic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class BoxCollider
{
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
        bool isCollidingBottom = disc.Position.Y + disc.Radius >= this.rectangle.Bottom;
        bool isCollidingTop = disc.Position.Y - disc.Radius <= this.rectangle.Top;
        return isCollidingBottom || isCollidingTop;
    }

    public bool IsCollidingHorizontal(DiscCollider disc)
    {
        bool isCollidingLeft = disc.Position.X - disc.Radius <= this.rectangle.Left;
        bool isCollidingRight = disc.Position.X + disc.Radius >= this.rectangle.Right;
        return isCollidingLeft || isCollidingRight;
    } 
}