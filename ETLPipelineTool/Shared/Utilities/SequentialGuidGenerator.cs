namespace ETLPipelineTool.Shared.Utilities
{
    public static class SequentialGuidGenerator
    {
        public static Guid NewGuid()
        {
            var guidArray = Guid.NewGuid().ToByteArray();

            var now = DateTime.UtcNow;
            var baseDate = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var days = BitConverter.GetBytes((short)(now - baseDate).Days);
            var msecs = BitConverter.GetBytes((int)(now.TimeOfDay.TotalMilliseconds / 3.333333));

            Array.Reverse(days);
            Array.Reverse(msecs);

            Array.Copy(days, 0, guidArray, 0, 2);
            Array.Copy(msecs, 0, guidArray, 2, 4);

            return new Guid(guidArray);
        }
    }
}
