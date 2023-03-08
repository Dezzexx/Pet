namespace Client
{
    struct VibrationEvent
    {
        public enum VibrationType
        {
            HeavyImpact,
            LightImpack,
            MediumImpact,
            RigitImpact,
            Selection,
            SoftImpact,
            Success,
            Warning
        }
        public VibrationType Vibration;
    }
}