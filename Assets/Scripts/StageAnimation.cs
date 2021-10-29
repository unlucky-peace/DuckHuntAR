using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageAnimation : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    private float sleepTime = 0.3f;
    [SerializeField] private string output = "";
    private const string stageClearSound = "Stage_clear";
    private const string dogBarkSound = "Dog_bark";
    
    public void StageAnimationStart()
    {
        StartCoroutine(StageAniamtion());
    }

    public void TextAimationStart()
    {
        StartCoroutine(TextAnimation());
    }

    IEnumerator StageAniamtion()
    {
        AudioManager.instance.playSE(stageClearSound);
        yield return new WaitForSeconds(1.5f);
        UIManager.instance.DuckClear();
        TextAimationStart();
        yield return new WaitForSeconds(sleepTime * (output.Length + 1) + 1); //아 여기 async로 하거나 그냥 아예 타임라인으로 해야 편한데
        AudioManager.instance.playSE(dogBarkSound);
        yield return new WaitForSeconds(sleepTime);
        AudioManager.instance.playSE(dogBarkSound);
        GameManager.Instance.animationPlay = false;
    }

    IEnumerator TextAnimation()
    {
        for (int i = 0; i < output.Length; i++)
        {
            stageText.text += output[i];
            yield return new WaitForSeconds(sleepTime);
        }

        if (SceneManager.GetActiveScene().name == "Main")
        {
            stageText.text += GameManager.Instance.stage;
            yield return new WaitForSeconds(sleepTime + 1);
            stageText.text = "";
        }
        else
        {
            yield return new WaitForSeconds(1f);
            stageText.text = "";
        }

    }
}
