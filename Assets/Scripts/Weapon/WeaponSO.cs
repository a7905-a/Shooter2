using UnityEngine;

public enum WeaponType
{
    Pistol,
    Rifle
}

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Object/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public WeaponType WeaponType;
    public GameObject WeaponPrefab;
    public ParticleSystem HitEffect;
    public bool IsAutomatic;
    public float Damege = 1.0f;
    public float FireRate = 0.5f;
    public int MaxAmmo = 30;
}
