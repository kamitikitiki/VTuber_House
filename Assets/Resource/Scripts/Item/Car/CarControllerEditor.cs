using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SeatInfo))]
public class CarControllerEditor : Editor
{
    private SeatInfo _target;
    private SerializedProperty _DriverState;
    private SerializedProperty _SteeringWheel;

    private void OnEnable()
    {
        _DriverState = serializedObject.FindProperty("driver");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //入力されたステータスを設定
        _target.driver = (DriverState)EditorGUILayout.EnumPopup("driver", _target.driver);

        //if(seat.driver == DriverState.driver)
        //{
        //    EditorGUILayout.LabelField("SteeringWheel_Left", seat.SteeringWheel.SteeringWheel_Left.ToString());
        //    EditorGUILayout.LabelField("SteeringWheel_Right", seat.SteeringWheel.SteeringWheel_Right.ToString());
        //}
    }
}
