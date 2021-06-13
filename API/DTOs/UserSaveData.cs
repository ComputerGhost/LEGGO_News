using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserSaveData
    {
        public string Username { get; set; }

        /// <summary>
        /// Leave this blank to not save a new password.
        /// </summary>
        public string Password { get; set; }

        public string DisplayName { get; set; }
    }
}
