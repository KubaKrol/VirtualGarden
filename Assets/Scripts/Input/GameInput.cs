using UnityEngine;

[CreateAssetMenu(fileName = "GameInput", menuName = "Input/GameInput")]
public class GameInput : ScriptableObject
{
    [SerializeField] public EPlatform currentPlatform;

    public IGameInput CurrentGameInput
    {
        get
        {
            if (currentPlatform == EPlatform.PC)
            {
                if (currentGameInput == null)
                    currentGameInput = new PCInput();

                return currentGameInput;
            }

            if (currentPlatform == EPlatform.VR)
            {
                if (currentGameInput == null)
                    currentGameInput = new VRInput();

                return currentGameInput;
            }

            return null; 
        }
    }

    private IGameInput currentGameInput;
}
