using System;

// Token: 0x02000097 RID: 151
public class Waypoint : IActionListener
{
	// Token: 0x060004C9 RID: 1225 RVA: 0x0002FCD8 File Offset: 0x0002DED8
	public Waypoint(short minX, short minY, short maxX, short maxY, bool isEnter, bool isOffline, string name)
	{
		this.minX = minX;
		this.minY = minY;
		this.maxX = maxX;
		this.maxY = maxY;
		name = Res.changeString(name);
		this.isEnter = isEnter;
		this.isOffline = isOffline;
		if ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && this.minX >= 0 && this.minX <= 24)
		{
			return;
		}
		if (((TileMap.mapID == 0 && global::Char.myCharz().cgender != 0) || (TileMap.mapID == 7 && global::Char.myCharz().cgender != 1) || (TileMap.mapID == 14 && global::Char.myCharz().cgender != 2)) && isOffline)
		{
			return;
		}
		if (!TileMap.isInAirMap() && TileMap.mapID != 47)
		{
			if (!isEnter && !isOffline)
			{
				this.popup = new PopUp(name, (int)minX, (int)(minY - 24));
				this.popup.command = new Command(null, this, 1, this);
				this.popup.isWayPoint = true;
				this.popup.isPaint = false;
				PopUp.addPopUp(this.popup);
			}
			else
			{
				if (TileMap.isTrainingMap())
				{
					this.popup = new PopUp(name, (int)minX, (int)(minY - 16));
				}
				else
				{
					int x = (int)(minX + (maxX - minX) / 2);
					this.popup = new PopUp(name, x, (int)(minY - ((minY == 0) ? -32 : 16)));
				}
				this.popup.command = new Command(null, this, 2, this);
				this.popup.isWayPoint = true;
				this.popup.isPaint = false;
				PopUp.addPopUp(this.popup);
			}
			TileMap.vGo.addElement(this);
			return;
		}
		if (minY > 150 && TileMap.isInAirMap())
		{
			return;
		}
		this.popup = new PopUp(name, (int)(minX + (maxX - minX) / 2), (int)(maxY - ((minX <= 100) ? 48 : 24)));
		this.popup.command = new Command(null, this, 1, this);
		this.popup.isWayPoint = true;
		this.popup.isPaint = false;
		PopUp.addPopUp(this.popup);
		TileMap.vGo.addElement(this);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0002FF34 File Offset: 0x0002E134
	public void perform(int idAction, object p)
	{
		if (idAction != 1)
		{
			if (idAction == 2)
			{
				GameScr.gI().auto = 0;
				if (global::Char.myCharz().isInEnterOfflinePoint() != null)
				{
					Service.gI().charMove();
					InfoDlg.showWait();
					Service.gI().getMapOffline();
					global::Char.ischangingMap = true;
				}
				else if (global::Char.myCharz().isInEnterOnlinePoint() != null)
				{
					if (Mod.DungPham.KoiOctiiu957.MainMod.isLockMap && !Mod.DungPham.KoiOctiiu957.AutoMap.isAutoChangeMap)
					{
						global::Char.myCharz().currentMovePoint = null;
					}
					else
					{
						Service.gI().charMove();
						Service.gI().requestChangeMap();
						global::Char.isLockKey = true;
						global::Char.ischangingMap = true;
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						InfoDlg.showWait();
					}
				}
				else
				{
					int xEnd = (int)((this.minX + this.maxX) / 2);
					int yEnd = (int)this.maxY;
					global::Char.myCharz().currentMovePoint = new MovePoint(xEnd, yEnd);
					global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
					global::Char.myCharz().endMovePointCommand = new Command(null, this, 2, null);
				}
			}
		}
		else
		{
			int xEnd2 = (int)((this.minX + this.maxX) / 2);
			int yEnd2 = (int)this.maxY;
			if (this.maxY > this.minY + 24)
			{
				yEnd2 = (int)((this.minY + this.maxY) / 2);
			}
			GameScr.gI().auto = 0;
			global::Char.myCharz().currentMovePoint = new MovePoint(xEnd2, yEnd2);
			global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
			Service.gI().charMove();
		}
	}

	// Token: 0x0400085A RID: 2138
	public short minX;

	// Token: 0x0400085B RID: 2139
	public short minY;

	// Token: 0x0400085C RID: 2140
	public short maxX;

	// Token: 0x0400085D RID: 2141
	public short maxY;

	// Token: 0x0400085E RID: 2142
	public bool isEnter;

	// Token: 0x0400085F RID: 2143
	public bool isOffline;

	// Token: 0x04000860 RID: 2144
	public PopUp popup;
}
