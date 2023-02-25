using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Catap.Object;
using Catap.Object.Mybullet;
using Catap.Object.Mybullet.BulletAoe;
using Catap.Object.Protagonist;
using tainicom.Aether.Physics2D.Dynamics;
using Catap.Models;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Catap.Managers;

namespace Catap.Stage
{
    public class Stage4 : DrawableGameComponent
    {
        // Sounds
        private SoundEffect _click;
        private Song _menuSong, _song, _win;

        private float _cooldownTimer;
        private float _cooldown = 0.5f;
        private Victory4 _victory;
        private Defeat _defeat;
        private Game _game;
        private List<Component> _components;

        // Buttons
        private MenuState _menuState;

        private SpriteBatch _spriteBatch;
        private BasicEffect _spriteBatchEffect;
        private SpriteFont _font;
        private AmmoWheel _wheel;
        private Town _town;
        private Wall _wall;
        private Player _player;
        private Bullet _bullet;
        private Normal _normalBullet;
        private Line _lineBullet;
        private Slow _slowBullet;
        private Burn _burnBullet;
        private Burnaoe _aoeBurn;
        private Bomb _bombBullet;
        private Bombaoe _aoeBomb;
        private Freez _freezBullet;
        private Freezaoe _aoeFreez;
        private Paralyze _paralyzeBullet;
        private Paralyzeaoe _aoeParalyze;
        private Blackhole _blackholeBullet;
        private Blackaoe _aoeBlackhole;
        //private Enemybullet _ebullet;
        private Enormalbullet _enormal;
        private Ebombbullet _ebomb;
        private Eflybullet _efly;
        //private Goblin _goblin;
        //private Bossgun _bossGun;

        private String _currentBullet = "normal";
        private Texture2D _normalTexture, _lineTexture, _burnTexture, _aoeburnTexture, _slowTexture, _bombTexture, _aoebombTexture, _freezTexture, _aoefreezTexture, _paralyzeTexture, _aoeparalyzeTexture;
        private Texture2D _blackholeTexture, _aoeholeTexture;
        private Texture2D _groundTexture;
        private Texture2D _playerText;
        private Texture2D _townTexture, _wallTexture, _fortTexture, _wheelTexture, _wheelCaseTexture;
        private Texture2D _goblinText, _goblinbTexture, _flyTexture, _bossgunTexture, _bosssumTexture, _suicideTexture, _balloonTexture;
        private Texture2D _enormalText, _ebombTexture, _eflyTexture;
        //private Vector2 _playerTextureSize;
        private Vector2 _groundTextureSize;
        private Vector2 _bgTextureSize;
        //private Vector2 _playerTextureOrigin;
        private Vector2 _groundTextureOrigin;
        private Vector2 _startPosition;
        private List<City> _protag;
        private List<Bullet> _bull;
        private List<Enemybullet> _ebull;
        private List<Enemy> _enemy;

        private Texture2D _backGround,_hpTexture;

        private KeyboardState _oldKeyState;
        private GamePadState _oldPadState;

        // Simple camera controls
        private Vector3 _cameraPosition = new Vector3(-1.3f, 3.0f, 0); // camera is 1.7 meters above the ground     (0, 1.7f, 0);
        float cameraViewWidth = 12.5f; // camera is 12.5 meters wide.

        // physics
        private World _world;
        private Body _groundBody;
        private Body _skyBody;

        //private float _playerBodyRadius = 1.5f / 2f; // player diameter is 1.5 meters
        private Vector2 _groundBodySize = new Vector2(300f, 1f); // ground is 8x1 meters
        private Vector2 _bgSize = new Vector2(30f, 8f);
        private Vector2 _skyBoxSize = new Vector2(0.5f, 4f);
        private Vector2 velo = new Vector2(0, 0);

        public int[] tierAll;
        public int[] tierLocal;
        public int[] amount;
        public String[] type;
        public double _myLife;
        public String status = "";

