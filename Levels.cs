using System;
using System.Collections.Generic;
public static class Levels {
    public enum Lvl {Easy, Medium, Hard};
    public enum Info {Speed, MaxSpeed, MinSpeed, ArrowInterval, InitialHealth, LoseHealthSpeed};

    public static Lvl SelectedLevel = Lvl.Easy;
    public static int getLevelInfo(Info info) {
        if (SelectedLevel == Lvl.Easy) {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 150 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 60 },
                { Info.LoseHealthSpeed, 1},
                { Info.ArrowInterval, 20},
                { Info.InitialHealth, 10}
            };
            return ret[info];
        }
        else if (SelectedLevel == Lvl.Medium) {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 200 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 80 },
                { Info.LoseHealthSpeed, 3},
                { Info.ArrowInterval, 40},
                { Info.InitialHealth, 6}
            };
            return ret[info];
        }
        else {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 300 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 300 },
                { Info.LoseHealthSpeed, 5},
                { Info.ArrowInterval, 60},
                { Info.InitialHealth, 4}
            };
            return ret[info];
        }
    }
}