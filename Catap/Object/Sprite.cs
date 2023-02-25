using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Catap.Object
{
    public class Sprite
    {
        protected Texture2D _texture;
        public float _rotation;
        public Vector2 _position;
        public Vector2 _origin;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            _origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, _position, null, Color.White, _rotation, _origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
