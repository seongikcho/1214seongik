using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MainCameraMove : MonoBehaviour
{
   

    public GameObject Target_A;
    public GameObject Target_B;
    public GameObject Target_C;  // ī�޶� ����ٴ� Ÿ��

    public float offsetX = 0.0f;            // ī�޶��� x��ǥ
    public float offsetY = 10.0f;           // ī�޶��� y��ǥ
    public float offsetZ = -10.0f;          // ī�޶��� z��ǥ

    public float CameraSpeed = 10.0f;       // ī�޶��� �ӵ�
    Vector3 TargetPos;                      // Ÿ���� ��ġ
    int count ;
    private void Awake()
    {
        
    }
    void Update()
    {

    }
    void FixedUpdate()
    {
        count++;
        if (count >= 200 && count < 500)
        {
            CameraMoving1();
        }
        else if(count >= 500 && count <800)
        {
            CameraMoving2();
        }
        else if (count >= 800 && count < 1100)
        {
            CameraMoving3();
        }
        else if (count >= 1100)
        {
            count = 200;
        }
        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }


    void CameraMoving1()
    {
        TargetPos = new Vector3(
            Target_A.transform.position.x + offsetX,
            Target_A.transform.position.y + offsetY,
            Target_A.transform.position.z + offsetZ
            );
    }
    void CameraMoving2()
    {
        TargetPos = new Vector3(
            Target_B.transform.position.x + offsetX,
            Target_B.transform.position.y + offsetY,
            Target_B.transform.position.z + offsetZ
            );
    }
    void CameraMoving3()
    {
        TargetPos = new Vector3(
            Target_C.transform.position.x + offsetX,
            Target_C.transform.position.y + offsetY,
            Target_C.transform.position.z + offsetZ
            );
    }
}

