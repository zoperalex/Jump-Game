using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private TextMeshProUGUI score;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image gameOverPanelImage;
    [SerializeField] private RectTransform gameOverTextTransform;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    void Start()
    {
        SetScore(0);
        gameOverPanel.SetActive(false);
        GameManager.Instance.uiManager = this;
    }

    public void SetScore(int s)
    {
        score.text = s.ToString();
    }

    public void GameOver(int score)
    {
        gameOverScoreText.text = "Score: " + score.ToString();
        StartCoroutine(GameOverFade());
        StartCoroutine(GameOverTextFade());
        scoreBoard.SetActive(false);
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator GameOverFade()
    {
        gameOverPanel.gameObject.SetActive(true);
        float t = 0;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.025f);
            t += 0.05f;
            gameOverPanelImage.color = new Color32(0, 0, 0, (byte)Mathf.Lerp(0, 150, t));
        }
        yield return null;
    }

    IEnumerator GameOverTextFade()
    {
        yield return new WaitForSeconds(0.2f);

        float t = 0;

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.015f);
            t += 0.05f;
            gameOverTextTransform.localPosition = new Vector2(0, Mathf.Lerp(200, 80, t));
            gameOverText.color = new Color32(214, 23, 23, (byte)Mathf.Lerp(0, 255, t));
            gameOverScoreText.color = new Color32(255, 255, 255, (byte)Mathf.Lerp(0, 255, t));
        }

        yield return null;
    }
}
