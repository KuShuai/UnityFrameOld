using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Int2
{
    public int x;
    public int y;

    public Int2(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"x:{x.ToString()}   y:{y.ToString()}";
    }

    public override int GetHashCode()
    {
        return x ^ (y * 256);
    }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != typeof(Int2))
            return false;
        Int2 int2 = (Int2) obj;
        return x == int2.x && y == int2.y;
    }

    public static bool operator==(Int2 a ,Int2 b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Int2 a, Int2 b)
    {
        return !a.Equals(b);
    }
}
