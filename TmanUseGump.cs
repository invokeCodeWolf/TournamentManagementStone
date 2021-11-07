using System;
using System.Collections;
using System.Text.RegularExpressions;

using Server;
using Server.Gumps;
using Server.Items;
using Server.Multis;
using Server.Prompts;
using Server.Network;

namespace Server.Gumps {

    public class TmanUseGump : Gump
    {
        private enum Buttons
        {
            EXIT,
            START,
            TOURNEYNAME,
            TOURNEYTIME,
            ADDPAYOUT,
            STRUCTUREPAYOUT,
            BACK,
            CONFIRMSTART,
            MANAGE,
            ADDPARTICIPANT,
            EDITNAME,
            EDITTIME,
            STARTNOW,
            VIEW,
            VIEWPARTICIPANT,
            VIEWBRACKET,
            VIEWRESULTS,
            DONATE,
            TEST
        }

        private enum Pages
        {
            MAIN,
            START,
            MANAGE,
            VIEW,
            TEST
        }

        private Mobile m_mobile;
        private TmanAddon m_Tmanstone;

        private void configureGump()
        {
            int height = 450;
            int width = 450;
            AddPage(0);
            AddBackground(10, 10, width, height, 0x242C);

        }


        public TmanUseGump(Mobile mobile, TmanAddon stone)
            : this(mobile, stone, Pages.MAIN)
        {
        }
        private TmanUseGump(Mobile mobile, TmanAddon stone, TmanUseGump.Pages page)
                : base(50, 50)
        {
            mobile.CloseGump(typeof(TmanUseGump));
            m_mobile = mobile;
            m_Tmanstone = stone;

            //m_Tmanstone = tmanaddon;
            //Buttons 0x15E1, 0x15E5, 4005, 4006

            configureGump();

            switch (page)
            {
                case Pages.MAIN:
                    MainPage();
                    break;
                case Pages.START:
                    StartPage();
                    break;
                case Pages.MANAGE:
                    ManagePage();
                    break;
                case Pages.VIEW:
                    ViewPage();
                    break;
                case Pages.TEST:
                    TestPage();
                    break;
            }//End Switch
        }//End TmanUseGump Function

        private void MainPage()
        {
            int width = 450;
            AddHtml(10, 20, width, 50, "<div align=CENTER>Tournament Management Stone</div>", false, false);
            AddHtml(10, 35, width, 50, string.Format("<div align=CENTER>Version 0.1</div>"), false, false);

            AddButton(30, 70, 4005, 4006, (int)Buttons.START, GumpButtonType.Reply, 0);
            AddHtml(70, 70, width, 50, string.Format("Start New Tournament"), false, false);

            AddButton(30, 105, 4005, 4006, (int)Buttons.MANAGE, GumpButtonType.Reply, 0);
            AddHtml(70, 105, width, 50, string.Format("Manage Existing Tournament"), false, false);

            AddButton(30, 140, 4005, 4006, (int)Buttons.VIEW, GumpButtonType.Reply, 0);
            AddHtml(70, 140, width, 50, string.Format("View Current Tournament"), false, false);

            AddButton(30, 175, 4005, 4006, 1, GumpButtonType.Reply, 0);
            AddHtml(70, 175, width, 50, string.Format("View Leaderboard"), false, false);
        }

        private void TestPage()
        {
            AddHtml(70, 70, 450, 50, string.Format("Start New Tournament"), false, false);
        }

        private void StartPage()
        {
            int width = 450;
            AddHtml(10, 20, width, 50, "<div align=CENTER>Tournament Management Stone</div>", false, false);
            AddHtml(10, 35, width, 50, string.Format("<div align=CENTER>Version 0.1</div>"), false, false);

            AddButton(30, 70, 4005, 4006, (int)Buttons.TOURNEYNAME, GumpButtonType.Reply, 0);
            AddHtml(70, 70, 450, 50, string.Format("Set Tournament Name"), false, false);

            AddButton(30, 105, 4005, 4006, (int)Buttons.TOURNEYTIME, GumpButtonType.Reply, 0);
            AddHtml(70, 105, 450, 50, string.Format("Set Tournament Timer"), false, false);

            AddButton(30, 140, 4005, 4006, (int)Buttons.ADDPAYOUT, GumpButtonType.Reply, 0);
            AddHtml(70, 140, 450, 50, string.Format("Add Tournament Payout"), false, false);

            AddButton(30, 175, 4005, 4006, (int)Buttons.STRUCTUREPAYOUT, GumpButtonType.Reply, 0);
            AddHtml(70, 175, 450, 50, string.Format("Tournament Payout Structure"), false, false);

            AddButton(30, 210, 4005, 4006, (int)Buttons.BACK, GumpButtonType.Reply, 0);
            AddHtml(70, 210, 450, 50, string.Format("Back"), false, false);

            AddButton(340, 210, 4005, 4006, (int)Buttons.CONFIRMSTART, GumpButtonType.Reply, 0);
            AddHtml(380, 210, 450, 50, string.Format("Confirm"), false, false);
        }

