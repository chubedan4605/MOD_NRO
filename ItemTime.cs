using System;

// Token: 0x02000069 RID: 105
public class ItemTime
{
	// Token: 0x060003B9 RID: 953 RVA: 0x00005EEE File Offset: 0x000040EE
	public ItemTime()
	{
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00024100 File Offset: 0x00022300
	public ItemTime(short idIcon, int s)
	{
		this.idIcon = idIcon;
		this.minute = s / 60;
		this.second = s % 60;
		this.time = s;
		this.coutTime = s;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isPaint_coolDownBar = (idIcon == 14);
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00024164 File Offset: 0x00022364
	public void initTimeText(sbyte id, string text, int time)
	{
		if (time == -1)
		{
			this.dontClear = true;
		}
		else
		{
			this.dontClear = false;
		}
		this.isText = true;
		this.minute = time / 60;
		this.second = time % 60;
		this.idIcon = (short)id;
		this.time = time;
		this.coutTime = time;
		this.text = text;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isPaint_coolDownBar = (this.idIcon == 14);
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000241E8 File Offset: 0x000223E8
	public void initTime(int time, bool isText)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.time = time;
		this.coutTime = time;
		this.isText = isText;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00024234 File Offset: 0x00022434
	public static bool isExistItem(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0002427C File Offset: 0x0002247C
	public static ItemTime getMessageById(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x000242C4 File Offset: 0x000224C4
	public static bool isExistMessage(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0002430C File Offset: 0x0002250C
	public static ItemTime getItemById(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00024354 File Offset: 0x00022554
	public void initTime(int time)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.coutTime = time;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00024390 File Offset: 0x00022590
	public void paint(mGraphics g, int x, int y)
	{
		SmallImage.drawSmallImage(g, (int)this.idIcon, x, y, 0, 3);
		string st = string.Empty;
		st = this.minute + "'";
		if (this.minute == 0)
		{
			st = this.second + "s";
		}
		mFont.tahoma_7b_white.drawString(g, st, x, y + 15, 2, mFont.tahoma_7b_dark);
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00024404 File Offset: 0x00022604
	public void paintText(mGraphics g, int x, int y)
	{
		if (this.isPaint_coolDownBar)
		{
			if (global::Char.myCharz() != null)
			{
				int num = 80;
				int x2 = GameCanvas.w / 2 - num / 2;
				int y2 = GameCanvas.h - 80;
				g.setColor(8421504);
				g.fillRect(x2, y2, num, 2);
				g.setColor(16777215);
				if (this.per > 0)
				{
					g.fillRect(x2, y2, num * this.per / 100, 2);
				}
			}
		}
		else
		{
			string str = string.Empty;
			str = this.minute + "'";
			if (this.minute < 1)
			{
				str = this.second + "s";
			}
			if (this.minute < 0)
			{
				str = string.Empty;
			}
			if (this.dontClear)
			{
				str = string.Empty;
			}
			mFont.tahoma_7b_white.drawString(g, this.text + " " + str, x, y, 0, mFont.tahoma_7b_dark);
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00024508 File Offset: 0x00022708
	public void update()
	{
		this.curr = mSystem.currentTimeMillis();
		if (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
		{
			return;
		}
		if (this.curr - this.last >= 1000L)
		{
			this.last = mSystem.currentTimeMillis();
			this.second--;
			this.coutTime--;
			if (this.second <= 0)
			{
				this.second = 60;
				this.minute--;
			}
			if (this.time > 0)
			{
				this.per = this.coutTime * 100 / this.time;
			}
		}
		if (this.minute < 0 && !this.isText)
		{
			global::Char.vItemTime.removeElement(this);
		}
		if (this.minute < 0 && this.isText && !this.dontClear)
		{
			GameScr.textTime.removeElement(this);
		}
	}

	// Token: 0x0400064F RID: 1615
	public short idIcon;

	// Token: 0x04000650 RID: 1616
	public int second;

	// Token: 0x04000651 RID: 1617
	public int minute;

	// Token: 0x04000652 RID: 1618
	private long curr;

	// Token: 0x04000653 RID: 1619
	private long last;

	// Token: 0x04000654 RID: 1620
	private bool isText;

	// Token: 0x04000655 RID: 1621
	private bool dontClear;

	// Token: 0x04000656 RID: 1622
	private string text;

	// Token: 0x04000657 RID: 1623
	public bool isPaint_coolDownBar;

	// Token: 0x04000658 RID: 1624
	public int time;

	// Token: 0x04000659 RID: 1625
	public int coutTime;

	// Token: 0x0400065A RID: 1626
	private int per = 100;
}
