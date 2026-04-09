using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Particle
{
    private Texture2D texture;
    private Vector2 pos;
    private Vector2 vel;

    public Particle(Vector2 _pos)
    {
        this.pos = _pos;
        this.vel = Vector2.Zero;
    }

    public void Update(GameTime gameTime)
    {
        pos += vel * gameTime.ElapsedGameTime.Milliseconds;
        vel += new Vector2(0, 0.098f);
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
                         0.1f, SpriteEffects.None, 0f);
    }
}