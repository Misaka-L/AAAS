using System;
using UdonSharp;
using VRC.SDKBase;
using Xuan25.PlayerVoiceSystem;

namespace AAAS.Xuan25.PlayerVoiceSystem.Ext.OneWayVoiceRoom {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class OneWayPlayerVoiceRoomController : PlayerVoiceScaler {
        public bool scalerOverrideIfIsolated = false;
        public float gainScaler = 1.0f;
        public float distanceNearScaler = 1.0f;
        public float distanceFarScaler = 0.01f;
        public float volumetricRadiusScaler = 1.0f;
        public bool lowpassDisable = false;

        public OneWayPlayerVoiceRoom[] playerVoiceRooms = new OneWayPlayerVoiceRoom[sizeof(int) * 8];

        [NonSerialized]
        public readonly int[] playerVoiceRoomMask = new int[80];

        private VRCPlayerApi _localPlayer;

        private void Start() {
            _localPlayer = Networking.LocalPlayer;

            for (var i = 0; i < playerVoiceRoomMask.Length; i++) {
                playerVoiceRoomMask[i] = 0;
            }

            for (var i = 0; i < playerVoiceRooms.Length; i++) {
                if (playerVoiceRooms[i] != null) {
                    playerVoiceRooms[i].Setup(this, i);
                }
            }
        }

        public void OnPlayerRoomEnter(VRCPlayerApi player, int roomId) {
            playerVoiceRoomMask[player.playerId] |= 1 << roomId;
            OnPlayerRoomChanged(player);
        }

        public void OnPlayerRoomLeave(VRCPlayerApi player, int roomId) {
            playerVoiceRoomMask[player.playerId] &= ~(1 << roomId);
            OnPlayerRoomChanged(player);
        }

        private void OnPlayerRoomChanged(VRCPlayerApi player) {
            if (!Utilities.IsValid(player)) return;

            if (player.playerId == _localPlayer.playerId) {
                playerVoiceController.UpdateAllPlayerVoice();
                return;
            }

            playerVoiceController.UpdatePlayerVoice(player);
        }

        private void ResetPlayerRoomMask(VRCPlayerApi player) {
            if (!Utilities.IsValid(player)) return;
            playerVoiceRoomMask[player.playerId] = 0;
        }

        public override void OnPlayerJoined(VRCPlayerApi player) {
            ResetPlayerRoomMask(player);
        }

        public override void OnPlayerLeft(VRCPlayerApi player) {
            ResetPlayerRoomMask(player);
        }

        public override void GetPlayerVoiceScaler(VRCPlayerApi player, out float gainScaler,
            out float distanceNearScaler,
            out float distanceFarScaler, out float volumetricRadiusScaler, out bool lowpassDisable,
            out bool scalerOverride) {
            var localPlayerRoomMask = playerVoiceRoomMask[_localPlayer.playerId];
            var targetPlayerId = player.playerId;

            if ((playerVoiceRoomMask[targetPlayerId] & localPlayerRoomMask) != 0 ||
                playerVoiceRoomMask[targetPlayerId] == 0) {
                // Player is in the same room as the local player or remote player not in any room
                gainScaler = 1.0f;
                distanceNearScaler = 1.0f;
                distanceFarScaler = 1.0f;
                volumetricRadiusScaler = 1.0f;
                lowpassDisable = false;
                scalerOverride = false;
                return;
            }

            // Player is not in the same room as the local player
            gainScaler = this.gainScaler;
            distanceNearScaler = this.distanceNearScaler;
            distanceFarScaler = this.distanceFarScaler;
            volumetricRadiusScaler = this.volumetricRadiusScaler;
            lowpassDisable = this.lowpassDisable;
            scalerOverride = this.scalerOverrideIfIsolated;
        }
    }
}