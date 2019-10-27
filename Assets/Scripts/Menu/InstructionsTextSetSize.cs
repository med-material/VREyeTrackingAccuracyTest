//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 10/7/2019
//
using TMPro;
using UnityEngine;

public class InstructionsTextSetSize : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI original_size, updated_size;

    void Update()
    {
        if (!original_size) return;

        updated_size.fontSize = original_size.fontSize;
        return;
    }
}
