using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lr;

    private Vector3[] segemntV; 

    public Transform targetDir;

    public Vector3[] segemntPoses;

    public int length;

    [Header("Var for line")]
    public float targetDist;
    public float smoothSpeed;

    [Header("Wiggle")]
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    public GameObject[] bodyParts;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        lr.positionCount = length;

        segemntPoses = new Vector3[length];
        segemntV = new Vector3[length];

        ResetPos();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LineFunction();
    }

    private void LineFunction()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segemntPoses[0] = targetDir.position;

        for (int i = 1; i < segemntPoses.Length; i++)
        {
            Vector3 targetPos = segemntPoses[i - 1] + (segemntPoses[i] - segemntPoses[i - 1]).normalized * targetDist;
            segemntPoses[i] = Vector3.SmoothDamp(segemntPoses[i], targetPos, ref segemntV[i], smoothSpeed);

            if (i - 1 < bodyParts.Length)
            {
                bodyParts[i - 1].transform.position = segemntPoses[i];
            }
        }
        lr.SetPositions(segemntPoses);
    }

    private void ResetPos()
    {
        segemntPoses[0] = targetDir.position;

        for(int i = 1;i < length; i++)
        {
            segemntPoses[i] = segemntPoses[i - 1] + targetDir.right * targetDist;
        }
        lr.SetPositions(segemntPoses);
    }
}
