using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    private void Awake() {
        barTransform = transform.Find( "bar" );
    }
    // Start is called before the first frame update
    void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.getResourceGeneratorData();
        
        transform.Find( "icon" ).GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find( "text" ).GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    // Update is called once per frame
    void Update()
    {
        barTransform.localScale = new Vector3( 1-resourceGenerator.GetTimerNormalized(), 1f,1f );
    }
}
