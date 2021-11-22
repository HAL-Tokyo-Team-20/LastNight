using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform Anchor;         //圆点
    public float Gravity = 9.8f;     //重力加速度
    private Vector3 m_rotateAxis;    //旋转轴
    private float w = 0;             //角速度
    
    void Start()
    {
        //求出旋转轴
        m_rotateAxis = Vector3.Cross(transform.position - Anchor.position, Vector3.down);
    }
    void DoPhysics()
    {
        float r = Vector3.Distance(Anchor.position, transform.position);
        float l = Vector3.Distance(new Vector3(Anchor.position.x, transform.position.y, Anchor.position.z), transform.position);
        //当钟摆摆动到另外一侧时，l为负，则角加速度alpha为负。
        Vector3 axis = Vector3.Cross(transform.position - Anchor.position, Vector3.down);
        if (Vector3.Dot(axis, m_rotateAxis) < 0)
        {
            l = -l;
        }
        float cosalpha = l / r;
        //求角加速度
        float alpha = (cosalpha * Gravity) / r;
        //累计角速度
        w += alpha * Time.deltaTime;
        //求角位移(乘以180/PI 是为了将弧度转换为角度)
        float thelta = w * Time.deltaTime * 180.0f / Mathf.PI;
        //绕圆点m_ahchor的旋转轴m_rotateAxis旋转thelta角度
        transform.RotateAround(Anchor.position, m_rotateAxis, thelta);
    }
    
    void Update()
    {
        DoPhysics();
    }
}