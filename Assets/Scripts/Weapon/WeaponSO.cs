using UnityEngine;

public enum WeaponType
{
    //다른 종류의 총기 추가 가능
    //Pistol,
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
