using System;
using System.IO;
using System.Threading;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class Rms
{
	// Token: 0x06000089 RID: 137 RVA: 0x000045EF File Offset: 0x000027EF
	public static void saveRMS(string filename, sbyte[] data)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Rms.__saveRMS(filename, data);
		}
		else
		{
			Rms._saveRMS(filename, data);
		}
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000461D File Offset: 0x0000281D
	public static sbyte[] loadRMS(string filename)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			return Rms.__loadRMS(filename);
		}
		return Rms._loadRMS(filename);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000A7BC File Offset: 0x000089BC
	public static string loadRMSString(string fileName)
	{
		sbyte[] array = Rms.loadRMS(fileName);
		if (array == null)
		{
			return null;
		}
		DataInputStream dataInputStream = new DataInputStream(array);
		try
		{
			string result = dataInputStream.readUTF();
			dataInputStream.close();
			return result;
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
		return null;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00009A20 File Offset: 0x00007C20
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if ((int)var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000A81C File Offset: 0x00008A1C
	public static void saveRMSString(string filename, string data)
	{
		DataOutputStream dataOutputStream = new DataOutputStream();
		try
		{
			dataOutputStream.writeUTF(data);
			Rms.saveRMS(filename, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Cout.println(ex.StackTrace);
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x0000A870 File Offset: 0x00008A70
	private static void _saveRMS(string filename, sbyte[] data)
	{
		if (Rms.status != 0)
		{
			Debug.LogError("Cannot save RMS " + filename + " because current is saving " + Rms.filename);
			return;
		}
		Rms.filename = filename;
		Rms.data = data;
		Rms.status = 2;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Rms.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO SAVE RMS " + filename);
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000A8FC File Offset: 0x00008AFC
	private static sbyte[] _loadRMS(string filename)
	{
		if (Rms.status != 0)
		{
			Debug.LogError("Cannot load RMS " + filename + " because current is loading " + Rms.filename);
			return null;
		}
		Rms.filename = filename;
		Rms.data = null;
		Rms.status = 3;
		int i;
		for (i = 0; i < 500; i++)
		{
			Thread.Sleep(5);
			if (Rms.status == 0)
			{
				break;
			}
		}
		if (i == 500)
		{
			Debug.LogError("TOO LONG TO LOAD RMS " + filename);
		}
		return Rms.data;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000A98C File Offset: 0x00008B8C
	public static void update()
	{
		if (Rms.status == 2)
		{
			Rms.status = 1;
			Rms.__saveRMS(Rms.filename, Rms.data);
			Rms.status = 0;
		}
		else if (Rms.status == 3)
		{
			Rms.status = 1;
			Rms.data = Rms.__loadRMS(Rms.filename);
			Rms.status = 0;
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x0000A9EC File Offset: 0x00008BEC
	public static int loadRMSInt(string file)
	{
		sbyte[] array = Rms.loadRMS(file);
		return (array != null) ? ((int)array[0]) : -1;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000AA10 File Offset: 0x00008C10
	public static void saveRMSInt(string file, int x)
	{
		try
		{
			Rms.saveRMS(file, new sbyte[]
			{
				(sbyte)x
			});
			if (file == ServerListScreen.RMS_svselect)
			{
				Debug.LogError(string.Concat(new object[]
				{
					">>>>>>>>Save saveRMSInt: ",
					file,
					"  index:",
					x
				}));
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00004645 File Offset: 0x00002845
	public static string GetiPhoneDocumentsPath()
	{
		try
		{
			string localDir = Path.GetFullPath(Application.dataPath + "/../data_rms");
			if (!Directory.Exists(localDir))
			{
				Directory.CreateDirectory(localDir);
			}
			return localDir;
		}
		catch
		{
			return Application.persistentDataPath;
		}
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000AA88 File Offset: 0x00008C88
	public static bool IsModCache(string filename)
	{
		if (string.IsNullOrEmpty(filename)) return false;
		string lower = filename.ToLower();
		
		// Mod Settings & Auto Features
		if (lower.StartsWith("ispaintbgr") ||
			lower.StartsWith("isreducegraphics") ||
			lower.StartsWith("sanboss") ||
			lower.StartsWith("showchar") ||
			lower.StartsWith("serverchat") ||
			lower.StartsWith("hideserverchat") ||
			lower.StartsWith("isauto") ||
			lower.StartsWith("target") ||
			lower.StartsWith("delayauto") ||
			lower.StartsWith("distanceauto") ||
			lower.StartsWith("auto") || // Covers AutoMap, AutoPean, AutoSkill, auto_manager, auto_pick_list, etc.
			lower.Contains("_list") ||
			lower.Contains("_part") ||
			lower.Contains("item_animation") ||
			lower.StartsWith("onscreenskill") ||
			lower.StartsWith("keyskill") ||
			lower.EndsWith("_head") ||
			lower.EndsWith("_body") ||
			lower.EndsWith("_leg") ||
			lower.EndsWith("_back") ||
			lower.EndsWith("_board"))
		{
			return true;
		}
		
		// Core Game Settings (Protect these too so users don't have to reconfigure)
		if (lower == "acc" ||
			lower == "pass" ||
			lower.StartsWith("userao") ||
			lower == "analog" ||
			lower == "isplaysound" ||
			lower.StartsWith("ispaintaura") ||
			lower == "lowgraphic" ||
			lower == "serverchat" ||
			lower == "viewchat" ||
			lower == "languageversion" ||
			lower == "indserver")
		{
			return true;
		}
		
		return false;
	}

	private static void __saveRMS(string filename, sbyte[] data)
	{
		try
		{
			string path = Rms.GetiPhoneDocumentsPath();
			if (IsModCache(filename))
			{
				path = path + "/ModCache";
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);
			}
			string text = path + "/" + filename;
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
			{
				byte[] raw = ArrayCast.cast(data);
				fileStream.Write(raw, 0, raw.Length);
				fileStream.Flush();
			}
			Main.setBackupIcloud(text);
		}
		catch (Exception ex)
		{
			Cout.println("Loi __saveRMS: " + ex.Message);
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0000AAD0 File Offset: 0x00008CD0
	private static sbyte[] __loadRMS(string filename)
	{
		sbyte[] result = null;
		try
		{
			string path = Rms.GetiPhoneDocumentsPath();
			string fullPath = path + "/" + filename;
			
			if (IsModCache(filename))
			{
				string modPath = path + "/ModCache/" + filename;
				if (File.Exists(modPath)) 
				{
					fullPath = modPath;
				}
			}
			if (File.Exists(fullPath))
			{
				using (FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					byte[] array = new byte[fileStream.Length];
					fileStream.Read(array, 0, array.Length);
					result = ArrayCast.cast(array);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Loi __loadRMS: " + ex.Message);
			result = null;
		}
		return result;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0000AB40 File Offset: 0x00008D40
	public static void clearAll()
	{
		Cout.LogError3("clean rms");
		foreach (FileInfo fileInfo in new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles())
		{
			fileInfo.Delete();
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000AB90 File Offset: 0x00008D90
	public static void DeleteStorage(string path)
	{
		try
		{
			File.Delete(Rms.GetiPhoneDocumentsPath() + "/" + path);
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000ABD0 File Offset: 0x00008DD0
	public static string ByteArrayToString(byte[] ba)
	{
		string text = BitConverter.ToString(ba);
		return text.Replace("-", string.Empty);
	}

	// Token: 0x06000099 RID: 153 RVA: 0x0000ABF4 File Offset: 0x00008DF4
	public static byte[] StringToByteArray(string hex)
	{
		int length = hex.Length;
		byte[] array = new byte[length / 2];
		for (int i = 0; i < length; i += 2)
		{
			array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return array;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000AC38 File Offset: 0x00008E38
	public static void deleteRecord(string name)
	{
		try
		{
			PlayerPrefs.DeleteKey(name);
		}
		catch (Exception ex)
		{
			Cout.println("loi xoa RMS --------------------------" + ex.ToString());
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000AC7C File Offset: 0x00008E7C
	public static void clearRMS()
	{
		Rms.deleteRecord("data");
		Rms.deleteRecord("dataVersion");
		Rms.deleteRecord("map");
		Rms.deleteRecord("mapVersion");
		Rms.deleteRecord("skill");
		Rms.deleteRecord("killVersion");
		Rms.deleteRecord("item");
		Rms.deleteRecord("itemVersion");
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000464C File Offset: 0x0000284C
	public static void saveIP(string strID)
	{
		Rms.saveRMSString("NRIPlink", strID);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000ACDC File Offset: 0x00008EDC
	public static string loadIP()
	{
		string text = Rms.loadRMSString("NRIPlink");
		if (text == null)
		{
			return null;
		}
		return text;
	}

	// Token: 0x0400004D RID: 77
	public static int status;

	// Token: 0x0400004E RID: 78
	public static sbyte[] data;

	// Token: 0x0400004F RID: 79
	public static string filename;

	// Token: 0x04000050 RID: 80
	private const int INTERVAL = 5;

	// Token: 0x04000051 RID: 81
	private const int MAXTIME = 500;
}
