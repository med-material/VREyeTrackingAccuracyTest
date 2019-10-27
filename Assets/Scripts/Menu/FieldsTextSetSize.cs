//
// Copyright: Aalborg University
// Author: YoNeXia <yohann.neraud@viacesi.fr> (https://gitlab.com/YoNeXia)
// Git: https://github.com/med-material/VREyeTrackingAccuracyTest
//
// Date: 10/26/2019
//
using UnityEngine;
using UnityEngine.UI;

public class FieldsTextSetSize : MonoBehaviour {

    [SerializeField]
    private Text original_size, updated_size_info, updated_size_edit;

    void Update()
    {
        if (!original_size) return;

        updated_size_info.fontSize = updated_size_edit.fontSize = original_size.cachedTextGenerator.fontSizeUsedForBestFit;
        return;
    }
}
