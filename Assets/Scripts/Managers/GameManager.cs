using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public PlatformManager platformManager;
    [HideInInspector] public UIManager uiManager;
    [HideInInspector] public TimeBarManager timeBarManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Lose(int score)
    {
        timeBarManager.OnLose();
        uiManager.GameOver(score);
    }
}
