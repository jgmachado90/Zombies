using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponInfo", order = 1)]
public class WeaponInfo : ScriptableObject
{
    public string prefabName;
    public GameObject weaponPrefab;
    public Sprite weaponSprite;

    public float shootSpeed;
    public float shootParticleSpeed;
    public int damage;

    public ParticleSystem shootParticle;
    public ParticleSystem muzzleParticle;
    public ParticleSystem feedbackParticle;

    public GameObject defaultBullet;

}