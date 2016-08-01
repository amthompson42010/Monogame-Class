using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace FishAndBears
{
    class Fish
    {
        #region Fields
        int windowWidth;
        int windowHeight;

        Texture2D sprite;
        const int IMAGES_PER_ROW = 2;
        int frameWidth;
        Rectangle drawRectangle = new Rectangle();
        Rectangle sourceRectangle;

        const int FISH_MOVE_AMOUNT = 5;

        Side front;
        bool active = true;
        #endregion

        #region Constructors
        public Fish(ContentManager contentMangager, string spriteName, int x, int y, int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            drawRectangle.X = x;
            drawRectangle.Y = y;
            LoadContent(contentMangager, spriteName);
        }
        #endregion

        #region Public Properties
        public Rectangle CollisionRectangle
        {
            get
            {
                return drawRectangle;
            }
        }

        public int Front
        {
            get
            {
                if (front == Side.Left)
                {
                    return drawRectangle.X;
                }
                else
                {
                    return drawRectangle.X + drawRectangle.Width;
                }
            }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        #endregion

        #region Private properties
        private int X
        {
            get { return drawRectangle.X + drawRectangle.Width / 2; }
            set
            {
                drawRectangle.X = value - drawRectangle.Width / 2;

                if(drawRectangle.Left < 0)
                {
                    drawRectangle.X = 0;
                }
                else if(drawRectangle.Right > windowWidth)
                {
                    drawRectangle.X = windowWidth - drawRectangle.Width;
                }
            }
        }

        private int Y
        {
            get { return drawRectangle.Y + drawRectangle.Height / 2; }
            set
            {
                drawRectangle.Y = value - drawRectangle.Height / 2;

                if(drawRectangle.Top < 0)
                {
                    drawRectangle.Y = 0;
                }
                else if(drawRectangle.Bottom > windowHeight)
                {
                    drawRectangle.Y = windowHeight - drawRectangle.Height;
                }
            }
        }
        #endregion

        #region Public methods
        public void Update(KeyboardState keyboard)
        {
            if(keyboard.IsKeyDown(Keys.Right))
            {
                X += FISH_MOVE_AMOUNT;

                sourceRectangle.X = 0;
                front = Side.Right;
            }
            if(keyboard.IsKeyDown(Keys.Left))
            {
                X -= FISH_MOVE_AMOUNT;

                sourceRectangle.X = frameWidth;
                front = Side.Left;
            }
            if(keyboard.IsKeyDown(Keys.Up))
            {
                Y -= FISH_MOVE_AMOUNT;
            }
            if(keyboard.IsKeyDown(Keys.Down))
            {
                Y += FISH_MOVE_AMOUNT;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(active)
            {
                spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
            }
        }
        #endregion

        #region Private methods
        private void LoadContent(ContentManager contentManager, string spriteName)
        {
            sprite = contentManager.Load<Texture2D>(spriteName);
            frameWidth = sprite.Width / IMAGES_PER_ROW;
            drawRectangle.Width = frameWidth;
            drawRectangle.Height = sprite.Height;

            X = drawRectangle.X;
            Y = drawRectangle.Y;

            sourceRectangle = new Rectangle(0, 0, frameWidth, sprite.Height);
        }
        #endregion
    }
}