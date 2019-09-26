
[export]
class TestCube
{
    Transform transform;
    Material mat;
    Light m_light;
    Matrix m_mat;
    float m_speed = 10.0f;
    public void Start()
    {
        GameObject obj = GameObject.Find("Directional light");
        m_light = obj.GetComponent<Light>();

        MeshRenderer r = transform.GetComponent<MeshRenderer>();
        if(r != null)
        {
            mat = r.material;
        }
    }
    public void Update()
    {
        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
        up *= Time.deltaTime * m_speed;        
        transform.Rotate(up);

        mat.color = new Color(os.sinf(Time.time*2.0f)*0.5f + 0.5f, 0, 0, 1.0f);
        m_light.color = new Color(os.sinf(Time.time * 2.0f) * 0.5f + 0.5f, 0, 0, 1.0f);
    }
}
