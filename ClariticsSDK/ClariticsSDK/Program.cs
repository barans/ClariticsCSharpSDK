using System;
using System.Threading;

//ADD DLL AND REFERENCE
using ClariticsSDK;
using System.Collections.Generic;


namespace ClariticsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            ClariticsClient client = new ClariticsClient("CLARITICS API KEY", false);
            Random rand = new Random();
            int sessionId = rand.Next();
            int playerLevel = 1;
            //FACEBOOK ID OF USER
            string currentUserId = "579366428";

            //our current user started a session and he is 26 years old male lives in turkey.
            client.NewSession(currentUserId, sessionId, playerLevel, 26, "M", "Turkey");

            //user registered with a session id, came from github ref, 
            client.NewUser(currentUserId, sessionId, "github", "http://www.github.com");

            //our current user sent "GiftItem" to player Ids 15, 17, 18
            string[] users = { "15", "17", "18" };
            client.SendGift(currentUserId, sessionId, playerLevel, "GiftItem", users);

            //player with id 15 accepted the "GiftItem" from our current user in 2500 milliseconds
            //TODO: delay in milliseconds?
            client.AcceptGift("15", sessionId, playerLevel, "GiftItem", currentUserId, 2500);

            //our current user sent a wall post type "share coin" in his session when he is in level 1
            client.WallPostSent(currentUserId, sessionId, playerLevel, "share coin");

            //our current user clicked a wall post type "share coin" of player with id "15" in 2346 milliseconds

            client.WallPostAccepted(currentUserId, sessionId, playerLevel, "share coin", "15", 2346);

            //our current user sent a request type "invite friend" to users 15,17,18 from above list.

            client.RequestSent(currentUserId, sessionId, playerLevel, "invite friend", users);

            //player with id "15" accepted "invite friend" request sent by our current user in 153163 milliseconds?

            client.RequestAccepted("15", sessionId, playerLevel, "invite friend", currentUserId, 153163);

            //our current user visited default.aspx page.
            //TODO: pagetype and entryflag not certain do we just make up?
            client.PageView(currentUserId, sessionId, playerLevel, "Default.aspx", 0, 0);

            //our current user sold 2 "gameItem" item with price 200 at location 4
            //TODO: soldFrom uncertain do we just make up?

            client.SellItem(currentUserId, sessionId, playerLevel, 4, 200, "gameItem", 2);

            //our current user used "gameItem"

            client.UseItem(currentUserId, sessionId, playerLevel, "gameItem");

            //our current user bought 3 "gameItem" with 240 coins at location 1
            //TODO: boughtFrom uncertain do we just make up?

            client.BuyItem(currentUserId, sessionId, playerLevel, 1, "coins", 240, "gameItem", 3);


            //our current user entered region id 8
            //TODO: regionId uncertain. do we just make up?

            client.EnterRegion(currentUserId, sessionId, playerLevel, 8);

            //our current user entered stage id R2S4V3 with powerups StrengthItem DiamondItem StrengthBonus
            //TODO: stageId uncertain. powerups are user's powerups or the stage powerups?

            client.EnterStage(currentUserId, sessionId, playerLevel, "R2S4V3",
                              "StrengthItem DiamondItem StrengthBonus");

            //our current user earned an achievement name "firstachievement"

            client.Achievement(currentUserId, sessionId, playerLevel, "firstachievement");

            //our current user leveled up to 14
            //TODO: current level or next level?

            client.LevelUp(currentUserId, sessionId, 14);

            //our current user made a successful payment via "Paypal". Local currency total is 5.1 TRY ~ 2.4 USD. 1000 Coins are bought and 0.04$ is cut as fee amount

            client.Payment(currentUserId, sessionId, playerLevel, "Paypal", true, 5.1, "TRY", 2.4, "Coins", 1000,
                           0.04);

            client.CustomEvent(currentUserId, sessionId, playerLevel, 10001, new Dictionary<string, object>()
            {
                {"your defined property", "your value"},
                {"your other property", 3}
            });

        }
    }
}
