using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BossPointer : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    public float StartPosX = 200;
    public float StartPosY = 45;

    private void Awake()
    {
        targetPosition = new Vector3(StartPosX, StartPosY);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

        if (isOffScreen)
        {
            Vector3 cappedTargetScrenPosition = targetPositionScreenPoint;
            if (cappedTargetScrenPosition.x <= 0) cappedTargetScrenPosition.x = 0f;
            if (cappedTargetScrenPosition.x >= Screen.width) cappedTargetScrenPosition.x = Screen.width;
            if (cappedTargetScrenPosition.y <= 0) cappedTargetScrenPosition.y = 0f;
            if (cappedTargetScrenPosition.y >= Screen.height) cappedTargetScrenPosition.y = Screen.height;
        }
    }
}
