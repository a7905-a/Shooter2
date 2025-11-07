using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Rigging : MonoBehaviour
{
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] TwoBoneIKConstraint rightHandIK;
    [SerializeField] TwoBoneIKConstraint leftHandIK;

    public void SetWeaponIKTargets(GameObject newSwitchWeapon)
    {
        WeaponIKTarget newtargets = newSwitchWeapon.GetComponent<WeaponIKTarget>();

        if (leftHandIK != null)
        {
            leftHandIK.data.target = newtargets.leftHandTarget;
        }

        if (rightHandIK != null)
        {
            rightHandIK.data.target = newtargets.rightHandTarget;
        }
        
        if (rigBuilder != null)
        {
            rigBuilder.Build();
        }
    }

}
