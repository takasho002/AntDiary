using System.Collections;
using System.Collections.Generic;
using AntDiary;
using UnityEngine;

public abstract class NestPathEdge
{
    public abstract NestPathNode A { get; }
    public abstract NestPathNode B { get; }
}
