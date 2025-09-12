using AAAS.Auth.Manager;
using UdonSharpEditor;
using UnityEditor;

namespace AAAS.Auth.Editor {
    [CustomEditor(typeof(RoleManager))]
    public class RoleManagerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            if (UdonSharpGUI.DrawDefaultUdonSharpBehaviourHeader(target)) return;
            
            base.OnInspectorGUI();
        }
    }
}