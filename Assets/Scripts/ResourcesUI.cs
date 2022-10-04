using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO,Transform> resourceTypeTransformDictionary;


    private void Awake() {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive( false );

        int i = 0;
        foreach ( ResourceTypeSO resourceType in resourceTypeList.list ) {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive( true );

            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2( offsetAmount * i,0);

            resourceTransform.Find( "image" ).GetComponent<Image>().sprite = resourceType.sprite;


            resourceTypeTransformDictionary[resourceType] = resourceTransform;

            i++;

        }
    }

    private void Start() {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }
    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e) {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount() {
        foreach ( ResourceTypeSO resourceType in resourceTypeTransformDictionary.Keys ) {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            int resourceAmount = (int)( ResourceManager.Instance?.GetResourceAmount(resourceType) );
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText( resourceAmount.ToString() );
        }

    }
}
