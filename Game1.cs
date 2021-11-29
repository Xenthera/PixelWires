using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using MonoGame.SplineFlower.Spline;
using System;

namespace MyGame
{
    public class Game1 : Game
    {

        public readonly int PIXEL_SCALE = 3;
        public int mouseX { get; private set; }
        public int mouseY { get; private set; }

        public int WIDTH { get { return displayWidth / PIXEL_SCALE; } }
        public int HEIGHT { get { return displayHeight / PIXEL_SCALE; } }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MouseState _mouseState;
        private int displayWidth = 1280;
        private int displayHeight = 720;

        Texture2D face;
        
        

        RenderTarget2D renderTarget;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = displayWidth;
            graphics.PreferredBackBufferHeight = displayHeight;

            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth / PIXEL_SCALE,
                GraphicsDevice.PresentationParameters.BackBufferHeight / PIXEL_SCALE,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            face = Content.Load<Texture2D>("face");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        

        private void DrawGame(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawCircle(mouseX, mouseY, 5, 30, Color.Lime);
            spriteBatch.Draw(face, new Vector2(mouseX, mouseY), Color.White);
            

            spriteBatch.End();
        }

        protected override void Draw(GameTime gameTime)
        {

            _mouseState = Mouse.GetState();
            mouseX = _mouseState.X / PIXEL_SCALE;
            mouseY = _mouseState.Y / PIXEL_SCALE;

            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawGame(gameTime);

            GraphicsDevice.SetRenderTarget(null);

            //GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                        SamplerState.PointClamp, DepthStencilState.Default,
                        RasterizerState.CullNone);

            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, displayWidth, displayHeight), Color.White);
            //spriteBatch.Draw(renderTarget, new Rectangle(0, 0, WIDTH, HEIGHT), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
