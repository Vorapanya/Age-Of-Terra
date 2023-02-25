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

namespace Catap.Object.Protagonist
{
    class AmmoWheel
    {
        private Player _player;
        private Texture2D _texture;
        private Texture2D _wheelCase;
        private SpriteFont _font;

        public float _rotation = 0f;
        public int _current = 1;
        public String[] _amount = new string[11] { "", "X", "", "", "", "", "", "", "", "", "" };
        public AmmoWheel(Texture2D wheel,Texture2D Case,SpriteFont font)
        {
            _texture = wheel;
            _wheelCase = Case;
            //_player = play;
            _font = font;
        }

        public virtual void update(GameTime gametime, Player play)
        {
            _player = play;

            _amount[2] = _player._slowAmount.ToString();
            _amount[3] = _player._sprayAmount.ToString();
            _amount[4] = _player._lineAmount.ToString();
            _amount[5] = _player._bombAmount.ToString();
            _amount[6] = _player._paralyzeAmount.ToString();
            _amount[7] = _player._freezAmount.ToString();
            _amount[8] = _player._burnAmount.ToString();
            _amount[9] = _player._blackholeAmount.ToString();

            if (_current < 1)
                _current = 9;
            if (_current > 9) 
                _current = 1;
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture, new Vector2(640, 740), null, Color.White, _rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 0.4f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_wheelCase, new Vector2(640, 740), null, Color.White, 0f, new Vector2(_wheelCase.Width / 2, _wheelCase.Height / 2), 0.4f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, _amount[_current], new Vector2(625, 675), Color.White);
        }
    }
}
