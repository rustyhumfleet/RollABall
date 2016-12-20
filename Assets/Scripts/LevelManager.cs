//using System;
//using System.Collections;
using UnityEngine;
//using System.Collections.Generic;     // Allows us to use Lists.
//using Random = UnityEngine.Random;    // Tells Random to use the Unity Engine random number generator.
using UnityEditor;                      // Access prefabs from assets folder
using System;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// What it should do
    /// Clear the level
    /// Create the ground plane
    /// Create the outer walls           DONE
    /// create the player   
    /// place the player in level
    /// respawn the player
    /// Create the Pickups               DONE
    /// </summary>

    // TODO: //
    //public GameObject[] wallTiles;                               // Array of wall prefabs
    //public GameObject exit;                                      // Prefab to spawn for exit
    //public GameObject[] playerSpawns;                            // Array of player spawn locations

    // Finished //
    public GameObject outerWallPrefab;                             // Outer wall prefab
    public GameObject groundPrefab;                                // Ground prefab
    public GameObject pickUpPrefab;                                // Pickup prefab

    // Hierarchy organizational holders             |level->outer walls
    //                                              |        ->wall
    //                                              |     ->pickups
    //                                              |        ->pickup
    //                                              |     ->ground
    //                                              |     ->player
    //                                              |     ->
    private Transform levelHolder;                                 // Reference to the transform of our Level object for Hierarchy.
    private Transform wallHolder;                                  // Reference to the transform of our Wall objects for Hierarchy.
    private Transform pickUpHolder;                                // Reference to the transform of our Pick Up objects for Hierarchy.

    //SetupScene initializes our level and calls the following functions to lay out the game level
    public void SetupScene(int level)
    {
        // Creates the hierarchy layout
        CreateHierarchyLayout();

        // Creates the outer walls, floor, and pickups.
        LevelSetup(level);

        //Instantiate the exit tile in the upper right hand corner of our game board
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

    //Sets up the outer walls and floor (background) of the game level.
    void LevelSetup(int level)
    {
        // instantiate grounds
        SpawnGrounds(level);
        // instantiate walls
        SpawnOuterWalls(level);
        // instantiate pickups
        SpawnPickups(level);
    }

    private void SpawnGrounds(int level)
    {
        if (level == 1)
        {
            //instantiate ground from the assets folder if its not in the scene
            UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Ground.prefab", typeof(GameObject));
            GameObject groundClone = Instantiate(prefab, new Vector3(0, -1, 0), Quaternion.identity) as GameObject;

            //Set the parent of our newly instantiated object instance to levelHolder, this is just organizational to avoid cluttering hierarchy.
            groundClone.transform.SetParent(levelHolder); 
        }// end level 1
    }

    private void SpawnPickups(int level)
    {
        if (level == 1)
        {
            GameObject pickUpClone;
            for (int x = -5; x < 5; x++)
            {
                for (int y = -5; y < 5; y++)
                {
                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current position cast it to GameObject.
                    pickUpClone =
                        Instantiate(pickUpPrefab, new Vector3(x, y, 0.5f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to pickupHolder, this is just organizational to avoid cluttering hierarchy.
                    pickUpClone.transform.SetParent(pickUpHolder);
                }
            }
        }// end level 1
    }// end SpawnPickups

    private void SpawnOuterWalls(int level)
    {
        if (level == 1)
        {
            // north is 0 0 10  scale 20.5 2 0.5
            // east is 10 0 0   scale 0.5 2 20.5
            // south is 0 0 -10 scale 20.5 2 0.5
            // west is -10 0 0  scale .5 2 20.5

            // Setup 
            GameObject outerWallClone;
            // Build the four walls but first set the position and rotation to 0 0 0
            var position = new Vector3();
            var rotate = new Vector3();

            // for walls
            for (int i = 0; i < 4; i++)
            {
                // reset for each wall
                position = new Vector3(0, 0, 0);
                rotate = new Vector3(0, 0, 0);

                switch (i)
                {
                    case 0: // North Wall
                        position.z = 10f;
                        break;
                    case 1:// East Wall
                        position.z = -10f;
                        break;
                    case 2:// South Wall
                        position.x = 10f;
                        rotate.y = -90f;
                        break;
                    case 3:// West Wall
                        position.x = -10f;
                        rotate.y = 90f;
                        break;
                    default:
                        break;
                }

                // move the prefab to a new position before spawning
                outerWallPrefab.transform.position = position;
                outerWallPrefab.transform.rotation = Quaternion.Euler(rotate);

                // spawn a clone
                outerWallClone = Instantiate(outerWallPrefab, outerWallPrefab.transform.position, outerWallPrefab.transform.rotation) as GameObject;

                // Add it to the hierchy
                outerWallClone.transform.SetParent(wallHolder);
            }// end for
        } // end level 1
    } // end BuildWalls

    private void CreateHierarchyLayout()
    {
        // Instantiate holders and set its transform to position in the hierarchy. 
        levelHolder = new GameObject("Level").transform;
        wallHolder = new GameObject("OuterWalls").transform;
        pickUpHolder = new GameObject("PickUps").transform;

        // set wall holder to levelholder in the hierarchy
        wallHolder.transform.SetParent(levelHolder);
        // set pickup holder to levelholder in the hierarchy
        pickUpHolder.transform.SetParent(levelHolder);
    }
}
