using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bunshin
{
    public GameObject obj;
    public Vector3 target;
}

public class shaka : MonoBehaviour
{
    public List<Bunshin> Bunshin_obs;
    public List<GameObject> Light_obs;
    public Color[] ShakaColor;

    public ParticleSystem par;

    public float anglespeed;
    public float movespeed;
    public float Maxmove;

    public float lighttime;

    public bool Shaka_on;

    private float timeleft;
    private float n_move;

    // Start is called before the first frame update
    void Start()
    {
        n_move = 0;
        Shaka_on = false;
        par.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Shaka_on)
        {
            timeleft += Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.forward, anglespeed);
            foreach (Bunshin bunshin_ob in Bunshin_obs)
            {
                if(n_move <= Maxmove)
                {
                    Vector3 move = new Vector3(bunshin_ob.target.x * movespeed, bunshin_ob.target.y * movespeed, bunshin_ob.target.z * movespeed);
                    bunshin_ob.obj.transform.position += new Vector3(move.x, move.y, move.z);
                }

                bunshin_ob.obj.transform.RotateAround(bunshin_ob.obj.transform.position, Vector3.forward, -anglespeed);
            }

            n_move += movespeed;

            if (timeleft <= 0)
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
        foreach (Bunshin bunshin_ob in Bunshin_obs)
        {
            bunshin_ob.obj.SetActive(true);
        }
        foreach (GameObject light_ob in Light_obs)
        {
            light_ob.SetActive(true);
        }
        Shaka_on = true;
        par.Play();

    }
}
