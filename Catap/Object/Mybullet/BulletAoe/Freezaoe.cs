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

namespace Catap.Object.Mybullet.BulletAoe
{
    class Freezaoe : Bullet
    {
        public Freezaoe(Texture2D texture, World world, Vector2 position, ContentManager _content)
            : base(texture, world, position)
        {
            _animations = new Dictionary<string, Animation>
                {
                    { "boom", new Animation(_content.Load<Texture2D>("Eff/FreezeRe"), 10 ,0.1f) },
                };
            _animationManager = new AnimationManager(_animations.First().Value);

            /* Circle */
            //_bulletPosition = new Vector2(0, _bulletRadius);
            name = "aoefreez";
            _bulletPosition = position;
            _bulletOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            _bulletRadius = 2.0f / 2f;

            // Create the player fixture

            _bulletBody = world.CreateBody(_bulletPosition, 0, BodyType.Static);
            var pfixture = _bulletBody.CreateCircle(_bulletRadius, 1f);
            pfixture.Tag = "Aoefreez";
            // Give it some bounce and friction
            pfixture.Restitution = 0.65f;
            pfixture.Friction = 1.0f;

            // We scale the ground and player textures at body dimensions
            _bulletSize = new Vector2(_texture.Width, _texture.Height);

            _bulletBody.OnCollision += _bulletBody_OnCollision;

            // We draw the ground and player textures at the center of the shapes
            _bulletOrigin = _bulletSize / 2f;
        }

        private bool _bulletBody_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            /*if ((string)other.Tag == "ground" || (string)other.Tag == "enemy" || (string)other.Tag == "ebullet")
            {
                life -= 1;
                if (life <= 0)
                {
                    isRemove = true;
                }
                //true mean allow collision
                //isRemove = true;
                return false;
            }
            //false mean allow no collision*/
            return false;

        }

        public override void update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _animationManager.Play(_animations["boom"]);
            _animationManager.Update(gameTime);
            if (_timer > 1)
            {
                isRemove = true;

            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _animationManager.Draw(spriteBatch, _bulletBody.Position, _bulletBody.Rotation, _bulletOrigin, new Vector2(_bulletRadius * 2f) / _bulletSize);
            //spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            //else
            //spriteBatch.Draw(_texture, _enemyBody.Position, null, Color.White, _enemyBody.Rotation, _enemyOrigin, new Vector2(_enemyRadius * 2f) / _enemySize, SpriteEffects.FlipVertically, 0f);
        }
    }
}