        public int stage = 1;
        public Double _playerDegree;
        public int firstX, firstY, lastX, lastY;

#if JOYSTICK
        const string Text = "Use left stick to move\n" +
                            "Press A to jump\n" +
                            "Use right stick to move camera\n";
#else
        string Text = "Press A or D to rotate the ball\n" +
                             "Press Space to jump\n" +
                             "Use arrow keys to move the camera";

        public object Content { get; private set; }
#endif

        public Stage4(Game game,Player play,double _life) : base(game)
        {
            _game = game;
            _player = play;
            _myLife = _life;
        }

        public override void Initialize()
        {

            //Create a world
            _world = new World();

            type = new string[3];
            tierLocal = new int[3];
            tierAll = new int[3];
            amount = new int[3];


            _startPosition = new Vector2(-5.5f, 3);

            /* Ground */
            Vector2 groundPosition = new Vector2(0, 0);

            // Create the ground fixture
            _groundBody = _world.CreateBody(groundPosition, 0, BodyType.Static);

            var gfixture = _groundBody.CreateRectangle(_groundBodySize.X, _groundBodySize.Y, 1f, Vector2.Zero);
            gfixture.Tag = "ground";
            gfixture.Restitution = 0.3f;
            gfixture.Friction = 1.0f;

            //sky box
            Vector2 skyPosition = new Vector2((float)-4.0, 5);
            _skyBody = _world.CreateBody(skyPosition, 0, BodyType.Static);

            var sfixture = _skyBody.CreateRectangle(_skyBoxSize.X, _skyBoxSize.Y, 1f, Vector2.Zero);
            sfixture.Tag = "skybox";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Sounds
            _click = Game.Content.Load<SoundEffect>("Sounds/SFX/Click");
            _win = Game.Content.Load<Song>("Sounds/BGM/Win");
            _song = Game.Content.Load<Song>("Sounds/BGM/TheEnd");
            MediaPlayer.Play(_song);
            var buttonTexture_Home = Game.Content.Load<Texture2D>("Buttons/btn-home");
            var homeButton = new Button(buttonTexture_Home)
            {
                Position = new Vector2(1153, 10),
            };
            homeButton.Click += HomeButton_Click;

            _components = new List<Component>()
            {
                homeButton,
            };
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            // We use a BasicEffect to pass our view/projection in _spriteBatch
            _spriteBatchEffect = new BasicEffect(Game.GraphicsDevice);
            _spriteBatchEffect.TextureEnabled = true;

            _font = Game.Content.Load<SpriteFont>("Font/font");

            // Load sprites
            _townTexture = Game.Content.Load<Texture2D>("Building/Town1");
            _wallTexture = Game.Content.Load<Texture2D>("Building/wall");
            _fortTexture = Game.Content.Load<Texture2D>("Building/FallenAgSta");
            _wheelTexture = Game.Content.Load<Texture2D>("PlayerAmmo/AmmoWheelFit");
            _wheelCaseTexture = Game.Content.Load<Texture2D>("PlayerAmmo/WheelCase");
            // Ammo
            _normalTexture = Game.Content.Load<Texture2D>("PlayerAmmo/Bullet");
            _lineTexture = Game.Content.Load<Texture2D>("PlayerAmmo/Line");
            _slowTexture = Game.Content.Load<Texture2D>("PlayerAmmo/Slow");

            _burnTexture = Game.Content.Load<Texture2D>("PlayerAmmo/AoeBurn");
            _aoeburnTexture = Game.Content.Load<Texture2D>("OriPic/BurnSta");

            _bombTexture = Game.Content.Load<Texture2D>("PlayerAmmo/AoeBomb");
            _aoebombTexture = Game.Content.Load<Texture2D>("OriPic/BombSta");

            _freezTexture = Game.Content.Load<Texture2D>("PlayerAmmo/AoeFreeze");
            _aoefreezTexture = Game.Content.Load<Texture2D>("OriPic/FreezeReSta");

            _paralyzeTexture = Game.Content.Load<Texture2D>("PlayerAmmo/AoeLightning");
            _aoeparalyzeTexture = Game.Content.Load<Texture2D>("OriPic/ParalyzReSta");

            _blackholeTexture = Game.Content.Load<Texture2D>("PlayerAmmo/Blackhole");
            _aoeholeTexture = Game.Content.Load<Texture2D>("OriPic/NewBlackHoleSta");

            // Enemy Ammo
            _enormalText = Game.Content.Load<Texture2D>("EnemyAmmo/Enormal");
            _ebombTexture = Game.Content.Load<Texture2D>("EnemyAmmo/Ewall");
            _eflyTexture = Game.Content.Load<Texture2D>("EnemyAmmo/EBomb");

            _playerText = Game.Content.Load<Texture2D>("white");

            _goblinText = Game.Content.Load<Texture2D>("OriPic/1Sta");
            //_goblinText = Game.Content.Load<Texture2D>("Walk/BalloonFly");
            _backGround = Game.Content.Load<Texture2D>("BG/Background2");
            _hpTexture = Game.Content.Load<Texture2D>("BG/HP");

            _goblinbTexture = Game.Content.Load<Texture2D>("OriPic/4Sta");
            _suicideTexture = Game.Content.Load<Texture2D>("OriPic/4Sta");//OriPic/3Sta
            _flyTexture = Game.Content.Load<Texture2D>("OriPic/5Sta");
            _balloonTexture = Game.Content.Load<Texture2D>("OriPic/BalSta");
            _bossgunTexture = Game.Content.Load<Texture2D>("OriPic/EleSta");
            _bosssumTexture = Game.Content.Load<Texture2D>("OriPic/SumSta");
            _groundTexture = Game.Content.Load<Texture2D>("GroundSprite");
            _protag = new List<City>()
            {
                new Town(_townTexture,_world,_myLife,Game.Content)
                {

                },
                new Wall(_wallTexture, _world,Game.Content)
                {

                },
                new Fort(_fortTexture, _world)
                {

                },
            };
            _bull = new List<Bullet>()
            {

            };
            _ebull = new List<Enemybullet>()
            {

            };
            _enemy = new List<Enemy>()
            {
                //wave1
                new Goblinb(_goblinbTexture, _world,14,0,Game.Content),
                new Goblin(_goblinText, _world,15,0,Game.Content),
                new Goblin(_goblinText, _world,16,0,Game.Content),
                new Goblin(_goblinText, _world,17,0,Game.Content),
                new Flying(_flyTexture, _world, 13, 4,Game.Content),
                new Flying(_flyTexture, _world, 19, 4,Game.Content),

                //wave2
                new Goblinb(_goblinbTexture, _world,27,0,Game.Content),
                new Suicide(_suicideTexture, _world, 28, 0,Game.Content),
                new Suicide(_suicideTexture, _world, 29, 0,Game.Content),
                new Goblinb(_goblinbTexture, _world,30,0,Game.Content),
                new Flying(_flyTexture, _world, 25, 4,Game.Content),

                //wave3
                new Flying(_flyTexture, _world, 52, 4,Game.Content),
                new Flying(_flyTexture, _world, 57, 4,Game.Content),
                new Flying(_flyTexture, _world, 58, 4,Game.Content),
                new Goblin(_goblinText, _world,50,0,Game.Content),
                new Goblin(_goblinText, _world,51,0,Game.Content),
                new Goblin(_goblinText, _world,52,0,Game.Content),
                new Goblin(_goblinText, _world,53,0,Game.Content),
                new Goblin(_goblinText, _world,54,0,Game.Content),
                new Goblinb(_goblinbTexture, _world,54,0,Game.Content),
                new Goblinb(_goblinbTexture, _world,55,0,Game.Content),
                new Goblinb(_goblinbTexture, _world,56,0,Game.Content),

                //wave4
                new Suicide(_suicideTexture, _world, 66, 0,Game.Content),
                new Goblinb(_goblinbTexture, _world,67,0,Game.Content),
                new Goblin(_goblinText, _world,68,0,Game.Content),

                new Suicide(_suicideTexture, _world, 73, 0,Game.Content),
                new Goblinb(_goblinbTexture, _world,74,0,Game.Content),
                new Goblin(_goblinText, _world,75,0,Game.Content),

                new Suicide(_suicideTexture, _world, 80, 0,Game.Content),
                new Goblinb(_goblinbTexture, _world,81,0,Game.Content),
                new Goblin(_goblinText, _world,82,0,Game.Content),


            };
            _wheel = new AmmoWheel(_wheelTexture, _wheelCaseTexture, _font);
            _groundTextureSize = new Vector2(_groundTexture.Width, _groundTexture.Height);
            _bgTextureSize = new Vector2(_backGround.Width, _backGround.Height);

            _groundTextureOrigin = _groundTextureSize / 2f;

        }


