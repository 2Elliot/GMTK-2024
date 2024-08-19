using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FeedbackHolder : MonoBehaviour
{
    public MMFeedbacks BigScreenShake { get; private set; }
    public MMFeedbacks MediumScreenShake { get; private set; }
    public MMFeedbacks SmallScreenShake { get; private set; }
    public MMFeedbacks MassiveScreenShake { get; private set; }


    [SerializeField] private MMFeedbacks _biScreenShake;
    [SerializeField] private MMFeedbacks _mediumScreenShake;
    [SerializeField] private MMFeedbacks _smallScreenShake;
    [SerializeField] private MMFeedbacks _massiveScreenShake;

    private void Awake() {
        BigScreenShake = _biScreenShake;
        MediumScreenShake = _mediumScreenShake;
        SmallScreenShake = _smallScreenShake;
        MassiveScreenShake = _massiveScreenShake;
    }
}
