using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaka : MonoBehaviour
{
    public List<GameObject> Bunshin_obs;
    public List<GameObject> Light_obs;
    public Color[] ShakaColor;

    public ParticleSystem par;

    public float anglespeed;

    public float lighttime;

    public bool Shaka_on;

    private float timeleft;

    // Start is called before the first frame update
    void Start()
    {
        Shaka_on = false;
        par.Stop();
        //ShakaColor = new Color[4];
    }

    // Update is called once per frame
    void Update()
    {
        if(Shaka_on)
        {
            timeleft += Time.deltaTime;
            //float roty = transform.rotation.y;
            //transform.rotation = Quaternion.Euler(0, roty + anglespeed, 0);
            transform.RotateAround(transform.position, Vector3.forward, anglespeed);
            foreach (GameObject bunshin_ob in Bunshin_obs)
            {
                //float broty = bunshin_ob.transform.rotation.y;
                //bunshin_ob.transform.rotation = Quaternion.Euler(0, roty + anglespeed, 0);
                bunshin_ob.transform.RotateAround(bunshin_ob.transform.position, Vector3.forward, -anglespeed);
            }
            if(timeleft <= 0)
            {
                foreach (GameObject light_ob in Light_obs)
                {
                    int r = Random.Range(0, ShakaColor.Length);
                    light_ob.GetComponent<Light>().color = ShakaColor[r];
                    timeleft = lighttime;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject bunshin_ob in Bunshin_obs)
        {
            bunshin_ob.SetActive(true);
        }
        foreach (GameObject light_ob in Light_obs)
        {
            light_ob.SetActive(true);
        }
        Shaka_on = true;
        par.Play();

    }
}
