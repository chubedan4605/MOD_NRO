using System;

// Token: 0x020000CB RID: 203
public class TileMap
{
	// Token: 0x06000A4B RID: 2635 RVA: 0x00008A09 File Offset: 0x00006C09
	public static void loadBg()
	{
		TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
		if (mGraphics.zoomLevel == 1 || Main.isIpod || Main.isIphone4)
		{
			return;
		}
		TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0009AFC4 File Offset: 0x000991C4
	public static bool isVoDaiMap()
	{
		return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x00008A49 File Offset: 0x00006C49
	public static bool isTrainingMap()
	{
		return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x00008A72 File Offset: 0x00006C72
	public static bool mapPhuBang()
	{
		return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0009B024 File Offset: 0x00099224
	public static BgItem getBIById(int id)
	{
		for (int i = 0; i < TileMap.vItemBg.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vItemBg.elementAt(i);
			if (bgItem.id == id)
			{
				return bgItem;
			}
		}
		return null;
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0009B06C File Offset: 0x0009926C
	public static bool isOfflineMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.offlineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x0009B0A8 File Offset: 0x000992A8
	public static bool isHighterMap()
	{
		for (int i = 0; i < TileMap.offlineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.highterId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x0009B0E4 File Offset: 0x000992E4
	public static bool isToOfflineMap()
	{
		for (int i = 0; i < TileMap.toOfflineId.Length; i++)
		{
			if (TileMap.mapID == TileMap.toOfflineId[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00008A95 File Offset: 0x00006C95
	public static void freeTilemap()
	{
		TileMap.imgTile = null;
		mSystem.gcc();
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x000045ED File Offset: 0x000027ED
	public static void loadTileCreatChar()
	{
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0009B120 File Offset: 0x00099320
	public static bool isExistMoreOne(int id)
	{
		if (id == 156 || id == 330 || id == 345 || id == 334)
		{
			return false;
		}
		if (TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 58 || TileMap.mapID == 59 || TileMap.mapID == 103)
		{
			return false;
		}
		int num = 0;
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			if (bgItem.id == id)
			{
				num++;
			}
		}
		return num > 2;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0009B1FC File Offset: 0x000993FC
	public static void loadTileImage()
	{
		if (TileMap.imgWaterfall == null)
		{
			TileMap.imgWaterfall = GameCanvas.loadImageRMS("/tWater/wtf.png");
		}
		if (TileMap.imgTopWaterfall == null)
		{
			TileMap.imgTopWaterfall = GameCanvas.loadImageRMS("/tWater/twtf.png");
		}
		if (TileMap.imgWaterflow == null)
		{
			TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts.png");
		}
		if (TileMap.imgWaterlowN == null)
		{
			TileMap.imgWaterlowN = GameCanvas.loadImageRMS("/tWater/wtsN.png");
		}
		if (TileMap.imgWaterlowN2 == null)
		{
			TileMap.imgWaterlowN2 = GameCanvas.loadImageRMS("/tWater/wtsN2.png");
		}
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x0009B28C File Offset: 0x0009948C
	public static void setTile(int index, int[] mapsArr, int type)
	{
		for (int i = 0; i < mapsArr.Length; i++)
		{
			if (TileMap.maps[index] == mapsArr[i])
			{
				TileMap.types[index] |= type;
				return;
			}
		}
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x0009B2D0 File Offset: 0x000994D0
	public static void loadMap(int tileId)
	{
		TileMap.pxh = TileMap.tmh * (int)TileMap.size;
		TileMap.pxw = TileMap.tmw * (int)TileMap.size;
		Res.outz("load tile ID= " + TileMap.tileID);
		int num = tileId - 1;
		try
		{
			int[] array = TileMap.getTileTypeCache(num);
			for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
			{
				int num2 = TileMap.maps[i];
				TileMap.types[i] = ((num2 >= 0 && num2 < array.Length) ? array[num2] : 0);
			}
		}
		catch (Exception ex)
		{
			Cout.println("Error Load Map");
			GameMidlet.instance.exit();
		}
	}

	private static int[] getTileTypeCache(int tileSetIndex)
	{
		if (TileMap.tileTypeCache == null || TileMap.tileTypeCache.Length != TileMap.tileType.Length)
		{
			TileMap.tileTypeCache = new int[TileMap.tileType.Length][];
		}
		if (TileMap.tileTypeCache[tileSetIndex] != null)
		{
			return TileMap.tileTypeCache[tileSetIndex];
		}
		int[] array = new int[256];
		for (int i = 0; i < TileMap.tileType[tileSetIndex].Length; i++)
		{
			int num = TileMap.tileType[tileSetIndex][i];
			int[] array2 = TileMap.tileIndex[tileSetIndex][i];
			for (int j = 0; j < array2.Length; j++)
			{
				int num2 = array2[j];
				if (num2 >= 0 && num2 < array.Length)
				{
					array[num2] |= num;
				}
			}
		}
		TileMap.tileTypeCache[tileSetIndex] = array;
		return array;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00008AA2 File Offset: 0x00006CA2
	public static bool isInAirMap()
	{
		return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0009B39C File Offset: 0x0009959C
	public static bool isDoubleMap()
	{
		return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0009B488 File Offset: 0x00099688
	public static void getTile()
	{
		if (Main.typeClient == 3 || Main.typeClient == 5)
		{
			if (mGraphics.zoomLevel == 1)
			{
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = GameCanvas.loadImage("/t/" + TileMap.tileID + ".png");
			}
			else
			{
				TileMap.imgTile = new Image[100];
				for (int i = 0; i < TileMap.imgTile.Length; i++)
				{
					TileMap.imgTile[i] = GameCanvas.loadImage(string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"/",
						i + 1,
						".png"
					}));
				}
			}
		}
		else
		{
			if (mGraphics.zoomLevel == 1)
			{
				if (TileMap.imgTile != null)
				{
					for (int j = 0; j < TileMap.imgTile.Length; j++)
					{
						if (TileMap.imgTile[j] != null)
						{
							TileMap.imgTile[j].texture = null;
							TileMap.imgTile[j] = null;
						}
					}
					UnityEngine.Resources.UnloadUnusedAssets();
				}
				TileMap.imgTile = new Image[100];
				string path = string.Empty;
				for (int k = 0; k < TileMap.imgTile.Length; k++)
				{
					if (k < 9)
					{
						path = string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"/t_0",
							k + 1
						});
					}
					else
					{
						path = string.Concat(new object[]
						{
							"/t/",
							TileMap.tileID,
							"/t_",
							k + 1
						});
					}
					TileMap.imgTile[k] = GameCanvas.loadImage(path);
				}
				return;
			}
			Image image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + ".png");
			if (image != null)
			{
				Rms.DeleteStorage("$");
				TileMap.imgTile = new Image[1];
				TileMap.imgTile[0] = image;
				return;
			}
			image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID + "$1.png");
			if (image != null)
			{
				Rms.DeleteStorage(string.Concat(new object[]
				{
					"x",
					mGraphics.zoomLevel,
					"t",
					TileMap.tileID
				}));
				TileMap.imgTile = new Image[100];
				for (int l = 0; l < TileMap.imgTile.Length; l++)
				{
					TileMap.imgTile[l] = GameCanvas.loadImageRMS(string.Concat(new object[]
					{
						"/t/",
						TileMap.tileID,
						"$",
						l + 1,
						".png"
					}));
				}
			}
		}
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0009B778 File Offset: 0x00099978
	public static void paintTile(mGraphics g, int frame, int indexX, int indexY)
	{
		if (Mod.DungPham.KoiOctiiu957.MainMod.isReduceGraphics)
		{
			bool isSolid = TileMap.tileTypeAt(indexX, indexY) != 0;
			bool isUnderground = false;
			if (!isSolid) {
				for (int i = indexY - 1; i >= 0; i--) {
					if (TileMap.tileAt(indexX, i) == 0) {
						isUnderground = false;
						break;
					}
					if (TileMap.tileTypeAt(indexX, i) != 0) {
						isUnderground = true;
						break;
					}
				}
			}
			if (isSolid || isUnderground)
			{
				int x = indexX * (int)TileMap.size;
				int y = indexY * (int)TileMap.size;
				int w = (int)TileMap.size;
				int h = (int)TileMap.size;
				g.setColor(0x404040);
				g.fillRect(x, y, w, h);
				g.setColor(0x222222);
				g.drawRect(x, y, w, h);
			}
			return;
		}
		if (TileMap.imgTile == null)
		{
			return;
		}
		if (TileMap.imgTile.Length == 1)
		{
			g.drawRegion(TileMap.imgTile[0], 0, frame * (int)TileMap.size, (int)TileMap.size, (int)TileMap.size, 0, indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
		}
		else
		{
			g.drawImage(TileMap.imgTile[frame], indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
		}
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0009B7F4 File Offset: 0x000999F4
	public static void paintTile(mGraphics g, int frame, int x, int y, int w, int h)
	{
		if (Mod.DungPham.KoiOctiiu957.MainMod.isReduceGraphics)
		{
			bool isSolid = TileMap.tileTypeAtPixel(x, y) != 0;
			bool isUnderground = false;
			if (!isSolid) {
				int indexX = x / (int)TileMap.size;
				int indexY = y / (int)TileMap.size;
				for (int i = indexY - 1; i >= 0; i--) {
					if (TileMap.tileAt(indexX, i) == 0) {
						isUnderground = false;
						break;
					}
					if (TileMap.tileTypeAt(indexX, i) != 0) {
						isUnderground = true;
						break;
					}
				}
			}
			if (isSolid || isUnderground)
			{
				g.setColor(0x404040);
				g.fillRect(x, y, w, h);
				g.setColor(0x222222);
				g.drawRect(x, y, w, h);
			}
			return;
		}
		if (TileMap.imgTile == null)
		{
			return;
		}
		if (TileMap.imgTile.Length == 1)
		{
			g.drawRegion(TileMap.imgTile[0], 0, frame * w, w, w, 0, x, y, 0);
		}
		else
		{
			g.drawImage(TileMap.imgTile[frame], x, y, 0);
		}
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0009B848 File Offset: 0x00099A48
	public static void paintTilemapLOW(mGraphics g)
	{
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				int num = TileMap.maps[j * TileMap.tmw + i] - 1;
				if (num != -1)
				{
					TileMap.paintTile(g, num, i, j);
				}
				if ((TileMap.tileTypeAt(i, j) & 32) == 32)
				{
					g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				else if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					Image arg;
					if (TileMap.tileID == 5)
					{
						arg = TileMap.imgWaterlowN;
					}
					else if (TileMap.tileID == 8)
					{
						arg = TileMap.imgWaterlowN2;
					}
					else
					{
						arg = TileMap.imgWaterflow;
					}
					g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
				}
				if ((TileMap.tileTypeAt(i, j) & 2048) == 2048)
				{
					if ((TileMap.tileTypeAt(i, j - 1) & 32) == 32)
					{
						g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 4), 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size, 0);
					}
					else if ((TileMap.tileTypeAt(i, j - 1) & 4096) == 4096)
					{
						TileMap.paintTile(g, 21, i, j);
					}
					TileMap.paintTile(g, TileMap.maps[j * TileMap.tmw + i] - 1, i, j);
				}
			}
		}
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0009BA5C File Offset: 0x00099C5C
	public static void paintTilemap(mGraphics g)
	{
		if (global::Char.isLoadingMap)
		{
			return;
		}
		GameScr.gI().paintBgItem(g, 1);
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			((ItemMap)GameScr.vItemMap.elementAt(i)).paintAuraItemEff(g);
		}
		for (int j = GameScr.gssx; j < GameScr.gssxe; j++)
		{
			for (int k = GameScr.gssy; k < GameScr.gssye; k++)
			{
				if (j != 0)
				{
					if (j != TileMap.tmw - 1)
					{
						int num = TileMap.maps[k * TileMap.tmw + j] - 1;
						if ((TileMap.tileTypeAt(j, k) & 256) != 256)
						{
							if ((TileMap.tileTypeAt(j, k) & 32) == 32)
							{
								g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if ((TileMap.tileTypeAt(j, k) & 128) == 128)
							{
								g.drawRegion(TileMap.imgTopWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if (TileMap.tileID != 13 || num == -1)
							{
								if (TileMap.tileID == 2 && (TileMap.tileTypeAt(j, k) & 512) == 512 && num != -1)
								{
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
								}
								if (TileMap.tileID == 3)
								{
								}
								if ((TileMap.tileTypeAt(j, k) & 16) == 16)
								{
									TileMap.bx = j * (int)TileMap.size - GameScr.cmx;
									TileMap.dbx = TileMap.bx - GameScr.gW2;
									TileMap.dfx = ((int)TileMap.size - 2) * TileMap.dbx / (int)TileMap.size;
									TileMap.fx = TileMap.dfx + GameScr.gW2;
									TileMap.paintTile(g, num, TileMap.fx + GameScr.cmx, k * (int)TileMap.size, 24, 24);
								}
								else if ((TileMap.tileTypeAt(j, k) & 512) == 512)
								{
									if (num != -1)
									{
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
									}
								}
								else if (num != -1)
								{
									TileMap.paintTile(g, num, j, k);
								}
							}
						}
					}
				}
			}
		}
		if (GameScr.cmx < 24)
		{
			for (int l = GameScr.gssy; l < GameScr.gssye; l++)
			{
				int num2 = TileMap.maps[l * TileMap.tmw + 1] - 1;
				if (num2 != -1)
				{
					TileMap.paintTile(g, num2, 0, l);
				}
			}
		}
		if (GameScr.cmx > GameScr.cmxLim)
		{
			int num3 = TileMap.tmw - 2;
			for (int m = GameScr.gssy; m < GameScr.gssye; m++)
			{
				int num4 = TileMap.maps[m * TileMap.tmw + num3] - 1;
				if (num4 != -1)
				{
					TileMap.paintTile(g, num4, num3 + 1, m);
				}
			}
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0009BDEC File Offset: 0x00099FEC
	public static bool isWaterEff()
	{
		return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0009BE4C File Offset: 0x0009A04C
	public static void paintOutTilemap(mGraphics g)
	{
		if (GameCanvas.lowGraphic || global::Char.isLoadingMap)
		{
			return;
		}
		int num = 0;
		for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
		{
			for (int j = GameScr.gssy; j < GameScr.gssye; j++)
			{
				num++;
				if ((TileMap.tileTypeAt(i, j) & 64) == 64)
				{
					Image arg;
					if (TileMap.tileID == 5)
					{
						arg = TileMap.imgWaterlowN;
					}
					else if (TileMap.tileID == 8)
					{
						arg = TileMap.imgWaterlowN2;
					}
					else
					{
						arg = TileMap.imgWaterflow;
					}
					if (!TileMap.isWaterEff())
					{
						g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 1, 0);
						g.drawRegion(arg, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 3, 0);
					}
					g.drawRegion(arg, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 12, 0);
					if (TileMap.yWater == 0 && TileMap.isWaterEff())
					{
						TileMap.yWater = j * (int)TileMap.size - 12;
						int color = 16777215;
						if (GameCanvas.typeBg == 2)
						{
							color = 10871287;
						}
						else if (GameCanvas.typeBg == 4)
						{
							color = 8111470;
						}
						else if (GameCanvas.typeBg == 7)
						{
							color = 5693125;
						}
						else if (GameCanvas.typeBg == 19)
						{
							color = 16711680;
						}
						BackgroudEffect.addWater(color, TileMap.yWater + 15);
					}
				}
			}
		}
		BackgroudEffect.paintWaterAll(g);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0009BFF4 File Offset: 0x0009A1F4
	public static void loadMapFromResource(int mapID)
	{
		DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID);
		TileMap.tmw = (int)((ushort)dataInputStream.read());
		TileMap.tmh = (int)((ushort)dataInputStream.read());
		TileMap.maps = new int[dataInputStream.available()];
		for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
		{
			TileMap.maps[i] = (int)((ushort)dataInputStream.read());
		}
		TileMap.types = new int[TileMap.maps.Length];
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0009C07C File Offset: 0x0009A27C
	public static int tileAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.maps[y * TileMap.tmw + x];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0009C0BC File Offset: 0x0009A2BC
	public static int tileTypeAt(int x, int y)
	{
		int result;
		try
		{
			result = TileMap.types[y * TileMap.tmw + x];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0009C0FC File Offset: 0x0009A2FC
	public static int tileTypeAtPixel(int px, int py)
	{
		int result;
		try
		{
			result = TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size];
		}
		catch (Exception ex)
		{
			result = 1000;
		}
		return result;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0009C14C File Offset: 0x0009A34C
	public static bool tileTypeAt(int px, int py, int t)
	{
		bool result;
		try
		{
			result = ((TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] & t) == t);
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x00008ACB File Offset: 0x00006CCB
	public static void setTileTypeAtPixel(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x00008AF3 File Offset: 0x00006CF3
	public static void setTileTypeAt(int x, int y, int t)
	{
		TileMap.types[y * TileMap.tmw + x] = t;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00008B05 File Offset: 0x00006D05
	public static void killTileTypeAt(int px, int py, int t)
	{
		TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00008B2E File Offset: 0x00006D2E
	public static int tileYofPixel(int py)
	{
		return py / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00008B2E File Offset: 0x00006D2E
	public static int tileXofPixel(int px)
	{
		return px / (int)TileMap.size * (int)TileMap.size;
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00008B3F File Offset: 0x00006D3F
	public static void loadMainTile()
	{
		if (TileMap.lastTileID != TileMap.tileID)
		{
			TileMap.getTile();
			TileMap.lastTileID = TileMap.tileID;
		}
	}

	// Token: 0x04001349 RID: 4937
	public const int T_EMPTY = 0;

	// Token: 0x0400134A RID: 4938
	public const int T_TOP = 2;

	// Token: 0x0400134B RID: 4939
	public const int T_LEFT = 4;

	// Token: 0x0400134C RID: 4940
	public const int T_RIGHT = 8;

	// Token: 0x0400134D RID: 4941
	public const int T_TREE = 16;

	// Token: 0x0400134E RID: 4942
	public const int T_WATERFALL = 32;

	// Token: 0x0400134F RID: 4943
	public const int T_WATERFLOW = 64;

	// Token: 0x04001350 RID: 4944
	public const int T_TOPFALL = 128;

	// Token: 0x04001351 RID: 4945
	public const int T_OUTSIDE = 256;

	// Token: 0x04001352 RID: 4946
	public const int T_DOWN1PIXEL = 512;

	// Token: 0x04001353 RID: 4947
	public const int T_BRIDGE = 1024;

	// Token: 0x04001354 RID: 4948
	public const int T_UNDERWATER = 2048;

	// Token: 0x04001355 RID: 4949
	public const int T_SOLIDGROUND = 4096;

	// Token: 0x04001356 RID: 4950
	public const int T_BOTTOM = 8192;

	// Token: 0x04001357 RID: 4951
	public const int T_DIE = 16384;

	// Token: 0x04001358 RID: 4952
	public const int T_HEBI = 32768;

	// Token: 0x04001359 RID: 4953
	public const int T_BANG = 65536;

	// Token: 0x0400135A RID: 4954
	public const int T_JUM8 = 131072;

	// Token: 0x0400135B RID: 4955
	public const int T_NT0 = 262144;

	// Token: 0x0400135C RID: 4956
	public const int T_NT1 = 524288;

	// Token: 0x0400135D RID: 4957
	public const int T_CENTER = 1;

	// Token: 0x0400135E RID: 4958
	public static int tmw;

	// Token: 0x0400135F RID: 4959
	public static int tmh;

	// Token: 0x04001360 RID: 4960
	public static int pxw;

	// Token: 0x04001361 RID: 4961
	public static int pxh;

	// Token: 0x04001362 RID: 4962
	public static int tileID;

	// Token: 0x04001363 RID: 4963
	public static int lastTileID = -1;

	// Token: 0x04001364 RID: 4964
	public static int[] maps;

	// Token: 0x04001365 RID: 4965
	public static int[] types;

	// Token: 0x04001366 RID: 4966
	public static Image[] imgTile;

	// Token: 0x04001367 RID: 4967
	public static Image imgTileSmall;

	// Token: 0x04001368 RID: 4968
	public static Image imgMiniMap;

	// Token: 0x04001369 RID: 4969
	public static Image imgWaterfall;

	// Token: 0x0400136A RID: 4970
	public static Image imgTopWaterfall;

	// Token: 0x0400136B RID: 4971
	public static Image imgWaterflow;

	// Token: 0x0400136C RID: 4972
	public static Image imgWaterlowN;

	// Token: 0x0400136D RID: 4973
	public static Image imgWaterlowN2;

	// Token: 0x0400136E RID: 4974
	public static Image imgWaterF;

	// Token: 0x0400136F RID: 4975
	public static Image imgLeaf;

	// Token: 0x04001370 RID: 4976
	public static sbyte size = 24;

	// Token: 0x04001371 RID: 4977
	private static int bx;

	// Token: 0x04001372 RID: 4978
	private static int dbx;

	// Token: 0x04001373 RID: 4979
	private static int fx;

	// Token: 0x04001374 RID: 4980
	private static int dfx;

	// Token: 0x04001375 RID: 4981
	public static string[] instruction;

	// Token: 0x04001376 RID: 4982
	public static int[] iX;

	// Token: 0x04001377 RID: 4983
	public static int[] iY;

	// Token: 0x04001378 RID: 4984
	public static int[] iW;

	// Token: 0x04001379 RID: 4985
	public static int iCount;

	// Token: 0x0400137A RID: 4986
	public static bool isMapDouble = false;

	// Token: 0x0400137B RID: 4987
	public static string mapName = string.Empty;

	// Token: 0x0400137C RID: 4988
	public static sbyte versionMap = 1;

	// Token: 0x0400137D RID: 4989
	public static int mapID;

	// Token: 0x0400137E RID: 4990
	public static int lastBgID = -1;

	// Token: 0x0400137F RID: 4991
	public static int zoneID;

	// Token: 0x04001380 RID: 4992
	public static int bgID;

	// Token: 0x04001381 RID: 4993
	public static int bgType;

	// Token: 0x04001382 RID: 4994
	public static int lastType = -1;

	// Token: 0x04001383 RID: 4995
	public static int typeMap;

	// Token: 0x04001384 RID: 4996
	public static sbyte planetID;

	// Token: 0x04001385 RID: 4997
	public static sbyte lastPlanetId = -1;

	// Token: 0x04001386 RID: 4998
	public static long timeTranMini;

	// Token: 0x04001387 RID: 4999
	public static MyVector vGo = new MyVector();

	// Token: 0x04001388 RID: 5000
	public static MyVector vItemBg = new MyVector();

	// Token: 0x04001389 RID: 5001
	public static MyVector vCurrItem = new MyVector();

	// Token: 0x0400138A RID: 5002
	public static string[] mapNames;

	// Token: 0x0400138B RID: 5003
	public static sbyte MAP_NORMAL = 0;

	// Token: 0x0400138C RID: 5004
	public static Image bong;

	// Token: 0x0400138D RID: 5005
	public const int TRAIDAT_DOINUI = 0;

	// Token: 0x0400138E RID: 5006
	public const int TRAIDAT_RUNG = 1;

	// Token: 0x0400138F RID: 5007
	public const int TRAIDAT_DAORUA = 2;

	// Token: 0x04001390 RID: 5008
	public const int TRAIDAT_DADO = 3;

	// Token: 0x04001391 RID: 5009
	public const int NAMEK_THUNGLUNG = 5;

	// Token: 0x04001392 RID: 5010
	public const int NAMEK_DOINUI = 4;

	// Token: 0x04001393 RID: 5011
	public const int NAMEK_RUNG = 6;

	// Token: 0x04001394 RID: 5012
	public const int NAMEK_DAO = 7;

	// Token: 0x04001395 RID: 5013
	public const int SAYAI_DOINUI = 8;

	// Token: 0x04001396 RID: 5014
	public const int SAYAI_RUNG = 9;

	// Token: 0x04001397 RID: 5015
	public const int SAYAI_CITY = 10;

	// Token: 0x04001398 RID: 5016
	public const int SAYAI_NIGHT = 11;

	// Token: 0x04001399 RID: 5017
	public const int KAMISAMA = 12;

	// Token: 0x0400139A RID: 5018
	public const int TIME_ROOM = 13;

	// Token: 0x0400139B RID: 5019
	public const int HELL = 15;

	// Token: 0x0400139C RID: 5020
	public const int BEERUS = 16;

	// Token: 0x0400139D RID: 5021
	public const int THE_HELL = 19;

	// Token: 0x0400139E RID: 5022
	public static Image[] bgItem = new Image[8];

	// Token: 0x0400139F RID: 5023
	public static MyVector vObject = new MyVector();

	// Token: 0x040013A0 RID: 5024
	public static int[] offlineId = new int[]
	{
		21,
		22,
		23,
		39,
		40,
		41
	};

	// Token: 0x040013A1 RID: 5025
	public static int[] highterId = new int[]
	{
		21,
		22,
		23,
		24,
		25,
		26
	};

	// Token: 0x040013A2 RID: 5026
	public static int[] toOfflineId = new int[]
	{
		0,
		7,
		14
	};

	// Token: 0x040013A3 RID: 5027
	public static int[][] tileType;

	// Token: 0x040013A4 RID: 5028
	public static int[][][] tileIndex;

	// Token: 0x040013A5 RID: 5029
	public static int[][] tileTypeCache;

	// Token: 0x040013A6 RID: 5030
	public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

	// Token: 0x040013A7 RID: 5031
	public static int sizeMiniMap = 2;

	// Token: 0x040013A8 RID: 5032
	public static int gssx;

	// Token: 0x040013A9 RID: 5033
	public static int gssxe;

	// Token: 0x040013AA RID: 5034
	public static int gssy;

	// Token: 0x040013AB RID: 5035
	public static int gssye;

	// Token: 0x040013AC RID: 5036
	public static int countx;

	// Token: 0x040013AD RID: 5037
	public static int county;

	// Token: 0x040013AE RID: 5038
	private static int[] colorMini = new int[]
	{
		5257738,
		8807192
	};

	// Token: 0x040013AF RID: 5039
	public static int yWater = 0;
}
