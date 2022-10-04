
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class YToSortingOrder : MonoBehaviour {

    [SerializeField]private Transform anchorToBeAboveOf;
    [SerializeField]private bool runOnce;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if ( spriteRenderer == null ) {
            Destroy( this );
        }
    }

    // Update is called once per frame
    private void LateUpdate() {
        spriteRenderer.sortingOrder = SortingOrderGenerator();
        if ( runOnce ) {
            Destroy( this );
        }
    }

    private int SortingOrderGenerator() {
        float precisionMultiplier = 10f;
        float currentPositionY = transform.position.y;
        float anchorY = anchorToBeAboveOf != null ? anchorToBeAboveOf.position.y : currentPositionY;
        float delta = anchorY-currentPositionY;

        return (int)( ( -( anchorY ) * precisionMultiplier ) - delta );
    }
}
