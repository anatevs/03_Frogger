using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class LevelConfig : ScriptableObject
    {
        //road rows
        //row: speed, carType


        //water rows
        //row: speed, distanceRange, objType:
        // - logObj: lengthRange
        // - bugsObj: lifetimeRange


        //items: log, car, 3 bugs, 3 cars, crocodile

        // row: item, optional item
    }
}