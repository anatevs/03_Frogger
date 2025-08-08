using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class PointsStorage : MonoBehaviour
    {
        public event Action<int> OnPointsChanged;

        private int _points;
    }
}