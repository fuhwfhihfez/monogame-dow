using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;

using UI;
using UI.Decoration;
using UI.Utils.Alignment;
using System.Windows.Forms;
using Main;

namespace Library;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont font;
    private int score;

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

        UI.Collection.Buttons = [
            new UI.Button(
                "Click me!",
                font,
                Color.White,
                Color.GreenYellow,
                new Vector2(150, 50),
                new Vector2(150, 0),
                () =>
                {
                    score++;
                },
                (Enter) =>
                {
                    if (Enter)
                    {
                        score += 2;
                    }
                    else
                    {
                        score += 5;
                    }
                },
                0.5f
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
            new UI.Button(
                "Quit",
                font,
                Color.White,
                Color.DarkRed,
                new Vector2(150, 50),
                Vector2.Zero,
                Exit
            ),
        ];

        UI.Collection.Texts = [
            new Text(
                $"{score}",
                new Vector2(0, 0),
                font,
                Color.White,
                () =>
                {
                    return $"{score}";
                },
                new Stroke(
                    5f,
                    Color.Red
                )
            )
        ];

        UI.Collection.Frames = [
            new Frame(
                new Vector2(50, 50),
                new Vector2(100, 200),
                Color.Purple
            )
        ];

        UIGrid grid = new UIGrid(
            GridMode.Automatic,
            new Vector2(0, 200),
            new Vector2(10, 5),
            new Vector2(500, 350),
            50000,
            20000,
            UI.Collection.Buttons.Cast<IUIElement>().ToList()
        );
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            Exit();

        UI.Collection.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        foreach(IUIElement element in UI.Collection.Elements)
        {
            if (element.Visible)
                element.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}