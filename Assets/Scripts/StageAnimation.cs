using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageAnimation : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    private float sleepTime = 0.3f;
    private string stage = "STAGE ";
    private const string stageClearSound = "Stage_clear";
    private const string dogBarkSound = "Dog_bark";
    
    public void StageAnimationStart()
    {
        StartCoroutine(StageTextAniamtion());
    }

    IEnumerator StageTextAniamtion()
    {
        AudioManager.instance.playSE(stageClearSound);
        yield return new WaitForSeconds(1.5f);
        UIManager.instance.DuckClear();

        for (int i = 0; i < stage.Length; i++)
        {
            stageText.text += stage[i];
            yield return new WaitForSeconds(sleepTime);
        }
        
        stageText.text += GameManager.Instance.stage;
        yield return new WaitForSeconds(sleepTime + 1);
        stageText.text = "";
        AudioManager.instance.playSE(dogBarkSound);
        yield return new WaitForSeconds(sleepTime);
        AudioManager.instance.playSE(dogBarkSound);
        GameManager.Instance.animationPlay = false;
    }
}
