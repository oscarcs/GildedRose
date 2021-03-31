using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class Program
    {
        IList<Item> Items;
        
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            UpdateQuality(app.Items);

            System.Console.ReadKey();

        }

        public static IList<Item> UpdateQuality(IList<Item> items)
        {
            foreach (var item in items)
            {
                UpdateableItem internalItem;
                switch (item.Name)
                {
                    case BackstagePasses:
                        internalItem = new BackstagePasses() {Name = item.Name, Quality = item.Quality, SellIn = item.SellIn};
                        break;
                    case Sulfuras:
                        internalItem = new Sulfuras() {Name = item.Name, Quality = item.Quality, SellIn = item.SellIn};
                        break;
                    case AgedBrie:
                        internalItem = new AgedBrie() {Name = item.Name, Quality = item.Quality, SellIn = item.SellIn};
                        break;
                    default:
                        internalItem = new NormalItem()
                            {Name = item.Name, Quality = item.Quality, SellIn = item.SellIn};
                        break;
                }

                internalItem.UpdateQuality();

                item.Name = internalItem.Name;
                item.Quality = internalItem.Quality;
                item.SellIn = internalItem.SellIn;
            }

            return items;
        }


    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

    public abstract class UpdateableItem : Item
    {
        protected const int MaxQuality = 50;
        protected const int MinQuality = 0;

        public abstract void UpdateQuality();

        protected void IncreaseQuality()
        {
            if (Quality < MaxQuality)
            {
                Quality = Quality + 1;
            }
        }

        protected void DecreaseQuality()
        {
            if (Quality > MinQuality)
            {
                {
                    Quality = Quality - 1;
                }
            }
        }
    }

    public class NormalItem : UpdateableItem
    {
        public override void UpdateQuality()
        {
            DecreaseQuality();
            
            SellIn = SellIn - 1;

            if (SellIn < 0)
            {
                DecreaseQuality();
            }
        }
    }
    
    public class AgedBrie : UpdateableItem
    {
        public override void UpdateQuality()
        {
            IncreaseQuality();
            
            SellIn = SellIn - 1;

            if (SellIn < 0)
            {
                IncreaseQuality();
            }
        }
    }

    public class BackstagePasses : UpdateableItem
    {
        public override void UpdateQuality()
        {
            IncreaseQuality();
            
            if (SellIn < 11)
            {
                IncreaseQuality();
            }

            if (SellIn < 6)
            {
                IncreaseQuality();
            }
            
            SellIn = SellIn - 1;

            if (SellIn < 0)
            {
                Quality = 0;
            }
        }
    }

    public class Sulfuras : UpdateableItem
    {
        public override void UpdateQuality()
        {
            
        }
    }
}
