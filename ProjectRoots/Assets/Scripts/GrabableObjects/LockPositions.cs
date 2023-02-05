using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GrabableItem))]
public class LockPositions : MonoBehaviour
{
    public bool blockXPos = false;
    public bool blockYPos = false;
    public bool blockZPos = false;
    public Vector2 xPosLimiter;
    public Vector2 yPosLimiter;
    public Vector2 zPosLimiter;
}
