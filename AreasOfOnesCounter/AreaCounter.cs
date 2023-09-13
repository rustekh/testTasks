internal class AreaCounter
{
    private List<AreaOfOnes> _areas = new List<AreaOfOnes>();
    private List<SpanOfOnes> _previousRowSpans = new List<SpanOfOnes>();
    private SpanOfOnes _currentSpan = new SpanOfOnes();
    private int _previousRowOverlappingSpanIndex = -1;
    private int _columnIndex = 0;

    public int Count(Stream stream)
    {
        using (var streamReader = new StreamReader(stream))
        {
            char nextChar;
            while (!streamReader.EndOfStream)
            {
                nextChar = (char)streamReader.Read();
                switch (nextChar)
                {
                    case ',':
                        continue;
                    case ';':
                        TryFinishSpan();

                        _columnIndex = 0;

                        _areas.ForEach(a => a.SwitchRows());
                        _previousRowSpans = _areas.SelectMany(a => a.PreviousRowSpans).OrderBy(s => s.StartIndex).ToList();
                        _previousRowOverlappingSpanIndex = _previousRowSpans.Any() ? 0 : -1;
                        continue;
                    case '1':
                        if (!_currentSpan.IsStarted)
                        {
                            _currentSpan.StartIndex = _columnIndex;
                        }

                        if (_previousRowOverlappingSpanIndex != -1)
                        {
                            var spanToCheck = _previousRowSpans[_previousRowOverlappingSpanIndex];
                            if (!IsSpanInArea(spanToCheck) && spanToCheck.EndIndex < _columnIndex)
                            {
                                _previousRowOverlappingSpanIndex++;
                                if (_previousRowOverlappingSpanIndex >= _previousRowSpans.Count)
                                {
                                    _previousRowOverlappingSpanIndex = -1;
                                }
                                else
                                {
                                    spanToCheck = _previousRowSpans[_previousRowOverlappingSpanIndex];
                                    IsSpanInArea(spanToCheck);
                                }
                            }
                        }
                        break;
                    case '0':
                        TryFinishSpan();
                        break;
                    default:
                        continue;
                }

                _columnIndex++;
            }

            TryFinishSpan();
        }

        return _areas.Count;
    }

    private bool IsSpanInArea(SpanOfOnes spanToCheck)
    {
        if (spanToCheck.StartIndex <= _columnIndex && _columnIndex <= spanToCheck.EndIndex)
        {
            if (_currentSpan.Area == null)
            {
                spanToCheck.Area!.AddSpan(_currentSpan);
            }
            else if (_currentSpan.Area != spanToCheck.Area)
            {
                _areas.Remove(spanToCheck.Area!);
                _currentSpan.Area.Merge(spanToCheck.Area!);
            }
            return true;
        }

        return false;
    }

    private void TryFinishSpan()
    {
        if (_currentSpan.IsStarted)
        {
            _currentSpan.EndIndex = _columnIndex - 1;

            if (_currentSpan.Area == null)
            {
                var newArea = new AreaOfOnes();
                newArea.CurrentRowSpans.Add(_currentSpan);
                _currentSpan.Area = newArea;
                _areas.Add(newArea);
            }

            _currentSpan = new SpanOfOnes();
        }
    }
}