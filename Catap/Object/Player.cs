using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Catap.Object
{
    public class Player : Sprite
    {

        public MouseState _mouseState, _previousMouseState;
        public int _slowAmount = 0;
        public int _burnAmount = 0;
        public int _bombAmount = 0;
        public int _freezAmount = 0;
        public int _paralyzeAmount = 0;
        public int _sprayAmount = 0;
        public int _lineAmount = 0;
        public int _blackholeAmount = 0;

        public Player(Texture2D texture)
            : base(texture)
        {
            
        }
        public override void Update()
        {
            //_previousMouseState = _mouseState;
            _mouseState = Mouse.GetState();
            
        }
    }
}
