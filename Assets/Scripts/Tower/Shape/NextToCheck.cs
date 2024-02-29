using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NextToCheck
{
    public static bool isNextTo(int x1, int y1, int x2, int y2, string name)
    {
        if (name == "hexagon")
        {
            return isNextToHex(y1, x1, y2, x2);
        }

        if (name == "cube")
        {
            return isNextToSquare(x1, y1, x2, y2);
        }

        if (name == "triangle")
        {
            return isNextToTriangle(x1, y1, x2, y2);
        }

        if (name == "pentagon")
        {
            return isNextToPentagon(x1, y1, x2, y2)||isNextToPentagon(x2,y2,x1,y1);
        }

        return false;
    }
    private static bool isNextToPentagon(int x1, int y1, int x2, int y2)
    {
        switch ((x1+((y1%2)*2))%4)
        {
            case 0: return isNextToPentagonFacingVertical(x1, y1, x2, y2, true);
            case 1: return isNextToPentagonFacingHorizontal(x1, y1, x2, y2, false);
            case 2: return isNextToPentagonFacingVertical(x1, y1, x2, y2, false);
            case 3: return isNextToPentagonFacingHorizontal(x1, y1, x2, y2, true);
        }
        return false;
    }

    private static bool isNextToPentagonFacingVertical(int x1, int y1, int x2, int y2, bool up)
    {
        for (int i = -1; i <= 1; i+=2)
        {
            for (int j = -1; j <= 0; j++)
            {
                if (x1 + i == x2 && y1 + j == y2)
                {
                    return true;
                }
            }
        }
        return (x1 == x2 && y1 + (up ? 1 : -1) == y2);
    }
    private static bool isNextToPentagonFacingHorizontal(int x1, int y1, int x2, int y2, bool right)
    {
        for (int i = -1; i <= 1; i+=2)
        {
            for (int j = -1; j <= 0; j++)
            {
                if (x1 + i == x2 && y1 + j == y2)
                {
                    return true;
                }
            }
        }
        return (x1 + (right ? -2:2) == x2 && y1 == y2);
    }

    private static bool isNextToHex(int x1, int y1, int x2, int y2)
    {
        if (x2 % 2 == 1)
        {
            if (y2 == y1 - 1)
            {
                return Mathf.Abs(x1 - x2) < 2;
            }

            if (y2 == y1)
            {
                return Mathf.Abs(x1 - x2) == 1;
            }

            if (y2 - 1 == y1)
            {
                return x1 == x2;
            }

            return false;
        }

        if (y2 - 1 == y1)
        {
            return Mathf.Abs(x1 - x2) < 2; 
        }

        if (y2 == y1)
        {
            return Mathf.Abs(x1 - x2) == 1;
        }

        if (y2 == y1 - 1)
        {
            return x1 == x2;
        }

        return false;
    }
    private static bool isNextToSquare(int x1, int y1, int x2, int y2)
    {
        if (x1 != x2 && y1 != y2) return false;
        if (Mathf.Abs(x1 - x2) > 1) return false;
        if (Mathf.Abs(y1 - y2) > 1) return false;
        if (x1 == x2 && y1 == y2) return false;
        return true;
    }
    private static bool isNextToTriangle(int x1, int y1, int x2, int y2)
    {
        if (x1 != x2 && y1 != y2) return false;
        if (Mathf.Abs(x1 - x2) > 1) return false;
        if (Mathf.Abs(y1 - y2) > 1) return false;
        if (x1 == x2 && y1 == y2) return false;
        if (y1 - y2 == 1 && (y1 + x1) % 2 == 1) return false;
        if (y1 - y2 == -1 && (y1 + x1) % 2 == 0) return false;
        return true;
    }
}
