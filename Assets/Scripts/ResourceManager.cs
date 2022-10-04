using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    private void Awake() {
        Instance = this;
        
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach ( ResourceTypeSO resourceType in resourceTypeList.list ) {
            resourceAmountDictionary[resourceType] = 0;
            if(resourceType.nameString == "Wood") {
                resourceAmountDictionary[resourceType] = 100;
            }
        }

        
    }


    public void AddResource(ResourceTypeSO resourceType,int amount) {
        resourceAmountDictionary[resourceType] += amount;

        OnResourceAmountChanged?.Invoke( this, EventArgs.Empty );
    }

    public int GetResourceAmount( ResourceTypeSO resourceType ) {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford( SerializableResourceTypeToIntDictionary resourceCostDictionary ) {
        foreach ( ResourceTypeSO resourceType in resourceCostDictionary.Keys ) {
            if ( resourceAmountDictionary[resourceType] < resourceCostDictionary[resourceType] ) {
                return false;
            }
        }
        return true;
    }

    public void SpendResources( SerializableResourceTypeToIntDictionary resourceCostDictionary ) {
        foreach ( ResourceTypeSO resourceType in resourceCostDictionary.Keys ) {
            resourceAmountDictionary[resourceType] -= resourceCostDictionary[resourceType];
        }
        OnResourceAmountChanged?.Invoke( this, EventArgs.Empty );
    }


}
