// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsmqTokenSender.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.MSMQ
{
    using System;
    using Experimental.System.Messaging;

    /// <summary>
    /// Msmq Token Sender
    /// </summary>
    public class MsmqTokenSender
    {
        /// <summary>
        /// Sends the MSMQ token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        public void SendMsmqToken(string email, string token)
        {
            //// Create the instance of MessageQueue
            MessageQueue MyQueue;

            //// Check if the Queue is exist or not, if not exist create a new queue
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                MyQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                //// Create the new Instance of queue
                MyQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }

            try
            {
                //// Here send() send the data into queue 
                MyQueue.Send(email, token);
            }
            catch (MessageQueueException mqe)
            {
                Console.Write(mqe.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                MyQueue.Close();
            }
        }
    }
}