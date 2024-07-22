//using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSector : MonoBehaviour
// UIBehavior
{
    public GameObject FieldCam;
    public GameObject Tank1DrivCam;
    public GameObject Tank2DrivCam;
    public GameObject Tank3DrivCam;

    public GameObject ClientCamSetPanel;
    public GameObject SerialPanel;
    public GameObject MiniMapTank1Drv;

    public bool PanelActive = false;
    public bool TerrainActive = false;

    private void Start()
    {
        ////client
        //if (!networkObject.IsServer)
        //{
        //    //ClientCamSetPanel.SetActive(true);
        //    MiniMapTank1Drv.SetActive(false);
        //    SerialPanel.SetActive(false);
        //    //SelectTerrein.SetActive(true);
        //}

        ////server
        //if (networkObject.IsServer)
        //{
        //    //ClientCamSetPanel.SetActive(true);
        //    MiniMapTank1Drv.SetActive(true);
        //    SerialPanel.SetActive(false);
        //    //SelectTerrein.SetActive(true);
        //}

    }
    void Update()
    {
        //if (!networkObject.IsServer)
        //{
        //    SerialPanel.SetActive(false);
        //    MiniMapTank1Drv.SetActive(false);

        //    //if (PanelActive == false && !ClientCamSetPanel.activeSelf)
        //    //{
        //    //    ClientCamSetPanel.SetActive(true);
        //    //}
        //    //else if (PanelActive == true)
        //    //{
        //    //    ClientCamSetPanel.SetActive(false);
        //    //}

        //    //if (TerrainActive == false && !SelectTerrein.activeSelf)
        //    //{
        //    //    SelectTerrein.SetActive(true);
        //    //}
        //    //else if (TerrainActive == true)
        //    //{
        //    //    SelectTerrein.SetActive(false);
        //    //}

        //    //if (Input.GetKeyDown("1"))
        //    //{
        //    //    FieldCamPos();
        //    //}
        //    //if (Input.GetKeyDown("2"))
        //    //{
        //    //    Tank2CamPos();
        //    //}
        //    //if (Input.GetKeyDown("3"))
        //    //{
        //    //    Tank3CamPos();
        //    //}

        //    //if (networkObject.TerreinTac)
        //    //{
        //    //    DrivingTerrein.SetActive(false);
        //    //    TacticalTerrein.SetActive(true);
        //    //   // networkObject.TerreinTac = false;
        //    //}

        //    //else if (networkObject.TerreinDrv)
        //    //{
        //    //    DrivingTerrein.SetActive(true);
        //    //    TacticalTerrein.SetActive(false);
        //    //    networkObject.TerreinDrv = false;
        //    //}

        //    //  TacticalTerrein.SetActive(true);
        //    //   DrivingTerrein.SetActive(true);
        //    //    SelectTerrein.SetActive(true);
        //}
        //else
        //{

        //    //DomeCamR.SetActive(true);
        //    //DomeCamL.SetActive(true);
        //}

    }

    public void Tank2CamPos()
    {
        FieldCam.SetActive(false);
        Tank1DrivCam.SetActive(false);
        Tank2DrivCam.SetActive(true);
        Tank3DrivCam.SetActive(false);

        //ClientCamSetPanel.SetActive(false);

        //PanelActive = true;
    }

    public void Tank3CamPos()
    {
        FieldCam.SetActive(false);
        Tank1DrivCam.SetActive(false);
        Tank2DrivCam.SetActive(false);
        Tank3DrivCam.SetActive(true);

        //ClientCamSetPanel.SetActive(false);

        //PanelActive = true;
    }

    public void FieldCamPos()
    {
        FieldCam.SetActive(true);
        Tank1DrivCam.SetActive(true);
        Tank2DrivCam.SetActive(false);
        Tank3DrivCam.SetActive(false);

        //ClientCamSetPanel.SetActive(false);

        //PanelActive = true;
    }
    public void PanelView()
    {
        ClientCamSetPanel.SetActive(true);
    }
    

    //public void TacTerreinSelector()
    //{
    //    //networkObject.TerreinTac = true;
    //  //  networkObject.TerreinDrv = false;

    //    DrivingTerrein.SetActive(false);
    //    TacticalTerrein.SetActive(true);
    //    SelectTerrein.SetActive(false);
    //    // SelectTerrein.SetActive(false);

    //    if (networkObject.IsServer)
    //    {
    //        Serial.SetActive(true);
    //    }
    //    else
    //        Serial.SetActive(false);

    //    TerrainActive = true;
    //}

    //public void DrivTerreinSelector()
    //{
    //   // networkObject.TerreinDrv = true;
    //   // networkObject.TerreinTac = false;

    //    TacticalTerrein.SetActive(false);
    //    DrivingTerrein.SetActive(true);
    //    //SelectTerrein.SetActive(false);

    //    SelectTerrein.SetActive(false);

    //    if (networkObject.IsServer)
    //    {
    //        Serial.SetActive(true);
    //    }
    //    else
    //        Serial.SetActive(false);

    //    TerrainActive = true;
    //}
    //public void disselect() {
    //    SelectTerrein.SetActive(false);
    //}



}
