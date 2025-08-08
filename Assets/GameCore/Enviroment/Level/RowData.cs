using System;
using UnityEngine;

namespace GameCore
{
    [Serializable]
    public struct RowData
    {
        [field: SerializeField]
        public int ZPos { get; private set; }

        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public float MinDistance { get; private set; }

        [field: SerializeField]
        public float MaxDistance { get; private set; }

        [field: SerializeField]
        public ItemRowData[] ItemsData { get; private set; }
    }
}