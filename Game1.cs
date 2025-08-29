using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoMandelbrot
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MandelbrotRenderer _mandelbrot;

        private const int DISPLAYWIDTH = 1200;
        private const int DISPLAYHEIGHT = 1200;

        private MouseState _prevMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = DISPLAYHEIGHT;
            _graphics.PreferredBackBufferWidth = DISPLAYWIDTH;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mandelbrot = new MandelbrotRenderer(_graphics, DISPLAYWIDTH, DISPLAYHEIGHT);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();

            bool mouseInsideCanvas = mouseState.X >= 0 && mouseState.X < DISPLAYWIDTH &&
                             mouseState.Y >= 0 && mouseState.Y < DISPLAYHEIGHT;

            if (mouseInsideCanvas)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
                {
                    var logicalCenter = _mandelbrot.canvas_to_logical(mouseState.X, mouseState.Y);
                    _mandelbrot.zoom(logicalCenter, 0.3f);
                    _mandelbrot.GenerateFractal();
                }

                if (mouseState.RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released)
                {
                    _mandelbrot.reset_view();
                    _mandelbrot.GenerateFractal();
                }
            }

            _prevMouseState = mouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_mandelbrot.Texture, Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
