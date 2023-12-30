using System.Collections;
using UnityEngine;

public class TimeBarManager : MonoBehaviour
{
    [SerializeField] private TimeBarController timeBar;
    float EPSILON = 2.71828f;
    float newTime;

    void Start()
    {
        GameManager.Instance.timeBarManager = this;
    }

    public void NewBar(int step)
    {
        StopCoroutine("Timer");
        newTime = 10 * Mathf.Pow(EPSILON, -step / 10) + 1;
        timeBar.SetMaxTime(newTime);
        StartCoroutine("Timer");
    }

    public void OnLose()
    {
        StopCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        float time = newTime;
        for (int i = 0; i < newTime / 0.01f; i++)
        {
            yield return new WaitForSeconds(0.01f);
            time -= 0.01f;
            timeBar.SetTime(time);
        }

        GameManager.Instance.platformManager.TimeLose();

        yield return null;
    }
}