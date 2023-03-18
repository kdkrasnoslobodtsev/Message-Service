using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Peergrade7.Models
{
    [DataContract]
    public class Post
    {
        /// <summary>
        /// Тема сообщения.
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Отправитель сообщения.
        /// </summary>
        [DataMember]
        public string SenderId { get; set; }

        /// <summary>
        /// Получтель сообщения.
        /// </summary>
        [DataMember]
        public string ReceiverId { get; set; }
        
        /// <summary>
        /// Конструктор с 4 параметрами.
        /// </summary>
        /// <param name="subject">Тема</param>
        /// <param name="message">Текст</param>
        /// <param name="sender">Отправитель</param>
        /// <param name="receiver">Получатель</param>
        public Post(string subject, string message, string sender, string receiver)
        {
            (Subject, Message, SenderId, ReceiverId) = (subject, message, sender, receiver);
        }
    }
}
