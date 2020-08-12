public abstract class UpdaterClass
{
    public int ticksBetweenUpdates = 5;
    public int ticksSinceLastUpdate = 0;

    protected UpdaterClass()
    {
    }

    public abstract void Update();
}

