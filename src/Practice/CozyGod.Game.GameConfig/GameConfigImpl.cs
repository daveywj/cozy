﻿using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CozyGod.Game.GameConfig
{
    public class GameConfigImpl : IGameConfig
    {
        private ConfigObject ConfigObj { get; set; }

        public string GetContentPath()
        {
            if(ConfigObj != null)
            {
                return ConfigObj.ContentPath;

            }
            return null;
        }

        public void Init()
        {
            string filepath = "./CozyGod.json";
            ReadFile(filepath);
        }

        private void ReadFile(string filepath)
        {
            if(!File.Exists(filepath))
            {
                using (var writer = new StreamWriter(filepath))
                {
                    ConfigObj = new ConfigObject();
                    writer.Write(JsonConvert.SerializeObject(ConfigObj));
                }
            }

            using (var reader = new StreamReader(filepath))
            {
                ConfigObj = JsonConvert.DeserializeObject<ConfigObject>(reader.ReadToEnd());
            }
        }
    }
}
