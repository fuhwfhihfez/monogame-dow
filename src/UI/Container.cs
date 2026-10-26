using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Dow.UI.Utils.Alignment;

namespace Dow.UI;

public class UIContainer : IUIElement
{
    public UIContainer Parent { get; set; }
    public List<IUIElement> Children = [];
    public Frame BackgroundFrame;
    public Vector2 Position { 
        get => BackgroundFrame.Position;
        set => BackgroundFrame.Position = value;
    }
    public Vector2 Size { 
        get => BackgroundFrame.Size;
        set => BackgroundFrame.Size = value;
    }
    public int ZIndex {
        get => BackgroundFrame.ZIndex;
        set => BackgroundFrame.ZIndex = value;
    }
    private bool _visible;
    public bool Visible {
        get => _visible;
        set
        {
            _visible = value;

            foreach (IUIElement child in Children)
            {
                child.Visible = value;
            }
        }
    }
    public Vector2 Anchor { get; set; }
    public IUIElement InteractionTarget { get; set; }
    public Thickness Padding;
    public Vector2 AbsolutePosition => BackgroundFrame.DrawPosition + new Vector2(Padding.Left, Padding.Top);
    public Vector2 DrawPosition => BackgroundFrame.DrawPosition + new Vector2(Padding.Left, Padding.Top);

    private Vector2 _dragOffset;

    public UIContainer(List<IUIElement> children, Vector2 position, Vector2 size, Color backgroundColor, Vector2 anchor, Thickness padding = default, float opacity = 1f)
    {
        Children = children;
        Anchor = anchor;
        Padding = padding;

        Visible = true;

        BackgroundFrame = new Frame(
            position,
            size + new Vector2(padding.Left + padding.Right, padding.Top + padding.Bottom),
            backgroundColor,
            anchor,
            0,
            opacity
        );

        ZIndex = 0;

        foreach (IUIElement child in Children)
        {
            child.Parent = this;
            child.InteractionTarget = this;

            if (child.ZIndex < ZIndex)
                ZIndex = child.ZIndex - 1;
        }

        BackgroundFrame.InteractionTarget = this;

        Collection.Containers.Add(this);
        Collection.Elements.Add(this);
    }

    public void OnHold(bool Init)
    {
        Vector2 mousePos = Mouse.GetState().Position.ToVector2();

        if (Init)
            _dragOffset = Position - mousePos;
        
        Position = mousePos + _dragOffset;
    }
}