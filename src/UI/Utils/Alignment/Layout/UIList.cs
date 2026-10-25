using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Dow.Main;

namespace Dow.UI.Utils.Alignment;

public class UIList
{
    public List<IUIElement> Elements = [];
    public Vector2 Origin;
    public float MaxHeight;
    public int MaxCount;
    public float Spacing;

    public UIList(Vector2 origin, float spacing = 0f, float maxHeight = float.PositiveInfinity, int maxCount = int.MaxValue, List<IUIElement> elements = null)
    {
        Origin = origin;
        Spacing = spacing;
        Elements = elements ?? [];

        MaxHeight = maxHeight;
        MaxCount = maxCount;

        Update();
    }

    public void Update()
    {
        float offset = 0f;

        for (int i = 0; i < Elements.Count && i < MaxCount; i++)
        {
            IUIElement element = Elements[i];

            // // Skip elements that exceed the list's height
            if (offset + element.Size.Y > MaxHeight)
            {
                element.Visible = false;
                continue;
            }

            // Skip elements that exceed the screen height
            if (Origin.Y + offset + element.Size.Y > Values.ScreenDims.Y)
            {
                element.Visible = false;
                continue;
            }

            element.Position = new Vector2(Origin.X, Origin.Y + offset);
            offset += element.Size.Y + Spacing;
        }
    }

    public void Set(List<IUIElement> elements)
    {
        Elements = elements;
        Update();
    }

    public void Add(IUIElement element)
    {
        Elements.Add(element);
        Update();
    }

    public void Remove(int index)
    {
        Elements.RemoveAt(index);
        Update();
    }
}