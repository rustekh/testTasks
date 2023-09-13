internal class AreaOfOnes
{
    public List<SpanOfOnes> PreviousRowSpans { get; set; } = new List<SpanOfOnes>();

    public List<SpanOfOnes> CurrentRowSpans { get; set; } = new List<SpanOfOnes>();

    public void SwitchRows()
    {
        var temp = PreviousRowSpans;
        PreviousRowSpans = CurrentRowSpans;
        CurrentRowSpans = temp;
        CurrentRowSpans.Clear();
    }

    public void Merge(AreaOfOnes anotherArea)
    {
        PreviousRowSpans.AddRange(anotherArea.PreviousRowSpans);
        CurrentRowSpans.AddRange(anotherArea.CurrentRowSpans);
        anotherArea.PreviousRowSpans.ForEach(s => s.Area = this);
        anotherArea.CurrentRowSpans.ForEach(s => s.Area = this);
    }

    public void AddSpan(SpanOfOnes span)
    {
        span.Area = this;
        this.CurrentRowSpans.Add(span);
    }
}