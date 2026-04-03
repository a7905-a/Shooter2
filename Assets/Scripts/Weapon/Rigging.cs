using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ProjectTwo.Weapon
{
    public class Rigging : MonoBehaviour
    {
        [SerializeField] TwoBoneIKConstraint rightHandIK;
        [SerializeField] TwoBoneIKConstraint leftHandIK;
        RigBuilder rigBuilder;

        void Awake()
        {
            rigBuilder = GetComponent<RigBuilder>();
        }
        public void SetWeaponIKTargets(WeaponIKTarget newtargets)
        {
            if (leftHandIK != null) leftHandIK.weight = 0f;
            if (rightHandIK != null) rightHandIK.weight = 0f;

            if(newtargets != null)
            {
                if (leftHandIK != null && newtargets.leftHandTarget != null)
                {
                    leftHandIK.data.target = newtargets.leftHandTarget;
                    leftHandIK.weight = 1f; 
                }

                if (rightHandIK != null && newtargets.rightHandTarget != null)
                {
                    rightHandIK.data.target = newtargets.rightHandTarget;
                    rightHandIK.weight = 1f;
                }
            }

            if (rigBuilder != null)
            {
                rigBuilder.Build();
            }
        }

    }
}
