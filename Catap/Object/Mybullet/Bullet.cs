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
using Catap.Models;
using Catap.Managers;
using System.Linq;

namespace Catap.Object
{
    class Bullet : ICloneable
    {
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        public Texture2D _texture;
        public float _timer;
        public Enemy _enemy;

        public Body _bulletBody;
        public float _bulletRadius; // player diameter is 1.5 meters
        public Vector2 _bulletPosition;
        public Vector2 _bulletOrigin;
        public Vector2 _bulletSize;
        public float _bulletRotation;
        public string name;
        public bool isRemove = false;
        //if aoe and hit something
        public bool isAoe = false;
        public int life = 2;

        public Bullet(Texture2D texture,World world, Vector2 position)
        {
            _texture = texture;

        }
        
        public virtual void update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _bulletBody.Rotation += 0.3f;
            if (_timer > 10)
            {
                isRemove = true;
                
            }
                
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, _bulletBody.Position, null, Color.White, _bulletBody.Rotation, _bulletOrigin, new Vector2(_bulletRadius * 2f) / _bulletSize, SpriteEffects.FlipVertically, 0f);
        }

        public void Force(float forceX,float forceY)
        {
            _bulletBody.ApplyLinearImpulse(new Vector2(forceX,forceY));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
