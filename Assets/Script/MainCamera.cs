using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GroundClickedEvent: UnityEvent<Vector3>{
    
}

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    Transform followObject;
    
    [SerializeField]
    float maximaDistancia = 15;

    [SerializeField]
    Vector3 RelativeToObject = new Vector3(0, -5, 0); 
    
    [SerializeField]
    LayerMask GroundLayer;

    public GroundClickedEvent groundClicked = new GroundClickedEvent();

    void FixedUpdate()
    {
        ///Maintain the camera following the object while maintaining the same position relative to the object
        if(followObject !=null){
            float distance = VectorTools.DistanceXZ(transform.position, followObject.position + RelativeToObject);
            if(distance >= maximaDistancia){
                float speed = distance/.4f;
                Vector3 dir = VectorTools.DirectionXZ(transform.position , followObject.transform.position+ RelativeToObject);
                transform.position += dir * Time.deltaTime * speed;
            }
            transform.position = new Vector3(transform.position.x, followObject.transform.position.y + RelativeToObject.y, transform.position.z);
        }

    }
    
}

