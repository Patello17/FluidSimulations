using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Particle
{
    private const float TEXTURE_SCALE = 0.0042f;
    private Texture2D texture;
    private DiscCollider collider;
    private Vector2 pos;
    private Vector2 vel;
    

    public Particle(Vector2 _pos, Vector2 _vel, float _radius)
    {
        this.pos = _pos;
        this.vel = _vel;
        this.collider = new DiscCollider(_pos, _radius);
    }

    public void Update(GameTime gameTime)
    {
        pos += vel * gameTime.ElapsedGameTime.Milliseconds;
        // vel += new Vector2(0, 0.098f);
    }
    
    public void LoadContent(ContentManager content)
    {
        this.texture = content.Load<Texture2D>("particle"); // 480 x 480
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(this.texture, this.pos,
                         null, Color.White,
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
        return this.collider.IsColliding(other.GetCollider());
    }
}