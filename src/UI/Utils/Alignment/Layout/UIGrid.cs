using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Dow.Main;

namespace Dow.UI.Utils.Alignment;

public enum GridMode
{
    Fixed,
    Automatic
}

public class UIGrid
{
    public List<IUIElement> Elements = [];
    public Vector2 Origin;
    public Vector2 Spacing;
    public GridMode Mode;
    public Vector2 Bounds;
    public int MaxXCount;
    public int MaxYCount;
    private List<IUIElement> UneligibleElements;

    public UIGrid(GridMode mode, Vector2 origin, Vector2 spacing = default, Vector2 bounds = default, int maxXCount = int.MaxValue, int maxYCount = int.MaxValue, List<IUIElement> elements = null)
    {
        Mode = mode;

        Origin = origin;
        Spacing = spacing;
        Bounds = bounds;

        MaxXCount = maxXCount;
        MaxYCount = maxYCount;

        Elements = elements ?? [];

        Update();
    }

    public void Update()
    {
        UneligibleElements = [.. Elements];

        switch (Mode)
        {
            case GridMode.Fixed:
            {
                int index = 0;
                float yOffset = 0f;

                for (int y = 0; y < MaxYCount && index < Elements.Count; y++)
                {
                    float xOffset = 0f;
                    float maxYSize = 0f;

                    for (int x = 0; x < MaxXCount && index < Elements.Count; x++)
                    {
                        IUIElement element = Elements[index];

                        // Place element to the right position
                        element.Position = Origin + new Vector2(xOffset, yOffset);
                        
                        // Calculate new row offset
                        xOffset += element.Size.X + Spacing.X;

                        // Update max Y size if this element has the most height in the row
                        maxYSize = Math.Max(maxYSize, element.Size.Y);

                        index++;
                        
                        element.Visible = true;
                        UneligibleElements.Remove(element);
                    }

                    // Update Y offset after every row
                    yOffset += maxYSize + Spacing.Y;
                }
                break;
            }
            
            case GridMode.Automatic:
            {
                int index = 0;
                float yOffset = 0f;

                for (int y = 0; y < MaxYCount && index < Elements.Count; y++)
                {
                    float xOffset = 0f;
                    float maxYSize = 0f;

                    for (int x = 0; x < MaxXCount && index < Elements.Count; x++)
                    {
                        IUIElement element = Elements[index];

                        // Start a new row for too wide elements
                        if (xOffset + element.Size.X > Bounds.X)
                        {
                            index++;
                            break;
                        }

                        // Start a new row for elements that go off-screen
                        if (Origin.X + xOffset + element.Size.X > Values.ScreenDims.X)
                        {
                            index++;
                            break;
                        }

                        // Continue if the element has too much height for its bounds
                        if (yOffset + element.Size.Y > Bounds.Y)
                        {
                            index++;
                            continue;
                        }

                        // Continue if the element has too much height for the screen
                        if (Origin.Y + yOffset + element.Size.Y > Values.ScreenDims.Y)
                        {
                            index++;
                            continue;
                        }

                        // Place element to the right position
                        element.Position = Origin + new Vector2(xOffset, yOffset);
                        
                        // Calculate new row offset
                        xOffset += element.Size.X + Spacing.X;

                        // Update max Y size if this element has the most height in the row
                        maxYSize = Math.Max(maxYSize, element.Size.Y);

                        index++;

                        element.Visible = true;
                        UneligibleElements.Remove(element);
                    }

                    // Update Y offset after every row
                    yOffset += maxYSize + Spacing.Y;
                }
                break;
            }
        }

        foreach (IUIElement uneligibleElement in UneligibleElements)
        {
            uneligibleElement.Visible = false;
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