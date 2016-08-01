using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MovingBears
{
    class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        TeddyBear bear0;
        TeddyBear bear1;
        TeddyBear bear2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bear0 = new TeddyBear(Content, "teddybear0", 100, 100, WINDOW_WIDTH, WINDOW_HEIGHT);
            bear1 = new TeddyBear(Content, "teddybear1", 200, 100, WINDOW_WIDTH, WINDOW_HEIGHT);
            bear2 = new TeddyBear(Content, "teddybear2", 300, 100, WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            bear0.Update();
            bear1.Update();
            bear2.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            bear0.Draw(spriteBatch);
            bear1.Draw(spriteBatch);
            bear2.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
