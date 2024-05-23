using System;
using System.Collections.Generic;
public static class Levels {
    public enum Lvl {Easy, Medium, Hard};
    public enum Info {Speed, MaxSpeed, MinSpeed};

    public static Lvl SelectedLevel = Lvl.Easy;
    public static int getLevelInfo(Info info) {
        if (SelectedLevel == Lvl.Easy) {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 150 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 60 },
            };
            return ret[info];
        }
        else if (SelectedLevel == Lvl.Medium) {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 200 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 80 },
            };
            return ret[info];
        }
        else {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 300 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 200 },
            };
            return ret[info];
        }
    }
}