namespace Client {
    struct AnimationSwitchEvent {
        public enum AnimationType
        {
            Idle, Run, Harvest
        }
        public AnimationType AnimationSwitcher;
    }
}