        public override void Update(GameTime gameTime)
        {
            HandleKeyboard(gameTime);

            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            status = "Town Hp: " + _protag[0].life
                + "\nWall Hp: " + _protag[1].life;

            if (_cameraPosition.X <= -1.3f)
                _cameraPosition.X = -1.3f;
            if (_cameraPosition.X >= 12f)
                _cameraPosition.X = 12f;

            _wheel.update(gameTime, _player);
            //We update the world
            _world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            foreach (var sprite in _protag.ToArray())
                sprite.update(gameTime);

            foreach (var sprite in _bull.ToArray())
                sprite.update(gameTime);

            foreach (var sprite in _ebull.ToArray())
                sprite.update(gameTime);

            foreach (var sprite in _enemy.ToArray())
                sprite.update(gameTime);
            foreach (var component in _components)
                component.Update(gameTime);

            for (int i = 0; i < _protag.Count; i++)
            {
                if (_protag[i].isRemove)
                {
                    _world.Remove(_protag[i]._cityBody);
                    _protag.RemoveAt(i);
                }
                if (_protag[i].isDefeat)
                {
                    stage = 3;
                }
            }

            if (_enemy.Count == 0)
            {
                stage = 4;
            }

            for (int i = 0; i < _bull.Count; i++)
            {
                if (_bull[i].isAoe)
                {
                    if (_bull[i].name == "burnbullet")
                    {
                        _aoeBurn = new Burnaoe(_aoeburnTexture, _world, _bull[i]._bulletBody.Position, Game.Content);
                        _bull.Add(_aoeBurn);
                    }
                    else if (_bull[i].name == "bombbullet")
                    {
                        _aoeBomb = new Bombaoe(_aoebombTexture, _world, _bull[i]._bulletBody.Position, Game.Content);
                        _bull.Add(_aoeBomb);
                    }
                    else if (_bull[i].name == "freezbullet")
                    {
                        _aoeFreez = new Freezaoe(_aoefreezTexture, _world, _bull[i]._bulletBody.Position, Game.Content);
                        _bull.Add(_aoeFreez);
                    }
                    else if (_bull[i].name == "paralyzebullet")
                    {
                        _aoeParalyze = new Paralyzeaoe(_aoeparalyzeTexture, _world, _bull[i]._bulletBody.Position, Game.Content);
                        _bull.Add(_aoeParalyze);
                    }
                    else if (_bull[i].name == "blackholebullet")
                    {
                        _aoeBlackhole = new Blackaoe(_aoeholeTexture, _world, _bull[i]._bulletBody.Position, Game.Content);
                        _bull.Add(_aoeBlackhole);
                    }
                }
                if (_bull[i].isRemove)
                {
                    _world.Remove(_bull[i]._bulletBody);
                    _bull.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < _ebull.Count; i++)
            {
                if (_ebull[i].isAoe)
                {
                    _aoeBomb = new Bombaoe(_aoebombTexture, _world, _ebull[i]._enemyBody.Position, Game.Content);
                    _bull.Add(_aoeBomb);
                }
                if (_ebull[i].isRemove)
                {
                    _world.Remove(_ebull[i]._enemyBody);
                    _ebull.RemoveAt(i);
                    i--;
                }

            }

            for (int i = 0; i < _enemy.Count; i++)
            {
                if (_enemy[i].isDropbomb)
                {
                    _efly = new Eflybullet(_eflyTexture, _world, _enemy[i]._enemyBody.Position);
                    _efly.Force(0, 0);
                    _ebull.Add(_efly);
                    _enemy[i].isDropbomb = false;
                    _enemy[i].isRemove = true;
                }
                if (_enemy[i].isSpecialbomb)
                {
                    var _efly1 = new Eflybullet(_eflyTexture, _world, _enemy[i]._enemyBody.Position);
                    _efly1.Force(0, 0);
                    _ebull.Add(_efly1);
                    var _efly2 = new Eflybullet(_eflyTexture, _world, _enemy[i]._enemyBody.Position);
                    _efly2.Force((float)0.5, 0);
                    _ebull.Add(_efly2);
                    var _efly3 = new Eflybullet(_eflyTexture, _world, _enemy[i]._enemyBody.Position);
                    _efly3.Force((float)-0.5, 0);
                    _ebull.Add(_efly3);
                    _enemy[i].isSpecialbomb = false;
                    _enemy[i].isRemove = true;
                }
                if (_enemy[i].isAoe)
                {
                    if (_enemy[i]._aoeName == "slow+fire")
                    {
                        _aoeBomb = new Bombaoe(_aoebombTexture, _world, _enemy[i]._enemyBody.Position, Game.Content);
                        _bull.Add(_aoeBomb);
                        _enemy[i].isAoe = false;
                    }
                    else if (_enemy[i]._aoeName == "slow+lightning")
                    {
                        _aoeFreez = new Freezaoe(_aoefreezTexture, _world, _enemy[i]._enemyBody.Position, Game.Content);
                        _bull.Add(_aoeFreez);
                        _enemy[i].isAoe = false;
                    }
                }
                if (_enemy[i].isRemove)
                {
                    _world.Remove(_enemy[i]._enemyBody);
                    _enemy.RemoveAt(i);
                    i--;
                }
                else if (_enemy[i].isShoot)
                {
                    _enormal = new Enormalbullet(_enormalText, _world, _enemy[i]._enemyBody.Position);
                    Random ran = new Random();
                    _enormal.Force((float)ran.Next(7, 14) / 10, (float)ran.Next(6, 18) / 10);
                    _ebull.Add(_enormal);
                    _enemy[i].isShoot = false;
                    //i--;
                }
                else if (_enemy[i].isShootbomb)
                {
                    _ebomb = new Ebombbullet(_ebombTexture, _world, _enemy[i]._enemyBody.Position);
                    Random ran = new Random();
                    _ebomb.Force((float)ran.Next(5, 12) / 10, (float)ran.Next(1, 12) / 10);
                    _ebull.Add(_ebomb);
                    _enemy[i].isShootbomb = false;
                }

                else if (_enemy[i].isSpecialgun)
                {
                    Random ran = new Random();
                    Vector2 v = _enemy[i]._enemyBody.Position;
                    v.Y += 1;
                    var _enormal1 = new Enormalbullet(_enormalText, _world, v);
                    _enormal1.Force((float)ran.Next(9, 16) / 10, (float)ran.Next(6, 20) / 10);
                    _ebull.Add(_enormal1);
                    var _enormal2 = new Enormalbullet(_enormalText, _world, v);
                    _enormal2.Force((float)ran.Next(9, 16) / 10, (float)ran.Next(6, 20) / 10);
                    _ebull.Add(_enormal2);
                    var _enormal3 = new Enormalbullet(_enormalText, _world, v);
                    _enormal3.Force((float)ran.Next(9, 16) / 10, (float)ran.Next(6, 20) / 10);
                    _ebull.Add(_enormal3);
                    var _ebomb1 = new Ebombbullet(_ebombTexture, _world, v);
                    _ebomb1.Force((float)ran.Next(9, 14) / 10, (float)ran.Next(1, 18) / 10);
                    _ebull.Add(_ebomb1);
                    var _ebomb2 = new Ebombbullet(_ebombTexture, _world, v);
                    _ebomb2.Force((float)ran.Next(9, 14) / 10, (float)ran.Next(1, 18) / 10);
                    _ebull.Add(_ebomb2);
                    var _ebomb3 = new Ebombbullet(_ebombTexture, _world, v);
                    _ebomb3.Force((float)ran.Next(9, 14) / 10, (float)ran.Next(1, 18) / 10);
                    _ebull.Add(_ebomb3);

                    _enemy[i].isSpecialgun = false;

                }
                else if (_enemy[i].isSpecialsummon)
                {

                    var _goblin1 = new Goblinb(_goblinbTexture, _world, (int)_enemy[i]._enemyBody.Position.X + 1, (int)_enemy[i]._enemyBody.Position.Y, Game.Content);
                    var _goblin2 = new Goblin(_goblinText, _world, (int)_enemy[i]._enemyBody.Position.X - 1, (int)_enemy[i]._enemyBody.Position.Y, Game.Content);
                    _enemy.Add(_goblin1);
                    _enemy.Add(_goblin2);
                    _enemy[i].isSpecialsummon = false;
                }
                else if (_enemy[i].isSuck)
                {
                    for (int sum = 0; sum < _bull.Count; sum++)
                    {
                        if (_bull[sum].name == "aoehole")
                        {
                            _enemy[i]._suckTimer += 0.001f;
                            _enemy[i]._enemyBody.SetTransform(_bull[sum]._bulletPosition, 0);
                            _enemy[i].isSuck = false;
                            break;
                        }
                    }
                }

            }

            switch (stage)
            {
                case 0:

                    break;
                //position 1
                case 1:
                    _player.Update();
                    _cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_player._mouseState.LeftButton == ButtonState.Pressed && _cooldownTimer >= _cooldown)
                    {
                        firstX = _player._mouseState.X;
                        firstY = _player._mouseState.Y;
                        _cooldownTimer = 0;
                        _cooldown = 0.5f;

                        stage = 2;
                    }

                    break;

                //position 2
                case 2:
                    _player.Update();
                    if (_player._mouseState.LeftButton == ButtonState.Pressed)
                    {
                        lastX = _player._mouseState.X;
                        lastY = _player._mouseState.Y;
                        int sumx = firstX - lastX;
                        int sumy = firstY - lastY;
                        if (sumx == 0 || sumy == 0)
                        {
                            sumx = 10;
                            sumy = 10;
                        }
                        Double range = Math.Sqrt((sumx * sumx) + (sumy * sumy));
                        int rangeX = sumx;
                        Double angle = (Math.Abs(rangeX)) / range;
                        _playerDegree = (Math.Acos(angle) * 180) / Math.PI;
                        Double degreeX = 90;
                        Double degreeY = 90;

                        degreeX -= _playerDegree;
                        degreeY -= degreeX;
                        if (range > 200)
                            range = 200;

                        if (_wheel._current == 4 && _player._lineAmount != 0)
                        {
                            range = 300;
                        }
                        velo = new Vector2((float)(degreeX / 10) * (float)(range / 100), (float)(degreeY / 10) * (float)(range / 100));
                        //velo = new Vector2(1f, 1f);
                    }

                    else if (_player._mouseState.LeftButton == ButtonState.Released)
                    {
                        
                        if (_wheel._current == 1)
                        {
                            _normalBullet = new Normal(_normalTexture, _world, _startPosition);
                            _normalBullet.Force(velo.X, velo.Y);
                            _bull.Add(_normalBullet);
                            stage = 1;
                        }
                        else if (_wheel._current == 4 && _player._lineAmount != 0)
                        {
                            _lineBullet = new Line(_lineTexture, _world, _startPosition);
                            _lineBullet.Force(velo.X, velo.Y);
                            _bull.Add(_lineBullet);
                            _player._lineAmount -= 1;

                            stage = 1;
                        }
                        else if (_wheel._current == 5 && _player._bombAmount != 0)
                        {
                            _bombBullet = new Bomb(_bombTexture, _world, _startPosition);
                            _bombBullet.Force(velo.X, velo.Y);
                            _bull.Add(_bombBullet);
                            _player._bombAmount -= 1;

                            stage = 1;
                        }
                        else if (_wheel._current == 2 && _player._slowAmount != 0)
                        {
                            _slowBullet = new Slow(_slowTexture, _world, _startPosition);
                            _slowBullet.Force(velo.X, velo.Y);
                            _bull.Add(_slowBullet);
                            _player._slowAmount -= 1;

                            stage = 1;
                        }
                        else if (_wheel._current == 8 && _player._burnAmount != 0)
                        {
                            _burnBullet = new Burn(_burnTexture, _world, _startPosition);
                            _burnBullet.Force(velo.X, velo.Y);
                            _bull.Add(_burnBullet);
                            _player._burnAmount -= 1;

                            stage = 1;
                        }
                        else if (_wheel._current == 7 && _player._freezAmount != 0)
                        {
                            _freezBullet = new Freez(_freezTexture, _world, _startPosition);
                            _freezBullet.Force(velo.X, velo.Y);
                            _bull.Add(_freezBullet);
                            _player._freezAmount -= 1;

                            stage = 1;
                        }
                        else if (_wheel._current == 6 && _player._paralyzeAmount != 0)
                        {
                            _paralyzeBullet = new Paralyze(_paralyzeTexture, _world, _startPosition);
                            _paralyzeBullet.Force(velo.X, velo.Y);
                            _bull.Add(_paralyzeBullet);
                            _player._paralyzeAmount -= 1;

                            stage = 1;
                        }
                        else if (_wheel._current == 3 && _player._sprayAmount != 0)
                        {
                            //bullet 1
                            var _normalBullet = new Normal(_normalTexture, _world, _startPosition);
                            _normalBullet.Force(velo.X+1, velo.Y+1);
                            _bull.Add(_normalBullet);
                            //bullet2
                            var _normalBullet1 = new Normal(_normalTexture, _world, _startPosition);
                            _normalBullet1.Force(velo.X, velo.Y);
                            _bull.Add(_normalBullet1);
                            var _normalBullet2 = new Normal(_normalTexture, _world, _startPosition);
                            _normalBullet2.Force(velo.X-1, velo.Y-1);
                            _bull.Add(_normalBullet2);
                            _player._sprayAmount -= 1;

                            stage = 1;
                        }
                        else if (_wheel._current == 9 && _player._blackholeAmount != 0)
                        {
                            _blackholeBullet = new Blackhole(_blackholeTexture, _world, _startPosition);
                            _blackholeBullet.Force(velo.X, velo.Y);
                            _bull.Add(_blackholeBullet);
                            _player._blackholeAmount -= 1;
                            _cooldown = 6;
                            //System.Diagnostics.Debug.WriteLine(_playerDegree);
                            stage = 1;
                        }
                        else
                        {
                            _normalBullet = new Normal(_normalTexture, _world, _startPosition);
                            _normalBullet.Force(velo.X, velo.Y);
                            _bull.Add(_normalBullet);
                            stage = 1;
                        }
                        //System.Diagnostics.Debug.WriteLine("0");
                        //stage = 3;
                    }
                    break;
                case 3:
                    MediaPlayer.Stop();
                    _defeat = new Defeat(_game);
                    _game.Components.Add(_defeat);
                    _game.Components.Remove(this);
                    break;
                case 4:
                    MediaPlayer.Stop();
                    _victory = new Victory4(_game, _player, _protag[0].life);
                    _game.Components.Add(_victory);
                    _game.Components.Remove(this);

                    break;
            }
        }

