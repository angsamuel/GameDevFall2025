using UnityEngine;

public static class SaveLoadBus
{
   public static void SavePlayerPosition(Vector3 position){
        SaverLoader.SaveVector3("player_save_pos", position);
        SaverLoader.Flush();
   }

   public static Vector3 LoadPlayerPosition(Vector3 defaultPosition){
    return SaverLoader.LoadVector3("player_save_pos",defaultPosition);
   }
}
