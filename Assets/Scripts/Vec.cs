using System;

public interface IVec { }

public class Vec2 : IVec
{
    public readonly int Item1, Item2;

    public Vec2(int item1, int item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}

public class Vec3 : IVec
{
    public readonly int Item1, Item2, Item3;

    public Vec3(int item1, int item2, int item3)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
    }
}

public class Vec4 : IVec
{
    public readonly int Item1, Item2, Item3, Item4;

    public Vec4(int item1, int item2, int item3, int item4)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4;
    }
}