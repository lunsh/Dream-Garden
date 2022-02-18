using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Methods : MonoBehaviour
{
    public static List<T> CreateList<T>(int capacity) => Enumerable.Repeat(default(T), capacity).ToList();
}
