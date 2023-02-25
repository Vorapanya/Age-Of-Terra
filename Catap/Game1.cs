using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Catap.Stage;

namespace Catap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private MenuState _menu;

        Texture2D bg;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.GraphicsProfile = GraphicsProfile.Reach;
            

        }

        protected override void Initialize()
        {
            //_helloWorld = new Test(this);
            //Components.Add(_helloWorld);
            _menu = new MenuState(this);
            Components.Add(_menu);
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();

        }

        protected override void LoadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}