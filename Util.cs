public abstract class Util {
    public static string LeftPad(int value, int desiredSize)
    {
		var isNegative = value < 0;

		if (isNegative)
			value *= -1;
		
		string valueStr = value.ToString();

		for (int i = valueStr.Length; i < desiredSize; i++) {
			valueStr = "0" + valueStr;
		}

		if (isNegative)
			valueStr = "-" + valueStr;
		
		return valueStr;
    }

	public static string FormatTime(int sec)
	{
		int seconds = sec % 60;
		int minutes = sec / 60;

		if (seconds < 10)
			return minutes + ":0" + seconds;
			
		return minutes + ":" + seconds;
	}
}