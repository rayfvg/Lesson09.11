using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private List<Item> _items;
    private int _maxSize;

    public int CurrentSize => _items.Sum(item => item.Count);

    public Inventory(List<Item> items, int maxSize)
    {
        _items = new List<Item>(items);
        _maxSize = maxSize;
    }

    public void Add(Item item)
    {
        Item existingItem = _items.FirstOrDefault(i => i.ID == item.ID);

        if (existingItem != null)
        {
            if (CurrentSize + item.Count <= _maxSize)
                existingItem.Count += item.Count;
        }
        else
        {
            if (CurrentSize + item.Count <= _maxSize)
                _items.Add(item);
        }
    }

    public List<Item> GetItemsBy(int id, int count)
    {
        List<Item> result = new List<Item>();

        var existingItem = _items.FirstOrDefault(item => item.ID == id);
        if (existingItem != null)
        {
            int itemsToRemove = count > existingItem.Count ? existingItem.Count : count;
            result.Add(new Item { ID = id, Count = itemsToRemove });
            existingItem.Count -= itemsToRemove;

            if (existingItem.Count == 0)
                _items.Remove(existingItem);
        }

        return result;
    }
}

public class Item
{
    public int ID;
    public int Count;
}