        private void ManagePage()
        {
            int width = 450;
            AddHtml(10, 20, width, 50, "<div align=CENTER>Tournament Management Stone</div>", false, false);
            AddHtml(10, 35, width, 50, string.Format("<div align=CENTER>Version 0.1</div>"), false, false);

            AddButton(30, 70, 4005, 4006, (int)Buttons.ADDPARTICIPANT, GumpButtonType.Reply, 0);
            AddHtml(70, 70, 450, 50, string.Format("Add Tournament Participant"), false, false);

            AddButton(30, 105, 4005, 4006, (int)Buttons.EDITNAME, GumpButtonType.Reply, 0);
            AddHtml(70, 105, 450, 50, string.Format("Edit Tournament Name"), false, false);

            AddButton(30, 140, 4005, 4006, (int)Buttons.EDITTIME, GumpButtonType.Reply, 0);
            AddHtml(70, 140, 450, 50, string.Format("Edit Tournament Time"), false, false);

            AddButton(30, 210, 4005, 4006, (int)Buttons.BACK, GumpButtonType.Reply, 0);
            AddHtml(70, 210, 450, 50, string.Format("Back"), false, false);

            AddButton(340, 210, 4005, 4006, (int)Buttons.STARTNOW, GumpButtonType.Reply, 0);
            AddHtml(380, 210, 450, 50, string.Format("Start Now"), false, false);
        }

        private void ViewPage()
        {
            int width = 450;
            AddHtml(10, 20, width, 50, "<div align=CENTER>Tournament Management Stone</div>", false, false);
            AddHtml(10, 35, width, 50, string.Format("<div align=CENTER>Version 0.1</div>"), false, false);

            AddButton(30, 70, 4005, 4006, (int)Buttons.VIEWPARTICIPANT, GumpButtonType.Reply, 0);
            AddHtml(70, 70, 450, 50, string.Format("View Participants"), false, false);

            AddButton(30, 105, 4005, 4006, (int)Buttons.VIEWBRACKET, GumpButtonType.Reply, 0);
            AddHtml(70, 105, 450, 50, string.Format("View Bracket"), false, false);

            AddButton(30, 140, 4005, 4006, (int)Buttons.VIEWRESULTS, GumpButtonType.Reply, 0);
            AddHtml(70, 140, 450, 50, string.Format("View Results"), false, false);

            AddButton(30, 175, 4005, 4006, (int)Buttons.DONATE, GumpButtonType.Reply, 0);
            AddHtml(70, 175, 450, 50, string.Format("Donate to Prize Pool"), false, false);

            AddButton(30, 210, 4005, 4006, (int)Buttons.BACK, GumpButtonType.Reply, 0);
            AddHtml(70, 210, 450, 50, string.Format("Back"), false, false);
        }
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            int button = info.ButtonID;
            switch (button)
            {
                case (int)Buttons.EXIT:
                    break;
                case (int)Buttons.START:
                    m_mobile.SendGump(new TmanUseGump(m_mobile, m_Tmanstone, Pages.START));
                    break;
                case (int)Buttons.MANAGE:
                    m_mobile.SendGump(new TmanUseGump(m_mobile, m_Tmanstone, Pages.MANAGE));
                    break;
                case (int)Buttons.VIEW:
                    m_mobile.SendGump(new TmanUseGump(m_mobile, m_Tmanstone, Pages.VIEW));
                    break;
                case (int)Buttons.BACK:
                    m_mobile.SendGump(new TmanUseGump(m_mobile, m_Tmanstone, Pages.MAIN));
                    break;
                case (int)Buttons.TEST:
                    m_mobile.SendGump(new TmanUseGump(m_mobile, m_Tmanstone, Pages.TEST));
                    break;
            }//End Switch

        }//End On Response

    }//End TmanUseGump Class
}//End Server.Gumps namespace