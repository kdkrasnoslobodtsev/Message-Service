using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Peergrade7.Models
{
    [DataContract]
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Конструктор с двумя параметрами
        /// </summary>
        /// <param name="userName">Имя</param>
        /// <param name="email">Идентификатор</param>
        public User(string userName, string email)
        {
            (UserName, Email) = (userName, email);
        }
    }
}
