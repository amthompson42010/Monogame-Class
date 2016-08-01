using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MovingBears
{
    class TeddyBear
    {
        #region Fields
        Texture2D sprite;
        Rectangle drawRectangle;

        Vector2 velocity = new Vector2(0, 0);

        int windowHeight;
        int windowWidth;

        #endregion

        #region Constructors

        public TeddyBear(ContentManager contentMangager, string spriteName, int x, int y, int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            LoadContent(contentMangager, spriteName, x, y);

            Random rand = new Random();
            int speed = rand.Next(5) + 3;
            double angle = 2 * Math.PI * rand.NextDouble();
            velocity.X = (float)Math.Cos(angle) * speed;
            velocity.Y = -1 * (float)Math.Sin(angle) * speed;
        }

        public TeddyBear(ContentManager contentManager, string spriteName, int x, int y, Vector2 velocity, int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            LoadContent(contentManager, spriteName, x, y);
            this.velocity = velocity;
        }

        public void Update()
        {
            drawRectangle.X += (int)(velocity.X);
            drawRectangle.Y += (int)(velocity.Y);

            BounceTopBottom();
            BounceLeftRight();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion

        #region Private methods

        private void LoadContent(ContentManager contentManager, string spriteName, int x, int y)
        {
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2, y - sprite.Height / 2, sprite.Width, sprite.Height);
        }

        private void BounceTopBottom()
        {
            if(drawRectangle.Y < 0)
            {
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }
            else if((drawRectangle.Y * drawRectangle.Height) > windowHeight)
            {
                drawRectangle.Y = windowHeight - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }

        private void BounceLeftRight()
        {
            if(drawRectangle.X < 0)
            {
                drawRectangle.X = 0;
                velocity.X *= -1;
            }
            else if((drawRectangle.X + drawRectangle.Width) > windowWidth)
            {
                drawRectangle.X = windowWidth - drawRectangle.Width;
                velocity.X *= -1;
            }
        }

        #endregion
    }
}
