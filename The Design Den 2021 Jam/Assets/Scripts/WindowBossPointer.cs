using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class WindowBossPointer : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    // Start is called before the first frame update
    public GameObject target = null;
    private RectTransform pointerRectTransform;
    void Awake()
    {
        pointerRectTransform = transform.Find("BossPointer").GetComponent<RectTransform>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = target.transform.position;
        Vector3 fromPosition = Camera.main.transform.position;

        toPosition.z = 0.0f;
        fromPosition.z = 0.0f;

        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);

        pointerRectTransform.localEulerAngles = new Vector3(0.0f, 0.0f, angle - 90.0f);

        float borderSize = 50f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target.transform.position);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize ||
                           targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPos = targetPositionScreenPoint;
            if (cappedTargetScreenPos.x <= borderSize) cappedTargetScreenPos.x = borderSize;
            if (cappedTargetScreenPos.y <= borderSize) cappedTargetScreenPos.y = borderSize;
            if (cappedTargetScreenPos.x >= Screen.width - borderSize) cappedTargetScreenPos.x = Screen.width - borderSize;
            if (cappedTargetScreenPos.y >= Screen.height - borderSize) cappedTargetScreenPos.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPos);

            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.position = new Vector3(pointerRectTransform.position.x, pointerRectTransform.position.y, 0.0f);
        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(new Vector3(Screen.width + 100f, Screen.height + 100f, 0.0f));

            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.position = new Vector3(pointerRectTransform.position.x, pointerRectTransform.position.y, 0.0f);
        }
        
        
    }
}
