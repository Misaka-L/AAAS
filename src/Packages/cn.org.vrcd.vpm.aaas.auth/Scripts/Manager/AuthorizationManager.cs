using AAAS.Auth.Tools;
using JetBrains.Annotations;
using UdonSharp;
using UnityEngine;

namespace AAAS.Auth.Manager {
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public sealed class AuthorizationManager : UdonSharpBehaviour {
        public RoleManager roleManager;
        
        [SerializeField] internal string[] authorizedUserIds;
        [SerializeField] internal int[] userRoleFlags;
        
        [PublicAPI]
        public bool _IsUserAuthorized(string userId, int requiredPermissionId) {
            var userRoles = _GetUserRoles(userId);
            var roleFlags = roleManager.GetRoleFlags();

            if (userRoles.Length == 0) {
                return false;
            }

            foreach (var roleFlag in roleFlags) {
                return roleManager.IsRoleHasPermission(roleFlag, requiredPermissionId);
            }

            return false;
        }

        [PublicAPI]
        public int[] _GetUserRoles(string userId) {
            for (var i = 0; i < authorizedUserIds.Length; i++) {
                if (authorizedUserIds[i] != userId) continue;
                
                var roleFlags = roleManager.GetRoleFlags();
                var userRoleFlag = userRoleFlags[i];
                var userRoles = new int[0];
                
                foreach (var roleFlag in roleFlags) {
                    if ((roleFlag & userRoleFlag) != roleFlag) continue;
                    
                    userRoles = ArrayTools.Add(userRoles, roleFlag);
                }

                return userRoles;
            }

            return new int[0];
        }
    }
}