        private void HandleKeyboard(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var vp = GraphicsDevice.Viewport;

            // Move camera
            if (state.IsKeyDown(Keys.A))
                _cameraPosition.X -= totalSeconds * cameraViewWidth;

            if (state.IsKeyDown(Keys.D))
                _cameraPosition.X += totalSeconds * cameraViewWidth;

            /*if (state.IsKeyDown(Keys.Up))
                _cameraPosition.Y += totalSeconds * cameraViewWidth;

            if (state.IsKeyDown(Keys.Down))
                _cameraPosition.Y -= totalSeconds * cameraViewWidth;

*/
            //select bullet type

            if (state.IsKeyDown(Keys.S) && _oldKeyState.IsKeyUp(Keys.S))
            {
                //System.Diagnostics.Debug.WriteLine(_current);
                _wheel._rotation += 0.698131701f;
                _wheel._current -= 1;
            }
            if (state.IsKeyDown(Keys.W) && _oldKeyState.IsKeyUp(Keys.W))
            {
                //System.Diagnostics.Debug.WriteLine(_current);
                _wheel._rotation -= 0.698131701f;
                _wheel._current += 1;
            }



            if (state.IsKeyDown(Keys.Escape))
            {
                try
                {
                    Game.Exit();
                }
                catch (PlatformNotSupportedException) { /* ignore */ }
            }

            _oldKeyState = state;
        }


