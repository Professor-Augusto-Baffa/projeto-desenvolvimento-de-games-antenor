using System;
using System.Collections.Generic;
public static class Levels {
    public enum Lvl {Easy, Medium, Hard};
    public enum Info {Speed, MaxSpeed, MinSpeed, OutOfPathFactor, ArrowInterval, MovableRange, InitialHealth};

    public static Lvl SelectedLevel = Lvl.Easy;
    public static int getLevelInfo(Info info) {
        if (SelectedLevel == Lvl.Easy) {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 150 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 60 },
                { Info.OutOfPathFactor, 1},
                { Info.ArrowInterval, 20},
                { Info.MovableRange, 50 },
                { Info.InitialHealth, 7}
            };
            return ret[info];
        }
        else if (SelectedLevel == Lvl.Medium) {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 200 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 80 },
                { Info.OutOfPathFactor, 5},
                { Info.ArrowInterval, 40},
                { Info.MovableRange, 100 },
                { Info.InitialHealth, 5}
            };
            return ret[info];
        }
        else {
            var ret = new Dictionary<Info, int>
            {
                { Info.Speed, 300 },
                { Info.MaxSpeed, 900 },
                { Info.MinSpeed, 300 },
                { Info.OutOfPathFactor, 10},
                { Info.ArrowInterval, 60},
                { Info.MovableRange, 150 },
                { Info.InitialHealth, 3}
            };
            return ret[info];
        }
    }
}