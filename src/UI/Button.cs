using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace UI;

class Button : IUIElement
{
    public Frame Frame;
    public Text Text;
    public string TextString
    {
        get => Text.TextString;
        set => Text.TextString = value;
    }
    public Vector2 Position
    {
        get => Frame.Rectangle.Position;
        set
        {
            Frame.Rectangle.Position = value;
            UpdateTextPosition();
        }
    }
    public Vector2 Size
    {
        get => Frame.Rectangle.Size;
        set => Frame.Rectangle.Size = value;
    }
    public bool Visible { get; set; }

    public Action OnClick;
    public Action<bool> OnHover;
    private MouseState lastMouseState;
    private bool lastHoverState = false;
    
    public Button(string textString, SpriteFont font, Color textColor, Color backgroundColor, Vector2 size, Vector2 position, Action onClick = null, Action<bool> onHover = null, float backgroundOpacity = 1f)
    {

        Frame = new Frame(
            position,
            size,
            backgroundColor,
            backgroundOpacity
        );

        Text = new Text(
            textString,
            Vector2.Zero,
            font,
            textColor
        );

        UpdateTextPosition();

        Position = position;
        Size = size;

        OnClick = onClick;
        OnHover = onHover;

        Visible = true;

        UI.Collection.Elements.Add(this);
    }

    void UpdateTextPosition()
    {
        Vector2 stringDims = Text.Font.MeasureString(TextString);
        Vector2 padding = Size / 2f;
        Vector2 offset = stringDims / 2f;

        Text.Position = Position + padding - offset;
    }

    public void Update()
    {
        MouseState mouse = Mouse.GetState();

        bool isHovering = Frame.Rectangle.Contains(mouse.Position.ToVector2());

        if (isHovering != lastHoverState)
        {
            OnHover?.Invoke(isHovering);
            lastHoverState = isHovering;
        }

        if (isHovering &&
            mouse.LeftButton == ButtonState.Released &&
            lastMouseState.LeftButton == ButtonState.Pressed)
        {
            OnClick?.Invoke();
        }

        lastMouseState = mouse;
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        _spriteBatch.FillRectangle(
            Frame.Rectangle,
            Frame.BackgroundColor
        );

        _spriteBatch.DrawString(
            Text.Font,
            Text.TextString,
            Text.Position,
            Text.TextColor
        );
    }
}