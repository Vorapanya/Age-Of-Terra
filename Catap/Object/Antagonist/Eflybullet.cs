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
    class Eflybullet : Enemybullet
    {

        /*public Body _bulletBody;
        public float _bulletRadius = 0.5f / 2f; // player diameter is 1.5 meters
        public Vector2 _bulletPosition;
        public Vector2 _bulletOrigin;
        public Vector2 _bulletSize;
        public float _bulletRotation;*/
        public int life = 1;
        //isHit = is hit player bullet?
        public bool isHit = false;

        public Eflybullet(Texture2D texture, World world, Vector2 position)
            : base(texture, world, position)
        {
            /* Circle */
            //_bulletPosition = new Vector2(0, _bulletRadius);
            //_enemyPosition = new Vector2(5, 1);
            _enemyPosition = position;
            _enemyRadius = 0.5f / 2f;
            _enemyOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            // Create the player fixture

            _enemyBody = world.CreateBody(_enemyPosition, 0, BodyType.Dynamic);
            //System.Diagnostics.Debug.WriteLine("1");
            var pfixture = _enemyBody.CreateCircle(_enemyRadius, 1f);
            pfixture.Tag = "efly";
            // Give it some bounce and friction
            pfixture.Restitution = 0.65f;
            pfixture.Friction = 1.0f;

            // We scale the ground and player textures at body dimensions
            _enemySize = new Vector2(_texture.Width, _texture.Height);

            _enemyBody.OnCollision += _bulletBody_OnCollision;

            // We draw the ground and player textures at the center of the shapes
            _enemyOrigin = _enemySize / 2f;
        }

        private bool _bulletBody_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            if ((string)other.Tag == "town" || (string)other.Tag == "wall")
            {
                isAoe = true;
                isRemove = true;
                //true mean allow collision
                //isRemove = true;
                return true;
            }
            if ((string)other.Tag == "ground")
            {
                isAoe = true;
                isRemove = true;
                //true mean allow collision
                //isRemove = true;
                return true;
            }
            if ((string)other.Tag == "normalbullet")
            {
                life = 1;
                isHit = true;
                sender.Tag = "blank";
                return true;
            }
            if((string)other.Tag == "blank")
            {
                life -= 1;
                isHit = true;
                sender.Tag = "blank";
                return true;
            }
            if ((string)other.Tag == "enemy" && (string)sender.Tag == "blank")
            {
                isAoe = true;
                isRemove = true;
                return false;
            }
            if ((string)other.Tag == "enemy" && (string)sender.Tag != "blank")
            {
                return false;
            }
            //false mean allow no collision
            return false;

        }

    }
}
