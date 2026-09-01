public interface IPushable
{
    int WeightLevel { get; }
    void Push(float velocityX);
}