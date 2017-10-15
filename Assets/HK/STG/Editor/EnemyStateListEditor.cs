using HK.STG.DanmakuSystems;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace HK.STG.EnemyStateControllers
{
    [CustomEditor(typeof(StateList))]
    public sealed class EnemyStateListEditor : Editor
    {
        private StateList StateList;

        void OnEnable()
        {
            this.StateList = (StateList)this.target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
//            EditorGUI.BeginChangeCheck();
//            // StateListのStateを描画する
//            for (int i = 0; i < this.StateList.Get.Count; i++)
//            {
//                var enemyStates = this.StateList.Get[i];
//                EditorGUILayout.BeginVertical(GUI.skin.box);
//                GUILayout.Label(string.Format("EnemyState[{0}]", i));
//                enemyStates.NextStateIndex = EditorGUILayout.IntField("NextStateIndex", enemyStates.NextStateIndex);
//                enemyStates.CoolTime = EditorGUILayout.IntField("CoolTime", enemyStates.CoolTime);
//                
//                foreach (var state in enemyStates.States)
//                {
//                    this.DrawState(state);
//                }
//                // MuzzleLaunchがアタッチされていない場合のみ追加ボタンを表示する
//                if (enemyStates.States.FindIndex(e => e.Type == StateType.MuzzleLaunch) == -1)
//                {
//                    if (GUILayout.Button("Add MuzzleLaunch"))
//                    {
//                        enemyStates.States.Add(new MuzzleLaunch());
//                    }
//                }
//                EditorGUILayout.EndVertical();
//            }
//            
//            // StateListのパラメーター類を描画
//            if (GUILayout.Button("New State"))
//            {
//                this.StateList.Get.Add(new StateList.EnemyStates());
//            }
//            
//            if (EditorGUI.EndChangeCheck())
//            {
//                EditorUtility.SetDirty(this.target);
//                Debug.Log("?");
//            }
        }

        private void DrawState(EnemyState state)
        {
            switch (state.Type)
            {
                case StateType.MuzzleLaunch:
                    this.DrawStartMuzzle((MuzzleLaunch)state);
                    break;
                default:
                    Assert.IsTrue(false, "未対応の値です {0}", state.Type);
                    break;
            }
        }

        private void DrawStartMuzzle(MuzzleLaunch muzzleLaunch)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("MuzzleLaunch");
            foreach (var muzzleAttach in muzzleLaunch.MuzzleAttaches)
            {
                muzzleAttach.MuzzleIndex = EditorGUILayout.IntField("MuzzleIndex", muzzleAttach.MuzzleIndex);
                muzzleAttach.Parameter = (MuzzleParameter)EditorGUILayout.ObjectField("Parameter", muzzleAttach.Parameter, typeof(MuzzleParameter), false);
            }

            if (GUILayout.Button("Add MuzzleAttach"))
            {
                muzzleLaunch.MuzzleAttaches.Add(new MuzzleLaunch.MuzzleAttach());
            }
            EditorGUILayout.EndVertical();
        }
    }
}
