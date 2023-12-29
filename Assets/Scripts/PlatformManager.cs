using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    Queue<Direction> directionQueue;
    public int score { get; private set; }

    void Start()
    {
        directionQueue = new Queue<Direction>();
        score = 0;
        GameManager.Instance.platformManager = this;
    }

    public void QueueDirection(Direction d)
    {
        directionQueue.Enqueue(d);
    }

    public void CheckDirection(Direction d)
    {
        if (!d.Equals(directionQueue.Dequeue())) GameManager.Instance.Lose(score);
        else
        {
            score++;
            GameManager.Instance.uiManager.SetScore(score);
        }
    }
}
