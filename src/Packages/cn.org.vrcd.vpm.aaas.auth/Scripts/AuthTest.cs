using AAAS.Auth.Manager;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace AAAS.Auth {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AuthTest : UdonSharpBehaviour {
        public AuthorizationManager AuthorizationManager;

        public string username = "local";
        public int requiredPermissionId;

        public void GetUserRoles() {
            var requestUsername = username;
            if (username == "local")
                requestUsername = Networking.LocalPlayer.displayName;
            
            var roles = AuthorizationManager._GetUserRoles(requestUsername);
            
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < roles.Length; i++) {
                sb.Append(roles[i]);
                if (i < roles.Length - 1) sb.Append(", ");
            }
            
            var rolesStr = sb.ToString();
            
            Debug.Log($"[AuthTest] User '{requestUsername}' has roles: {rolesStr}");
        }
        
        public void IsUserAuthorized() {
            var requestUsername = username;
            if (username == "local")
                requestUsername = Networking.LocalPlayer.displayName;
            
            var isAuthorized = AuthorizationManager._IsUserAuthorized(requestUsername, requiredPermissionId);
            Debug.Log($"[AuthTest] Is user '{requestUsername}' authorized for permission ID {requiredPermissionId}: {isAuthorized}");
        }
    }
}