// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FireBaseNotification.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.Model
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using Nancy.Json;

    /// <summary>
    /// FireBaseNotification
    /// </summary>
    public class FireBaseNotification
    {
        /// <summary>
        /// Notifications the specified note description.
        /// </summary>
        /// <param name="noteDescription">The note description.</param>
        /// <returns>noteDescription</returns>
        public NotesModel Notification(NotesModel noteDescription)
        {
            var applicationId = "fundooproject - 5057d";
            var senderId = 990503402753;
            
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";

            var data = new
            {
                //// to = "";
                notification = new
                {
                    body = noteDescription           
                }
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);

            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationId));
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentLength = byteArray.Length;
            
            //// Create the instance of Stream
            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
           //// dataStream.Close();
         
            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            dataStream.Close();           
            tResponse.Close();
            return noteDescription;            
        }
    }
}