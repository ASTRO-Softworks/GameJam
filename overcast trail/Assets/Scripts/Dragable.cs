using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    public virtual bool CanBeDragged()
    {
        return true;
    }

    public virtual Transform GetTransform()
    {
        return gameObject.transform;
    }


}
