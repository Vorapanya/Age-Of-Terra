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
using Catap.Models;
using Catap.Managers;
using System.Linq;

namespace Catap.Object
{
    class Goblin : Enemy
    {
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        public Enemybullet _ebullet;
        public double life = 3;
        public int cooldown = 3;
        public int second = 1;
        public Vector2 speed =new Vector2 ((float)-0.01,0);
        public float speedTime = (float)0.001;
        public bool isSlow = false;
        public bool isBurn = false;
        public bool isFreez = false;
        public bool isparalyze = false;
        public Random ran = new Random();
        public float stop;
        /*
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        private List<SpriteAnimation> _sprites;
        */
        //public bool isShoot = false;

        public Goblin(Texture2D texture, World world, int positionx,int positiony, ContentManager _content)
            : base(texture,world)
        {
            _animations = new Dictionary<string, Animation>
                {
                    { "WalkLeft", new Animation(_content.Load<Texture2D>("Walk/1Walk"), 3,0.2f) },
                    { "Attack", new Animation(_content.Load<Texture2D>("Atk/1Attack"), 5,0.2f) }
                };
            _animationManager = new AnimationManager(_animations.First().Value);
            /* Circle */
            stop = ran.Next(10, 40);
            
            _enemyRadius = 1.0f / 2f;
            _enemyPosition = new Vector2(positionx, positiony);
            _enemyOrigin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            

            // Create the player fixture
            
            _enemyBody = world.CreateBody(_enemyPosition, 0, BodyType.Dynamic);
            
            var pfixture = _enemyBody.CreateCircle(_enemyRadius, 1f);
            pfixture.Tag = "enemy";
            // Give it some bounce and friction
            pfixture.Restitution = 0.65f;
            pfixture.Friction = 1.0f;
            
            // We scale the ground and player textures at body dimensions
            _enemySize = new Vector2(_texture.Width, _texture.Height);

            _enemyBody.OnCollision += _enemyBody_OnCollision;
            
            
            // We draw the ground and player textures at the center of the shapes
            _enemyOrigin = _enemySize / 2f;
            
            //Pangpond add code Here-------------------------------------------------------------------
            /*_sprites = new List<SpriteAnimation>()
            {
                new SpriteAnimation(new Dictionary<string, Animation>()
                {
                    { "WalkLeft", new Animation(texture, 4) },
                })
                {Position = new Vector2(1, 1), }
            };*/
        }

        private bool _enemyBody_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            if ((string)other.Tag == "blank")
            {
                life -= 1;
                //System.Diagnostics.Debug.WriteLine("1");
                return false;
            }
            if ((string)other.Tag == "normalbullet")
            {
                life -= 1;
                //System.Diagnostics.Debug.WriteLine(life);
                return false;
            }
            if ((string)other.Tag == "linebullet")
            {
                life -= 1;
                //System.Diagnostics.Debug.WriteLine(life);
                return false;
            }
            if ((string)other.Tag == "slowbullet")
            {
                life -= 2;
                //System.Diagnostics.Debug.WriteLine(life);
                isSlow = true;
                return false;
            }
            if ((string)other.Tag == "Aoebomb")
            {
                life -= 0.2;
                return false;
            }
            if ((string)other.Tag == "Aoeburn")
            {
                //System.Diagnostics.Debug.WriteLine(life);
                isBurn = true;
                return false;
            }
            if ((string)other.Tag == "Aoefreez")
            {
                //System.Diagnostics.Debug.WriteLine(life);
                isFreez = true;
                return false;
            }
            if ((string)other.Tag == "Aoeparalyze")
            {
                life -= 0.1;
                isparalyze = true;
                return false;
            }
            if ((string)other.Tag == "Aoehole")
            {
                isSuck = true;
                life -= _suckTimer;

                return false;
            }
            if ((string)other.Tag == "enemy")
            {
                return false;
            }
                return true;
        }

        public override void update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Vector2 v = new Vector2(10, 1);
            //_enemyBody.SetTransform(v,0);

            if (_timer > speedTime)
            {
                if (_enemyBody.Position.X > 10)
                {
                    _animationManager.Play(_animations["WalkLeft"]);
                    _animationManager.Update(gameTime);
                    speedTime += (float)0.001;
                    _enemyBody.Position += speed;
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine(stop);
                    if (_enemyBody.Position.X >= stop / 10)
                    {
                        _animationManager.Play(_animations["WalkLeft"]);
                        _animationManager.Update(gameTime);
                        //System.Diagnostics.Debug.WriteLine(stop/10 + "   " + _enemyBody.Position.X);
                        speedTime += (float)0.001;
                        _enemyBody.Position += speed;
                    }
                    if (_enemyBody.Position.X <= stop / 10)
                    {
                        _animationManager.Play(_animations["Attack"]);
                        _animationManager.Update(gameTime);
                    }
                    if (_shootTimer >= cooldown)
                    {
                        _shootTimer = 0;
                        isShoot = true;
                    }

                }
            }
            if (isSlow && isBurn)
            {
                //System.Diagnostics.Debug.WriteLine(life);
                _aoeName = "slow+fire";
                isAoe = true;
                _slowTimer = 0;
                speed.X = (float)-0.01;
                _burnTimer = 0;
                second = 0;
                isBurn = false;
                isSlow = false;
            }
            if (isSlow && isparalyze)
            {
                //System.Diagnostics.Debug.WriteLine(life);
                _aoeName = "slow+lightning";
                isAoe = true;
                _slowTimer = 0;
                speed.X = (float)-0.01;
                _paralyzeTimer = 0;
                cooldown = 3;
                isparalyze = false;
                isSlow = false;
            }
            if(isSlow && isFreez)
            {
                _slowTimer = 0;
                _freezTimer = -3;
                isSlow = false;
            }
            if (isSlow)
            {
                if (!isFreez)
                {
                    _slowTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    speed.X = (float)-0.005;
                    if (_slowTimer >= 5)
                    {
                        _slowTimer = 0;
                        speed.X = (float)-0.01;
                        isSlow = false;
                    }
                }
            }
            
            if (isFreez)
            {
                _freezTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                _animationManager.Stop();
                speed.X = 0;
                _shootTimer = 0;
                if(_freezTimer >= 3)
                {
                    _freezTimer = 0;
                    speed.X = (float)-0.01;
                    isFreez = false;
                }
            }
            if (isBurn)
            {
                _burnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //System.Diagnostics.Debug.WriteLine(life);
                if(_burnTimer >= second) 
                {
                    second += 1;
                    life -= 2;
                }
                if (_burnTimer >= 5)
                {
                    _burnTimer = 0;
                    second = 0;
                    isBurn = false;
                }
            }
            if (isparalyze)
            {
                _paralyzeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                cooldown = 99;
                if(_paralyzeTimer >= 5)
                {
                    _paralyzeTimer = 0;
                    cooldown = 3;
                    isparalyze = false;
                }
            }
            if (isSuck == false)
            {
                _suckTimer = 0;
            }
            if (life <= 0)
            {
                isRemove = true;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _animationManager.Draw(spriteBatch, _enemyBody.Position, _enemyBody.Rotation, _enemyOrigin, new Vector2(_enemyRadius * 2f) / _enemySize);
            //spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            //else
            //spriteBatch.Draw(_texture, _enemyBody.Position, null, Color.White, _enemyBody.Rotation, _enemyOrigin, new Vector2(_enemyRadius * 2f) / _enemySize, SpriteEffects.FlipVertically, 0f);
        }
    }
}
