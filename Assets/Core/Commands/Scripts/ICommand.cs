namespace GameSystemsCookbook
{
    /// <summary>
    /// This interface defines the minimum functionality for creating the command design pattern
    /// (for ScriptableObjects, MonoBehaviours, or general System.Objects).
    ///
    /// In the concrete class implementating the functionality, the Undo method contains the logic
    /// to restore the object's state after running the Execute method.
    /// </summary>
    public interface ICommand
    {
        public abstract void Execute();
        public abstract void Undo();
    }
}