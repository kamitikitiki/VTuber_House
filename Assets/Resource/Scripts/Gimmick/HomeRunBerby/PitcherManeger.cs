using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PitcherManeger : MonoBehaviour
{
    //投球者データ
    [SerializeField]
    public List<PitcherInfo> pitcherInfos;

    private PitcherInfo now_pitcherInfo;
    private bool MountIn;

    // Start is called before the first frame update
    void Start()
    {
        MountIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.M))
        {
            if(!MountIn)
            {
                foreach (PitcherInfo pitcherInfo in pitcherInfos)
                {
                    if (pitcherInfo.name == "Pitcher_1")
                    {
                        now_pitcherInfo = pitcherInfo;
                        now_pitcherInfo.IsActive();
                        Debug.Log("Pitcher_1マウントに立つ！！");
                        continue;
                    }
                }
                MountIn = true;
            }
        }

        if(MountIn)
        {
            if (now_pitcherInfo.MoundOut())
            {
                MountIn = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!MountIn)
        {
            foreach (PitcherInfo pitcherInfo in pitcherInfos)
            {
                if (other.transform.root.name == pitcherInfo.name)
                {
                    now_pitcherInfo = pitcherInfo;
                    now_pitcherInfo.IsActive();
                    continue;
                }
            }
            MountIn = true;
        }
    }
}
