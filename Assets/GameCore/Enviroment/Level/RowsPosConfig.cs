using System.Collections;
using UnityEngine;

namespace GameCore
{
    public sealed class RowsPosConfig : ScriptableObject
    {
        [SerializeField]
        private float[] _rowZPos = new float[10];
    }
}