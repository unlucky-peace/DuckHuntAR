using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageAnimation : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    private float sleepTime = 0.3f;
    private string stage = "STAGE ";
    
    public void StageAnimationStart()
    {
        GameManager.Instance.animationPlay = true;
        StartCoroutine(StageTextAniamtion());
    }

    IEnumerator StageTextAniamtion()
    {
        yield return new WaitForSeconds(1.5f);
        //이거 이렇게까지 해야하나 누가 도움좀

        for (int i = 0; i < stage.Length; i++)
        {
            stageText.text += stage[i];
            yield return new WaitForSeconds(sleepTime);
        }
        
        stageText.text += GameManager.Instance.stage;
        yield return new WaitForSeconds(sleepTime + 1);
        stageText.text = "";
        GameManager.Instance.animationPlay = false;
    }
}
