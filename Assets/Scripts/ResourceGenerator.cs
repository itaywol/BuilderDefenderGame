using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData,Vector3 position) {
        int nearbyResources=0;
        foreach ( Collider2D collider in Physics2D.OverlapCircleAll( position, resourceGeneratorData.resourceDetectionRadius ) ) {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
            if ( resourceNode != null ) {
                if ( resourceGeneratorData.resourceType == resourceNode.resourceType ) {
                    nearbyResources++;
                }
            }
        }
        nearbyResources = Mathf.Clamp( nearbyResources, 0, resourceGeneratorData.maxEffectiveResourceNodes );
        return nearbyResources;
    }
    
    private float timer, timerMax;
    private BuildingTypeSO buildingType;
    private int maxEffectiveResourceNodes;


    private void Awake() {
        buildingType = GetComponent<Building>().BuildingType;
        timerMax = buildingType.resourceGeneratorData.timerMax;
        maxEffectiveResourceNodes = buildingType.resourceGeneratorData.maxEffectiveResourceNodes;
    }

    private void Start() {
        int nearbyResources = GetNearbyResourceAmount( buildingType.resourceGeneratorData, transform.position );

        if ( nearbyResources == 0 ) {
            enabled = false;
        } else {
            timerMax = ( timerMax / 2f ) + timerMax * ( 1 - (float)nearbyResources / maxEffectiveResourceNodes );
        }
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if ( timer <= 0f ) {
            timer += timerMax;
            ResourceManager.Instance.AddResource( buildingType.resourceGeneratorData.resourceType, 1 );
        }

    }

    public ResourceGeneratorData getResourceGeneratorData() {
        return buildingType.resourceGeneratorData;
    }

    public float GetTimerNormalized() {
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond() {
        return 1 / timerMax;
    }
}
