using System;

namespace GameCore
{
    [Serializable]
    public struct RowData
    {
        public int ZPos;
        public float Speed;
        public float[] DistanceRange;
        public ItemRowData[] ItemsData;

        public RowData(float speed,
            int zPos,
            float[] distanceRange,
            ItemRowData[] itemsData)
        {
            Speed = speed;
            ZPos = zPos;
            DistanceRange = distanceRange;
            ItemsData = itemsData;
        }
    }
}