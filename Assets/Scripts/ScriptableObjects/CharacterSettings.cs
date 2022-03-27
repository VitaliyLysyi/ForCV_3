using UnityEngine;

[CreateAssetMenu(menuName = "CharacterSettings")]
public class CharacterSettings : ScriptableObject
{
    public float deadDistance = 0.01f;
    public float moveSpeed = 2f;

    public float massToScaleMultiplier = 1000f;
    public float massMinRange = 0.5f;
    public float massMaxRange = 1.5f;
}