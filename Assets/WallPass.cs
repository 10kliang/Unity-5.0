using UnityEngine;

public class WallPass : MonoBehaviour
{
    // Define the tag for passable walls
    public string passableWallTag = "PassableWall";

    // Called when the GameObject collides with another collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider's tag matches the passable wall tag
        if (other.CompareTag(passableWallTag))
        {
            // Temporarily disable collision between this GameObject and the passable wall
            Physics.IgnoreCollision(GetComponent<Collider>(), other, true);
        }
    }

    // Called when the GameObject exits a collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the collider's tag matches the passable wall tag
        if (other.CompareTag(passableWallTag))
        {
            // Re-enable collision between this GameObject and the passable wall
            Physics.IgnoreCollision(GetComponent<Collider>(), other, false);
        }
    }
}

