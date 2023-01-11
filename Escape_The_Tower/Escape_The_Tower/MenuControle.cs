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
    
        public class MenuControle : GameScreen
        {
            private Game1 _myGame;
            private Texture2D _fondControle;
            private Rectangle retour;

            public MenuControle(Game1 game) : base(game)
            {
                _myGame = game;
                retour = new Rectangle(390, 676, 661, 96);
            }

            public override void LoadContent()
            {
                _fondControle = Content.Load<Texture2D>("Controle");

                base.LoadContent();
            }

            public override void Update(GameTime gameTime)
            {
                MouseState _mouseState = Mouse.GetState();
                if (_mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (retour.Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        _myGame.Etat = Game1.Etats.Menu;
                    }                    
                }
            }

            public override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.Black);
                _myGame.SpriteBatch.Begin();
                _myGame.SpriteBatch.Draw(_fondControle, new Vector2(0, 0), Color.White);
                _myGame.SpriteBatch.End();
            }
        }
}
