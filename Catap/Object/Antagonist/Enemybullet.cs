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
using Catap.Object;

namespace Catap.Object
{
    class Enemybullet
    {

        /*public Body _bulletBody;
        public float _bulletRadius = 0.5f / 2f; // player diameter is 1.5 meters
        public Vector2 _bulletPosition;
        public Vector2 _bulletOrigin;
        public Vector2 _bulletSize;
        public float _bulletRotation;*/
        public float _timer;

        public Texture2D _texture;
        public Texture2D _blankText;
        public Body _enemyBody;
        public float _enemyRadius = 1.5f / 2f; // player diameter is 1.5 meters
        public Vector2 _enemyPosition;
        public Vector2 _enemyOrigin;
        public Vector2 _enemySize;
        public float _enemyRotation;
        public bool isRemove = false;
        public bool isAoe = false;

        public Enemybullet(Texture2D texture, World world,Vector2 position)
        {
            _texture = texture;
        }

        public virtual void update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _enemyBody.Rotation += 0.3f;
            if (_timer > 10)
            {
                isRemove = true;
                
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, _enemyBody.Position, null, Color.White, _enemyBody.Rotation, _enemyOrigin, new Vector2(_enemyRadius * 2f) / _enemySize, SpriteEffects.FlipVertically, 0f);
        }

        public void Force(float forceX, float forceY)
        {
            _enemyBody.ApplyLinearImpulse(new Vector2(-forceX, forceY));
        }

    }
}
