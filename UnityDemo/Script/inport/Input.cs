using System;


class  Input : Object
{
    public Input(){}
    public static bool compensateSensors { get;  set; }
    public static Gyroscope gyro { get; }
    public static Vector3 mousePosition { get; }
    public static Vector2 mouseScrollDelta { get; }
    public static bool mousePresent { get; }
    public static bool simulateMouseWithTouches { get;  set; }
    public static bool anyKey { get; }
    public static bool anyKeyDown { get; }
    public static StringA inputString { get; }
    public static Vector3 acceleration { get; }
    public static List<AccelerationEvent> accelerationEvents { get; }
    public static int accelerationEventCount { get; }
    public static List<Touch> touches { get; }
    public static int touchCount { get; }
    public static bool touchPressureSupported { get; }
    public static bool stylusTouchSupported { get; }
    public static bool touchSupported { get; }
    public static bool multiTouchEnabled { get;  set; }
    public static LocationService location { get; }
    public static Compass compass { get; }
    public static DeviceOrientation deviceOrientation { get; }
    public static IMECompositionMode imeCompositionMode { get;  set; }
    public static StringA compositionString { get; }
    public static bool imeIsSelected { get; }
    public static Vector2 compositionCursorPos { get;  set; }
    public static bool backButtonLeavesApp { get;  set; }
    public static float GetAxis(StringA axisName){ return default(float); }
    public static float GetAxisRaw(StringA axisName){ return default(float); }
    public static bool GetButton(StringA buttonName){ return default(bool); }
    public static bool GetButtonDown(StringA buttonName){ return default(bool); }
    public static bool GetButtonUp(StringA buttonName){ return default(bool); }
    public static bool GetKey(StringA name){ return default(bool); }
    public static bool GetKey(KeyCode key){ return default(bool); }
    public static bool GetKeyDown(StringA name){ return default(bool); }
    public static bool GetKeyDown(KeyCode key){ return default(bool); }
    public static bool GetKeyUp(StringA name){ return default(bool); }
    public static bool GetKeyUp(KeyCode key){ return default(bool); }
    public static List<StringA> GetJoystickNames(){ return null; }
    public static bool GetMouseButton(int button){ return default(bool); }
    public static bool GetMouseButtonDown(int button){ return default(bool); }
    public static bool GetMouseButtonUp(int button){ return default(bool); }
    public static void ResetInputAxes(){}
    public static AccelerationEvent GetAccelerationEvent(int index){ return default(AccelerationEvent); }
    public static Touch GetTouch(int index){ return default(Touch); }
};

