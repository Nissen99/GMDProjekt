namespace DefaultNamespace
{
    public interface IPlayerMovement: IMovement
    {
        public void IncreaseMovementSpeed(float multiplier);
        public void DecreaseMovementSpeed(float multiplier);
    }
}