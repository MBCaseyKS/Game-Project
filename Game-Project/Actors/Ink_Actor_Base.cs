using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Project.Actors
{
    public abstract class Ink_Actor_Base
    {
        protected Vector2 position;

        /// <summary>
        /// The actor's position.
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// The world.
        /// </summary>
        public GameProject World { get; set; }

        /// <summary>
        /// Loads the actor's content.
        /// </summary>
        /// <param name="content">The content manager.</param>
        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// Updates the actor, as in a Tick.
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Updates the actor's visuals.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        /// <param name="spriteBatch">The sprite batch provided.</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        /// <summary>
        /// Spawns an actor.
        /// </summary>
        /// <param name="actor">The actor to spawn.</param>
        /// <param name="actorPosition">Where the actor should spawn.</param>
        public void Spawn(Ink_Actor_Base actor, Vector2 actorPosition)
        {
            if (World != null)
                World.AddActor(actor, actorPosition);
        }

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <returns>The mouse position.</returns>
        public Vector2 GetMousePosition()
        {
            MouseState currentMouseState = Mouse.GetState();
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        /// <summary>
        /// Returns if a mouse is in the area or not.
        /// </summary>
        /// <param name="position">The area's position.</param>
        /// <param name="area">The area's size.</param>
        /// <returns></returns>
        public bool IsMouseInArea(Vector2 position, Vector2 area)
        {
            Vector2 mousePos = GetMousePosition();
            return Math.Abs(mousePos.X - position.X) <= area.X*0.5 && Math.Abs(mousePos.Y - position.Y) <= area.Y * 0.5;
        }

        /// <summary>
        /// Lerps between two float values.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="alpha">The current alpha, typically from 0-1</param>
        /// <returns>The lerped value.</returns>
        public static float Lerp(float start, float end, float alpha)
        {
            return start + (end - start) * alpha;
        }

        /// <summary>
        /// Lerps between two float values.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="alpha">The current alpha, typically from 0-1</param>
        /// <returns>The lerped value.</returns>
        public static double Lerp(double start, double end, double alpha)
        {
            return start + (end - start) * alpha;
        }
    }
}
