using CelAnimation;
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
    private CelAnimationPlayer _idlePlayer, _bobPlayer;
    private CelAnimationSequence _personIdle, _bobRunning;
    private CelAnimationPlayerMultiRow _multiRedPlayer;
    private CelAnimationSequenceMultiRow _multiRed;

    private float _bobX = 200, _bobSpeed = 1, _redX, _redY, _redSpeedX = 1, _redSpeedY = 1;
    private bool _bobRight = true;
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

        //idle animation bald guy
        Texture2D idleSpriteSheet = Content.Load<Texture2D>("Idle");
        _personIdle = new CelAnimationSequence(idleSpriteSheet, 37, 1 / 4f);
        _idlePlayer = new CelAnimationPlayer();
        _idlePlayer.Play(_personIdle);

        //bob with red hat
        Texture2D bobSpriteSheet = Content.Load<Texture2D>("Bob");
        _bobRunning = new CelAnimationSequence(bobSpriteSheet, 128, 1 / 6f);
        _bobPlayer = new CelAnimationPlayer();
        _bobPlayer.Play(_bobRunning);

        Texture2D redSpriteSheet = Content.Load<Texture2D>("Red");
        _multiRed = new CelAnimationSequenceMultiRow(redSpriteSheet, 64, 1 / 6f, 2);
        _multiRedPlayer = new CelAnimationPlayerMultiRow();
        _multiRedPlayer.Play(_multiRed);


    }

    protected override void Update(GameTime gameTime)
    {
        _idlePlayer.Update(gameTime);

        //Hit m key and he will turn around
        _bobPlayer.Update(gameTime);
        KeyboardState kbCurrentState = Keyboard.GetState();
        if (kbCurrentState.IsKeyDown(Keys.M))
        {
            _bobSpeed *= -1;
            _bobRight = !_bobRight;
        }

        if (_bobX > 500)
        {
            _bobX = 500;
        }
        else if (_bobX < 190)
        {
            _bobX = 190;
        }
        else
        {
            _bobX += _bobSpeed;
        }

        _multiRedPlayer.Update(gameTime);
        if (kbCurrentState.IsKeyDown(Keys.S))
        {
            _multiRedPlayer.celSourceRectangle.Y = 0;
            _redY += _redSpeedY;
            if (_redSpeedY < 0) _redSpeedY *= -1;
        }
        if (kbCurrentState.IsKeyDown(Keys.A))
        {
            _multiRedPlayer.celSourceRectangle.Y = 64;
            _redX += _redSpeedX;
            if (_redSpeedX > 0) _redSpeedX *= -1;
        }
        if (kbCurrentState.IsKeyDown(Keys.D))
        {
            _multiRedPlayer.celSourceRectangle.Y = 128;
            _redX += _redSpeedX;
            if (_redSpeedX < 0) _redSpeedX *= -1;
        }
        if (kbCurrentState.IsKeyDown(Keys.W))
        {
            _multiRedPlayer.celSourceRectangle.Y = 194;
            _redY += _redSpeedY;
            if (_redSpeedY > 0) _redSpeedY *= -1;
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        //background image
        _spriteBatch.Draw(_backgroundImage, Vector2.Zero, null,
        Color.White, 0f, Vector2.Zero, 0.35f, SpriteEffects.None, 0f);

        //still image
        _spriteBatch.Draw(_eous, new Vector2(5, 175), null,
        Color.White, 0f, Vector2.Zero, 0.2f, SpriteEffects.None, 0f);

        //idle animation bald guy
        _idlePlayer.Draw(_spriteBatch, new Vector2(65, 175), SpriteEffects.None);

        //bob running guy
        if (_bobRight == true)
        {
            _bobPlayer.Draw(_spriteBatch, new Vector2(_bobX, 100), SpriteEffects.None);
        }
        else if (_bobRight == false)
        {
            _bobPlayer.Draw(_spriteBatch, new Vector2(_bobX, 100), SpriteEffects.FlipHorizontally);
        }

        //red pokemon guy
        _multiRedPlayer.Draw(_spriteBatch, new Vector2(_redX, _redY), SpriteEffects.None);


        _spriteBatch.End();
        base.Draw(gameTime);
    }
}