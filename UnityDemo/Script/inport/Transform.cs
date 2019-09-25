using UnityEngine;
using System;
using System.Collections;


class  Transform : Component
{
    public Vector3 position { get;  set; }
    public Vector3 localPosition { get;  set; }
    public Vector3 eulerAngles { get;  set; }
    public Vector3 localEulerAngles { get;  set; }
    public Vector3 right { get;  set; }
    public Vector3 up { get;  set; }
    public Vector3 forward { get;  set; }
    public Quaternion rotation { get;  set; }
    public Quaternion localRotation { get;  set; }
    public Vector3 localScale { get;  set; }
    public Transform parent { get;  set; }
    public Matrix worldToLocalMatrix { get; }
    public Matrix localToWorldMatrix { get; }
    public Transform root { get; }
    public int childCount { get; }
    public Vector3 lossyScale { get; }
    public bool hasChanged { get;  set; }
    public int hierarchyCapacity { get;  set; }
    public int hierarchyCount { get; }
    public void SetParent(Transform parent){}
    public void SetParent(Transform parent,bool worldPositionStays){}
    public void SetPositionAndRotation(Vector3 position,Quaternion rotation){}
    public void Translate(Vector3 translation){}
    public void Translate(Vector3 translation,Space relativeTo){}
    public void Translate(float x,float y,float z){}
    public void Translate(float x,float y,float z,Space relativeTo){}
    public void Translate(Vector3 translation,Transform relativeTo){}
    public void Translate(float x,float y,float z,Transform relativeTo){}
    public void Rotate(Vector3 eulerAngles){}
    public void Rotate(Vector3 eulerAngles,Space relativeTo){}
    public void Rotate(float xAngle,float yAngle,float zAngle){}
    public void Rotate(float xAngle,float yAngle,float zAngle,Space relativeTo){}
    public void Rotate(Vector3 axis,float angle){}
    public void Rotate(Vector3 axis,float angle,Space relativeTo){}
    public void RotateAround(Vector3 point,Vector3 axis,float angle){}
    public void LookAt(Transform target){}
    public void LookAt(Transform target,Vector3 worldUp){}
    public void LookAt(Vector3 worldPosition,Vector3 worldUp){}
    public void LookAt(Vector3 worldPosition){}
    public Vector3 TransformDirection(Vector3 direction){ return default(Vector3); }
    public Vector3 TransformDirection(float x,float y,float z){ return default(Vector3); }
    public Vector3 InverseTransformDirection(Vector3 direction){ return default(Vector3); }
    public Vector3 InverseTransformDirection(float x,float y,float z){ return default(Vector3); }
    public Vector3 TransformVector(Vector3 vector){ return default(Vector3); }
    public Vector3 TransformVector(float x,float y,float z){ return default(Vector3); }
    public Vector3 InverseTransformVector(Vector3 vector){ return default(Vector3); }
    public Vector3 InverseTransformVector(float x,float y,float z){ return default(Vector3); }
    public Vector3 TransformPoint(Vector3 position){ return default(Vector3); }
    public Vector3 TransformPoint(float x,float y,float z){ return default(Vector3); }
    public Vector3 InverseTransformPoint(Vector3 position){ return default(Vector3); }
    public Vector3 InverseTransformPoint(float x,float y,float z){ return default(Vector3); }
    public void DetachChildren(){}
    public void SetAsFirstSibling(){}
    public void SetAsLastSibling(){}
    public void SetSiblingIndex(int index){}
    public int GetSiblingIndex(){ return default(int); }
    public Transform Find(StringA name){ return default(Transform); }
    public bool IsChildOf(Transform parent){ return default(bool); }
    public IEnumerator GetEnumerator(){ return default(IEnumerator); }
    public Transform GetChild(int index){ return default(Transform); }
};

