/*
 *	This program is the CONFIDENTIAL and PROPRIETARY property 
 *	of Tomasello Software LLC. Any unauthorized use, reproduction or
 *	transfer of this computer program is strictly prohibited.
 *
 *      Copyright (c) 2004 Tomasello Software LLC.
 *	This is an unpublished work, and is subject to limited distribution and
 *	restricted disclosure only. ALL RIGHTS RESERVED.
 *
 *			RESTRICTED RIGHTS LEGEND
 *	Use, duplication, or disclosure by the Government is subject to
 *	restrictions set forth in subparagraph (c)(1)(ii) of the Rights in
 * 	Technical Data and Computer Software clause at DFARS 252.227-7013.
 *
 *	Angel Island UO Shard	Version 1.0
 *			Release A
 *			March 25, 2004
 */
using System;
using System.IO;
using System.Xml;
using System.Collections;

using Server;
using Server.Misc;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;
using Server.Multis;
using Server.Commands;


//: base(0xED4) //Flippable 0xED5
namespace Server.Items
 {
    
    public class TmanAddon : BaseAddon
    {

        public override BaseAddonDeed Deed
        {
            get
            {
                return new TmanAddonDeed();
            }
        }

        private TmanAddon m_Tmanstone;
        public TmanAddon Tmanstone
        {
            get
            {
                return m_Tmanstone;
            }
            set
            {
                m_Tmanstone = value;
            }
        }


        [Constructable]
        public TmanAddon()
        {
            // Constructed from classes for flexibility, as per flagstone
            AddComponent(new TmanBase(), 0, 0, 0);
            //AddComponent(new TmanRune(), 0, 0, 0);
        }

        public TmanAddon(Serial serial)
             : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            //reset all graphics back to base animation, incase we saved and crashed, or went down
            //before bellow timer could reset graphics.
            /*try
            {
                ((AddonComponent)this.Components[0]).ItemID = 1306;
                ((AddonComponent)this.Components[1]).ItemID = 3686;
                
            }
            catch (Exception exc)
            {
                LogHelper.LogException(exc);
                Console.WriteLine("Exception TmanAddon addon Deserialization");
                System.Console.WriteLine(exc.Message);
                System.Console.WriteLine(exc.StackTrace);
            }*/
        }

    }
    

    public class TmanAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new TmanAddon();
            }
        }
        [Constructable]
        public TmanAddonDeed()
        {
            Name = "TmanAddonDeed";
        }

       public TmanAddonDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TmanBase : AddonComponent
    {
        [Constructable]
        public TmanBase()
            : this(15750)
            {
            Name = "Tournament Management Stone";
            Hue = 0;
            }
        public TmanBase(int itemID)
           : base(itemID)
        {
        }

        public TmanBase(Serial serial)
        : base(serial)
        {
        }
        public override void OnDoubleClick(Mobile from)
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (from.InRange(this, 2))
            {
                if ((from.AccessLevel >= AccessLevel.Counselor ||
                    (house != null && (house.IsOwner(from) || house.IsCoOwner(from)) && house.Contains(this))))
                {
                    from.CloseGump(typeof(TmanUseGump));
                    from.SendGump(new TmanUseGump(from, ((TmanAddon)Addon)));
                }
                else
                    from.SendMessage("You must be the house owner to access that.");
            }
            else
                from.SendMessage("You must be closer. ");
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

  /*  public class TmanRune : AddonComponent
    {
        [Constructable]
        public TmanRune()
            : this(3686)
        {
            Name = "a tman stone";
            Hue = 153;
        }

        public TmanRune(int itemID)
            : base(itemID)
        {
        }
        public TmanRune(Serial serial)
        : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }*/
    }//end namespace