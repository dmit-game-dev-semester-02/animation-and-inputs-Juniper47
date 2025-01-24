using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimationInputJuniper;

public class AnimationInput : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _eous, _backgroundImage;
    private const int _WindowHeight = 320;
    private const int _WindowWidth = 640;

    public AnimationInput()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _backgroundImage = Content.Load<Texture2D>("SixthStreet");
        _eous = Content.Load<Texture2D>("Eous");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundImage, Vector2.Zero, null,
        Color.White, 0f, Vector2.Zero, 0.35f, SpriteEffects.None, 0f);

        _spriteBatch.Draw(_eous, new Vector2(5,175), null,
        Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
