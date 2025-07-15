using System;

namespace GameCore
{
    public abstract class MovingItem
    {
        public event Action<LogComponent> OnEndPassed;


    }
}