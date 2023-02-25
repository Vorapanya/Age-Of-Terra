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
    class Wall : City
    {

        //public bool isShoot = false;
        private Texture2D _wall2,_wall3;

        public Wall(Texture2D texture, World world,ContentManager _content)
            : base(texture, world)
        {

            _wall2 = _content.Load<Texture2D>("Building/Wall2");
            _wall3 = _content.Load<Texture2D>("Building/Wall3");
            /* Circle */
            _citySize = new Vector2(1f, 3f);
            _cityPosition = new Vector2(-1.6f, 1.5f);

            life = 100;

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
                life -= 2;
                
                return false;
            }
            if ((string)other.Tag == "suicide")
            {
                life -= 10;

                return true;
            }
            if ((string)other.Tag == "ebullet")
            {
                //System.Diagnostics.Debug.WriteLine(other.Tag);
                return true;
            }
            if ((string)other.Tag == "efly")
            {
                
                life -= 4;
                //System.Diagnostics.Debug.WriteLine(life);
                return false;
            }
            return false;
        }

        public override void update(GameTime gameTime)
        {
            if(life <= 70 && life > 40)
            {
                _texture = _wall2;
            }
            if (life <= 40)
            {
                _texture = _wall3;
            }
            if (life <= 0)
            {
                isRemove = true;
            }
        }
    }
}
