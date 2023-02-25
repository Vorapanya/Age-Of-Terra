using Catap.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catap.Stage
{
    public class MenuState : DrawableGameComponent
    {
        private List<Component> _components;
        private Texture2D _menuBG, _logo;
        private Song _menuSong;
        private SoundEffect _click;

        private Stage1 _start;
        private Game _game;
        private SpriteBatch spriteBatch;

        public MenuState(Game game)
          : base(game)
        {
            _game = game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            var buttonTexture_Play = Game.Content.Load<Texture2D>("Buttons/btn-play");
            var buttonTexture_Quit = Game.Content.Load<Texture2D>("Buttons/btn-exit");

            _menuBG = Game.Content.Load<Texture2D>("BG/bgMenu");

            // Sounds
            _click = Game.Content.Load<SoundEffect>("Sounds/SFX/Click");
            _menuSong = Game.Content.Load<Song>("Sounds/BGM/MenuSong");

            // Logo
            //_logo = _content.Load<Texture2D>("logo3");

            var playButton = new Button(buttonTexture_Play)
            {
                Position = new Vector2(515, 348),
            };

            playButton.Click += PlayButton_Click;

            var quitButton = new Button(buttonTexture_Quit)
            {
                Position = new Vector2(515, 448),
            };

            quitButton.Click += QuitButton_Click;

            _components = new List<Component>()
      {
        playButton,
        quitButton,
      };
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(_logo, new Vector2(500, 100), Color.White);
            spriteBatch.Draw(_menuBG, new Vector2(0, 0), Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            MediaPlayer.Stop();
            _start = new Stage1(_game);
            _game.Components.Add(_start);
            _game.Components.Remove(this);
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
