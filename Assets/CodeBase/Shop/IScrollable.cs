using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackBall
{
    public interface IScrollable
    {
        int selectedBallID { get; }
        GameObject[] ballPrefabs { get; }
        int ballCount { get; }
    }
}
