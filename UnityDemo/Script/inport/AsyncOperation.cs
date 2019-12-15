using System;


class  AsyncOperation : YieldInstruction
{
    public AsyncOperation(){}
    public bool isDone { get; }
    public float progress { get; }
    public int priority { get;  set; }
    public bool allowSceneActivation { get;  set; }
};

