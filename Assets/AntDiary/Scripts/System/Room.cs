﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public abstract class Room<T> : NestBuildableElement<T> where T : RoomData
    {
        
    }
}