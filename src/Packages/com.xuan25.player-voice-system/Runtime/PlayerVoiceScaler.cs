using System;
using UdonSharp;
using VRC.SDKBase;

namespace Xuan25.PlayerVoiceSystem {
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class PlayerVoiceScaler : UdonSharpBehaviour {
        [NonSerialized] public PlayerVoiceController playerVoiceController;

        public virtual void Setup(PlayerVoiceController controller) {
            playerVoiceController = controller;
        }

        public abstract void GetPlayerVoiceScaler(VRCPlayerApi player, out float gainScaler,
            out float distanceNearScaler, out float distanceFarScaler, out float volumetricRadiusScaler,
            out bool lowpassDisable, out bool scalerOverride);
    }
}