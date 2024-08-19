using System;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FeedbackHolder : MonoBehaviour
{
    public MMFeedbacks BigScreenShake { get; private set; }
    public MMFeedbacks MediumScreenShake { get; private set; }
    public MMFeedbacks SmallScreenShake { get; private set; }
    public MMFeedbacks MassiveScreenShake { get; private set; }
    public MMFeedbacks CustomerIn { get; private set; }
    public MMFeedbacks CustomerOut { get; private set; }


    [SerializeField] private MMFeedbacks _biScreenShake;
    [SerializeField] private MMFeedbacks _mediumScreenShake;
    [SerializeField] private MMFeedbacks _smallScreenShake;
    [SerializeField] private MMFeedbacks _massiveScreenShake;
    [SerializeField] private MMFeedbacks _customerIn;
    [SerializeField] private MMFeedbacks _customerOut;

    private void Awake() {
        BigScreenShake = _biScreenShake;
        MediumScreenShake = _mediumScreenShake;
        SmallScreenShake = _smallScreenShake;
        MassiveScreenShake = _massiveScreenShake;
        CustomerIn = _customerIn;
        CustomerOut = _customerOut;
    }
}
