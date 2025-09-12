using JetBrains.Annotations;
using UdonSharp;
using UnityEngine;

namespace AAAS.Auth.Manager {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RoleManager : UdonSharpBehaviour {
        [SerializeField] internal string[] roleNames;
        [SerializeField] internal int[] roleFlags;
        [SerializeField] internal int[] roleAuthorizationFlags;

        [SerializeField] internal string[] permissionNames;
        [SerializeField] internal int[] permissionFlags;
        
        [PublicAPI]
        public int[] GetRoleFlags() {
            return roleFlags;
        }
        
        [PublicAPI]
        public bool IsRoleHasPermission(int roleFlag, int permissionFlag) {
            for (var i = 0; i < roleFlags.Length; i++) {
                if (roleFlags[i] != roleFlag) continue;
                
                return (roleAuthorizationFlags[i] & permissionFlag) == permissionFlag;
            }

            return false;
        }
    }
}