        public override void Draw(GameTime gameTime)
        {
            // Update camera View and Projection.
            var vp = GraphicsDevice.Viewport;
            _spriteBatchEffect.View = Matrix.CreateLookAt(_cameraPosition, _cameraPosition + Vector3.Forward, Vector3.Up);
            _spriteBatchEffect.Projection = Matrix.CreateOrthographic(cameraViewWidth, cameraViewWidth / vp.AspectRatio, 0f, -1f);

            // Draw player and ground. 
            // Our View/Projection requires RasterizerState.CullClockwise and SpriteEffects.FlipVertically.
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, RasterizerState.CullClockwise, _spriteBatchEffect);

            //_spriteBatch.Draw(_groundTexture, _groundBody.Position, null, Color.White, _groundBody.Rotation, _groundTextureOrigin, _groundBodySize / _groundTextureSize, SpriteEffects.FlipVertically, 0f);

            //_spriteBatch.Draw(_groundTexture, _skyBody.Position, null, Color.White, _skyBody.Rotation, _groundTextureOrigin, _skyBoxSize / _groundTextureSize, SpriteEffects.FlipVertically, 0f);
            _spriteBatch.Draw(_backGround, new Vector2(5f, 2.7f), null, Color.White, 0f, origin(_backGround), _bgSize / _bgTextureSize, SpriteEffects.FlipVertically, 0f);
            if (stage == 2)
            {
                for (int i = 0; i < 60; i += 3)
                {
                    Vector2 pos = getTrajectoryPoint(_startPosition, velo, i);
                    _spriteBatch.Draw(_playerText, pos, null, Color.White, 0f, origin(_playerText), 0.0001f, SpriteEffects.FlipVertically, 0f);
                }
            }
            foreach (var sprite in _protag)
                sprite.Draw(_spriteBatch);
            foreach (var sprite in _bull)
                sprite.Draw(_spriteBatch);
            foreach (var sprite in _enemy)
                sprite.Draw(_spriteBatch);
            foreach (var sprite in _ebull)
                sprite.Draw(_spriteBatch);
            _spriteBatch.End();


