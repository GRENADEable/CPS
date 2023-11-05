using UnityEngine;

[CreateAssetMenu(fileName = "GameManager_Data", menuName = "Managers/Game Manager Data")]
public class GameManagerData : ScriptableObject
{
    public EssaySceneVerion currEssayVersion = EssaySceneVerion.None;
    public enum EssaySceneVerion { Version1, Version2, None };
}