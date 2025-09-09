using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project.Actors
{
    public class Ink_Titlescreen : Ink_Actor_Base
    {
        private Texture2D starTexture;
        private Texture2D titletexture;

        private double IntroTimer;
        private double BobProgress;
        private double ExitTimer;
        private double ScrollProgress;

        /// <summary>
        /// Updates the actor, as in a Tick.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void LoadContent(ContentManager content)
        {
            titletexture = content.Load<Texture2D>("T2D_TitleText_Placeholder");
            starTexture = content.Load<Texture2D>("T2D_FourPointStar");
        }

        /// <summary>
        /// Updates the actor's visuals.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        /// <param name="spriteBatch">The sprite batch provided.</param>
        public override void Update(GameTime gameTime)
        {
            double prev = IntroTimer;
            IntroTimer = Math.Min(IntroTimer + gameTime.ElapsedGameTime.TotalSeconds, 0.9);
            if (prev < 0.9 && IntroTimer >= 0.9)
            {
                Spawn(new Ink_Button_Base("CurseCasual", "PLAY!", Color.SeaGreen, OnPlay), Position + new Vector2(-200, 200));
                Spawn(new Ink_Button_Base("CurseCasual", "Exit", Color.DarkRed, OnExit), Position + new Vector2(200, 200));
            }

            ScrollProgress = (ScrollProgress + gameTime.ElapsedGameTime.TotalSeconds) % 2.0;
            if (IntroTimer >= 0.9)
                BobProgress = (BobProgress + gameTime.ElapsedGameTime.TotalSeconds) % 4.0;
            if (ExitTimer > 0)
            {
                ExitTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (ExitTimer <= 0)
                    OnExit(null);
            }
        }

        /// <summary>
        /// Loads the actor's content.
        /// </summary>
        /// <param name="content">The content manager.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float alpha = (float)Math.Min(IntroTimer * 2, 1.0);

            if (starTexture != null)
            {
                int rowStars = World.GraphicsDevice.Viewport.Bounds.Width / 128 + 2;
                int colStars = World.GraphicsDevice.Viewport.Bounds.Height / 128 + 2;
                float scrollOffset = (float)Lerp(0.0, 128, ScrollProgress * 0.5);
                Vector2 currentPos = new Vector2(scrollOffset - 128, scrollOffset - 128);
                for (int i = 0; i < rowStars; i++)
                {
                    for (int e = 0; e < colStars; e++)
                    {
                        spriteBatch.Draw(starTexture, currentPos, null, Color.DarkOliveGreen, (float)(Math.PI * ScrollProgress * 0.25), new Vector2(256,256), 0.125f, SpriteEffects.None, 0);
                        currentPos.Y += 128;
                    }

                    currentPos.X += 128;
                    currentPos.Y = scrollOffset - 128;
                }
            }

            alpha = (float)Math.Min(IntroTimer / 0.4, 1.0);
            float HeightOffset = (1.0f - alpha) * -250;

            alpha = (float)Math.Clamp((IntroTimer - 0.4) * 2, 0.0, 1.0);
            HeightOffset -= (float)Math.Sin(Math.PI*alpha) * 100;
            HeightOffset -= 10 * (float)Math.Sin(Math.PI * 0.5 * BobProgress);

            Vector2 positionOffset = new Vector2(0, HeightOffset);
            spriteBatch.Draw(titletexture, Position + positionOffset, null, Color.White, 0, new Vector2(512,256), 0.5f, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Handles when the play button is clicked.
        /// </summary>
        /// <param name="button">The button clicked.</param>
        public void OnPlay(Ink_Button_Base button)
        {
            ExitTimer = 0.5;
            button.Message = "No. :)";
        }

        /// <summary>
        /// Handles when the play button is clicked.
        /// </summary>
        /// <param name="button">The button clicked.</param>
        public void OnExit(Ink_Button_Base button)
        {
            if (World != null)
                World.Exit();
        }
    }
}
