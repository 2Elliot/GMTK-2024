using UnityEngine;
using MoreMountains.Tools;

public class SFXController : MonoBehaviour {
  [SerializeField] private AudioClip _audioClip;

  [SerializeField] private AudioClip _happy;
  [SerializeField] private AudioClip _sad;

  public void PlaySound() {
    MMSoundManagerSoundPlayEvent.Trigger(_audioClip, MMSoundManager.MMSoundManagerTracks.Sfx,
      this.transform.position, soloSingleTrack: this);
  }

  public void PlayHappy() {
    MMSoundManagerSoundPlayEvent.Trigger(_happy, MMSoundManager.MMSoundManagerTracks.Sfx,
      this.transform.position, soloSingleTrack: this);
  }
  
  public void PlaySad() {
    MMSoundManagerSoundPlayEvent.Trigger(_sad, MMSoundManager.MMSoundManagerTracks.Sfx,
      this.transform.position, soloSingleTrack: this);
  }
}