using UnityEngine;

namespace PlanetGearScheme.Core.Dictionares {
    public static class DetailAnimatorConstants {
        public static readonly int SplitAnimationKey = Animator.StringToHash("is-split");
    }

    public static class StateDrivenCameraAnimatorConstants {
        public const string MainViewState = "main-view";
        public const string ReviewState = "review";
        public const string PartViewState = "part-view";
    }
}
