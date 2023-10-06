using Contracts.ServiceContracts;
using GaHealthcareNurses.Entity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PushNotificationService : IPushNotificationService
    {
        //public async Task<bool> SendNotification(string to, string title, string body)
        //{
        //    string applicationID = "AAAALBL83q4:APA91bEFUA4tJexbSuTHI8oBorHvtqC6_ss7zCqkypWCExczIF7-l2X7IfqWrR_5xxFOil-Eb8umbNzxSzGER59nC-54KVAMteCH_VTCOkKfWELjWrIw2Nwn0xh5xFfGuvBme09sGPyK";
        //    string senderId = "189297122990";
        //    string deviceId = "e8EHtMwqsZY:APA91bFUktufXdsDLdXXXXXX..........XXXXXXXXXXXXXX";
        //    try
        //    {
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";
        //        var data = new
        //        {
        //            to = deviceId,
        //            notification = new
        //            {
        //                body = "New assessment created",
        //                title = "Assessment",
        //            },
        //        };
        //        string jsonNotificationFormat = JsonConvert.SerializeObject(data);
        //        byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;
        //        bool status = false;
        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        string sResponseFromServer = tReader.ReadToEnd();
        //                        //string str = sResponseFromServer;
        //                        FCMResponse response = JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
        //                        if (response.success == 1)
        //                            status = true;
        //                        else if (response.failure == 1)
        //                            status = false;
        //                    }
        //                }
        //            }
        //        }
        //        return status;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    //string content = string.Empty;
        //    //HttpClient httpClient = null;
        //    //HttpResponseMessage response = null;

        //    //try
        //    //{
        //    //    string serverApiKey = "FireBaseServerApiKey"; // Get this from your Firebase Developer Console Login  
        //    //    string senderId = "FirebaseSenderId"; // Get this from your Firebase Developer Console Login  
        //    //    string apiEndpoint = "https://fcm.googleapis.com/fcm/notification";

        //    //    using (httpClient = new HttpClient())
        //    //    {
        //    //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", (string.Format("key={0}", serverApiKey)));
        //    //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("project_id", senderId);

        //    //        var dynamicPostData = new
        //    //        {
        //    //            operation = "create",
        //    //            notification_key_name = notificationKey,
        //    //            registration_ids = new List<string>() { fcmToken }
        //    //        };

        //    //        response = httpClient.PostAsJsonAsync(new Uri(apiEndpoint), dynamicPostData).Result;
        //    //        content = await response.Content.ReadAsStringAsync();
        //    //        response.EnsureSuccessStatusCode();

        //    //        FCMGroupResponse resp = JsonConvert.DeserializeObject<FCMGroupResponse>(content);
        //    //        return resp.notification_key;
        //    //    }
        //    //}
        //}

        public async Task<bool> SendNotification(object data)
        {
            try
            {
                //integrate FCM config.
                string server_apiKey = "AAAAAYc_K1E:APA91bGEhr5KUgfuoyoqRVHDuJmjisZ2e6Nd7YqljzSGXJM_EwOj9yBRxkqwzRQ1dYk8_CNMacCXTxHLWrHGfQIWq4C3r3jtqI0kKRCgx9_xwYAXF7hLIN9qXDQRW9N3qAo0B6S2B9F_";
                string sender_id = "6564031313";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization:key={server_apiKey}");
                tRequest.Headers.Add($"Sender:id={sender_id}");

                string jsonData = JsonConvert.SerializeObject(data);
                byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
                tRequest.ContentLength = byteArray.Length;
                bool status = false;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                string sResponseFromServer = tReader.ReadToEnd();
                                FCMResponse response = JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                                if (response.success == 1)
                                    status = true;
                                else if (response.failure == 1)
                                    status = false;
                            }
                        }
                    }
                }
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendNotificationToAllNurses(List<Nurse> nurses, object data)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    foreach (var nurse in nurses)
                    {
                        if (!string.IsNullOrEmpty(nurse.FirebaseToken))
                        {
                            if (nurse.IsUserAvailableForJob)
                            {
                                SendNotification(new
                                {
                                    priority = "high",
                                    to = nurse.FirebaseToken,
                                    notification = data
                                });
                            }
                        }
                    }
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        public class FCMResult
        {
            public string message_id { get; set; }
        }
    }
}
