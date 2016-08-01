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

namespace FishAndBears
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int WINDOW_WIDTH = 584;
        const int WINDOW_HEIGHT = 438;

        Menu mainMenu;

        Fish fish;
        List<TeddyBear> bears = new List<TeddyBear>();

        static GameState state;

        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            audioEngine = new AudioEngine(@"Content\GameAudio.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Sound Bank.xsb");

            soundBank.PlayCue("backgroundMusic");

            mainMenu = new Menu(Content, WINDOW_WIDTH, WINDOW_HEIGHT);

            fish = new Fish(Content, "fish", WINDOW_WIDTH / 6, WINDOW_WIDTH / 6, WINDOW_WIDTH, WINDOW_HEIGHT);

            string baseSpriteName = "teddybear";
            Random rand = new Random();
            for(int x = 150; x <= 600; x += 150)
            {
                for(int y = 150; y <= 450; y += 150)
                {
                    bears.Add(new TeddyBear(Content, baseSpriteName + rand.Next(3), x, y, WINDOW_WIDTH, WINDOW_HEIGHT));
                }
            }
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if(state == GameState.MainMenu)
            {
                mainMenu.Update(Mouse.GetState(), soundBank);
            }
            else if(state == GameState.Play)
            {
                fish.Update(Keyboard.GetState());

                foreach(TeddyBear bear in bears)
                {
                    bear.Update();
                }

                CheckAndResolveFishBearCollisions();

                CheckAndResolveBearCollisions();

                if(bears.Count == 0)
                {
                    ChangeState(GameState.Quit);
                }
            }
            else
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if(state == GameState.MainMenu)
            {
                mainMenu.Draw(spriteBatch);
            }
            else if(state == GameState.Play)
            {
                fish.Draw(spriteBatch);
                foreach(TeddyBear bear in bears)
                {
                    bear.Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void ChangeState(GameState newState)
        {
            state = newState;
        }

        private void CheckAndResolveBearCollisions()
        {
            for(int i = 0; i < bears.Count; i++)
            {
                for(int j = i + 1; j < bears.Count; j++)
                {
                    if(bears[i].Active && bears[j].Active && bears[i].CollisionRectangle.Intersects(bears[j].CollisionRectangle))
                    {
                        bears[i].Bounce();
                        bears[j].Bounce();
                    }
                }
            }
        }

        private void CheckAndResolveFishBearCollisions()
        {
            for(int i = bears.Count - 1; i >= 0; i--)
            {
                if(fish.Active && bears[i].Active && fish.CollisionRectangle.Intersects(bears[i].CollisionRectangle))
                {
                    Rectangle overlap = Rectangle.Intersect(fish.CollisionRectangle, bears[i].CollisionRectangle);
                    if(overlap.Left <= fish.Front && overlap.Right >= fish.Front)
                    {
                        bears.RemoveAt(i);

                        soundBank.PlayCue("destroy");
                    }
                    else
                    {
                        bears[i].Bounce();

                        soundBank.PlayCue("bounce");
                    }
                }
            }
        }

        private void RemoveDeadTeddyBears()
        {
            for(int i = bears.Count - 1; i >= 0; i--)
            {
                if(!bears[i].Active)
                {
                    bears.RemoveAt(i);
                }
            }
        }

        private void CheckBearsAllDead()
        {
            bool allBearsDead = true;
            foreach(TeddyBear bear in bears)
            {
                if(bear.Active)
                {
                    allBearsDead = false;
                    break;
                }
                if(allBearsDead)
                {
                    ChangeState(GameState.Quit);
                }
            }
        }
    }
}
