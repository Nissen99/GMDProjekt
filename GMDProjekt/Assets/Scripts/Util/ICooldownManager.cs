namespace Util
{
    public interface ICooldownManager
    {
        bool IsReady();
        void Use();
        float GetCooldown();
        float GetRemainingCooldown();
    }
}