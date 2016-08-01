using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FishAndBears
{
    class TeddyBear
    {
        #region Fields
        Texture2D sprite;
        Rectangle drawRectangle = new Rectangle();

        Vector2 velocity = new Vector2(0, 0);

        bool active = true;

        int windowWidth;
        int windowHeight;
        #endregion

        #region Constructors
        public TeddyBear(ContentManager contentManager, string spriteName, int x, int y, int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            LoadContent(contentManager, spriteName, x, y);

            Random rand = new Random();
            double angle = 2 * Math.PI * rand.NextDouble();
            double xDirection = Math.Cos(angle);
            double yDirection = -1 * Math.Sin(angle);
            int speed = rand.Next(5) + 2;

            velocity.X = (float)(speed * xDirection);
            velocity.Y = (float)(speed * yDirection);
        }
        #endregion

        #region Properties
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        #endregion

        #region Public Methods
        public void Update()
        {
            if(active)
            {
                drawRectangle.X += (int)(velocity.X);
                drawRectangle.Y += (int)(velocity.Y);

                BounceTopBottom();
                BounceLeftRight();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(active)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }
        }

        public void Bounce()
        {
            velocity.X *= -1;
            velocity.Y *= -1;
        }
        #endregion

        #region Private Methods
        private void LoadContent(ContentManager contentManager, string spriteName, int x, int y)
        {
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2, y - sprite.Height / 2, sprite.Width, sprite.Height);
        }

        private void BounceTopBottom()
        {
            if(drawRectangle.Top > 0)
            {
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }
            else if(drawRectangle.Bottom > windowHeight)
            {
                drawRectangle.Y = windowHeight - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }

        private void BounceLeftRight()
        {
            if(drawRectangle.Left < 0)
            {
                drawRectangle.X = 0;
                velocity.X *= -1;
            }
            else if(drawRectangle.Right > windowWidth)
            {
                drawRectangle.X = windowWidth - drawRectangle.Width;
                velocity.X *= -1;
            }
        }
        #endregion
    }
}
