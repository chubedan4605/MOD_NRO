using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class Main : MonoBehaviour
{
	// Token: 0x0600013B RID: 315 RVA: 0x0000E000 File Offset: 0x0000C200
	private void Start()
	{
		UnityEngine.QualitySettings.antiAliasing = 0;
		UnityEngine.QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = Mod.DungPham.KoiOctiiu957.MainMod.targetFPS;
		if (!Main.started)
		{
			Time.timeScale = 2.2f;
			if (Thread.CurrentThread.Name != "Main")
			{
				Thread.CurrentThread.Name = "Main";
			}
			Main.mainThreadName = Thread.CurrentThread.Name;
			Main.isPC = true;
			Main.started = true;
			if (Main.isPC)
			{
				int num;
				int num2;
				if (Main.tryLoadCustomResolution(out num, out num2))
				{
					Screen.SetResolution(num, num2, false);
					return;
				}
				this.level = Rms.loadRMSInt("levelScreenKN");
				if (this.level == 1)
				{
					Screen.SetResolution(460, 300, false);
					return;
				}
				Screen.SetResolution(960, 540, false);
			}
		}
	}

	private static bool tryLoadCustomResolution(out int width, out int height)
	{
		width = 0;
		height = 0;
		try
		{
			string configPath = Main.getWindowSizeConfigPath();
			if (!File.Exists(configPath))
			{
				return false;
			}
			string text = File.ReadAllText(configPath).Trim();
			if (text == string.Empty)
			{
				return false;
			}
			string[] array = text.ToLower().Split(new char[]
			{
				'x',
				',',
				';',
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 2)
			{
				return false;
			}
			int num;
			int num2;
			if (!int.TryParse(array[0], out num) || !int.TryParse(array[1], out num2))
			{
				return false;
			}
			if (num < 320 || num2 < 240)
			{
				return false;
			}
			width = num;
			height = num2;
			return true;
		}
		catch
		{
		}
		return false;
	}

	private static string getWindowSizeConfigPath()
	{
		try
		{
			string text = Directory.GetParent(Application.dataPath).FullName;
			return Path.Combine(text, "pc_window_size.txt");
		}
		catch
		{
		}
		return Path.Combine(Environment.CurrentDirectory, "pc_window_size.txt");
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00004BBE File Offset: 0x00002DBE
	private void SetInit()
	{
		base.enabled = true;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00004BC7 File Offset: 0x00002DC7
	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000E0AC File Offset: 0x0000C2AC
	private void OnGUI()
	{
		if (this.count < 10)
		{
			return;
		}
		if (this.fps == 0)
		{
			this.timefps = mSystem.currentTimeMillis();
		}
		else if (mSystem.currentTimeMillis() - this.timefps > 1000L)
		{
			this.max = this.fps;
			this.fps = 0;
			this.timefps = mSystem.currentTimeMillis();
		}
		this.fps++;
		this.checkInput();
		Session_ME.update();
		Session_ME2.update();
		if (Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
		{
			ScaleGUI.BeginGUI();
			GameMidlet.gameCanvas.paint(Main.g);
			ScaleGUI.EndGUI();
			this.paintCount++;
			Main.g.reset();
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000E190 File Offset: 0x0000C390
	public void setsizeChange()
	{
		if (!this.isRun)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
			Application.runInBackground = true;
			Application.targetFrameRate = Mod.DungPham.KoiOctiiu957.MainMod.targetFPS;
			base.useGUILayout = false;
			Main.isCompactDevice = Main.detectCompactDevice();
			if (Main.main == null)
			{
				Main.main = this;
			}
			this.isRun = true;
			ScaleGUI.initScaleGUI();
			if (Main.isPC)
			{
				Main.IMEI = SystemInfo.deviceUniqueIdentifier;
			}
			else
			{
				Main.IMEI = this.GetMacAddress();
			}
			Main.isPC = true;
			if (Main.isPC)
			{
				Screen.fullScreen = false;
			}
			if (Main.isWindowsPhone)
			{
				Main.typeClient = 6;
			}
			if (Main.isPC)
			{
				Main.typeClient = 4;
			}
			if (Main.IphoneVersionApp)
			{
				Main.typeClient = 5;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen)
			{
				Main.isIpod = true;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPhone4)
			{
				Main.isIphone4 = true;
			}
			Main.g = new mGraphics();
			Main.midlet = new GameMidlet();
			TileMap.loadBg();
			Paint.loadbg();
			PopUp.loadBg();
			GameScr.loadBg();
			InfoMe.gI().loadCharId();
			Panel.loadBg();
			Menu.loadBg();
			Key.mapKeyPC();
			SoundMn.gI().loadSound(TileMap.mapID);
			Main.g.CreateLineMaterial();
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x000045ED File Offset: 0x000027ED
	public static void setBackupIcloud(string path)
	{
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
	public string GetMacAddress()
	{
		string empty = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		for (int i = 0; i < allNetworkInterfaces.Length; i++)
		{
			PhysicalAddress physicalAddress = allNetworkInterfaces[i].GetPhysicalAddress();
			if (physicalAddress.ToString() != string.Empty)
			{
				return physicalAddress.ToString();
			}
		}
		return string.Empty;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x0000E334 File Offset: 0x0000C534
	public void doClearRMS()
	{
		if (!Main.isPC)
		{
			return;
		}
		int num = Rms.loadRMSInt("lastZoomlevel");
		if (num != mGraphics.zoomLevel)
		{
			Rms.clearAll();
			Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
			Rms.saveRMSInt("levelScreenKN", this.level);
		}
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00004BE8 File Offset: 0x00002DE8
	public static void closeKeyBoard()
	{
		if (global::TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000E388 File Offset: 0x0000C588
	private void FixedUpdate()
	{
		Rms.update();
		this.count++;
		if (this.count < 10)
		{
			return;
		}
		if (this.up == 0)
		{
			this.timeup = mSystem.currentTimeMillis();
		}
		else if (mSystem.currentTimeMillis() - this.timeup > 1000L)
		{
			this.upmax = this.up;
			this.up = 0;
			this.timeup = mSystem.currentTimeMillis();
		}
		this.up++;
		this.setsizeChange();
		this.updateCount++;
		ipKeyboard.update();
		GameMidlet.gameCanvas.update();
		Image.update();
		DataInputStream.update();
		SMS.update();
		Net.update();
		Main.f++;
		if (Main.f > 8)
		{
			Main.f = 0;
		}
		if (!Main.isPC)
		{
			int num = 1 / Main.a;
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x000045ED File Offset: 0x000027ED
	private void Update()
	{
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0000E47C File Offset: 0x0000C67C
	private void checkInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.x = ScaleGUI.scaleX(mousePosition.x);
			mousePosition.y = ScaleGUI.scaleY(mousePosition.y);
			float screenH = ScaleGUI.scaleScreen ? ScaleGUI.HEIGHT : (float)Screen.height;
			GameMidlet.gameCanvas.pointerPressed((int)(mousePosition.x / (float)mGraphics.zoomLevel), (int)((screenH - mousePosition.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = Input.mousePosition;
			mousePosition2.x = ScaleGUI.scaleX(mousePosition2.x);
			mousePosition2.y = ScaleGUI.scaleY(mousePosition2.y);
			float screenH = ScaleGUI.scaleScreen ? ScaleGUI.HEIGHT : (float)Screen.height;
			GameMidlet.gameCanvas.pointerDragged((int)(mousePosition2.x / (float)mGraphics.zoomLevel), (int)((screenH - mousePosition2.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition2.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition2.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mousePosition3 = Input.mousePosition;
			mousePosition3.x = ScaleGUI.scaleX(mousePosition3.x);
			mousePosition3.y = ScaleGUI.scaleY(mousePosition3.y);
			float screenH = ScaleGUI.scaleScreen ? ScaleGUI.HEIGHT : (float)Screen.height;
			this.lastMousePos.x = mousePosition3.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition3.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			GameMidlet.gameCanvas.pointerReleased((int)(mousePosition3.x / (float)mGraphics.zoomLevel), (int)((screenH - mousePosition3.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
		}
		if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
		{
			int num = MyKeyMap.map(Event.current.keyCode);
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				KeyCode keyCode = Event.current.keyCode;
				if (keyCode != KeyCode.Alpha2)
				{
					if (keyCode == KeyCode.Minus)
					{
						num = 95;
					}
				}
				else
				{
					num = 64;
				}
			}
			if (num != 0)
			{
				GameMidlet.gameCanvas.keyPressedz(num);
			}
		}
		if (Event.current.type == EventType.KeyUp)
		{
			int num2 = MyKeyMap.map(Event.current.keyCode);
			if (num2 != 0)
			{
				GameMidlet.gameCanvas.keyReleasedz(num2);
			}
		}
		if (Main.isPC)
		{
			GameMidlet.gameCanvas.scrollMouse((int)(Input.GetAxis("Mouse ScrollWheel") * 10f));
			float x = ScaleGUI.scaleX(Input.mousePosition.x);
			float y = ScaleGUI.scaleY(Input.mousePosition.y);
			float screenH = ScaleGUI.scaleScreen ? ScaleGUI.HEIGHT : (float)Screen.height;
			int x2 = (int)x / mGraphics.zoomLevel;
			int y2 = (int)(screenH - y) / mGraphics.zoomLevel;
			GameMidlet.gameCanvas.pointerMouse(x2, y2);
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00004C05 File Offset: 0x00002E05
	private void OnApplicationQuit()
	{
		Debug.LogWarning("APP QUIT");
		GameCanvas.bRun = false;
		Session_ME.gI().close();
		Session_ME2.gI().close();
		if (Main.isPC)
		{
			Application.Quit();
		}
	}

	private void OnApplicationPause(bool paused)
	{
		Main.isResume = !paused;
		if (global::TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00004C3A File Offset: 0x00002E3A
	public static void exit()
	{
		if (Main.isPC)
		{
			Main.main.OnApplicationQuit();
		}
		else
		{
			Main.a = 0;
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00004C5B File Offset: 0x00002E5B
	public static bool detectCompactDevice()
	{
		return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00004C8C File Offset: 0x00002E8C
	public static bool checkCanSendSMS()
	{
		return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
	}

	// Token: 0x040000FE RID: 254
	public static Main main;

	// Token: 0x040000FF RID: 255
	public static mGraphics g;

	// Token: 0x04000100 RID: 256
	public static GameMidlet midlet;

	// Token: 0x04000101 RID: 257
	public static string res = "res";

	// Token: 0x04000102 RID: 258
	public static string mainThreadName;

	// Token: 0x04000103 RID: 259
	public static bool started;

	// Token: 0x04000104 RID: 260
	public static bool isIpod;

	// Token: 0x04000105 RID: 261
	public static bool isIphone4;

	// Token: 0x04000106 RID: 262
	public static bool isPC;

	// Token: 0x04000107 RID: 263
	public static bool isWindowsPhone;

	// Token: 0x04000108 RID: 264
	public static bool isIPhone;

	// Token: 0x04000109 RID: 265
	public static bool IphoneVersionApp;

	// Token: 0x0400010A RID: 266
	public static string IMEI;

	// Token: 0x0400010B RID: 267
	public static int versionIp;

	// Token: 0x0400010C RID: 268
	public static int numberQuit = 1;

	// Token: 0x0400010D RID: 269
	public static int typeClient = 4;

	// Token: 0x0400010E RID: 270
	public const sbyte PC_VERSION = 4;

	// Token: 0x0400010F RID: 271
	public const sbyte IP_APPSTORE = 5;

	// Token: 0x04000110 RID: 272
	public const sbyte WINDOWSPHONE = 6;

	// Token: 0x04000111 RID: 273
	private int level;

	// Token: 0x04000112 RID: 274
	public const sbyte IP_JB = 3;

	// Token: 0x04000113 RID: 275
	private int updateCount;

	// Token: 0x04000114 RID: 276
	private int paintCount;

	// Token: 0x04000115 RID: 277
	private int count;

	// Token: 0x04000116 RID: 278
	private int fps;

	// Token: 0x04000117 RID: 279
	private int max;

	// Token: 0x04000118 RID: 280
	private int up;

	// Token: 0x04000119 RID: 281
	private int upmax;

	// Token: 0x0400011A RID: 282
	private long timefps;

	// Token: 0x0400011B RID: 283
	private long timeup;

	// Token: 0x0400011C RID: 284
	private bool isRun;

	// Token: 0x0400011D RID: 285
	public static int waitTick;

	// Token: 0x0400011E RID: 286
	public static int f;

	// Token: 0x0400011F RID: 287
	public static bool isResume;

	// Token: 0x04000120 RID: 288
	public static bool isMiniApp = true;

	// Token: 0x04000121 RID: 289
	public static bool isQuitApp;

	// Token: 0x04000122 RID: 290
	private Vector2 lastMousePos = default(Vector2);

	// Token: 0x04000123 RID: 291
	public static int a = 1;

	// Token: 0x04000124 RID: 292
	public static bool isCompactDevice = true;
}
