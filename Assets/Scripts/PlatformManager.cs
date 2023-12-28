using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    Queue<Direction> directionQueue;


    void Start()
    {
        directionQueue = new Queue<Direction>();
        GameManager.Instance.platformManager = this;
    }

    public void QueueDirection(Direction d)
    {
        directionQueue.Enqueue(d);
    }

    public void CheckDirection(Direction d)
    {
        if (!d.Equals(directionQueue.Dequeue()))
        {
            Debug.Log("Game Over");
            //GameOver
        }
        else
        {
            Debug.Log("All Good");
        }
    }
}
