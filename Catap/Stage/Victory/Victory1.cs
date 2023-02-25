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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Catap.Stage
{
    class Victory1 : DrawableGameComponent
    {
        // Sounds
        private SoundEffect _click;
        private Song _menuSong, _s1Song, _s2Song, _s3Song, _s4Song, _win;

        private Game _game;
        private Player _player;
        private double _myLife;
        private KeyboardState _oldKeyState;
        private SpriteBatch _spriteBatch;
        private Texture2D _slow,_line,_spray,_bomb,_paralyze,_burn,_freez,_blackhole,_heal,_healmeme;
        private Texture2D _backGround;
        private Stage2 _test;
        private SpriteFont _font;

        public int stage = 0;
        public int limit = 0;
        public int[] tierAll = new int[4];
        public int[] tierLocal = new int[4];
        public int[] amount = new int[4];
        public int[] type = new int[4];
        public String[] _amount = new string[4] {"","","",""};

        public Victory1(Game game, Player play, double _life)
            : base(game)
        {
            _player = play;
            _myLife = _life;
            _game = game;
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            // Sounds
            _click = Game.Content.Load<SoundEffect>("Sounds/SFX/Click");
            _win = Game.Content.Load<Song>("Sounds/BGM/Win");
            _menuSong = Game.Content.Load<Song>("Sounds/BGM/BlackCauldron");
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.Play(_win);

            _font = Game.Content.Load<SpriteFont>("Font/font");

            _backGround = Game.Content.Load<Texture2D>("BG/VictoryBackground2");
            _line = Game.Content.Load<Texture2D>("Card/card_high-speed");
            _slow = Game.Content.Load<Texture2D>("Card/card_slow");
            _spray = Game.Content.Load<Texture2D>("Card/card_spray");
            _burn = Game.Content.Load<Texture2D>("Card/card_burn");
            _freez = Game.Content.Load<Texture2D>("Card/card_freeze");
            _bomb = Game.Content.Load<Texture2D>("Card/card_bomb");
            _paralyze = Game.Content.Load<Texture2D>("Card/card_lightning");
            _blackhole = Game.Content.Load<Texture2D>("Card/card_backhole");
            _heal = Game.Content.Load<Texture2D>("Card/card_repair");
            _healmeme = Game.Content.Load<Texture2D>("Card/card_MEME");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (limit == 2)
                stage = 3;
            switch (stage)
            {
                case 0:
                    ranType();
                    for (int i = 0; i <= 2; i++)
                    {
                        _amount[i] = amount[i].ToString();
                    }
                    stage = 1;
                    break;
                case 1:
                    
                    if (state.IsKeyDown(Keys.D1) && _oldKeyState.IsKeyUp(Keys.D1))
                    {
                        switch (type[0])
                        {
                            case 1:
                                _player._slowAmount += amount[0];
                                break;
                            case 2:
                                _player._lineAmount += amount[0];
                                break;
                            case 3:
                                _player._sprayAmount += amount[0];
                                break;
                            case 4:
                                _player._bombAmount += amount[0];
                                break;
                            case 5:
                                _player._paralyzeAmount += amount[0];
                                break;
                            case 6:
                                _player._burnAmount += amount[0];
                                break;
                            case 7:
                                _player._freezAmount += amount[0];
                                break;
                            case 8:
                                _player._blackholeAmount += amount[0];
                                break;
                            case 10:
                                _myLife = 100;
                                break;
                            case 11:
                                _myLife += 50;
                                break;
                        }
                        stage = 2;
                    }
                    else if (state.IsKeyDown(Keys.D2) && _oldKeyState.IsKeyUp(Keys.D2))
                    {
                        switch (type[1])
                        {
                            case 1:
                                _player._slowAmount += amount[1];
                                break;
                            case 2:
                                _player._lineAmount += amount[1];
                                break;
                            case 3:
                                _player._sprayAmount += amount[1];
                                break;
                            case 4:
                                _player._bombAmount += amount[1];
                                break;
                            case 5:
                                _player._paralyzeAmount += amount[1];
                                break;
                            case 6:
                                _player._burnAmount += amount[1];
                                break;
                            case 7:
                                _player._freezAmount += amount[1];
                                break;
                            case 8:
                                _player._blackholeAmount += amount[1];
                                break;
                            case 10:
                                _myLife = 100;
                                break;
                            case 11:
                                _myLife += 50;
                                break;
                        }
                        stage = 2;
                    }
                    else if (state.IsKeyDown(Keys.D3) && _oldKeyState.IsKeyUp(Keys.D3))
                    {
                        switch (type[2])
                        {
                            case 1:
                                _player._slowAmount += amount[2];
                                break;
                            case 2:
                                _player._lineAmount += amount[2];
                                break;
                            case 3:
                                _player._sprayAmount += amount[2];
                                break;
                            case 4:
                                _player._bombAmount += amount[2];
                                break;
                            case 5:
                                _player._paralyzeAmount += amount[2];
                                break;
                            case 6:
                                _player._burnAmount += amount[2];
                                break;
                            case 7:
                                _player._freezAmount += amount[2];
                                break;
                            case 8:
                                _player._blackholeAmount += amount[2];
                                break;
                            case 10:
                                _myLife = 100;
                                break;
                            case 11:
                                _myLife += 50;
                                break;
                        }
                        stage = 2;
                    }
                    
                    break;
                case 2:
                    limit += 1;
                    stage = 0;
                    break;
                case 3:
                    limit = 0;
                    stage = 4;
                    break;
                case 4:
                    MediaPlayer.Stop();
                    _test = new Stage2(_game, _player,_myLife);
                    _game.Components.Add(_test);
                    _game.Components.Remove(this);
                    break;
            }
            
            
            _oldKeyState = state;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backGround, new Vector2(640, 360), null, Color.White, 0f, origin(_backGround), 1f, SpriteEffects.None, 0f);
            
            for (int i = 0; i <= 2; i++)
            {
                _spriteBatch.DrawString(_font, _amount[i] + "X", new Vector2((520 + (300 * i)), 200), Color.Black);
                _spriteBatch.DrawString(_font, _amount[i] + "X", new Vector2((520 + (300 * i)), 200), Color.White);
                if (type[i] == 1)
                    _spriteBatch.Draw(_slow, new Vector2((400+(300*i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 2)
                    _spriteBatch.Draw(_line, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 3)
                    _spriteBatch.Draw(_spray, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 4)
                    _spriteBatch.Draw(_bomb, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 5)
                    _spriteBatch.Draw(_paralyze, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 6)
                    _spriteBatch.Draw(_burn, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 7)
                    _spriteBatch.Draw(_freez, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 8)
                    _spriteBatch.Draw(_blackhole, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 10)
                    _spriteBatch.Draw(_heal, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
                else if (type[i] == 11)
                    _spriteBatch.Draw(_healmeme, new Vector2((400 + (300 * i)), 380), null, Color.White, 0f, origin(_slow), 1f, SpriteEffects.None, 0f);
            }
           

            _spriteBatch.End();
        }

        public void ranType()
        {
            Random ran = new Random();
            for (int i = 0; i <= 2; i++)
            {
                int t = ran.Next(1, 101);
                if (t == 1)
                {
                    tierAll[i] = 0;
                    //break;
                }
                else if (t > 1 && t <= 7)
                    tierAll[i] = 3;
                else if (t > 7 && t <= 27)
                    tierAll[i] = 2;
                else if (t > 27 && t <= 37)
                    tierAll[i] = 4;
                else
                    tierAll[i] = 1;

            }
            for (int o = 0; o <= 2; o++)
            {
                if (tierAll[o] == 1)
                {
                    tierLocal[o] = ran.Next(1, 4);
                }
                else if (tierAll[o] == 2 || tierAll[o] == 3)
                {
                    tierLocal[o] = ran.Next(1, 3);
                }
                else if (tierAll[o] == 4)
                {
                    tierLocal[o] = ran.Next(1, 11);
                }
                else
                    tierLocal[o] = 0;
            }
            for (int s = 0; s <= 2; s++)
            {
                if (tierAll[s] == 1)
                {
                    if (tierLocal[s] == 1)
                    {
                        type[s] = 1;//"slow";
                    }
                    else if (tierLocal[s] == 2)
                    {
                        type[s] = 2;//"line";
                    }
                    else if (tierLocal[s] == 3)
                    {
                        type[s] = 3;// "spray";
                    }
                    amount[s] = ran.Next(3, 7);
                }
                else if (tierAll[s] == 2)
                {
                    if (tierLocal[s] == 1)
                    {
                        type[s] = 4;// "bomb";
                    }
                    else if (tierLocal[s] == 2)
                    {
                        type[s] = 5;//"paralyze";
                    }
                    amount[s] = ran.Next(1, 5);
                }
                else if (tierAll[s] == 3)
                {
                    if (tierLocal[s] == 1)
                    {
                        type[s] = 6;// "burn";
                    }
                    else if (tierLocal[s] == 2)
                    {
                        type[s] = 7;// "freez";
                    }
                    amount[s] = ran.Next(1, 3);
                }
                else if (tierAll[s] == 4)
                {
                    if (tierLocal[s] > 1)
                    {
                        type[s] = 10;// "heal";
                    }
                    else if (tierLocal[s] == 1)
                    {
                        type[s] = 11;// "heal meme";
                    }
                    amount[s] = 1;
                }
                else if (tierAll[s] == 0 && tierLocal[s] == 0)
                {
                    type[s] = 8;// "blackhole";
                    amount[s] = 1;
                }
            }
        }
        public Vector2 origin(Texture2D ori)
        {
            Vector2 _origin;
            return _origin = new Vector2(ori.Width / 2, ori.Height / 2);
        }
    }
}
