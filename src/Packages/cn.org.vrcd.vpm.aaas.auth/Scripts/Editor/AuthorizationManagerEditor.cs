using AAAS.Auth.Manager;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace AAAS.Auth.Editor {
    [CustomEditor(typeof(AuthorizationManager))]
    public class AuthorizationManagerEditor : UnityEditor.Editor {
        private SerializedProperty roleManagerProperty;
    
        void OnEnable()
        {
            roleManagerProperty = serializedObject.FindProperty(nameof(AuthorizationManager.roleManager));
        }
        
        public override void OnInspectorGUI() {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;

            EditorGUILayout.PropertyField(roleManagerProperty);
            
            base.OnInspectorGUI();
        }
    }
}