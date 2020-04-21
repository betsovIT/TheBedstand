namespace TheBedstand.Common.Helpers
{
    using System;

    public static class TimeDifferenceHelper
    {
        public static string GetTimeDifference(DateTime datePosted)
        {
            var difference = DateTime.UtcNow - datePosted;

            if (difference < TimeSpan.FromHours(1))
            {
                return $"{difference.Minutes.ToString()} minutes ago";
            }
            else if (difference < TimeSpan.FromDays(1))
            {
                return $"{difference.Hours.ToString()} hours ago";
            }
            else if (difference < TimeSpan.FromDays(30))
            {
                return $"{difference.Days.ToString()} days ago";
            }
            else if (difference < TimeSpan.FromDays(365))
            {
                return $"{(difference.Days / 30).ToString()} months ago";
            }
            else
            {
                return $"{(difference.Days / 365).ToString()} years ago";
            }
        }
    }
}
