using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public class BuildingManagerEventArgs : EventArgs {
        public BuildingTypeSO buildingType;
    }

    public static BuildingManager Instance { get; private set; }
    public event EventHandler<BuildingManagerEventArgs> OnActiveBuildingTypeChanged;

    private BuildingTypeListSO buildingTypes;
    private BuildingTypeSO selectedBuildingType;


    private void Awake() {
        Instance = this;
        buildingTypes = Resources.Load<BuildingTypeListSO>( typeof( BuildingTypeListSO ).Name );

    }

    private void Start() {
        SetCurrentBuildingType( buildingTypes.list[0] );
    }
    // Start is called before the first frame update
    private void Update() {
        Vector3 mousePosition = MouseUtils.GetMouseWorldPosition();
        if ( Input.GetMouseButtonDown( 0 ) && !EventSystem.current.IsPointerOverGameObject() && selectedBuildingType != null && CanSpawnBuilding( selectedBuildingType, mousePosition ) ) {
            Instantiate( selectedBuildingType.prefab, mousePosition, Quaternion.identity );
            ResourceManager.Instance.SpendResources( selectedBuildingType.buildingCost );
        }
    }


    public void SetCurrentBuildingType( BuildingTypeSO buildingType ) {
        selectedBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke( this, new BuildingManagerEventArgs { buildingType = buildingType } );
    }

    public BuildingTypeSO GetCurrerntBuildingType() {
        return selectedBuildingType;
    }

    public static bool CanSpawnBuilding( BuildingTypeSO buildingType, Vector3 position ) {
        if ( buildingType == null ) return false;

        if ( !ResourceManager.Instance.CanAfford( buildingType.buildingCost ) ) return false;

        BoxCollider2D boxCollider = buildingType.prefab.GetComponent<BoxCollider2D>();


        Collider2D[] colliders = Physics2D.OverlapBoxAll( position+(Vector3)boxCollider.offset, boxCollider.size, 0f );

        if ( colliders.Length != 0 ) return false;

        colliders = Physics2D.OverlapCircleAll( position, buildingType.minimumDistanceBetweenBuildings )
            .Where( collider => collider.GetComponent<Building>()?.BuildingType == buildingType ).ToArray();

        if ( colliders.Length != 0 ) return false;

        float maxConstructionRadius = 25f;

        colliders = Physics2D.OverlapCircleAll( position, maxConstructionRadius )
            .Where( collider => collider.GetComponent<Building>() != null ).ToArray();
        if ( colliders.Length == 0 ) return false;

        return true;
    }
}
