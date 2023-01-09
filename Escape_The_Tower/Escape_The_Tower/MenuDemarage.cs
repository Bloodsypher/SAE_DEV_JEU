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

namespace Escape_The_Tower
{
    public class MenuDemarage : GameScreen
    {
        private Game1 _myGame;
        private Texture2D _fondMenu;
        private Rectangle[] lesBoutons;


        public MenuDemarage(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[3];
            lesBoutons[0] = new Rectangle(75, 110, 640, 160);
            lesBoutons[1] = new Rectangle(75, 320, 640, 160);
            lesBoutons[2] = new Rectangle(75, 528, 640, 160);
        }

        public override void LoadContent()
        {
            _fondMenu = Content.Load<Texture2D>("menu");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lesBoutons.Length; i++)
                {
                    if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        if (i == 0)
                            _myGame.Etat = Game1.Etats.Jouer;
                        else if (i == 1)
                            _myGame.Etat = Game1.Etats.Regle    ;
                        else
                            _myGame.Etat = Game1.Etats.Quit;
                        break;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_fondMenu, new Vector2(0,0), Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}
