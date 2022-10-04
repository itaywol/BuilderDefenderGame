using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    private SpriteRenderer buildingGhostRenderer;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    private BuildingTypeSO currentBuildingType;
    private void Awake() {
        buildingGhostRenderer = transform.Find( "sprite" ).GetComponent<SpriteRenderer>();
        resourceNearbyOverlay = transform.Find( "pfResourceNearbyOverlay" ).GetComponent<ResourceNearbyOverlay>();
    }
    // Start is called before the first frame update
    void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;
    }

    private void BuildingManager_onActiveBuildingTypeChanged( object sender, BuildingManager.BuildingManagerEventArgs e ) {
        currentBuildingType = e.buildingType;
        if ( e.buildingType == null ) {
            ChangeBuildingGhostSprite( null );
            ToggleBuildingGhost( false );
        } else {
            ChangeBuildingGhostSprite( e.buildingType.sprite );
            ToggleBuildingGhost( true );
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = MouseUtils.GetMouseWorldPosition();
        transform.position = mousePosition;

        if(BuildingManager.CanSpawnBuilding( currentBuildingType, mousePosition )) {
            buildingGhostRenderer.color = new Color( 0, 255, 0, buildingGhostRenderer.color.a );
        }else {
            buildingGhostRenderer.color = new Color( 255, 0, 0, buildingGhostRenderer.color.a );
        }
    }

    private void ChangeBuildingGhostSprite( Sprite sprite ) {
        if ( sprite != null ) {

            buildingGhostRenderer.sprite = sprite;

        }
    }

    private void ToggleBuildingGhost(bool visible) {
        buildingGhostRenderer.enabled = visible;

        if(visible) {
            resourceNearbyOverlay.Show( currentBuildingType.resourceGeneratorData );
        }else {
            resourceNearbyOverlay.Hide();
        }
    }
}
