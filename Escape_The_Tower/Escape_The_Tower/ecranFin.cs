using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace Escape_The_Tower
{
    internal class ecranFin : GameScreen
    {
        private Game1 _myGame;
        private Texture2D _fondFin;
        public Song _bcgMusic;

        public ecranFin(Game1 game) : base(game)
        {
            _myGame = game;

        }

        public override void Initialize()
        {
            MediaPlayer.IsRepeating = true;
        }

        public override void LoadContent()
        {
            _fondFin = Content.Load<Texture2D>("EcranFin");
            _bcgMusic = Content.Load<Song>("ThemeMenu");
            MediaPlayer.Play(_bcgMusic);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {



        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_fondFin, new Vector2(0, 0), Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}