using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Indicator : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Transform target;


    public void SetupIndicator(Character targetToTrack)
    {
        this.target = targetToTrack.AttachIndicatorPoint;
    }

    private void Update()
    {
        if (target == null) return;

        image.transform.rotation = Quaternion.identity;

        float minX = image.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = image.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 calculatePos = Camera.main.WorldToScreenPoint(target.position);

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector3 pos = calculatePos;


        if (calculatePos.z < 0)
        {
            calculatePos *= -1;
        }

        if (calculatePos.x < minX)
        {
            pos.x = minX;
            pos.y = ((minX - center.x) / (calculatePos.x - center.x)) * (calculatePos.y - center.y) + center.y;
        }
        else if (calculatePos.x > maxX)
        {
            pos.x = maxX;
            pos.y = ((maxX - center.x) / (calculatePos.x - center.x)) * (calculatePos.y - center.y) + center.y;
        }
        

        if (calculatePos.y < minY)
        {
            pos.y = minY;
            pos.x = ((minY - center.y) / (calculatePos.y - center.y)) * (calculatePos.x - center.x) + center.x;
        }
        else if (calculatePos.y > maxY)
        {
            pos.y = maxY;
            pos.x = ((maxY - center.y) / (calculatePos.y - center.y)) * (calculatePos.x - center.x) + center.x;
        }


        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        image.transform.position = pos;

        

        if ((calculatePos - pos).sqrMagnitude < 0.1f) return;

        Vector2 dir = new Vector2(pos.x, pos.y) - center;
        float angleToRotate = Vector2.Angle(Vector2.down, dir);
        image.transform.Rotate(0f, 0f, dir.x > 0 ? angleToRotate : -angleToRotate);

    }
}
