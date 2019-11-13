using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class ContainterItem
    {
        public System.Guid Id;
        public string weaponName;
        public int maximumAmmo;

        public int amountTaken;

        public ContainterItem()
        {
            Id = System.Guid.NewGuid();
        }

        public int Remaining
        {
            get
            {
                return maximumAmmo - amountTaken;
            }
        }

        public int Get(int value)
        {
            if (amountTaken + value > maximumAmmo)
            {
                int toMuch = (amountTaken + value) - maximumAmmo;
                amountTaken = maximumAmmo;
                return value - toMuch;
            }
            amountTaken += value;
            return value;
        }

        public void Set(int amount)
        {
            amountTaken -= amount;
            if (amountTaken < 0)
                amountTaken = 0;
        }
    }

    public List<ContainterItem> items;
    public event System.Action OnContainerReady;

    private void Awake()
    {
        items = new List<ContainterItem>();
        OnContainerReady();
    }

    public System.Guid Add(string name, int maximum)
    {
        items.Add(new ContainterItem { Id = System.Guid.NewGuid(), maximumAmmo = maximum, weaponName = name });
        return items.Last().Id;
    }

    public void Put(string name, int amount)
    {
        var containerItem = items.Where(x => x.weaponName == name).FirstOrDefault();
        if (containerItem == null)
            return;
        containerItem.Set(amount);
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Get(amount);
    }

    public int GetAmountRemaining(System.Guid id)
    {
        var containerItem = GetContainerItem(id);
        if (containerItem == null)
            return -1;
        return containerItem.Remaining;
    }

    private ContainterItem GetContainerItem(System.Guid id)
    {
        var containerItem = items.Where(x => x.Id == id).FirstOrDefault();
        return containerItem;
    }
}
