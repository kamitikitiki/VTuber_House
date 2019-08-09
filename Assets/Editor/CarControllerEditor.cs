using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CarController))]
public class CarControllerEditor : Editor
{
    private CarController _target = null;
    //private SeatInfo[] seatInfos;
    //private SerializedProperty _DriverState;
    private SerializedProperty _SteeringWheel = null;

    private void OnEnable()
    {
        _target = target as CarController;
        _SteeringWheel = serializedObject.FindProperty("SteeringWheel");
    }

    public override void OnInspectorGUI()
    {
        _target = target as CarController;
        _SteeringWheel = serializedObject.FindProperty("SteeringWheel");

        base.OnInspectorGUI();

        serializedObject.Update();

        //EditorGUILayout.HelpBox("hello",MessageType.Info,true);

        //seatInfos = new SeatInfo[_target.seatInfos.Count];

        foreach(SeatInfo seatInfo in _target.seatInfos)
        {
            if(seatInfo.seatState == SeatState.driver)
            {
                //EditorGUILayout.PropertyField(_SteeringWheel);
                //EditorGUILayout.HelpBox("hello", MessageType.Info, true);
            }
        }

        /*
        for (int i = 0; i < _target.seatInfos.Count; i++)
        {
            
            if(seatInfos[i].driver == DriverState.driver)
            {
                //EditorGUILayout.LabelField("SteeringWheel"seatInfos[i].SteeringWheel);
                //EditorGUILayout.ObjectField("SteeringWheel", seatInfos[i].SteeringWheel);
                EditorGUILayout.PropertyField(_SteeringWheel);
                EditorGUILayout.HelpBox("hello", MessageType.Info, true);
            }
        }
        */
        
        //入力されたステータスを設定
        
        //sw.driver = (DriverState)EditorGUILayout.EnumPopup("driver", sw.driver);
        
        /*
        if(sw.driver == DriverState.driver)
        {
            EditorGUILayout.PropertyField(_SteeringWheel);
        }
        */

        serializedObject.ApplyModifiedProperties();
        
    }
}
#endif
