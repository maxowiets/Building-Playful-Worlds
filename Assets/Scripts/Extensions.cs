using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static float CheckPlaneDistanceTo(this Vector3 a, Vector3 b)
    {
        a.y = 0;
        b.y = 0;
        float distance = Vector3.Distance(a, b);
        return distance;
    }

    public static int CalculateRemainder(this int begin, int divide)
    {
        while (begin - divide >= 0)
        {
            begin -= divide;
        }

        return begin;
    }
}