            // Display instructions
            _spriteBatch.Begin();
            //_player.Draw(_spriteBatch);
            _wheel.Draw(_spriteBatch);
            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);
            _spriteBatch.Draw(_hpTexture, new Vector2(180, 60), null, Color.White, 0f, origin(_hpTexture), 0.3f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(_font, status, new Vector2(40f, 14f), Color.Black);
            _spriteBatch.DrawString(_font, status, new Vector2(40f, 12f), Color.White);
            _spriteBatch.End();

        }

        public Vector2 getTrajectoryPoint(Vector2 startingPosition, Vector2 startingVelocity, float n)
        {
            //velocity and gravity are given per second but we want time step values here
            float t = 1 / 60.0f; // seconds per time step (at 60fps)
            Vector2 stepVelocity = t * startingVelocity; // m/s
            Vector2 stepGravity = t * t * _world.Gravity; // m/s/s

            return startingPosition + n * stepVelocity + 0.5f * (n * n + n) * stepGravity;
        }
        public Vector2 origin(Texture2D ori)
        {
            Vector2 _origin;
            return _origin = new Vector2(ori.Width / 2, ori.Height / 2);
        }
        private void HomeButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            MediaPlayer.Stop();
            _menuState = new MenuState(_game);
            _game.Components.Add(_menuState);
            _game.Components.Remove(this);
        }
    }
}