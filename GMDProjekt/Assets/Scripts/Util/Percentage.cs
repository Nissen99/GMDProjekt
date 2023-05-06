namespace Util
{
    public class Percentage
    {
        public static float GetPercentage(float current, float max)
        {
            var currentPercentage = (float)current / max;
            if (currentPercentage < 0)
            {
                currentPercentage = 0;
            } else if (currentPercentage > 1)
            {
                currentPercentage = 1;
            }
            return currentPercentage;
        }
    }
}