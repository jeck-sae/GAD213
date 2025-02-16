using UnityEngine;

public class GameDebugShortcuts : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                var tile = GridManager.Instance.Get(Helpers.Camera.ScreenToWorldPoint(Input.mousePosition));
                tile.IsWalkable = !tile.IsWalkable;
                tile.gfx.SetFillColor(Color.black);
            }
        }
    }
}
