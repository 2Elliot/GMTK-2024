using System.Collections;
using UnityEngine;
using MoreMountains.Tools;

public class SoundVolumeSlider : MonoBehaviour
{
    private void Start() {
        GetComponent<MMSoundManagerTrackVolumeSlider>().Mode = MMSoundManagerTrackVolumeSlider.Modes.Read;

        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();

        GetComponent<MMSoundManagerTrackVolumeSlider>().Mode = MMSoundManagerTrackVolumeSlider.Modes.Write;
    }
}
