using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FeedbackHolder : MonoBehaviour
{
    public MMFeedbacks ScreenShake { get; private set; }

    [SerializeField] private MMFeedbacks _screenShake;

    private void Awake() {
        ScreenShake = _screenShake;
    }
}
