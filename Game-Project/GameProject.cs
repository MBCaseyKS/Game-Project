using Game_Project.Actors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game_Project
{
    public class GameProject : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont cursecasual;

        private List<Ink_Actor_Base> _actors;

        public GameProject()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _actors = new List<Ink_Actor_Base>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            AddActor(new Ink_Titlescreen(), new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height * 0.3f));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            cursecasual = Content.Load<SpriteFont>("CurseCasual");

            foreach (Ink_Actor_Base actor in _actors)
                actor.LoadContent(Content);
        }

        /// <summary>
        /// Spawns an actor and adds it to the list.
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Ink_Actor_Base actor, Vector2 actorPosition)
        {
            actor.World = this;
            actor.Position = actorPosition;
            actor.LoadContent(Content);
            _actors.Add(actor);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            List<Ink_Actor_Base> currentActors = new List<Ink_Actor_Base>(_actors);

            foreach (Ink_Actor_Base actor in currentActors)
                actor.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            List<Ink_Actor_Base> currentActors = new List<Ink_Actor_Base>(_actors);
            foreach (Ink_Actor_Base actor in currentActors)
                actor.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
