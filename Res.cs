using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class Res
{
	// Token: 0x060009C9 RID: 2505 RVA: 0x00095280 File Offset: 0x00093480
	public static void init()
	{
		if (Res.cosz != null)
		{
			return;
		}
		Res.cosz = new short[91];
		Res.tanz = new int[91];
		for (int i = 0; i <= 90; i++)
		{
			Res.cosz[i] = Res.sinz[90 - i];
			if (Res.cosz[i] == 0)
			{
				Res.tanz[i] = int.MaxValue;
			}
			else
			{
				Res.tanz[i] = ((int)Res.sinz[i] << 10) / (int)Res.cosz[i];
			}
		}
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x000952F4 File Offset: 0x000934F4
	public static int sin(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.sinz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)Res.sinz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.sinz[a - 180]);
		}
		return (int)(-(int)Res.sinz[360 - a]);
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x00095364 File Offset: 0x00093564
	public static int cos(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return (int)Res.cosz[a];
		}
		if (a >= 90 && a < 180)
		{
			return (int)(-(int)Res.cosz[180 - a]);
		}
		if (a >= 180 && a < 270)
		{
			return (int)(-(int)Res.cosz[a - 180]);
		}
		return (int)Res.cosz[360 - a];
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x000953D4 File Offset: 0x000935D4
	public static int tan(int a)
	{
		a = Res.fixangle(a);
		if (a >= 0 && a < 90)
		{
			return Res.tanz[a];
		}
		if (a >= 90 && a < 180)
		{
			return -Res.tanz[180 - a];
		}
		if (a >= 180 && a < 270)
		{
			return Res.tanz[a - 180];
		}
		return -Res.tanz[360 - a];
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x00095444 File Offset: 0x00093644
	public static int atan(int a)
	{
		for (int i = 0; i <= 90; i++)
		{
			if (Res.tanz[i] >= a)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x0009546C File Offset: 0x0009366C
	public static int angle(int dx, int dy)
	{
		int num;
		if (dx != 0)
		{
			num = Res.atan(global::Math.abs((dy << 10) / dx));
			if (dy >= 0 && dx < 0)
			{
				num = 180 - num;
			}
			if (dy < 0 && dx < 0)
			{
				num = 180 + num;
			}
			if (dy < 0 && dx >= 0)
			{
				num = 360 - num;
			}
		}
		else
		{
			num = ((dy <= 0) ? 270 : 90);
		}
		return num;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x0000871B File Offset: 0x0000691B
	public static int fixangle(int angle)
	{
		if (angle >= 360)
		{
			angle -= 360;
		}
		if (angle < 0)
		{
			angle += 360;
		}
		return angle;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000045EA File Offset: 0x000027EA
	public static sbyte[] TakeSnapShot()
	{
		return null;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0000873C File Offset: 0x0000693C
	public static void outz(string s)
	{
		Debug.Log(s);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0000874B File Offset: 0x0000694B
	public static void outz(string s, int logIndex)
	{
		Debug.Log(Res.LOG_CAT[logIndex] + s);
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00008766 File Offset: 0x00006966
	public static void err(string s)
	{
		Debug.LogError(s);
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x000045ED File Offset: 0x000027ED
	public static void outz2(string s)
	{
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x000045ED File Offset: 0x000027ED
	public static void onScreenDebug(string s)
	{
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x000045ED File Offset: 0x000027ED
	public static void paintOnScreenDebug(mGraphics g)
	{
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x000045ED File Offset: 0x000027ED
	public static void updateOnScreenDebug()
	{
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x00008775 File Offset: 0x00006975
	public static string changeString(string str)
	{
		return str;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x00006C81 File Offset: 0x00004E81
	public static string replace(string _text, string _searchStr, string _replacementStr)
	{
		return _text.Replace(_searchStr, _replacementStr);
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x00008778 File Offset: 0x00006978
	public static int xetVX(int goc, int d)
	{
		return Res.cos(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x0000878A File Offset: 0x0000698A
	public static int xetVY(int goc, int d)
	{
		return Res.sin(Res.fixangle(goc)) * d >> 10;
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0000879C File Offset: 0x0000699C
	public static int random(int a, int b)
	{
		if (a == b)
		{
			return a;
		}
		return a + Res.r.nextInt(b - a);
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x000087B3 File Offset: 0x000069B3
	public static int random(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x000954D0 File Offset: 0x000936D0
	public static int random_Am(int a, int b)
	{
		int num = a + Res.r.nextInt(b - a);
		if (Res.random(2) == 0)
		{
			num = -num;
		}
		return num;
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x000954FC File Offset: 0x000936FC
	public static int random_Am_0(int a)
	{
		int num;
		for (num = 0; num == 0; num = Res.r.nextInt() % a)
		{
		}
		return num;
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x00095520 File Offset: 0x00093720
	public static int s2tick(int currentTimeMillis)
	{
		int num = currentTimeMillis * 16 / 1000;
		if (currentTimeMillis * 16 % 1000 >= 5)
		{
			num++;
		}
		return num;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x000087C0 File Offset: 0x000069C0
	public static int distance(int x1, int y1, int x2, int y2)
	{
		return Res.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x000087D6 File Offset: 0x000069D6
	public static int getDistance(int x, int y)
	{
		return Res.sqrt(x * x + y * y);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0009554C File Offset: 0x0009374C
	public static int sqrt(int a)
	{
		if (a <= 0)
		{
			return 0;
		}
		int num = (a + 1) / 2;
		int num2;
		do
		{
			num2 = num;
			num = num / 2 + a / (2 * num);
		}
		while (global::Math.abs(num2 - num) > 1);
		return num;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x000087B3 File Offset: 0x000069B3
	public static int rnd(int a)
	{
		return Res.r.nextInt(a);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x000087E4 File Offset: 0x000069E4
	public static int abs(int i)
	{
		if (i > 0)
		{
			return i;
		}
		return -i;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x000087EE File Offset: 0x000069EE
	public static bool inRect(int x1, int y1, int width, int height, int x2, int y2)
	{
		return x2 >= x1 && x2 <= x1 + width && y2 >= y1 && y2 <= y1 + height;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00095580 File Offset: 0x00093780
	public static string[] split(string original, string separator, int count)
	{
		int num = original.IndexOf(separator);
		string[] array;
		if (num >= 0)
		{
			array = Res.split(original.Substring(num + separator.Length), separator, count + 1);
		}
		else
		{
			array = new string[count + 1];
			num = original.Length;
		}
		array[count] = original.Substring(0, num);
		return array;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x000955D0 File Offset: 0x000937D0
	public static string formatNumber(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 100000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			if (num > 0L)
			{
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 1000000L)
		{
			text2 = mResources.million;
			long num2 = number % 1000000L / 100000L;
			number /= 1000000L;
			text = number + string.Empty;
			if (num2 > 0L)
			{
				string text4 = text;
				text = string.Concat(new object[]
				{
					text4,
					",",
					num2,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else
		{
			text = number + string.Empty;
		}
		return text;
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x000956F0 File Offset: 0x000938F0
	public static string formatNumber2(long number)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		text = string.Empty;
		if (number >= 1000000000L)
		{
			text2 = mResources.billion;
			long num = number % 1000000000L / 10000000L;
			number /= 1000000000L;
			text = number + string.Empty;
			if (num >= 10L)
			{
				if (num % 10L == 0L)
				{
					num /= 10L;
				}
				string text3 = text;
				text = string.Concat(new object[]
				{
					text3,
					",",
					num,
					text2
				});
			}
			else if (num > 0L)
			{
				string text4 = text;
				text = string.Concat(new object[]
				{
					text4,
					",0",
					num,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 1000000L)
		{
			text2 = mResources.million;
			long num2 = number % 1000000L / 10000L;
			number /= 1000000L;
			text = number + string.Empty;
			if (num2 >= 10L)
			{
				if (num2 % 10L == 0L)
				{
					num2 /= 10L;
				}
				string text5 = text;
				text = string.Concat(new object[]
				{
					text5,
					",",
					num2,
					text2
				});
			}
			else if (num2 > 0L)
			{
				string text6 = text;
				text = string.Concat(new object[]
				{
					text6,
					",0",
					num2,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else if (number >= 10000L)
		{
			text2 = "k";
			long num3 = number % 1000L / 10L;
			number /= 1000L;
			text = number + string.Empty;
			if (num3 >= 10L)
			{
				if (num3 % 10L == 0L)
				{
					num3 /= 10L;
				}
				string text7 = text;
				text = string.Concat(new object[]
				{
					text7,
					",",
					num3,
					text2
				});
			}
			else if (num3 > 0L)
			{
				string text8 = text;
				text = string.Concat(new object[]
				{
					text8,
					",0",
					num3,
					text2
				});
			}
			else
			{
				text += text2;
			}
		}
		else
		{
			text = number + string.Empty;
		}
		return text;
	}

	// Token: 0x0400126D RID: 4717
	private static short[] sinz = new short[]
	{
		0,
		18,
		36,
		54,
		71,
		89,
		107,
		125,
		143,
		160,
		178,
		195,
		213,
		230,
		248,
		265,
		282,
		299,
		316,
		333,
		350,
		367,
		384,
		400,
		416,
		433,
		449,
		465,
		481,
		496,
		512,
		527,
		543,
		558,
		573,
		587,
		602,
		616,
		630,
		644,
		658,
		672,
		685,
		698,
		711,
		724,
		737,
		749,
		761,
		773,
		784,
		796,
		807,
		818,
		828,
		839,
		849,
		859,
		868,
		878,
		887,
		896,
		904,
		912,
		920,
		928,
		935,
		943,
		949,
		956,
		962,
		968,
		974,
		979,
		984,
		989,
		994,
		998,
		1002,
		1005,
		1008,
		1011,
		1014,
		1016,
		1018,
		1020,
		1022,
		1023,
		1023,
		1024,
		1024
	};

	// Token: 0x0400126E RID: 4718
	private static short[] cosz;

	// Token: 0x0400126F RID: 4719
	private static int[] tanz;

	// Token: 0x04001270 RID: 4720
	public static string[] LOG_CAT = new string[]
	{
		"<color=#ff0000ff>[  LOG_CAT  ]</color>",
		"<color=#ff0000ff>[LOG_SESSION]</color>",
		"<color=#ffff00ff>[LOG_SESSION]</color>",
		"<color=#ff0000ff>[LOG_MOBILE ]</color>",
		string.Empty
	};

	// Token: 0x04001271 RID: 4721
	public static int count;

	// Token: 0x04001272 RID: 4722
	public static bool isIcon;

	// Token: 0x04001273 RID: 4723
	public static bool isBig;

	// Token: 0x04001274 RID: 4724
	public static MyVector debug = new MyVector();

	// Token: 0x04001275 RID: 4725
	public static MyRandom r = new MyRandom();
}
