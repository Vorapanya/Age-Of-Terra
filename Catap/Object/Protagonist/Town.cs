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
    public class Town : City
    {
        //public bool isShoot = false;
        private Texture2D _town2,_town3;

        public Town(Texture2D texture, World world,double _life, ContentManager _content)
            : base(texture, world)
        {
            _town2 = _content.Load<Texture2D>("Building/Town2");
            _town3 = _content.Load<Texture2D>("Building/Town3");
            /* Circle */
            _citySize = new Vector2(3.5f, 1.3f);
            _cityPosition = new Vector2(-3.8f, 0.7f);

            life = _life;

            // Create the player fixture

            _cityBody = world.CreateBody(_cityPosition, 0, BodyType.Static);
            var cfixture = _cityBody.CreateRectangle(_citySize.X, _citySize.Y, 1f, Vector2.Zero);
            cfixture.Tag = "town";
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
            if ((string)other.Tag == "ebullet")
            {
                life -= 2;
                
                return false;
            }
            if ((string)other.Tag == "suicide")
            {
                life -= 2;
                return true;
            }

            if ((string)other.Tag == "ebomb")
            {
                life -= 1;
                
                return false;
            }
            if ((string)other.Tag == "efly")
            {
                life -= 10;
                
                return false;
            }
            return false;
        }

        public override void update(GameTime gameTime)
        {
            if (life <= 70 && life > 40)
            {
                _texture = _town2;
            }
            if (life <= 40)
            {
                _texture = _town3;
            }
            if (life <= 0)
            {
                isDefeat = true;
            }
        }
    }
}
