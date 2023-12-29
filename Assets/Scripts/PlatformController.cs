using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    List<GameObject> children;
    System.Random rnd;
    int currentPos;
    Direction currentDir;
    bool removing;

    private void Start()
    {
        Setup();
    }

    void Setup()
    {
        children = new List<GameObject>();
        rnd = new System.Random();
        currentPos = 4;
        currentDir = Direction.Right;
        removing = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }
        children[0].transform.position = new Vector3(1, 1, 0); //sets initial position of first platform to the right of the player.
        children[0].SetActive(true);
        GameManager.Instance.platformManager.QueueDirection(Direction.Right);
        for (int i = 1; i < 5; i++)
        {
            CalcNextPosition(i);
        }
    }

    public void OnClick(int d)
    {
        if (d == 0)
        {
            StartCoroutine(MoveScreen(currentDir == Direction.Left ? 1 : -1));
        }
        else
        {
            StartCoroutine(MoveScreen(currentDir == Direction.Left ? -1 : 1));
            currentDir = currentDir == Direction.Left ? Direction.Right : Direction.Left;
        }

        GameManager.Instance.platformManager.CheckDirection(currentDir);

        if (currentPos == transform.childCount - 1) currentPos = 0;
        else currentPos++;

        //checks if we can start removing, only after 12 platforms have already spawned
        if (!removing && currentPos == 11) removing = true;

        CalcNextPosition(currentPos);
    }

    void CalcNextPosition(int i)
    {
        int prev;
        int removePos;
        if (i == 0) prev = transform.childCount - 1;
        else prev = i - 1;

        if (rnd.Next(0, 2) == 0)
        {
            children[i].transform.position = children[prev].transform.position + new Vector3(1, 1, 0);
            GameManager.Instance.platformManager.QueueDirection(Direction.Right);
        }
        else
        {
            children[i].transform.position = children[prev].transform.position + new Vector3(-1, 1, 0);
            GameManager.Instance.platformManager.QueueDirection(Direction.Left);
        }

        children[i].SetActive(true);

        //removing unused platform
        if (!removing) return;
        removePos = i - 10;
        if (removePos < 0) removePos = transform.childCount + removePos;
        children[removePos].SetActive(false);
    }

    IEnumerator MoveScreen(int xPos)
    {
        float t = 0;
        Vector3 initialPos = transform.position;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.0001f);
            t += 0.05f;
            transform.position = initialPos + new Vector3(Mathf.Lerp(0, xPos, t), Mathf.Lerp(0, -1, t), 0);
        }
        yield return null;
    }
}
