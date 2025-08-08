using System;

namespace GameCore
{
    [Serializable]
    public struct ItemRowData
    {
        public float AppearProb;
        public float[] LengthScaleRange;
        public MovingItem Prefab;

        public MovingItemPool Pool
        {
            readonly get => _pool;
            set => _pool = value;
        }

        public float GetRandomLength()
        {
            return UnityEngine.Random.Range(
                LengthScaleRange[0],
                LengthScaleRange[^1]);
        }

        private MovingItemPool _pool;
    }
}