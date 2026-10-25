using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Dow.UI.Utils;
using Dow.UI.Handler;
namespace Dow.UI;

static public class Collection
{
    static public List<Text> Texts = [];
    static public List<Button> Buttons = [];
    static public List<Frame> Frames = [];
    static public List<Hitbox> Hitboxes = [];
    static public List<IUIElement> Elements = [];

    public static void Update()
    {
        MouseState mouse = Mouse.GetState();
        Vector2 mousePos = mouse.Position.ToVector2();

        IUIElement topmost = InputHandler.GetTopmostElement(mousePos);
        InputHandler.UpdateInput(topmost);

        // Text
        foreach (Text text in Texts)
        {
            if (text.TextUpdater != null)
            {
                text.UpdateText();
            }

            if (text.PositionUpdater != null)
            {
                text.UpdatePosition();
            }
        }
    }
}

public interface IUIElement
{
    Vector2 Position { get; set; }
    Vector2 Size { get; }
    UIContainer Parent { get; set; }
    Vector2 Anchor { get; set; }
    bool Visible { get; set; }

    int ZIndex { get; set; }

    void Draw(SpriteBatch _spriteBatch) { }
    
    void OnHold(bool Init) { }
    void OnHover(bool Enter) { }
    void OnClick() { }
}