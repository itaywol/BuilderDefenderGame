using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BuildingTypeSelectUI : MonoBehaviour {

    [SerializeField] private Sprite arrowSprite;

    private Transform arrowBtnTransform;

    private Dictionary<BuildingTypeSO,Transform> buildingTypeButtons;
    // Start is called before the first frame update
    private void Awake() {
        buildingTypeButtons = new Dictionary<BuildingTypeSO, Transform>();

        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive( false );

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>( typeof(BuildingTypeListSO).Name );

        arrowBtnTransform = Instantiate( btnTemplate, transform );
        arrowBtnTransform.gameObject.SetActive( true );


        float offsetAmount = 130f;
        int index=0;
        arrowBtnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2( offsetAmount * index, 0 );
        arrowBtnTransform.Find( "image" ).GetComponent<Image>().sprite = arrowSprite;
        arrowBtnTransform.Find( "image" ).GetComponent<RectTransform>().sizeDelta = new Vector2( 0, -30 );
        arrowBtnTransform.GetComponent<Button>().onClick.AddListener( () => {
            BuildingManager.Instance.SetCurrentBuildingType( null );
        } );

        ToggleSelectedMark( null, false );
        index++;


        foreach ( BuildingTypeSO buildingType in buildingTypeList.list ) {
            Transform btnTransform = Instantiate( btnTemplate, transform );
            buildingTypeButtons[buildingType] = btnTransform;
            btnTransform.gameObject.SetActive( true );

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2( offsetAmount * index, 0 );
            btnTransform.Find( "image" ).GetComponent<Image>().sprite = buildingType.sprite;
            
            btnTransform.GetComponent<Button>().onClick.AddListener( () => {
                BuildingManager.Instance.SetCurrentBuildingType( buildingType );
            } );

            ToggleSelectedMark( buildingType, false );

            index++;

        }
    }
    private void ToggleSelectedMark( BuildingTypeSO buildingType, bool visible ) {
        if ( buildingType == null ) {
            arrowBtnTransform.Find( "selected" ).GetComponent<Image>().enabled = visible;
        } else {
            buildingTypeButtons[buildingType].Find( "selected" ).GetComponent<Image>().enabled = visible;
        }
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_onActiveBuildingTypeChanged;
    }

    private void BuildingManager_onActiveBuildingTypeChanged( object sender, BuildingManager.BuildingManagerEventArgs e ) {

        if ( e.buildingType == null ) {
            ToggleSelectedMark( null, true );
        } else {
            ToggleSelectedMark( null, false );
        }

        foreach ( BuildingTypeSO buildingType in buildingTypeButtons.Keys ) {
            if ( buildingType != e.buildingType ) {
                ToggleSelectedMark( buildingType, false );
            } else {
                ToggleSelectedMark( buildingType, true );
            }
        }
    }
}
