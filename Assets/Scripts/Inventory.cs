using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private List<Item> _items;

    public Inventory(List<Item> items, int maxSize)
    {
        _items = new List<Item>(items); //копирую в лист то, что передал
        MaxSize = maxSize;
    }
    public int MaxSize { get; private set; }

    public int CurrentSize => _items.Sum(item => item.Count); //магичестки нахожу сумму ВСЕХ предметов. (3 меча + 5 топора)

    public IReadOnlyList<Item> Items => _items;


    public bool IsEnoughtSpace(Item item) => CurrentSize + item.Count <= MaxSize; //хватит ли места?

    public void Add(Item item)
    {
        if (IsEnoughtSpace(item) == false) // ну и поидеи, если место есть, всё проолжится играть
            throw new ArgumentOutOfRangeException("Нет места");

        Item tempItem = _items.FirstOrDefault(i => i.ID == item.ID); //мама я не хочу. мы типо берем список наш. проходиммся по всем айди и сравниваем с нашим
        if (tempItem != null)
            tempItem.IncreaseCount(item.Count);  //если такой предмет уже был, то добавляем количество
        else
            _items.Add(item);
    }

    public List<Item> GetItemsBy(int id, int count)
    {
        // _items = new List<Item>(); я не понимаю зачем это, он же уже создан? в констукторе
        List<Item> removedItems = new List<Item>();

        if(count < 0)
            throw new ArgumentOutOfRangeException("Нельзя взять отрицательное значение");

        for (int i = 0; i < count; i++)
        {
            Item item = _items.FirstOrDefault(item => item.ID == id); //находим первый ID который совпадает. и если нет таких, то не сломается поидеи

            if (item == null) //если count больше, чем у нас есть предметов, будет налл
                throw new ArgumentOutOfRangeException("Нет столько предметов с этим ID");

            removedItems.Add(item); // сюда добавил предмет, который удалил. или много предметов
            _items.Remove(item); //просто его удаляем типо? окей, выбросили на землю скажем
        }
        return removedItems; //мы должны получить предметы, а не инвентарь наш
        //return _items; 
    }
}

public class Item
{
    public int ID { get; private set; } //номер предмета (3 - меч)
    public int Count { get; private set; } //количество таких предметов  (3шт)

    public Item(int id, int count)
    {
        ID = id;
        Count = count;
    }

    public void DecreaseCount(int amount)
    {
        if (amount > Count)
            throw new ArgumentOutOfRangeException("Нельзя уменьшить количество больше, чем есть");
        Count -= amount;
    }

    public void IncreaseCount(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("Нельзя увеличить количество на отрицательное значение.");
        Count += amount;
    }
}