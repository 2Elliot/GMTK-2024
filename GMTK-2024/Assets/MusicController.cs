using UnityEngine;
using MoreMountains.Tools;

public class MusicController : MonoBehaviour {
    [SerializeField] private AudioClip _musicClip;

    public void PlayMusic() {
        MMSoundManagerSoundPlayEvent.Trigger(_musicClip, MMSoundManager.MMSoundManagerTracks.Music,
            this.transform.position);
    }
}
