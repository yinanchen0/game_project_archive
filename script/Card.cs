using UnityEngine;

[System.Serializable]
public class Card
{
    public int CardNumber { get; set; }
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Duration { get; set; }
    public Sprite Sprite { get; set; }
    public string Active { get; set; }

    public Card(int cardNumber, string name, int attack, int duration, Sprite sprite, string active)
    {
        CardNumber = cardNumber;
        Name = name;
        Attack = attack;
        Duration = duration;
        Sprite = sprite;
        Active = active;
    }
}


