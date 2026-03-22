using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Rigging : MonoBehaviour
{
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] TwoBoneIKConstraint rightHandIK;
    [SerializeField] TwoBoneIKConstraint leftHandIK;

    public void SetWeaponIKTargets(GameObject newSwitchWeapon)
    {
        if (leftHandIK != null) leftHandIK.weight = 0f;
        if (rightHandIK != null) rightHandIK.weight = 0f;

        WeaponIKTarget newtargets = newSwitchWeapon.GetComponent<WeaponIKTarget>();

        // 💡 [안전장치 2] 새 무기(newSwitchWeapon)는 이 함수가 실행되기 전에 반드시 SetActive(true) 상태여야 합니다!

        if (leftHandIK != null && newtargets.leftHandTarget != null)
        {
            leftHandIK.data.target = newtargets.leftHandTarget;
            leftHandIK.weight = 1f; // 타겟을 찾았으니 다시 힘을 줍니다.
        }

        if (rightHandIK != null && newtargets.rightHandTarget != null)
        {
            rightHandIK.data.target = newtargets.rightHandTarget;
            rightHandIK.weight = 1f; // 타겟을 찾았으니 다시 힘을 줍니다.
        }
        
        if (rigBuilder != null)
        {
            rigBuilder.Build();
        }
    }

}
