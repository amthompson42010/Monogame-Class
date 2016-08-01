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
    class Menu
    {
        #region Fields
        MenuButton playButton;
        MenuButton quitButton;
        #endregion

        #region Constructors
        public Menu(ContentManager contentManager, int windowWidth, int windowHeight)
        {
            int centerX = windowWidth / 2;
            int topCenterY = windowHeight / 4;
            Vector2 buttonCenter = new Vector2(centerX, topCenterY);

            playButton = new MenuButton(contentManager.Load<Texture2D>("playbutton"),
                buttonCenter, GameState.Play);
            buttonCenter.Y += windowHeight / 2;
            quitButton = new MenuButton(contentManager.Load<Texture2D>("quitbutton"),
                buttonCenter, GameState.Quit);
        }
        #endregion

        #region Public Methods
        public void Update(MouseState mouse, SoundBank soundBank)
        {
            playButton.Update(mouse, soundBank);
            quitButton.Update(mouse, soundBank);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
        }
        #endregion
    }
}
