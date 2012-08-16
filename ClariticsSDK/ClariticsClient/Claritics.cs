using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace ClariticsSDK
{
    public class ClariticsClient
    {
        public string ApiKey { get; set; }
        public string Url { get; set; }

        public ClariticsClient(string apiKey, bool https)
        {
            this.ApiKey = apiKey;
            this.Url = https ? "https://data2.claritics.com/load/data/" : "http://data2.claritics.com/load/data/";
            Console.WriteLine("constructor çağrıldı");
        }

        private ulong UnixTime()
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan span = DateTime.UtcNow.Subtract(unixEpoch);
            return Convert.ToUInt64(span.TotalMilliseconds);
        }
        
        private void CurlPostData(string json)
        {
            try
            {
                WebRequest request = WebRequest.Create(this.Url);
                request.Method = "POST";
                byte[] byteArray = Encoding.Default.GetBytes(json);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                Console.WriteLine(json);
                Console.WriteLine("---------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void NewUser(string userId, int sessionId, string refCode, string refUrl)
        {

            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("refCode", refCode);
            data.Add("refURL", refUrl);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 1);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));


        }

        public void SendGift(string userId, int sessionId, int playerLevel, string itemId, string[] sentUsers)
        {

            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("itemId", itemId);
            data.Add("sentTo", serializer.Serialize(sentUsers));

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 2);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs).Replace("\\\\", ""));
        }

        public void AcceptGift(string userId, int sessionId, int playerLevel, string itemId, string senderId, ulong delayInMilliSeconds)
        {

            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("itemId", itemId);
            data.Add("sender", senderId);
            data.Add("delay", delayInMilliSeconds);


            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 3);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void WallPostSent(string userId, int sessionId, int playerLevel, string wallpostType)
        {

            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("postType", wallpostType);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 4);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void WallPostAccepted(string userId, int sessionId, int playerLevel, string wallpostType, string senderId, ulong delayInMilliSeconds)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("postType", wallpostType);
            data.Add("sender", senderId);
            data.Add("delay", delayInMilliSeconds);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 5);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void RequestSent(string userId, int sessionId, int playerLevel, string requestType, string[] sentUsers)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("postType", requestType);
            data.Add("target", serializer.Serialize(sentUsers));

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 6);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs).Replace("\\\\", ""));
        }

        public void RequestAccepted(string userId, int sessionId, int playerLevel, string requestType, string senderId, ulong delayInMilliSeconds)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("postType", requestType);
            data.Add("sender", senderId);
            data.Add("delay", delayInMilliSeconds);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 7);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void PageView(string userId, int sessionId, int playerLevel, string pageName, int pageType, int pageEntryFlag)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("pageName", pageName);
            data.Add("pageType", pageType);
            data.Add("entryFlag", pageEntryFlag);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 8);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void SellItem(string userId, int sessionId, int playerLevel, int locationId, int itemPrice, string itemId, int quantity)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("soldFrom", locationId);
            data.Add("itemPrice", itemPrice);
            data.Add("itemId", itemId);
            data.Add("itemQty", quantity);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 21);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void UseItem(string userId, int sessionId, int playerLevel, string itemId)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("itemId", itemId);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 22);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void BuyItem(string userId, int sessionId, int playerLevel, int locationId, string gameCurrencyName, int itemPrice, string itemId, int quantity)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("boughtFrom", locationId);
            data.Add("itemId", itemId);

            Dictionary<string, int> itemPriceJson = new Dictionary<string, int>();
            itemPriceJson.Add(gameCurrencyName, itemPrice);
            data.Add("itemPrice", serializer.Serialize(itemPriceJson));
            
            data.Add("itemQty", quantity);
            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 23);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs).Replace("\\\\", ""));
        }

        public void EnterRegion(string userId, int sessionId, int playerLevel, int regionId)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("regionId", regionId);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 24);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void EnterStage(string userId, int sessionId, int playerLevel, string stageId, string powerUps)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("stageId", stageId);
            data.Add("powerups", powerUps);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 25);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void Achievement(string userId, int sessionId, int playerLevel, string achievementName)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("achievementName", achievementName);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 26);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void LevelUp(string userId, int sessionId, int playerLevel)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 27);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void Payment(string userId, int sessionId, int playerLevel, string paymentType, bool success, double amount, string currencyName, double usdAmount, string itemType, int itemAmount, double feeAmount)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("paymentType", playerLevel);
            data.Add("success", success);
            data.Add("amount", amount);
            data.Add("currency", currencyName);
            data.Add("usdAmount", usdAmount);
            data.Add("itemType", itemType);
            data.Add("itemQty", itemAmount);
            data.Add("feeAmount", feeAmount);


            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 33);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        public void NewSession(string userId, int sessionId, int playerLevel, int age, string gender, string country)
        {
            var serializer = new JavaScriptSerializer();
            var data = new Dictionary<string, object>();
            data.Add("playerLevel", playerLevel);
            data.Add("Age", age);
            data.Add("Country", country);
            data.Add("Gender", gender);

            var rows = new List<Dictionary<string, object>>();

            var item = new Dictionary<string, object>();
            item.Add("data", serializer.Serialize(data));
            item.Add("timeInMillis", this.UnixTime());
            item.Add("playerId", userId);
            item.Add("actionType", 34);
            item.Add("sessionId", sessionId);

            rows.Add(item);

            var fs = new Dictionary<string, object>();
            fs.Add("rows", rows);
            fs.Add("authCode", this.ApiKey);

            this.CurlPostData(serializer.Serialize(fs));
        }

        //TODO: Custom event fonksiyonu da eklenecek bir örnek clariticsden açılıp bakılacak
    }
}
