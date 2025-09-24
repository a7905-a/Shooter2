using UnityEditor.Animations.Rigging;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Object/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public ParticleSystem HitEffect;
    public float Damege = 1.0f;
    public float FireRate = 0.5f;
    public int MaxAmmo = 30;
}
