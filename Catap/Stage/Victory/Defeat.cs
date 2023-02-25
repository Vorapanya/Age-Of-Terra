using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Catap.Object;
using Catap.Object.Mybullet;
using Catap.Object.Mybullet.BulletAoe;
using Catap.Object.Protagonist;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Catap.Managers;

namespace Catap.Stage
{
    class Defeat : DrawableGameComponent
    {
        // Sounds
        private SoundEffect _click;
        private Song _lose;

        private List<Component> _components;
        private MenuState _menuState;

        private Game _game;
        private SpriteBatch _spriteBatch;
        private Texture2D _backGround;
        private Stage1 _test;
        private SpriteFont _font;



        public Defeat(Game game)
            : base(game)
        {
            _game = game;
        }

        protected override void LoadContent()
        {
            var buttonTexture_Home = Game.Content.Load<Texture2D>("Buttons/btn-home");
            var homeButton = new Button(buttonTexture_Home)
            {
                Position = new Vector2(620, 460),
            };
            homeButton.Click += HomeButton_Click;

            _components = new List<Component>()
            {
                homeButton,
            };
            // Sounds
            _click = Game.Content.Load<SoundEffect>("Sounds/SFX/Click");
            _lose = Game.Content.Load<Song>("Sounds/BGM/Lose");
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(_lose);

            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            _font = Game.Content.Load<SpriteFont>("Font/font");

            _backGround = Game.Content.Load<Texture2D>("BG/GameOverBackground");

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backGround, new Vector2(640, 360), null, Color.White, 0f, origin(_backGround), 1f, SpriteEffects.None, 0f);
            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
        }
        public Vector2 origin(Texture2D ori)
        {
            Vector2 _origin;
            return _origin = new Vector2(ori.Width / 2, ori.Height / 2);
        }
        private void HomeButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            MediaPlayer.Stop();
            _menuState = new MenuState(_game);
            _game.Components.Add(_menuState);
            _game.Components.Remove(this);
        }
    }
}
