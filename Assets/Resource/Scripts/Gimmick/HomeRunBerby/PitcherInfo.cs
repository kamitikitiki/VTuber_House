using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PitcherInfo : MonoBehaviour
{
    //球種
    [SerializeField]
    public PitchingLine[] pitchingTypes = new PitchingLine[9];
    public bool SampleKey_Flag = false;

    private bool active;
    private bool pitchingStart;
    private int pt_no;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        pitchingStart = false;
        pt_no = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if (!pitchingStart)
            {
                if (SampleKey_Flag)
                {
                    //サンプルキー
                    SampleKey();
                }

                //投球開始
                if (pitchingStart)
                {
                    if(pitchingTypes[pt_no] != null)
                    {
                        pitchingTypes[pt_no].PitcheingStart();
                        Debug.Log(pitchingTypes[pt_no].name + "：スタート");
                    }
                    else
                    {
                        pitchingStart = false;
                    }
                }
            }
            else
            {
                if(pitchingTypes[pt_no].PitchingHit())
                {
                    pitchingStart = false;
                }
                //投球終了
                else if (pitchingTypes[pt_no].PitchingEnd())
                {
                    pitchingStart = false;
                }
            }
        }
    }

    public void IsActive()
    {
        active = true;
    }

    public bool MoundOut()
    {
        if (!active) return true;
        return false;
    } 

    //テストキー
    private void SampleKey()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //1キー
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                pitchingStart = true;
                pt_no = 0;
            }
            //2キー
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                pitchingStart = true;
                pt_no = 1;
            }
            //3キー
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                pitchingStart = true;
                pt_no = 2;
            }
            //4キー
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                pitchingStart = true;
                pt_no = 3;
            }
            //5キー
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                pitchingStart = true;
                pt_no = 4;
            }
            //6キー
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                pitchingStart = true;
                pt_no = 5;
            }
            //7キー
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                pitchingStart = true;
                pt_no = 6;
            }
            //8キー
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                pitchingStart = true;
                pt_no = 7;
            }
            //9キー
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                pitchingStart = true;
                pt_no = 8;
            }
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            active = false;
            Debug.Log("マウントアウト");
        }
    }
}
