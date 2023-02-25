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

namespace Catap.Object.Mybullet.BulletAoe
{
    class Blackhole : Bullet
    {
        public Blackhole(Texture2D texture, World world, Vector2 position)
            : base(texture, world, position)
        {
            /* Circle */
            //_bulletPosition = new Vector2(0, _bulletRadius);
            name = "blackholebullet";
            _bulletPosition = position;
            _bulletOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            _bulletRadius = 0.5f / 2f;

            // Create the player fixture

            _bulletBody = world.CreateBody(_bulletPosition, 0, BodyType.Dynamic);
            var pfixture = _bulletBody.CreateCircle(_bulletRadius, 1f);
            pfixture.Tag = "blackholebullet";
            // Give it some bounce and friction
            pfixture.Restitution = 0.65f;
            pfixture.Friction = 1.0f;

            _bulletBody.Mass = 1;
            // We scale the ground and player textures at body dimensions
            _bulletSize = new Vector2(_texture.Width, _texture.Height);

            _bulletBody.OnCollision += _bulletBody_OnCollision;

            // We draw the ground and player textures at the center of the shapes
            _bulletOrigin = _bulletSize / 2f;
        }

        private bool _bulletBody_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {

            if ((string)other.Tag == "enemy" || (string)other.Tag == "ground"|| (string)other.Tag == "suicide")
            {
                isAoe = true;
                isRemove = true;
            }
            //false mean allow no collision
            return false;

        }
    }
}
