using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepRotation : MonoBehaviour
{

    private Image _i;
    private Transform _p;

    private void Start() {
        _i = GetComponent<Image>();
        _p = transform.parent.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 rotation = new Vector3(-_p.rotation.x,-_p.rotation.y,-_p.rotation.z);
        _i.rectTransform.rotation = Quaternion.Euler(rotation);
    }
}
