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

namespace Catap.Object
{
    class Enemy : ICloneable
    {
        
        public float _timer;
        public float _slowTimer, _burnTimer, _freezTimer, _paralyzeTimer, _suckTimer;
        public float _shootTimer;

        public Texture2D _texture;
        public Body _enemyBody;
        public float _enemyRadius; //= 1.0f / 2f; // player diameter is 1.5 meters
        public Vector2 _enemyPosition;
        public Vector2 _enemyOrigin;
        public Vector2 _enemySize;
        public float _enemyRotation;
        public bool isRemove = false;
        public bool isShoot = false;
        public bool isShootbomb = false;
        public bool isDropbomb = false;
        public bool isSpecialgun = false;
        public bool isSpecialsummon = false;
        public bool isSpecialbomb = false;
        public bool isSuck = false;
        public bool isAoe = false;
        public String _aoeName;


        public Enemy(Texture2D texture, World world)
        {
            _texture = texture;
        }

        public virtual void update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(_texture, _enemyBody.Position, null, Color.White, _enemyBody.Rotation, _enemyOrigin, new Vector2(_enemyRadius * 2f) / _enemySize, SpriteEffects.FlipVertically, 0f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
