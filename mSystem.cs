using System;
using System.Globalization;
using System.Text;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class mSystem
{
	// Token: 0x06000109 RID: 265 RVA: 0x000045ED File Offset: 0x000027ED
	public static void AddIpTest()
	{
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00004AD6 File Offset: 0x00002CD6
	public static void resetCurInapp()
	{
		mSystem.curINAPP = 0;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00004ADE File Offset: 0x00002CDE
	public static int getWidth(Image img)
	{
		if (mSystem.clientType == 5)
		{
			return img.getWidth();
		}
		return img.getWidth();
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00004AF8 File Offset: 0x00002CF8
	public static int getHeight(Image img)
	{
		if (mSystem.clientType == 5)
		{
			return img.getHeight();
		}
		return img.getWidth();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000D504 File Offset: 0x0000B704
	public static string getTimeCountDown(long timeStart, int secondCount, bool isOnlySecond, bool isShortText)
	{
		string result = string.Empty;
		long num = (timeStart + (long)(secondCount * 1000) - mSystem.currentTimeMillis()) / 1000L;
		if (num <= 0L)
		{
			return string.Empty;
		}
		long num2 = 0L;
		long num3 = 0L;
		long num4 = num / 60L;
		long num5 = num;
		if (isOnlySecond)
		{
			return num5 + string.Empty;
		}
		if (num >= 86400L)
		{
			num2 = num / 86400L;
			num3 = num % 86400L / 3600L;
		}
		else if (num >= 3600L)
		{
			num3 = num / 3600L;
			num4 = num % 3600L / 60L;
		}
		else if (num >= 60L)
		{
			num4 = num / 60L;
			num5 = num % 60L;
		}
		else
		{
			num5 = num;
		}
		if (isShortText)
		{
			if (num2 > 0L)
			{
				return num2 + "d";
			}
			if (num3 > 0L)
			{
				return num3 + "h";
			}
			if (num4 > 0L)
			{
				return num4 + "m";
			}
			if (num5 > 0L)
			{
				return num5 + "s";
			}
		}
		if (num2 > 0L)
		{
			if (num2 >= 10L)
			{
				if (num3 < 1L)
				{
					result = num2 + "d";
				}
				else if (num3 < 10L)
				{
					result = string.Concat(new object[]
					{
						num2,
						"d0",
						num3,
						"h"
					});
				}
				else
				{
					result = string.Concat(new object[]
					{
						num2,
						"d",
						num3,
						"h"
					});
				}
			}
			else if (num2 < 10L)
			{
				if (num3 < 1L)
				{
					result = num2 + "d";
				}
				else if (num3 < 10L)
				{
					result = string.Concat(new object[]
					{
						num2,
						"d0",
						num3,
						"h"
					});
				}
				else
				{
					result = string.Concat(new object[]
					{
						num2,
						"d",
						num3,
						"h"
					});
				}
			}
		}
		else if (num3 > 0L)
		{
			if (num3 >= 10L)
			{
				if (num4 < 1L)
				{
					result = num3 + "h";
				}
				else if (num4 < 10L)
				{
					result = string.Concat(new object[]
					{
						num3,
						"h0",
						num4,
						"m"
					});
				}
				else
				{
					result = string.Concat(new object[]
					{
						num3,
						"h",
						num4,
						"m"
					});
				}
			}
			else if (num3 < 10L)
			{
				if (num4 < 1L)
				{
					result = num3 + "h";
				}
				else if (num4 < 10L)
				{
					result = string.Concat(new object[]
					{
						num3,
						"h0",
						num4,
						"m"
					});
				}
				else
				{
					result = string.Concat(new object[]
					{
						num3,
						"h",
						num4,
						"m"
					});
				}
			}
		}
		else if (num4 > 0L)
		{
			if (num4 >= 10L)
			{
				if (num5 >= 10L)
				{
					result = string.Concat(new object[]
					{
						num4,
						"m",
						num5,
						string.Empty
					});
				}
				else if (num5 < 10L)
				{
					result = string.Concat(new object[]
					{
						num4,
						"m0",
						num5,
						string.Empty
					});
				}
			}
			else if (num4 < 10L)
			{
				if (num5 >= 10L)
				{
					result = string.Concat(new object[]
					{
						num4,
						"m",
						num5,
						string.Empty
					});
				}
				else if (num5 < 10L)
				{
					result = string.Concat(new object[]
					{
						num4,
						"m0",
						num5,
						string.Empty
					});
				}
			}
		}
		else
		{
			result = ((num5 >= 10L) ? (num5 + string.Empty) : ("0" + num5 + string.Empty));
		}
		return result;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000DA08 File Offset: 0x0000BC08
	public static string numberTostring2(int aa)
	{
		string result;
		try
		{
			string text = string.Empty;
			string str = string.Empty;
			string text2 = aa + string.Empty;
			if (text2.Equals(string.Empty))
			{
				result = text;
			}
			else
			{
				if (text2[0] == '-')
				{
					str = "-";
					text2 = text2.Substring(1);
				}
				for (int i = text2.Length - 1; i >= 0; i--)
				{
					if ((text2.Length - 1 - i) % 3 == 0 && text2.Length - 1 - i > 0)
					{
						text = text2[i] + "." + text;
					}
					else
					{
						text = text2[i] + text;
					}
				}
				result = str + text;
			}
		}
		catch (Exception ex)
		{
			result = aa + string.Empty;
		}
		return result;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00004B12 File Offset: 0x00002D12
	public static string numberTostring(long number)
	{
		return number.ToString("#,0", new CultureInfo("vi-VN"));
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00004B2A File Offset: 0x00002D2A
	public static void callHotlinePC()
	{
		Application.OpenURL("http://ngocrongonline.com/");
	}

	// Token: 0x06000111 RID: 273 RVA: 0x000045ED File Offset: 0x000027ED
	public static void callHotlineJava()
	{
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000045ED File Offset: 0x000027ED
	public static void callHotlineIphone()
	{
	}

	// Token: 0x06000113 RID: 275 RVA: 0x000045ED File Offset: 0x000027ED
	public static void callHotlineWindowsPhone()
	{
	}

	// Token: 0x06000114 RID: 276 RVA: 0x000045ED File Offset: 0x000027ED
	public static void closeBanner()
	{
	}

	// Token: 0x06000115 RID: 277 RVA: 0x000045ED File Offset: 0x000027ED
	public static void showBanner()
	{
	}

	// Token: 0x06000116 RID: 278 RVA: 0x000045ED File Offset: 0x000027ED
	public static void createAdmob()
	{
	}

	// Token: 0x06000117 RID: 279 RVA: 0x000045ED File Offset: 0x000027ED
	public static void checkAdComlete()
	{
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00004B36 File Offset: 0x00002D36
	public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
	{
		g.fillRect(x, y, w + 10, h, 0, 90);
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00004B49 File Offset: 0x00002D49
	public static void arraycopy(sbyte[] scr, int scrPos, sbyte[] dest, int destPos, int lenght)
	{
		Array.Copy(scr, scrPos, dest, destPos, lenght);
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000DB10 File Offset: 0x0000BD10
	public static void arrayReplace(sbyte[] scr, int scrPos, ref sbyte[] dest, int destPos, int lenght)
	{
		if (scr == null || dest == null || scrPos + lenght > scr.Length)
		{
			return;
		}
		sbyte[] array = new sbyte[dest.Length + lenght];
		for (int i = 0; i < destPos; i++)
		{
			array[i] = dest[i];
		}
		for (int j = destPos; j < destPos + lenght; j++)
		{
			array[j] = scr[scrPos + j - destPos];
		}
		for (int k = destPos + lenght; k < array.Length; k++)
		{
			array[k] = dest[destPos + k - lenght];
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
	public static long currentTimeMillis()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000L;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00004B56 File Offset: 0x00002D56
	public static void freeData()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
	public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000DC14 File Offset: 0x0000BE14
	public static sbyte[] convertToSbyte(string scr)
	{
		ASCIIEncoding asciiencoding = new ASCIIEncoding();
		byte[] bytes = asciiencoding.GetBytes(scr);
		return mSystem.convertToSbyte(bytes);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00009A20 File Offset: 0x00007C20
	public static byte[] convetToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			if ((int)scr[i] > 0)
			{
				array[i] = (byte)scr[i];
			}
			else
			{
				array[i] = (byte)((int)scr[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0000DC38 File Offset: 0x0000BE38
	public static char[] ToCharArray(sbyte[] scr)
	{
		char[] array = new char[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (char)scr[i];
		}
		return array;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000DC6C File Offset: 0x0000BE6C
	public static int currentHour()
	{
		return DateTime.Now.Hour;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00004B63 File Offset: 0x00002D63
	public static void println(object str)
	{
		Debug.Log(str);
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00004B56 File Offset: 0x00002D56
	public static void gcc()
	{
		mGraphics.clearCachedTextures();
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00004B6B File Offset: 0x00002D6B
	public static mSystem gI()
	{
		if (mSystem.instance == null)
		{
			mSystem.instance = new mSystem();
		}
		return mSystem.instance;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00004B86 File Offset: 0x00002D86
	public static void onConnectOK()
	{
		Controller.isConnectOK = true;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00004B8E File Offset: 0x00002D8E
	public static void onConnectionFail()
	{
		Controller.isConnectionFail = true;
		Debug.LogError(">>>>>>>> Controller.isConnectionFail = true;");
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00004BA0 File Offset: 0x00002DA0
	public static void onDisconnected()
	{
		Controller.isDisconnected = true;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x000045ED File Offset: 0x000027ED
	public static void exitWP()
	{
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000DC88 File Offset: 0x0000BE88
	public static void paintFlyText(mGraphics g)
	{
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] != -1)
			{
				if (GameCanvas.isPaint(GameScr.flyTextX[i], GameScr.flyTextY[i]))
				{
					if (GameScr.flyTextColor[i] == mFont.RED)
					{
						mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.YELLOW)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.GREEN)
					{
						mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.FATAL)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.FATAL_ME)
					{
						mFont.bigNumber_green.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MISS)
					{
						mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.tahoma_7_grey);
					}
					else if (GameScr.flyTextColor[i] == mFont.ORANGE)
					{
						mFont.bigNumber_orange.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
					}
					else if (GameScr.flyTextColor[i] == mFont.ADDMONEY)
					{
						mFont.bigNumber_yellow.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MISS_ME)
					{
						mFont.bigNumber_While.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.HP)
					{
						mFont.bigNumber_red.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
					else if (GameScr.flyTextColor[i] == mFont.MP)
					{
						mFont.bigNumber_blue.drawStringBorder(g, GameScr.flyTextString[i], GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
					}
				}
			}
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x000045ED File Offset: 0x000027ED
	public static void endKey()
	{
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000DF78 File Offset: 0x0000C178
	public static FrameImage getFraImage(string nameImg)
	{
		FrameImage result = null;
		MainImage mainImage = null;
		if (mainImage == null)
		{
			mainImage = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
		}
		if (mainImage.img != null)
		{
			int num = mainImage.img.getHeight() / (int)mainImage.nFrame;
			if (num < 1)
			{
				num = 1;
			}
			result = new FrameImage(mainImage.img, mainImage.img.getWidth(), num);
		}
		return result;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00004BA8 File Offset: 0x00002DA8
	public static Image loadImage(string path)
	{
		return GameCanvas.loadImage(path);
	}

	// Token: 0x040000EC RID: 236
	public static bool isTest = true;

	// Token: 0x040000ED RID: 237
	public static string strAdmob;

	// Token: 0x040000EE RID: 238
	public static bool loadAdOk;

	// Token: 0x040000EF RID: 239
	public static string publicID;

	// Token: 0x040000F0 RID: 240
	public static string android_pack;

	// Token: 0x040000F1 RID: 241
	public static int clientType = 4;

	// Token: 0x040000F2 RID: 242
	public static sbyte LANGUAGE;

	// Token: 0x040000F3 RID: 243
	public static sbyte curINAPP;

	// Token: 0x040000F4 RID: 244
	public static sbyte maxINAPP = 5;

	// Token: 0x040000F5 RID: 245
	public const int JAVA = 1;

	// Token: 0x040000F6 RID: 246
	public const int ANDROID = 2;

	// Token: 0x040000F7 RID: 247
	public const int IP_JB = 3;

	// Token: 0x040000F8 RID: 248
	public const int PC = 4;

	// Token: 0x040000F9 RID: 249
	public const int IP_APPSTORE = 5;

	// Token: 0x040000FA RID: 250
	public const int WINDOWS_PHONE = 6;

	// Token: 0x040000FB RID: 251
	public const int GOOGLE_PLAY = 7;

	// Token: 0x040000FC RID: 252
	public static mSystem instance;

	// Token: 0x040000FD RID: 253
	internal static bool isANDROID;
}
