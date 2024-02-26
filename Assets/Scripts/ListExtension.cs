using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtension
{
    // Group the logic to get a random element from a list
    public static T PickRandom<T>(this List<T> list)
    {
        var upperBound = list.Count;
        var random = Random.Range(0, upperBound - 1);
        return list.Skip(random).Take(1).First();
    } 
}