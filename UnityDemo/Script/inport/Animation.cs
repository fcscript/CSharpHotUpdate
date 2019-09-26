using System;
using System.Collections;


class  Animation : Behaviour
{
    public Animation(){}
    public AnimationClip clip { get;  set; }
    public bool playAutomatically { get;  set; }
    public WrapMode wrapMode { get;  set; }
    public bool isPlaying { get; }
    public bool animatePhysics { get;  set; }
    public AnimationCullingType cullingType { get;  set; }
    public Bounds localBounds { get;  set; }
    public void Stop(){}
    public void Stop(StringA name){}
    public void Rewind(StringA name){}
    public void Rewind(){}
    public void Sample(){}
    public bool IsPlaying(StringA name){ return default(bool); }
    public bool Play(){ return default(bool); }
    public bool Play(PlayMode mode){ return default(bool); }
    public bool Play(StringA animation,PlayMode mode){ return default(bool); }
    public bool Play(StringA animation){ return default(bool); }
    public void CrossFade(StringA animation,float fadeLength,PlayMode mode){}
    public void CrossFade(StringA animation,float fadeLength){}
    public void CrossFade(StringA animation){}
    public void Blend(StringA animation,float targetWeight,float fadeLength){}
    public void Blend(StringA animation,float targetWeight){}
    public void Blend(StringA animation){}
    public AnimationState CrossFadeQueued(StringA animation,float fadeLength,QueueMode queue,PlayMode mode){ return default(AnimationState); }
    public AnimationState CrossFadeQueued(StringA animation,float fadeLength,QueueMode queue){ return default(AnimationState); }
    public AnimationState CrossFadeQueued(StringA animation,float fadeLength){ return default(AnimationState); }
    public AnimationState CrossFadeQueued(StringA animation){ return default(AnimationState); }
    public AnimationState PlayQueued(StringA animation,QueueMode queue,PlayMode mode){ return default(AnimationState); }
    public AnimationState PlayQueued(StringA animation,QueueMode queue){ return default(AnimationState); }
    public AnimationState PlayQueued(StringA animation){ return default(AnimationState); }
    public void AddClip(AnimationClip clip,StringA newName){}
    public void AddClip(AnimationClip clip,StringA newName,int firstFrame,int lastFrame,bool addLoopFrame){}
    public void AddClip(AnimationClip clip,StringA newName,int firstFrame,int lastFrame){}
    public void RemoveClip(AnimationClip clip){}
    public void RemoveClip(StringA clipName){}
    public int GetClipCount(){ return default(int); }
    public void SyncLayer(int layer){}
    public IEnumerator GetEnumerator(){ return default(IEnumerator); }
    public AnimationClip GetClip(StringA name){ return default(AnimationClip); }
};

