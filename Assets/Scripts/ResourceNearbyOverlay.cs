using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour {
    private ResourceGeneratorData resourceGeneratorData;

    [SerializeField] private Transform parent;

    private void Update() {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData,parent.position);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxEffectiveResourceNodes*100);
        transform.Find( "text" ).GetComponent<TextMeshPro>().SetText( percent + "%" );
    }
    public void Show( ResourceGeneratorData resourceGeneratorData ) {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive( true );

        transform.Find( "icon" ).GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;


    }

    public void Hide() {
        gameObject.SetActive( false );
    }
}
