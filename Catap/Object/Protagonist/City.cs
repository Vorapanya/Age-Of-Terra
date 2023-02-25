using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;

namespace Catap.Object.Protagonist
{
    public class City 
    {

        public float _timer;

        public Texture2D _texture;
        public double life;
        public Body _cityBody;
        //public float _cityRadius; //= 1.0f / 2f; // player diameter is 1.5 meters
        public Vector2 _cityPosition;
        public Vector2 _cityOrigin;
        public Vector2 _citySize;
        public Vector2 _cityTextureSize;
        public float _cityRotation;
        public bool isRemove = false;
        public bool isDefeat = false;


        public City(Texture2D texture, World world)
        {
            _texture = texture;
        }

        public virtual void update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, _cityBody.Position, null, Color.White, _cityBody.Rotation, _cityOrigin, _citySize / _cityTextureSize, SpriteEffects.FlipVertically, 0f);
        }
    }
}
