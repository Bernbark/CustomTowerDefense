using System.Collections;
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

    private int buildCost;

    //EnemySpawner spawn;
    private List<GameObject> turrets;
    Snap snappingTool;
    private const int MAX_TURRETS = 100;
    //PlayerData data;
    
    public Player player;
    public EnemyBehavior enemy;
    public TurretBehavior tower;
    
    [SerializeField] private BuildingTypeSO defaultType; // Red square for now
    [SerializeField] private BuildingTypeSO activeBuildingType;

    //GraphUpdateObject guo;
    // Start is called before the first frame update
    void Awake()
    {
        //Bounds bounds = prefab.GetComponent<BoxCollider2D>().bounds;
        //guo = new GraphUpdateObject(bounds);
        
        player = GameObject.Find("Player").GetComponent<Player>();
        turrets = new List<GameObject>();
        snappingTool = gameObject.GetComponent<Snap>();
        //spawn = spawnPoint.gameObject.GetComponent<EnemySpawner>();
        changeColor = cursor.GetComponent<ColorChange>();
        
        
        //guo.updatePhysics=true;
    }

    

    private void Update()
    {
        this.transform.position = GameUtils.GetMouseWorldPosition();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            changeColor.SetIsValidPosition(true);
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(player.GetGold() >= buildCost)
                {
                    BuildTurret();
                }
                else
                {
                    Debug.Log("Out of gold, build cost is " + buildCost);
                }
                //AstarPath.active.UpdateGraphs(guo);
               
                
                //var graphToScan = AstarPath.active.data.gridGraph;
                //AstarPath.active.Scan(graphToScan);
            }
            
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            changeColor.SetIsValidPosition(false);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Undo();
            //var graphToScan = AstarPath.active.data.gridGraph;
            //AstarPath.active.Scan(graphToScan);
        }
    }

    // Update is called once per frame
    /**
    void Update()
    {
        this.transform.position = GameUtils.GetMouseWorldPosition();
        if (EventSystem.current.IsPointerOverGameObject()){
            changeColor.SetIsValidPosition(false);
        }
        else if (Input.GetMouseButtonDown(0) && buildCount < MAX_TURRETS-1 && !EventSystem.current.IsPointerOverGameObject())
        {
            changeColor.SetIsValidPosition(true);
            Vector3 mouseWorldPosition = GameUtils.GetMouseWorldPosition();
            Vector3 snappedPosition = snappingTool.SnapToGrid(mouseWorldPosition);
            GraphNode node1 = AstarPath.active.GetNearest(spawnPoint.position, NNConstraint.Default).node;
            GraphNode node2 = AstarPath.active.GetNearest(endPoint.position, NNConstraint.Default).node;
            if (CanSpawnBuilding(activeBuildingType, snappedPosition) && isUnobstructed)
            {
                GameObject obj = Instantiate(activeBuildingType.prefab, snappedPosition, Quaternion.identity).gameObject;
                turrets.Add(obj);
                path.Scan();
                if (!PathUtilities.IsPathPossible(node1, node2))
                {
                    Destroy(obj);
                    Debug.Log("Path not possible, building not allowed here (object destroyed)");
                }

                path.Scan();
            }
            else
            {
                Debug.Log("Path not possible, building not allowed here");
            }
            //Debug.LogError(buildCount+ " is the index of last built");



        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Undo();
        }
    }
    */

    private void Undo()
    {
        if (mostRecentBuilt != null)
        {
            
            
            
            //player.AddGold(buildCost);
            
            TurretBehavior turret = mostRecentBuilt.GetComponent<TurretBehavior>();
            
            turret.DestroyThisProperly();
            player.AddGold(GetCost());
        }
        
    }

    public void DestroyTurret(TurretBehavior tower)
    {
        
        tower.DestroyThisProperly();
        player.AddGold(GetCost());
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
        Vector3 mouseWorldPosition = GameUtils.GetMouseWorldPosition();
        Vector3 snappedPosition = snappingTool.SnapToGrid(mouseWorldPosition);
        if (CanSpawnBuilding(activeBuildingType, snappedPosition))
        {
            mostRecentBuilt = Instantiate(activeBuildingType.prefab, snappedPosition, Quaternion.identity).gameObject;
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
                Debug.Log("Path not possible, building not allowed here (object destroyed)");
            }
            SetCost(SaveGameManager.Instance.SaveableObjects.Count());
            
        }
    }

    public void SetCost(int cost)
    {
        buildCost = cost * 100 + 10;
    }

    public int GetCost()
    {
        
        SetCost(SaveGameManager.Instance.SaveableObjects.Count());
        
        return buildCost;
    }

    
}
