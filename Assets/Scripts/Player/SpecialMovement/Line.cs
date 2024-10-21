using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lr;

    private Vector3[] segemntV;

    private int length;

    public Transform targetDir;

    public Vector3[] segemntPoses;

    [Header("Var for line")]
    public float targetDist;
    public float smoothSpeed;

    public List<GameObject> bodyParts;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        CheckLength();

        ResetPos();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            AddBodyPart();
        }
    }

    private void FixedUpdate()
    {
        LineFunction();
    }

    private void CheckLength()
    {
        Vector3[] oldSegmentPoses = null;

        if (segemntPoses != null && segemntPoses.Length > 0)
        {
            oldSegmentPoses = new Vector3[segemntPoses.Length];
            for (int i = 0; i < segemntPoses.Length; i++)
            {
                oldSegmentPoses[i] = segemntPoses[i];
            }
        }

        length = bodyParts.Count + 1;

        segemntPoses = new Vector3[length];
        segemntV = new Vector3[length];

        if (oldSegmentPoses != null)
        {
            for (int i = 0; i < oldSegmentPoses.Length; i++)
            {
                segemntPoses[i] = oldSegmentPoses[i];
            }
        }
    }

    private void LineFunction()
    {
        segemntPoses[0] = targetDir.position;

        for (int i = 1; i < segemntPoses.Length; i++)
        {
            Vector3 targetPos = segemntPoses[i - 1] + (segemntPoses[i] - segemntPoses[i - 1]).normalized * targetDist;
            segemntPoses[i] = Vector3.SmoothDamp(segemntPoses[i], targetPos, ref segemntV[i], smoothSpeed);

            if (i - 1 < bodyParts.Count)
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

    private void AddBodyPart()
    {
        GameObject addedBodyPart = Instantiate(bodyParts[0], transform.position, Quaternion.identity);

        bodyParts.Add(addedBodyPart);

        CheckLength();
    }
}
