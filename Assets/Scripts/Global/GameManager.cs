using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent endGame;
    public int maxOrbs = 10;
    public int collectedOrbs = 0;

    private void Start()
    {
        endGame.AddListener(() => { StartCoroutine(WaitForFade()); });
    }

    public void AddOrb()
    {
        collectedOrbs += 1;
        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (collectedOrbs >= maxOrbs)
        {
            endGame?.Invoke();
        }
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(2f);
        SceneLoader.LoadScene("MenuScene");
    }
}
