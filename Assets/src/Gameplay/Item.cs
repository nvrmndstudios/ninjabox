using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Collectable,
        Obstacle        
    }

    [SerializeField] private ItemType _itemType = ItemType.Collectable;
    public Vector3 targetPosition = Vector3.zero;

    [SerializeField] private float moveSpeed = 3.0f;

    public ItemType GetItemType() => _itemType;

    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}