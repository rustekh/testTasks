internal class SpanOfOnes
{
    private int _startIndex = -1;
    public int StartIndex
    {
        get => _startIndex;
        set
        {
            _startIndex = value;
            IsStarted = true;
        }
    }
    public int EndIndex { get; set; }
    public bool IsStarted { get; private set; }
    public AreaOfOnes? Area { get; set; }
}