using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEnd : MonoBehaviour
{
    public GameObject Target = null;
    public float Speed = 0.01f;

    [Header("Road End Points")]
    public int TargetBoundMin;
    public int TargetBoundMax;

    private int dir = 1;

    [Header("Gizmos")]
    [SerializeField] Color target_color = Color.grey;
    [SerializeField] float target_size = 0.25f;


    // Use this for initialization
    void Update()
    {
        var tPos = Target.transform.position;

        tPos.x += Speed * dir * Time.deltaTime;

        if(tPos.x <= TargetBoundMin)
        {
            dir = 1;
        }

        if(tPos.x >= TargetBoundMax)
        {
            dir = -1;
        }
    }



#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(Target == null)
            return;

        Gizmos.color = target_color;
        Gizmos.DrawWireSphere(Target.transform.position, target_size);
    }
#endif
}
