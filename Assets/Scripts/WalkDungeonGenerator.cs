using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WalkDungeonGenerator : MonoBehaviour
{
    public int seed = 100;
    public int maxSteps = 50;
    public int boxSize = 5;
    public GameObject blockPrefab;





    [Header("Decorations")]
    public List<GameObject> blockDecorations;
    public List<float> decorationVerticalOffsets;
    List<Vector3> dungeonBoxInnerPositions;

    [Header("Debug")]
    public Transform visualCube;
    public float visualCubeHeight = 10;
    public float pauseTime = 0.01f;
    //trackers
    Vector3 startPos = Vector3.zero;
    Vector3 currentPos;
    List<Vector3> directions;
    Vector3 moveDirection;
    Dictionary<Vector3, DungeonBox> dungeonBoxes = new Dictionary<Vector3, DungeonBox>();


    void Start(){
        Random.InitState(seed);
        currentPos = startPos;
        directions = new List<Vector3>();
        directions.Add(Vector3.right);
        directions.Add(Vector3.left);
        directions.Add(Vector3.forward);
        directions.Add(Vector3.back);


        dungeonBoxInnerPositions = new List<Vector3>();
        for (int x = -boxSize / 2; x < boxSize / 2; x++)
        {
            for (int z = -boxSize / 2; z < boxSize / 2; z++)
            {
                dungeonBoxInnerPositions.Add(new Vector3(x, .5f, z));
            }
        }

        StartCoroutine(GenerateDungeonRoutine());
    }


    IEnumerator GenerateDungeonRoutine()
    {
        int step = 0;
        while (step < maxSteps)
        {
            step++;

            if (!dungeonBoxes.ContainsKey(currentPos))
            {
                dungeonBoxes[currentPos] = Instantiate(blockPrefab, currentPos * (boxSize+1), Quaternion.identity).GetComponent<DungeonBox>();
                DecorateBox(dungeonBoxes[currentPos]);
            }

            DungeonBox currentBox = dungeonBoxes[currentPos];
            visualCube.position = currentBox.transform.position + new Vector3(0, visualCubeHeight, 0);

            RemoveWall(currentBox, -moveDirection);

            if(step % 2 == 1){
                moveDirection = directions[Random.Range(0, directions.Count)];
            }




            RemoveWall(currentBox, moveDirection);

            currentPos += moveDirection;

            yield return new WaitForSeconds(pauseTime);
        }
        //create a final box to close things off
        if (!dungeonBoxes.ContainsKey(currentPos))
        {
            dungeonBoxes[currentPos] = Instantiate(blockPrefab, currentPos * (boxSize + 1), Quaternion.identity).GetComponent<DungeonBox>();
            DecorateBox(dungeonBoxes[currentPos]);
            RemoveWall(dungeonBoxes[currentPos], -moveDirection);
        }
    }

    void RemoveWall(DungeonBox box, Vector3 direction)
    {
        if (direction.x == 1) box.rightWall.SetActive(false);
        else if (direction.x == -1) box.leftWall.SetActive(false);
        else if (direction.z == 1) box.frontWall.SetActive(false);
        else if (direction.z == -1) box.backWall.SetActive(false);
    }

    public void DecorateBox(DungeonBox box){

        List<Vector3> tempBoxInnerPositions = new List<Vector3>(dungeonBoxInnerPositions); //make a copy
        int numDecorations = Random.Range(0, boxSize);

        for(int i = 0; i< numDecorations; i++){
            int randomPosIndex = Random.Range(0, tempBoxInnerPositions.Count);

            int decorationChoice = Random.Range(0, blockDecorations.Count);
            Vector3 randomPos = tempBoxInnerPositions[randomPosIndex] + box.transform.position + new Vector3(0, decorationVerticalOffsets[decorationChoice], 0);

            GameObject decoration = Instantiate(blockDecorations[decorationChoice], randomPos, Quaternion.identity);
            tempBoxInnerPositions.RemoveAt(randomPosIndex);
        }
    }
}
