using UnityEngine;
using MoreMountains.Tools;

public class SFXController : MonoBehaviour {
  [SerializeField] private AudioClip _audioClip;

  public void PlaySound() {
    MMSoundManagerSoundPlayEvent.Trigger(_audioClip, MMSoundManager.MMSoundManagerTracks.Sfx,
      this.transform.position);
  }
}