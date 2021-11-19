using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NBMFilteringService.Test
{
    [TestClass]
    public class NBMFSTest
    {
        [TestMethod]
        public void TestSMSCategorisation()
        {
            string id = "S123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "+447413444615", "Testing SMS Categorisation", "This is to test correct categorisation");
            Assert.AreEqual("S123333333", DAO.smsList[0].ID);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestEmailCategorisation()
        {
            string id = "E123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "suparnodeb@hotmail.co.uk", "Testing Email Categorisation", "This is to test correct categorisation");
            Assert.AreEqual("E123333333", DAO.emailList[0].ID);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestTweetCategorisation()
        {
            string id = "T123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "@YoYo", "Testing Tweet Categorisation", "This is to test correct categorisation");
            Assert.AreEqual("T123333333", DAO.tweetList[0].ID);

            DAO.ClearList(); 
        }

        [TestMethod]
        public void TestSMS()
        {
            string id = "S123456789";
            string sender = "+447373474747";
            string subject = "Test SMS";
            string body = "Test to check if SMS message has been properly processed";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, sender, subject, body);

            id = "S987654321";
            sender = "+447363666363";
            subject = "2nd SMS message";
            body = "Test to check if 2nd SMS message has been properly processed";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, sender, subject, body);

            Assert.AreEqual("S123456789", DAO.smsList[0].ID);
            Assert.AreEqual("+447373474747", DAO.smsList[0].Sender);
            Assert.AreEqual("Test SMS", DAO.smsList[0].Subject);
            Assert.AreEqual("Test to check if SMS message has been properly processed", DAO.smsList[0].Text);

            Assert.AreEqual("S987654321", DAO.smsList[1].ID);
            Assert.AreEqual("+447363666363", DAO.smsList[1].Sender);
            Assert.AreEqual("2nd SMS message", DAO.smsList[1].Subject);
            Assert.AreEqual("Test to check if 2nd SMS message has been properly processed", DAO.smsList[1].Text);
            DAO.ClearList();
        }

        [TestMethod]
        public void TestEmail()
        {
            string id = "E123456789";
            string sender = "suparnodeb@hotmail.co.uk";
            string subject = "Test Email";
            string body = "Test to check if Email message has been properly processed";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, sender, subject, body);

            id = "E987654321";
            sender = "debsuparno@hotmail.co.uk";
            subject = "2nd Email message";
            body = "Test to check if 2nd Email message has been properly processed";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, sender, subject, body);

            Assert.AreEqual("E123456789", DAO.emailList[0].ID);
            Assert.AreEqual("suparnodeb@hotmail.co.uk", DAO.emailList[0].Sender);
            Assert.AreEqual("Test Email", DAO.emailList[0].Subject);
            Assert.AreEqual("Test to check if Email message has been properly processed", DAO.emailList[0].Text);

            Assert.AreEqual("E987654321", DAO.emailList[1].ID);
            Assert.AreEqual("debsuparno@hotmail.co.uk", DAO.emailList[1].Sender);
            Assert.AreEqual("2nd Email message", DAO.emailList[1].Subject);
            Assert.AreEqual("Test to check if 2nd Email message has been properly processed", DAO.emailList[1].Text);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestTweet()
        {
            string id = "T123456789";
            string sender = "@YoYo";
            string body = "Test to check if Tweet message has been properly processed";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, sender, null, body);

            id = "T987654321";
            sender = "@UserTweet";
            body = "Test to check if 2nd Tweet message has been properly processed";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, sender, null, body);

            Assert.AreEqual("T123456789", DAO.tweetList[0].ID);
            Assert.AreEqual("@YoYo", DAO.tweetList[0].Sender);
            Assert.AreEqual("Test to check if Tweet message has been properly processed", DAO.tweetList[0].Text);

            Assert.AreEqual("T987654321", DAO.tweetList[1].ID);
            Assert.AreEqual("@UserTweet", DAO.tweetList[1].Sender);
            Assert.AreEqual("Test to check if 2nd Tweet message has been properly processed", DAO.tweetList[1].Text);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestSMSAbbreviationExpansion()
        {
            DAO.LoadDict();
            string id = "S123333333";
            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "+447413444615", "Testing SMS Abbreviation Expansion", "LOL AAP BTW");
            Assert.AreEqual("LOL <Laughing out loud> AAP <Always a pleasure> BTW <By the way>", DAO.smsList[0].Text);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestTweetAbbreviationExpansion()
        {
            DAO.LoadDict();
            string id = "T123333333";
            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "@YoYo", "Testing Tweet Abbreviation Expansion", "LMAO Hello World FYI");
            Assert.AreEqual("LMAO <Laughing my a** off> Hello World FYI <For your information>", DAO.tweetList[0].Text);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestEmailURLQuarantine()
        {
            string id = "E123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "suparnodeb@hotmail.co.uk", "Testing Email URL Quarantine", "Today I went on www.google.com and then I was redirected to www.bing.com");
            Assert.AreEqual("Today I went on <URL Quarantined> and then I was redirected to <URL Quarantined>", DAO.emailList[0].Text);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestQuarantineList()
        {
            string id = "E123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "suparnodeb@hotmail.co.uk", "Testing to check if URL's are written to the quarantine list", "Today I went on www.google.com and then I was redirected to www.bing.com");
            Assert.AreEqual("www.google.com", DAO.quarantineList[0]);
            Assert.AreEqual("www.bing.com", DAO.quarantineList[1]);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestSIR()
        {
            MessageProcess.CheckSIR();
            Assert.IsTrue(MessageProcess.isSIR());

            MessageProcess.uncheckSIR();
            Assert.IsFalse(MessageProcess.isSIR());
        }

        [TestMethod]
        public void TestSIRList()
        {
            string sortCode = "11-12-13";
            string natureOfIncident = "theft";
            MessageProcess.addSIR(sortCode, natureOfIncident);

            Assert.AreEqual("11-12-13", DAO.SIRList[0].SortCode);
            Assert.AreEqual("theft", DAO.SIRList[0].NatureOfIncident);

            DAO.ClearList();
        }

        [TestMethod]
        public void TestFilePath()
        {
            MessageProcess.setFilePath("C:/Users/supar/Downloads");
            Assert.AreEqual("C:/Users/supar/Downloads", MessageProcess.getFilePath());
        }

        [TestMethod]
        public void TestResetType()
        {
            string id = "E123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.ResetType();
            MessageProcess.SanitiseMessage(id, "suparnodeb@hotmail.co.uk", "Testing Message Type Reset", "Message should not be processed");

            foreach(Email e in DAO.emailList)
            {
                Assert.AreNotEqual("E123333333", e.ID);
            }

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "suparnodeb@hotmail.co.uk", "Testing Message Type Reset", "Message should be processed");

            foreach (Email e in DAO.emailList)
            {
                Assert.AreEqual("E123333333", e.ID);
            }
            DAO.ClearList();
        }

        [TestMethod]
        public void TestGroupHashtagList()
        {
            string id = "T123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "@YoYo", "Testing Hashtag Record", "This is to test that hashtags are added to #hashtag #list");
            MessageProcess.GroupHashtagList();

            Assert.AreEqual("#hashtag", DAO.hashtagList[0]);
            Assert.AreEqual("#list", DAO.hashtagList[1]);
            DAO.ClearList();
        }

        [TestMethod]
        public void TestHashtagCountInList()
        {
            string id = "T123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "@YoYo", "Testing Hashtag Count", "This is a test to see if #hashtag are counted properly in #hashtag #hashtag #list");
            MessageProcess.GroupHashtagList();

            Assert.AreEqual(3, DAO.groupedHashtagList[0].Count);
            Assert.AreEqual(1, DAO.groupedHashtagList[1].Count);
            DAO.ClearList();
        }

        [TestMethod]
        public void TestGroupMentionsList()
        {
            string id = "T123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "@YoYo", "Testing Mentions Record", "@Eden @Hazard come back to CFC");
            MessageProcess.GroupMentionsList();

            Assert.AreEqual("@Eden", DAO.mentionsList[0]);
            Assert.AreEqual("@Hazard", DAO.mentionsList[1]);
            DAO.ClearList();
        }

        [TestMethod]
        public void TestMentionsCountInList()
        {
            string id = "T123333333";

            MessageProcess.CategoriseMessage(id);
            MessageProcess.SanitiseMessage(id, "@YoYo", "Testing Mentions Count", "@Chelsea is the best club in the world. @Chelsea is European Champions. Whereas, @Arsenal and @Spurs are really poor at the moment");
            MessageProcess.GroupMentionsList();

            Assert.AreEqual(2, DAO.groupedMentionsList[0].Count);
            Assert.AreEqual(1, DAO.groupedMentionsList[1].Count);
            Assert.AreEqual(1, DAO.groupedMentionsList[2].Count);
            DAO.ClearList();
        }

        [TestMethod]
        public void TestPopulateIncidentList()
        {
            DAO.populateIncidentList();

            Assert.AreEqual("theft", DAO.incidentsList[0]);
            Assert.AreEqual("staff attack", DAO.incidentsList[1]);
            Assert.AreEqual("atm theft", DAO.incidentsList[2]);
            Assert.AreEqual("raid", DAO.incidentsList[3]);
            Assert.AreEqual("customer attack", DAO.incidentsList[4]);
            Assert.AreEqual("staff abuse", DAO.incidentsList[5]);
            Assert.AreEqual("bomb threat", DAO.incidentsList[6]);
            Assert.AreEqual("terrorism", DAO.incidentsList[7]);
            Assert.AreEqual("suspicious incident", DAO.incidentsList[8]);
            Assert.AreEqual("intelligence", DAO.incidentsList[9]);
            Assert.AreEqual("cash loss", DAO.incidentsList[10]);

            DAO.ClearList();
        }
    }
}
