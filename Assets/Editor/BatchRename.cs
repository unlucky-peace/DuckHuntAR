using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BatchRename : ScriptableWizard
{
    //기본 이름
    public string BaseName = "MyObject_";
    
    //시작 숫자
    public int StartNumber = 0;

    //증가치
    public int Increment = 1;

    [MenuItem("Edit/BatchRename")]
    static void CreateWizard() {
        ScriptableWizard.DisplayWizard("Batch Rename", typeof(BatchRename), "Rename");
    }

    //창이 처음 나타날 때 호출
    private void OnEnable() {
        UpdateSelectionHelper();
    }

    //씬에서 선택 영역이 변경 될 때 호출되는 함수
    private void OnSelectionChange() {
        UpdateSelectionHelper();
    }

    //선택된 개수를 업데이트 한다
    private void UpdateSelectionHelper() {
        helpString = "";

        if(Selection.objects != null) {
            helpString = "Number of objects selectes: " + Selection.objects.Length;
        }
    }

    //이름 변경
    private void OnWizardCreate() {
        //선택된 것이 없으면 종료
        if(Selection.objects == null) {
            return;
        }

        //현재 증가치
        int PostFix = StartNumber;

        //순회하며 이름을 변경한다
        foreach (Object O in Selection.objects) {
            O.name = BaseName + PostFix;
            PostFix += Increment;
        }
    }
}
