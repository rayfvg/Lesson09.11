using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private List<Item> _items;

    public Inventory(List<Item> items, int maxSize)
    {
        _items = new List<Item>(items); //������� � ���� ��, ��� �������
        MaxSize = maxSize;
    }
    public int MaxSize { get; private set; }

    public int CurrentSize => _items.Sum(item => item.Count); //���������� ������ ����� ���� ���������. (3 ���� + 5 ������)

    public IReadOnlyList<Item> Items => _items;


    public bool IsEnoughtSpace(Item item) => CurrentSize + item.Count <= MaxSize; //������ �� �����?

    public void Add(Item item)
    {
        if (IsEnoughtSpace(item) == false) // �� � ������, ���� ����� ����, �� ���������� ������
            throw new ArgumentOutOfRangeException("��� �����");

        Item tempItem = _items.FirstOrDefault(i => i.ID == item.ID); //���� � �� ����. �� ���� ����� ������ ���. ����������� �� ���� ���� � ���������� � �����
        if (tempItem != null)
            tempItem.IncreaseCount(item.Count);  //���� ����� ������� ��� ���, �� ��������� ����������
        else
            _items.Add(item);
    }

    public List<Item> GetItemsBy(int id, int count)
    {
        // _items = new List<Item>(); � �� ������� ����� ���, �� �� ��� ������? � �����������
        List<Item> removedItems = new List<Item>();

        if(count < 0)
            throw new ArgumentOutOfRangeException("������ ����� ������������� ��������");

        for (int i = 0; i < count; i++)
        {
            Item item = _items.FirstOrDefault(item => item.ID == id); //������� ������ ID ������� ���������. � ���� ��� �����, �� �� ��������� ������

            if (item == null) //���� count ������, ��� � ��� ���� ���������, ����� ����
                throw new ArgumentOutOfRangeException("��� ������� ��������� � ���� ID");

            removedItems.Add(item); // ���� ������� �������, ������� ������. ��� ����� ���������
            _items.Remove(item); //������ ��� ������� ����? ����, ��������� �� ����� ������
        }
        return removedItems; //�� ������ �������� ��������, � �� ��������� ���
        //return _items; 
    }
}

public class Item
{
    public int ID { get; private set; } //����� �������� (3 - ���)
    public int Count { get; private set; } //���������� ����� ���������  (3��)

    public Item(int id, int count)
    {
        ID = id;
        Count = count;
    }

    public void DecreaseCount(int amount)
    {
        if (amount > Count)
            throw new ArgumentOutOfRangeException("������ ��������� ���������� ������, ��� ����");
        Count -= amount;
    }

    public void IncreaseCount(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException("������ ��������� ���������� �� ������������� ��������.");
        Count += amount;
    }
}