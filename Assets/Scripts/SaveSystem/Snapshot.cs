using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// There can be more than 1 snapshot per object, but it's recommended to keep the number of snapshots as low as possible.
/// Snapshots are used only as a data storage for single object instance, should never be used as a global data.
/// Check SaveSystem.SaveRecord and SaveSystem.GetRecord for a global data serialization.
///
/// Snapshots use UniqueID in order to pair the data with it's owner via Globally Unique ID.
///
/// Base Snapshot component is a generic snapshot that contains most of the default logic that has to be snapshoted - like activity and transform.
/// </summary>
[RequireComponent(typeof(UniqueId))]
public class Snapshot : MonoBehaviour, ISnapshot
{
#region Inspector Variables

    [InfoBox("The main controller script on the object already handles snapshotting - contact programmers in order to check if snapshot component is needed on this object", InfoMessageType.Warning)]
    [ShowIf("snapshotWarning")]
    [ReadOnly] [ShowInInspector] private bool snapshotWarning;

#endregion Inspector Variables


#region Unity Methods

    private void OnValidate()
    {
        Validate();
    }

#endregion UnityMethods


#region Public Variables

    public UniqueId MyUniqueID
    {
        get
        {
            if (myUniqueId == null)
                myUniqueId = GetComponent<UniqueId>();

            return myUniqueId;
        }
    }

#endregion Public Variables


#region Public Methods
    
    public virtual SnapshotData SaveMe()
    {
        var snapshotData = new GenericSnapshotData();
        snapshotData.enabled = gameObject.activeSelf;
        var myTransform = transform;
        snapshotData.position = myTransform.position;
        snapshotData.rotation = myTransform.rotation;
        snapshotData.scale = myTransform.localScale;
        return snapshotData;
    }

    public virtual void LoadMe(SnapshotData snapshotData)
    {
        if (snapshotData is GenericSnapshotData genericSnapshotData)
        {
            gameObject.SetActive(genericSnapshotData.enabled);  
            var myTransform = transform;
            myTransform.position = genericSnapshotData.position;
            myTransform.rotation = genericSnapshotData.rotation;
            myTransform.localScale = genericSnapshotData.scale;
        }
    }

#endregion Public Methods


#region Private Variables

    private UniqueId myUniqueId;
    
#endregion Private Variables


#region Private Methods

    private void Validate()
    {
        var allSnapshotsOnTheObject = GetComponents<ISnapshot>();

        foreach (var snapshot in allSnapshotsOnTheObject)
        {
            if (snapshot.GetType().BaseType != typeof(Snapshot))
            {
                snapshotWarning = true;
                return;
            }
        }
        
        snapshotWarning = false;
    }

#endregion Private Methods


#region Coroutines

    //IEnumerators<>


#endregion Coroutines


#region Public Types


    public class GenericSnapshotData : SnapshotData
    {
        public bool enabled;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
    
#endregion Public Types
}
