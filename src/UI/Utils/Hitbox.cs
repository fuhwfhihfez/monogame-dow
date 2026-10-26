
using System;
using Microsoft.Xna.Framework;

namespace Dow.UI.Utils;

public class Hitbox : IUIElement
{
    public UIContainer Parent { get; set; }
    public int ZIndex { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public bool Visible { get; set; }
    public Vector2 Anchor { get; set; }
    public IUIElement InteractionTarget { get; set; }

    public Vector2 AbsolutePosition => Parent == null ? Position : Parent.AbsolutePosition + Position;
    public Vector2 DrawPosition => AbsolutePosition - Size * Anchor;
    public Action _OnClick;
    public Action<bool> _OnHover;
    public Action _OnHold;

    public Hitbox(Vector2 position, Vector2 size, int zIndex = 0, Action onClick = null, Action<bool> onHover = null, Action onHold = null)
    {
        Position = position;
        Size = size;
        _OnClick = onClick;
        _OnHover = onHover;
        _OnHold = onHold;

        ZIndex = zIndex;

        Visible = false;

        Collection.Hitboxes.Add(this);
        Collection.Elements.Add(this);
    }

    public void OnClick()
    {
        _OnClick?.Invoke();
    }

    public void OnHover(bool Enter)
    {
        _OnHover?.Invoke(Enter);
    }

    public void OnHold()
    {
        _OnHold?.Invoke();
    }
}