using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using Catap.Object;
using System.Collections.Generic;
using System;

namespace Catap.Object.Protagonist
{
    class Fort : City
    {

        //public bool isShoot = false;

        public Fort(Texture2D texture, World world)
            : base(texture, world)
        {

            /* Circle */
            _citySize = new Vector2(2f, 3f);
            _cityPosition = new Vector2(-6.5f, 1.5f);

            // Create the player fixture

            _cityBody = world.CreateBody(_cityPosition, 0, BodyType.Static);
            var cfixture = _cityBody.CreateRectangle(_citySize.X, _citySize.Y, 1f, Vector2.Zero);
            cfixture.Tag = "wall";
            // Give it some bounce and friction
            cfixture.Restitution = 0.65f;
            cfixture.Friction = 1.0f;

            // We scale the ground and player textures at body dimensions
            _cityTextureSize = new Vector2(_texture.Width, _texture.Height);

            _cityBody.OnCollision += _enemyBody_OnCollision;


            // We draw the ground and player textures at the center of the shapes
            _cityOrigin = _cityTextureSize / 2f;
        }

        private bool _enemyBody_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {

            if ((string)other.Tag == "ebomb")
            {
                return true;
            }
            if ((string)other.Tag == "ebullet")
            {
                //System.Diagnostics.Debug.WriteLine(other.Tag);
                return true;
            }
            if ((string)other.Tag == "efly")
            {
                return true;
            }
            return false;
        }

        public override void update(GameTime gameTime)
        {

        }
    }
}
