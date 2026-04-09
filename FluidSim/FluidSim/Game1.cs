using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FluidSim;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Particle particleA = new Particle(new Vector2(200, 400),
                                              new Vector2(1, 0), 50);
    private Particle particleB = new Particle(new Vector2(800, 400),
                                              new Vector2(-1, 0), 50);

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 1800;
        _graphics.PreferredBackBufferHeight = 1200;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        particleA.LoadContent(Content);
        particleB.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        particleA.Update(gameTime);
        particleB.Update(gameTime);
        Debug.WriteLine(particleA.IsColliding(particleB));

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        particleA.Draw(this._spriteBatch);
        particleB.Draw(this._spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
