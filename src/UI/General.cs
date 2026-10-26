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
    static public List<Sprite> Sprites = [];
    static public List<Hitbox> Hitboxes = [];
    static public List<UIContainer> Containers = [];
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

public struct Thickness
{
    public float Left;
    public float Top;
    public float Right;
    public float Bottom;

    public Thickness(float uniform)
    {
        Left = Top = Right = Bottom = uniform;
    }

    public Thickness(float vertical, float horizontal)
    {
        Left = Right = horizontal;
        Top = Bottom = vertical;
    }

    public Thickness(float top, float right, float bottom, float left)
    {
        Top = top;
        Right = right;
        Bottom = bottom;
        Left = left;
    }
}

public interface IUIElement
{
    Vector2 Position { get; set; }
    Vector2 Size { get; }
    UIContainer Parent { get; set; }
    Vector2 Anchor { get; set; }
    Vector2 AbsolutePosition { get; }
    IUIElement InteractionTarget { get; set; }
    Vector2 DrawPosition { get; }
    bool Visible { get; set; }

    int ZIndex { get; set; }

    void Draw(SpriteBatch _spriteBatch) { }
    
    void OnHold(bool Init) { }
    void OnHover(bool Enter) { }
    void OnClick() { }
}