using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections.Generic;

using MonoGame.Extended;

using Dow.UI;
using Dow.UI.Utils.Alignment;

namespace Dow.Main;

class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Values.ScreenDims = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        font = Content.Load<SpriteFont>("Font");

        Vector2 position = Position.ScreenRel(0.5f, 0.5f);

        List<IUIElement> children = [
            new Text(
                "This is my container",
                position,
                font,
                Color.DarkRed,
                2
            ),
            new Button(
                "This is a button",
                font,
                Color.Black,
                Color.GreenYellow,
                new Vector2(150, 50),
                position + new Vector2(150, 50),
                2
            )
        ];

        new UIContainer(
            children,
            position,
            new Vector2(350, 350),
            Color.Gray,
            0.5f
        );

        new Frame(
            Position.ScreenRel(0.5f, 0.5f),
            new Vector2(150, 150),
            Color.Black,
            10
        );
    }

    protected override void Update(GameTime gameTime)
    {
        UI.Collection.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();

        foreach(IUIElement element in Collection.Elements.OrderBy(e => e.ZIndex))
        {
            if (element.Visible)
                element.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}