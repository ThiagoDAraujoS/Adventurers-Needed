using System.Collections;

public class HellIterator
{
    public int value = 0;

    public ICollection colection;

    public HellIterator(ICollection colection = null)
    {
        this.colection = colection;
    }

    public void Reset()
    {
        value = 0;
    }

    public bool Next()
    {
        return ++value < colection.Count;
    }

    public bool Loop()
    {
        bool result = Next();
        if (!result)
            Reset();
        return result;
    }
}
