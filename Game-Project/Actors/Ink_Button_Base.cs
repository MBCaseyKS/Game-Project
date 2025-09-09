using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project.Actors
{
    public class Ink_Button_Base : Ink_Actor_Base
    {
        MouseState currentMouseState, previousMouseState;

        private Texture2D buttonTexture;
        private string fontName;
        private string buttonMessage;
        private Color buttonColor = Color.DarkGray;
        private SpriteFont myFont;

        /// <summary>
        /// The button's message.
        /// </summary>
        public string Message
        {
            get => buttonMessage; 
            set => buttonMessage = value;
        }

        /// <summary>
        /// The button's color.
        /// </summary>
        public Color ButtonColor
        {
            get => buttonColor;
            set => buttonColor = value;
        }

        private double introProgress;

        private bool IsHovered;
        private bool WasPressed;
        private double hoverAlpha;

        public delegate void OnButtonClick(Ink_Button_Base button);
        private OnButtonClick buttonClick;

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="fontName">The font name for the button.</param>
        /// <param name="fontText">The button's display text.</param>
        public Ink_Button_Base(string fontName, string fontText)
        {
            this.fontName = fontName;
            Message = fontText;
        }

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="fontName">The font name for the button.</param
        /// <param name="fontText">The button's display text.</param>
        /// <param name="buttonColor">The button's color.</param>
        public Ink_Button_Base(string fontName, string fontText, Color buttonColor)
        {
            this.fontName = fontName;
            Message = fontText;
            ButtonColor = buttonColor;
        }

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="fontName">The font name for the button.</param>
        /// <param name="fontText">The button's display text.</param>
        /// <param name="buttonClickDel">The delegate for clicking the button.</param>
        public Ink_Button_Base(string fontName, string fontText, OnButtonClick buttonClickDel)
        {
            this.fontName = fontName;
            Message = fontText;
            this.buttonClick = buttonClickDel;
        }

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="fontName">The font name for the button.</param>
        /// <param name="fontText">The button's display text.</param>
        /// <param name="buttonColor">The button's color.</param>
        /// <param name="buttonClickDel">The delegate for clicking the button.</param>
        public Ink_Button_Base(string fontName, string fontText, Color buttonColor, OnButtonClick buttonClickDel)
        {
            this.fontName = fontName;
            Message = fontText;
            ButtonColor = buttonColor;
            this.buttonClick = buttonClickDel;
        }

        /// <summary>
        /// Updates the actor, as in a Tick.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void LoadContent(ContentManager Content)
        {
            buttonTexture = Content.Load<Texture2D>("T2D_Button_Basic");

            if (fontName != "")
                myFont = Content.Load<SpriteFont>(fontName);
        }

        /// <summary>
        /// Updates the actor's visuals.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        /// <param name="spriteBatch">The sprite batch provided.</param>
        public override void Update(GameTime gameTime)
        {
            if (introProgress < 1.0)
                introProgress = Math.Min(introProgress + gameTime.ElapsedGameTime.TotalSeconds*4, 1.0);

            IsHovered = introProgress >= 1.0 && IsMouseInArea(Position, new Vector2(256, 128));
            hoverAlpha = Math.Clamp(hoverAlpha + gameTime.ElapsedGameTime.TotalSeconds * (IsHovered ? 8 : -8), 0.0, 1.0);

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (IsHovered && !WasPressed && currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                WasPressed = true;
                if (buttonClick != null)
                    buttonClick(this);
            }
        }

        /// <summary>
        /// Loads the actor's content.
        /// </summary>
        /// <param name="content">The content manager.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float scale = Lerp(0.01f, 1.0f, (float)introProgress);
            scale *= (float) Lerp(1.0, 1.1, hoverAlpha);
            spriteBatch.Draw(buttonTexture, Position, null, ButtonColor, 0, new Vector2(256, 128), scale * 0.5f, SpriteEffects.None, 0);
            if (Message != "" && myFont != null)
            {
                Vector2 strLen = myFont.MeasureString(Message);
                spriteBatch.DrawString(myFont, Message, Position, Color.White, 0f, new Vector2(0, 0) + strLen * 0.5f, scale * 2f, SpriteEffects.None, 1);
            }
        }
    }
}
