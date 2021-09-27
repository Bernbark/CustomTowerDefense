
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinding;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform spawnPoint;
    public GameObject prefab;
    private GameObject mostRecentBuilt;
    public GameObject cursor;
    ColorChange changeColor;
    bool firstScan = false;
    private float timer;

    private int buildCost;
    

    //EnemySpawner spawn;
    private List<GameObject> turrets;
    Snap snappingTool;
    
    //PlayerData data;
    
    public Player player;
    //public EnemyBehavior enemy;
    public TurretBehavior[] towers;
    
    [SerializeField] private BuildingTypeSO defaultType; // Red square for now
    [SerializeField] private BuildingTypeSO activeBuildingType;

    public UI_TextEvents textEvents;

    GraphUpdateObject guo;
    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = prefab.GetComponent<BoxCollider2D>().bounds;
        guo = new GraphUpdateObject(bounds);
        guo.updatePhysics = true;
        AstarPath.active.Scan();
        player = GameObject.Find("Player").GetComponent<Player>();
        turrets = new List<GameObject>();
        snappingTool = gameObject.GetComponent<Snap>();
        //spawn = spawnPoint.gameObject.GetComponent<EnemySpawner>();
        changeColor = cursor.GetComponent<ColorChange>();
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
        GraphNode node1 = AstarPath.active.GetNearest(spawnPoint.position, NNConstraint.Default).node;
        GraphNode node2 = AstarPath.active.GetNearest(endPoint.position, NNConstraint.Default).node;
        PathUtilities.IsPathPossible(node1, node2);
        
    }

    

    private void Update()
    {
        timer += Time.deltaTime;
        if (!firstScan && timer < .2f)
        {
            AstarPath.active.Scan();
            firstScan = true;
        }
        this.transform.position = GameUtils.Instance.GetMouseWorldPositionForUtils();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            changeColor.SetIsValidPosition(true);
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(player.GetGold() >= buildCost)
                {
                    BuildTurret();
                }                             
            }
            
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            changeColor.SetIsValidPosition(false);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Undo();
        }
    }

    private void Undo()
    {
        if (mostRecentBuilt != null)
        {
            TurretBehavior turret = mostRecentBuilt.GetComponent<TurretBehavior>();
            
            turret.DestroyThisProperly();
            player.AddGold(GetCost());
        }
        
    }

    public void DestroyTurret(TurretBehavior tower)
    {
        
        tower.DestroyThisProperly();
        player.AddGold(GetRefundPrice());
        AstarPath.active.Scan();
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingTypeSO)
    {
        activeBuildingType = buildingTypeSO;
    }

    public void SetDefaultActiveBuildingType()
    {
        activeBuildingType = defaultType;
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO building, Vector3 position)
    {
        BoxCollider2D buildingBoxCollider = building.prefab.GetComponent<BoxCollider2D>();

        if(Physics2D.OverlapBox(position + (Vector3)buildingBoxCollider.offset, buildingBoxCollider.size, 0) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void BuildTurret()
    {
        Vector3 mouseWorldPosition = GameUtils.Instance.GetMouseWorldPositionForUtils();
        Vector3 snappedPosition = snappingTool.SnapToGrid(mouseWorldPosition);
        if (CanSpawnBuilding(activeBuildingType, snappedPosition))
        {
            mostRecentBuilt = Instantiate(activeBuildingType.prefab, snappedPosition, Quaternion.identity).gameObject;
            //AstarPath.active.UpdateGraphs(mostRecentBuilt.GetComponent<BoxCollider2D>().bounds);
            turrets.Add(mostRecentBuilt);
            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
            GraphNode node1 = AstarPath.active.GetNearest(spawnPoint.position, NNConstraint.Default).node;
            GraphNode node2 = AstarPath.active.GetNearest(endPoint.position, NNConstraint.Default).node;
            player.SubtractGold(buildCost);
            
            
            if (!PathUtilities.IsPathPossible(node1, node2))
            {
                Undo();
                AstarPath.active.Scan(graphToScan);
                
            }
            SetCost(SaveGameManager.Instance.SaveableObjects.Count());
            
        }
    }

    

    public void SetCost(int cost)
    {
        if (activeBuildingType.name.Equals("ShotgunTurret"))
        {
            buildCost = 10 * cost * 100 + 10;
        }
        else if(activeBuildingType.name.Equals("LaserTurret"))
        {
            buildCost = cost * 100000 + 10000;
        }
        else
        {
            buildCost = cost * 100 + 10;
        }
        
    }

    public int GetCost()
    {
        
        SetCost(SaveGameManager.Instance.SaveableObjects.Count());
        
        return buildCost;
    }

    public int GetRefundPrice()
    {
        int refund;
        int cost = SaveGameManager.Instance.SaveableObjects.Count();
        if (UpgradeOverlay.GetTurret_Static().tag.Equals("ShotgunTurret"))
        {
            refund = 10 * cost * 100 + 10;
        }
        else if (UpgradeOverlay.GetTurret_Static().tag.Equals("LaserTurret"))
        {
            refund = cost * 100000 + 10000;
        }
        else
        {
            refund = cost * 100 + 10;
        }
        return refund;
    }

    
}
