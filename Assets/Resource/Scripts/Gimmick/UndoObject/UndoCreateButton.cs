using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoCreateButton : MonoBehaviour
{
    public Transform MainMachine;
    public string CreateObjectName;

    private UndoMachineManager MachineManager;

    private void Start()
    {
        MachineManager = MainMachine.GetComponent<UndoMachineManager>();
    }

    //プレイヤーが触れたときにCreateObjectNameのPrefabsを生成する
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MachineManager.CreateUndoObject(CreateObjectName);
        }
    }
}
