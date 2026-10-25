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
        set
        {
            Vector2 delta = value - BackgroundFrame.Position;
            BackgroundFrame.Position = value;

            foreach (IUIElement child in Children)
            {
                if (child != BackgroundFrame)
                    child.Position += delta;
            }
        }
    }
    public Vector2 Size { 
        get => BackgroundFrame.Size;
        set => BackgroundFrame.Size = value;
    }
    public int ZIndex { get; set; }
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

    private Vector2 _dragOffset;

    public UIContainer(List<IUIElement> children, Vector2 position, Vector2 size, Color backgroundColor, int zIndex = 0, float opacity = 1f)
    {
        Children = children;

        Visible = true;

        BackgroundFrame = new Frame(
            position,
            size,
            backgroundColor,
            zIndex,
            opacity
        );

        Anchor = AnchorPosition.Center;

        ZIndex = zIndex;

        foreach (IUIElement child in children)
        {
            child.Parent = this;
        }

        BackgroundFrame.Parent = this;

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