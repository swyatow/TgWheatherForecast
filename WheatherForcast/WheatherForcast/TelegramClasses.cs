﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheatherForcast
{

    public class Update
    {
        public bool ok { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public int update_id { get; set; }
        public Message message { get; set; }
        public Message edited_message { get; set; }
    }

    public class Message
    {
        public int message_id { get; set; }
        public From from { get; set; }
        public Chat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
    }

    public class From
    {
        public int id { get; set; }
        public bool is_bot { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public string language_code { get; set; }
    }

    public class Chat
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string username { get; set; }
        public string type { get; set; }
    }


}
