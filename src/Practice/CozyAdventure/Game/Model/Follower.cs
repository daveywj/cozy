﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Model
{
    public class Follower
    {
        // 名字
        public string Name { get; set; }

        // 描述
        public string Desc { get; set; }

        // 星级
        public int MaxStar { get; set; } = 0;
        public int CurStar { get; set; } = 0;

        // 等级
        public int CurLevel { get; set; } = 0;

        // 阶层
        public int CurRank { get; set; } = 0;
    }
}