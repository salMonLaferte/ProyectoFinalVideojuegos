using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class to use for getting distance and directions in specific planes.
/// </summary>
public static class VectorTools
{
    /// <summary>
    /// Returns the normalized direction of the proyected vectors in the XZ plane.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
     public static Vector3 DirectionXZ(Vector3 from, Vector3 to){
        if(from==null || to == null)
            return Vector3.zero;
        Vector2 dir;
        Vector2 a = new Vector2(from.x, from.z);
        Vector2 b = new Vector2(to.x, to.z);
        dir = b-a;
        dir.Normalize();
        Vector3 dir3 = new Vector3(dir.x, 0, dir.y);
        return dir3;
    }
    /// <summary>
    /// Returns the distance between the proyected vectors int he XZ plane
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float DistanceXZ(Vector3  a, Vector3 b){
        if(a==null || b == null)
            return 0;
        Vector2 v1 = new Vector2(a.x, a.z);
        Vector2 v2 = new Vector2(b.x, b.z);
        float distance = Vector2.Distance( v1, v2 );
        return distance;
    }
    /// <summary>
    /// Returns the distance from the vector a.transform.position proyected in the XZ plane to the vector
    /// b.transform.position proyected in the XZ plane
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float DistanceXZ(Transform  a, Transform b){
        if(a==null || b == null)
            return 0;
        return DistanceXZ(a.position, b.position);
    }

    /// <summary>
    /// Returns the direction of the transform.position(s) vectors proyected in the XZ plane.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static Vector3 DirectionXZ(Transform from, Transform to){
        if(from==null || to == null)
            return Vector3.zero;
        return DirectionXZ(from.position, to.position);
    }


}
