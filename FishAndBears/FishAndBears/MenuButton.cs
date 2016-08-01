using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FishAndBears
{
    public class MenuButton
    {
        #region Fields
        Texture2D sprite;
        const int IMAGES_PER_ROW = 2;
        int buttonWidth;

        Rectangle drawRectangle;
        Rectangle sourceRectangle;

        GameState clickState;
        bool clickStarted = false;
        bool buttonReleased = true;
        #endregion

        #region Constructors
        public MenuButton(Texture2D sprite, Vector2 center, GameState clickState)
        {
            this.sprite = sprite;
            this.clickState = clickState;
            Initialize(center);
        }
        #endregion

        #region Public Methods
        public void Update(MouseState mouse, SoundBank soundBank)
        {
            if(drawRectangle.Contains(mouse.X, mouse.Y))
            {
                sourceRectangle.X = buttonWidth;

                if(mouse.LeftButton == ButtonState.Pressed && buttonReleased)
                {
                    clickStarted = true;
                    buttonReleased = false;
                    soundBank.PlayCue("buttonClick");
                }
                else if(mouse.LeftButton == ButtonState.Released)
                {
                    buttonReleased = true;

                    if(clickStarted)
                    {
                        Game1.ChangeState(clickState);
                        clickStarted = false;
                    }
                }
            }
            else
            {
                sourceRectangle.X = 0;

                clickStarted = false;
                buttonReleased = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, sourceRectangle, Color.White);
        }
        #endregion

        #region Private Methods
        private void Initialize(Vector2 center)
        {
            buttonWidth = sprite.Width / IMAGES_PER_ROW;

            drawRectangle = new Rectangle(
                (int)(center.X - buttonWidth / 2),
                (int)(center.Y - sprite.Height / 2),
                buttonWidth, sprite.Height);
            sourceRectangle = new Rectangle(0, 0, buttonWidth, sprite.Height);
        }
        #endregion
    }
}
