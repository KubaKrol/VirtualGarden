/// <summary>
/// Every class that implements IAmSaved interface has to define their own ISnapshotData struct with all of the fields that needs to be snapped. 
/// </summary>
public interface ISnapshot
{
    /// <summary>
    /// Create new ISnapshotData and initialize its values.
    /// </summary>
    /// <returns></returns>
    SnapshotData SaveMe();
    
    /// <summary>
    /// Overwrite class values with values from ISnapshotData struct.
    /// </summary>
    /// <param name="snapshotData"></param>
    void LoadMe(SnapshotData snapshotData);
    
    /// <summary>
    /// UniqueID provided by UniqueID component
    /// </summary>
    UniqueId MyUniqueID { get; }
}
