using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// 어떤 오브젝트가 터치한 장소에 보이게 하려면, 세상 그 자체를 얼만큼 평행이동해야 할지 알고 그렇게 이동함
public class MoveWorldPointPivot : MonoBehaviour
{
    public Transform target;
    public ARRaycastManager raycastManager;
    public ARSessionOrigin arsessionOrigin;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        
        if (raycastManager.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            arsessionOrigin.MakeContentAppearAt(target, hitPose.position, hitPose.rotation);
        }
    }
}
