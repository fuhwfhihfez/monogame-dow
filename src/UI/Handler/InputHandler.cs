
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dow.UI.Handler;

public static class InputHandler
{
    private static readonly Dictionary<IUIElement, bool> lastHoverState = new();

    private static MouseState lastMouseState;
    private static IUIElement activeElement;
    private static bool wasDragged;

    private const float DragThreshold = 3f;
    private static Vector2 pressPosition;

    public static IUIElement GetTopmostElement(Vector2 position)
    {
        IUIElement topmost = null;

        foreach (IUIElement element in Collection.Elements)
        {
            if (!element.Visible)
                continue;

            bool isHovering =
                position.X >= element.Position.X &&
                position.X <= element.Position.X + element.Size.X &&
                position.Y >= element.Position.Y &&
                position.Y <= element.Position.Y + element.Size.Y;

            if (isHovering &&
                (topmost == null || element.ZIndex > topmost.ZIndex))
            {
                topmost = element;
            }
        }

        return topmost;
    }

    public static void UpdateInput(IUIElement hoveredElement)
    {
        MouseState mouse = Mouse.GetState();
        Vector2 mousePos = mouse.Position.ToVector2();

        if (hoveredElement != null && hoveredElement.Parent != null)
        {
            hoveredElement = hoveredElement.Parent;
        }

        bool justPressed =
            mouse.LeftButton == ButtonState.Pressed &&
            lastMouseState.LeftButton == ButtonState.Released;

        bool justReleased =
            mouse.LeftButton == ButtonState.Released &&
            lastMouseState.LeftButton == ButtonState.Pressed;

        // Start an interaction with the topmost element.
        if (justPressed && hoveredElement != null)
        {
            activeElement = hoveredElement;
            pressPosition = mousePos;
            wasDragged = false;

            activeElement.OnHold(true);
        }

        // While interacting, the active element takes priority over Z-index.
        if (activeElement != null)
        {
            if (!activeElement.Visible)
            {
                activeElement = null;
            }
            else if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (Vector2.DistanceSquared(mousePos, pressPosition) > DragThreshold * DragThreshold)
                {
                    wasDragged = true;
                }

                activeElement.OnHold(false);
            }
            else if (justReleased)
            {
                if (!wasDragged && hoveredElement == activeElement)
                    activeElement.OnClick();

                activeElement = null;
            }
        }

        // While an element is being interacted with, it remains the
        // effective hovered element even when the cursor leaves its bounds.
        IUIElement effectiveHoveredElement = activeElement ?? hoveredElement;

        foreach (IUIElement element in Collection.Elements)
        {
            bool isHovering = element == effectiveHoveredElement;

            if (!lastHoverState.TryGetValue(element, out bool wasHovering))
                wasHovering = false;

            if (isHovering != wasHovering)
                element.OnHover(isHovering);

            lastHoverState[element] = isHovering;
        }

        lastMouseState = mouse;
    }
}