using System;

// Token: 0x020000C7 RID: 199
public class ServerScr : mScreen, IActionListener
{
	// Token: 0x06000A20 RID: 2592 RVA: 0x00098B58 File Offset: 0x00096D58
	public ServerScr()
	{
		TileMap.bgID = (int)((byte)(mSystem.currentTimeMillis() % 9L));
		if (TileMap.bgID == 5 || TileMap.bgID == 6)
		{
			TileMap.bgID = 4;
		}
		GameScr.loadCamera(true, -1, -1);
		GameScr.cmx = 100;
		GameScr.cmy = 200;
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00098C48 File Offset: 0x00096E48
	public override void switchToMe()
	{
		Res.outz("switchToMe >>>>ServerScr: " + Rms.loadRMSInt(ServerListScreen.RMS_svselect));
		SoundMn.gI().stopAll();
		base.switchToMe();
		this.loadIconHead();
		this.mainSelect = ServerListScreen.ipSelect;
		this.numw = 1;
		this.numh = 1;
		this.Load_NewUI();
		if (!this.isPaintNewUi && !this.isChooseArea)
		{
			this.cmdGlobal = new Command(this.strArea[0], this, 98, null);
			this.cmdGlobal.x = 0;
			this.cmdGlobal.y = 0;
			this.cmdVietNam = new Command(this.strArea[1], this, 97, null);
			this.cmdVietNam.x = 50;
			this.cmdVietNam.y = 0;
			this.vecServer = new MyVector();
			this.vecServer.addElement(this.cmdGlobal);
			this.vecServer.addElement(this.cmdVietNam);
			this.sort();
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00098D50 File Offset: 0x00096F50
	private void sort()
	{
		this.mainSelect = ServerListScreen.ipSelect;
		this.w2c = 5;
		this.wc = 76;
		this.hc = mScreen.cmdH;
		this.numw = 2;
		if (this.vecServer.size() > 2)
		{
			this.numw = GameCanvas.w / (this.wc + this.w2c);
		}
		this.numh = this.vecServer.size() / this.numw + ((this.vecServer.size() % this.numw != 0) ? 1 : 0);
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			if (command != null)
			{
				int num = GameCanvas.hw - this.numw * (this.wc + this.w2c) / 2;
				int num2 = num + i % this.numw * (this.wc + this.w2c);
				int num3 = GameCanvas.hh - this.numh * (this.hc + this.w2c) / 2;
				int num4 = num3 + i / this.numw * (this.hc + this.w2c);
				command.x = num2;
				command.y = num4;
				command.w = this.wc;
			}
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00098EA8 File Offset: 0x000970A8
	private void sort_newUI()
	{
		this.mainSelect = ServerListScreen.ipSelect;
		this.w2c = 5;
		this.wc = 76;
		this.hc = mScreen.cmdH;
		this.numw = 1;
		int num = this.xsub + this.wsub / 2 + 3;
		this.ysubpaint = this.ysub + 5;
		this.numw = this.wsub / (this.wc + this.w2c);
		this.numh = this.vecServer.size() / this.numw + ((this.vecServer.size() % this.numw != 0) ? 1 : 0);
		this.xsubpaint = num - this.numw * (this.wc + this.w2c) / 2;
		for (int i = 0; i < this.vecServer.size(); i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			if (command != null)
			{
				int num2 = this.xsubpaint + i % this.numw * (this.wc + this.w2c);
				int num3 = this.ysubpaint + i / this.numw * (this.hc + this.w2c);
				command.x = num2;
				command.y = num3;
				command.w = this.wc;
			}
		}
		this.list = new ListNew(this.xsub, this.ysub, this.wsub, this.hsub, 0, 0, 0, true);
		this.list.setMaxCamera(this.numh * (this.hc + this.w2c) - this.hsub);
		this.list.resetList();
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00099054 File Offset: 0x00097254
	public override void update()
	{
		GameScr.cmx++;
		if (GameScr.cmx > GameCanvas.w * 3 + 100)
		{
			GameScr.cmx = 100;
		}
		if (!this.isPaintNewUi)
		{
			for (int i = 0; i < this.vecServer.size(); i++)
			{
				Command command = (Command)this.vecServer.elementAt(i);
				if (!GameCanvas.isTouch)
				{
					if (i == this.mainSelect)
					{
						if (GameCanvas.gameTick % 10 < 4)
						{
							command.isFocus = true;
						}
						else
						{
							command.isFocus = false;
						}
						this.cmdCheck = new Command(mResources.SELECT, this, command.idAction, null);
						this.center = this.cmdCheck;
					}
					else
					{
						command.isFocus = false;
					}
				}
				else if (command != null && command.isPointerPressInside())
				{
					command.performAction();
				}
			}
		}
		this.UpdTouch_NewUI();
		this.UpdTouch_NewUI_Popup();
		ServerListScreen.updateDeleteData();
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00099158 File Offset: 0x00097358
	public override void paint(mGraphics g)
	{
		GameCanvas.paintBGGameScr(g);
		if (this.isChooseArea)
		{
			this.paintChooseArea(g);
		}
		else if (this.isPaintNewUi)
		{
			this.paintNewSelectMenu(g);
			if (ServerListScreen.cmdDeleteRMS != null)
			{
				mFont.tahoma_7_white.drawString(g, mResources.xoadulieu, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
			}
		}
		else
		{
			for (int i = 0; i < this.vecServer.size(); i++)
			{
				if (this.vecServer.elementAt(i) != null)
				{
					((Command)this.vecServer.elementAt(i)).paint(g);
				}
			}
		}
		base.paint(g);
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00099214 File Offset: 0x00097414
	public override void updateKey()
	{
		base.updateKey();
		int num = this.mainSelect % this.numw;
		int num2 = this.mainSelect / this.numw;
		if (GameCanvas.keyPressed[4])
		{
			if (num > 0)
			{
				this.mainSelect--;
			}
			GameCanvas.keyPressed[4] = false;
		}
		else if (GameCanvas.keyPressed[6])
		{
			if (num < this.numw - 1)
			{
				this.mainSelect++;
			}
			GameCanvas.keyPressed[6] = false;
		}
		else if (GameCanvas.keyPressed[2])
		{
			if (num2 > 0)
			{
				this.mainSelect -= this.numw;
			}
			GameCanvas.keyPressed[2] = false;
		}
		else if (GameCanvas.keyPressed[8])
		{
			if (num2 < this.numh - 1)
			{
				this.mainSelect += this.numw;
			}
			GameCanvas.keyPressed[8] = false;
		}
		if (this.mainSelect < 0)
		{
			this.mainSelect = 0;
		}
		if (this.mainSelect >= this.vecServer.size())
		{
			this.mainSelect = this.vecServer.size() - 1;
		}
		if (GameCanvas.keyPressed[5])
		{
			((Command)this.vecServer.elementAt(num)).performAction();
			GameCanvas.keyPressed[5] = false;
		}
		GameCanvas.clearKeyPressed();
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00099380 File Offset: 0x00097580
	public void perform(int idAction, object p)
	{
		Res.outz("idAction >>>>   " + idAction);
		switch (idAction)
		{
		case 97:
			if (!this.isPaintNewUi)
			{
				this.vecServer.removeAllElements();
				for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
				{
					if ((int)ServerListScreen.language[i] != 0)
					{
						this.vecServer.addElement(new Command(ServerListScreen.nameServer[i], this, 100 + i, null));
					}
				}
				this.sort();
			}
			break;
		case 98:
			if (!this.isPaintNewUi)
			{
				this.vecServer.removeAllElements();
				for (int j = 0; j < ServerListScreen.nameServer.Length; j++)
				{
					if ((int)ServerListScreen.language[j] == 0)
					{
						this.vecServer.addElement(new Command(ServerListScreen.nameServer[j], this, 100 + j, null));
					}
				}
				this.sort();
			}
			break;
		case 99:
			Session_ME.gI().clearSendingMessage();
			ServerListScreen.SetIpSelect(this.mainSelect, false);
			GameCanvas.serverScreen.selectServer();
			GameCanvas.serverScreen.switchToMe();
			break;
		default:
			if (idAction != 999)
			{
				ServerListScreen.SetIpSelect(idAction - 100, true);
				if (GameCanvas.serverScreen == null)
				{
					GameCanvas.serverScreen = new ServerListScreen();
				}
				GameCanvas.serverScreen.selectServer();
				ServerListScreen.ConnectIP();
				GameCanvas.serverScreen.switchToMe();
			}
			else
			{
				this.Save_RMS_Area();
				this.SetNewSelectMenu((int)this.select_Area, 0);
			}
			break;
		}
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0009951C File Offset: 0x0009771C
	public void SetNewSelectMenu(int area, int typeSv)
	{
		this.isChooseArea = false;
		if (mSystem.clientType != 1)
		{
			this.isPaintNewUi = true;
		}
		this.wCheck = 10;
		this.w = GameCanvas.w / 3 * 2;
		this.h = GameCanvas.h / 3 * 2;
		this.x = (GameCanvas.w - this.w) / 2;
		this.y = (GameCanvas.h - this.h) / 2 + 20;
		this.xName = GameCanvas.w / 2;
		this.yName = this.y - 30;
		this.wsub = this.w / 3 * 2;
		this.wPop = this.w - this.wsub - 15;
		if (this.wPop < 80)
		{
			this.wPop = 80;
			this.wsub = this.w - this.wPop - 15;
		}
		this.hsub = this.h - 10 - this.wCheck;
		this.xsub = this.x + this.w - this.wsub - 5;
		this.ysub = this.y + 5;
		this.xPop = this.x + 5;
		this.yPop = this.y + 5;
		this.hPop = 20;
		this.xinfo = this.x + 5;
		this.yinfo = this.y + this.strTypeSV.Length * (this.hPop + 5) + 5;
		this.winfo = this.wPop;
		this.hinfo = this.h - (5 + this.strTypeSV.Length * (this.hPop + 5) + 5) - this.wCheck;
		this.yBox = 10;
		this.wBox = 70;
		this.hBox = 20;
		this.GetVecTypeSv((sbyte)area, (sbyte)typeSv);
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x000996EC File Offset: 0x000978EC
	private void GetVecTypeSv(sbyte area, sbyte typeSv)
	{
		this.vecServer.removeAllElements();
		this.ntypeSv = 1;
		this.select_Area = area;
		mResources.loadLanguague(area);
		for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
		{
			if ((int)area == 1)
			{
				if ((int)ServerListScreen.language[i] != 0 && (int)ServerListScreen.typeSv[i] == 1)
				{
					this.ntypeSv = 2;
				}
			}
			else if ((int)ServerListScreen.typeSv[i] == 1)
			{
				this.ntypeSv = 2;
			}
		}
		if ((int)typeSv > (int)((sbyte)(this.ntypeSv - 1)))
		{
			typeSv = (sbyte)(this.ntypeSv - 1);
		}
		this.select_typeSv = typeSv;
		for (int j = 0; j < ServerListScreen.nameServer.Length; j++)
		{
			if ((int)area == 1)
			{
				if ((int)ServerListScreen.language[j] != 0)
				{
					if ((int)ServerListScreen.typeSv[j] == 1)
					{
						this.ntypeSv = 2;
					}
					if ((int)ServerListScreen.typeSv[j] == (int)typeSv)
					{
						int num = -1;
						if (ServerListScreen.typeClass != null && j < ServerListScreen.typeClass.Length)
						{
							num = (int)ServerListScreen.typeClass[j];
						}
						if (!ServerScr.isShowSv_HaveChar || num != -1)
						{
							Command command = new Command(ServerListScreen.nameServer[j], this, 100 + j, null);
							command.isPaintNew = ((int)ServerListScreen.isNew[j] == 1);
							if (num > -1)
							{
								command.imgBtn = ServerScr.iconHead[num];
							}
							this.vecServer.addElement(command);
						}
					}
				}
			}
			else
			{
				if ((int)ServerListScreen.typeSv[j] == 1)
				{
					this.ntypeSv = 2;
				}
				if ((int)ServerListScreen.language[j] == 0 && (int)ServerListScreen.typeSv[j] == (int)typeSv)
				{
					int num2 = -1;
					if (ServerListScreen.typeClass != null && j < ServerListScreen.typeClass.Length)
					{
						num2 = (int)ServerListScreen.typeClass[j];
					}
					if (!ServerScr.isShowSv_HaveChar || num2 != -1)
					{
						Command command2 = new Command(ServerListScreen.nameServer[j], this, 100 + j, null);
						command2.isPaintNew = ((int)ServerListScreen.isNew[j] == 1);
						if (num2 > -1)
						{
							command2.imgBtn = ServerScr.iconHead[num2];
						}
						this.vecServer.addElement(command2);
					}
				}
			}
		}
		this.Sort_NewSv();
		this.sort_newUI();
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00099938 File Offset: 0x00097B38
	private void paintChooseArea(mGraphics g)
	{
		if (!this.isChooseArea)
		{
			return;
		}
		this.paint_Area(g, GameCanvas.hw - this.wBox / 2, this.yBox);
		this.paint_Lang(g, GameCanvas.hw + 20, this.yBox);
		this.cmdChooseArea.paint(g);
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00099990 File Offset: 0x00097B90
	private void paintNewSelectMenu(mGraphics g)
	{
		if (!this.isPaintNewUi)
		{
			return;
		}
		g.setColor(14601141);
		g.fillRect(this.x, this.y, this.w, this.h);
		PopUp.paintPopUp(g, this.xName - 50, this.yName, 100, 20, 0, true);
		mFont.tahoma_7b_dark.drawString(g, mResources.selectServer2, this.xName, this.yName + 5, 2);
		for (int i = 0; i < this.ntypeSv; i++)
		{
			int num = this.yPop + i * (this.hPop + 5);
			PopUp.paintPopUp(g, this.xPop, num, this.wPop, this.hPop, ((int)this.select_typeSv != i) ? 0 : 1, true);
			mFont.tahoma_7b_dark.drawString(g, this.strTypeSV[i], this.xPop + this.wPop / 2, num + 5, 2);
		}
		g.setColor(10254674);
		g.fillRect(this.xinfo, this.yinfo, this.winfo, this.hinfo);
		string[] array = mFont.tahoma_7.splitFontArray(this.strTypeSV_info[(int)this.select_typeSv], this.winfo - 10);
		for (int j = 0; j < array.Length; j++)
		{
			mFont.tahoma_7_white.drawString(g, array[j], this.xinfo + 5, this.yinfo + 5 + j * 11, 0);
		}
		this.paintShowAllCheck(g);
		this.paint_Area(g, 10, this.yBox);
		this.paint_Lang(g, GameCanvas.w - this.wBox - 10, this.yBox);
		g.setColor(10254674);
		g.fillRect(this.xsub, this.ysub, this.wsub, this.hsub);
		g.setClip(this.xsub, this.ysub, this.wsub, this.hsub);
		g.translate(0, -this.list.cmx);
		for (int k = 0; k < this.vecServer.size(); k++)
		{
			Command command = (Command)this.vecServer.elementAt(k);
			if (command != null)
			{
				command.paint(g);
				if (command.isPaintNew && GameCanvas.gameTick % 10 > 1)
				{
					g.drawImage(Panel.imgNew, command.x + 60, command.y, 0);
				}
			}
		}
		GameCanvas.resetTrans(g);
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00099C18 File Offset: 0x00097E18
	private void paint_Area(mGraphics g, int x, int y)
	{
		x -= 5;
		this.xPopUp_Area = x;
		PopUp.paintPopUp(g, x, y, this.wBox, this.hBox, 0, true);
		mFont.tahoma_7b_dark.drawString(g, this.strArea[(int)this.select_Area], x + (this.wBox - 10) / 2, y + 5, 2);
		g.drawRegion(Mob.imgHP, 0, 30, 9, 6, 0, x + this.wBox - 10, y + 14, mGraphics.BOTTOM | mGraphics.HCENTER);
		if (this.isPaint_select_area)
		{
			this.yPopUp_Area = y + this.hBox + 5;
			g.setColor(10254674);
			g.fillRect(x, this.yPopUp_Area, this.wBox, this.strArea.Length * this.htext + 1);
			for (int i = 0; i < this.strArea.Length; i++)
			{
				mFont.tahoma_7_white.drawString(g, this.strArea[i], x + this.wBox / 2, this.yPopUp_Area + i * this.htext + 2, 2);
				if ((int)this.select_Area == i)
				{
					g.setColor(15591444);
					g.drawRect(x + 2, this.yPopUp_Area + i * this.htext + 1, this.wBox - 4, this.htext - 2);
				}
			}
		}
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x000045ED File Offset: 0x000027ED
	private void paint_Lang(mGraphics g, int x, int y)
	{
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00099D74 File Offset: 0x00097F74
	private void UpdTouch_NewUI()
	{
		if (!this.isPaintNewUi)
		{
			return;
		}
		int num = 0;
		if (this.list != null)
		{
			this.list.moveCamera();
			if (GameCanvas.isPointer(this.xsub, 0, this.wsub, GameCanvas.h))
			{
				this.list.update_Pos_UP_DOWN();
			}
			num = this.list.cmx;
		}
		if (GameCanvas.isPointer(this.xsub, this.ysub, this.wsub, this.hsub))
		{
			int num2 = (GameCanvas.px - this.xsubpaint) / (this.wc + this.w2c) + (GameCanvas.py - this.ysubpaint + num) / (this.hc + this.w2c) * this.numw;
			int num3 = this.vecServer.size();
			if (num2 >= 0 && num2 < num3)
			{
				this.mainSelect = num2;
				for (int i = 0; i < this.vecServer.size(); i++)
				{
					Command command = (Command)this.vecServer.elementAt(i);
					if (command != null)
					{
						if (i == this.mainSelect)
						{
							if (command.isPointerPressInsideCamera(0, num))
							{
								command.performAction();
							}
						}
						else
						{
							command.isFocus = false;
						}
					}
				}
			}
		}
		if (GameCanvas.isPointer(this.xinfo - 2, this.yinfo + this.hinfo, this.wCheck + 4, this.wCheck + 4) && GameCanvas.isPointerJustRelease)
		{
			ServerScr.isShowSv_HaveChar = !ServerScr.isShowSv_HaveChar;
			this.GetVecTypeSv(this.select_Area, this.select_typeSv);
		}
		if (this.ntypeSv == 1)
		{
			return;
		}
		sbyte b = 0;
		while ((int)b < this.ntypeSv)
		{
			int num4 = this.yPop + (int)b * (this.hPop + 5);
			if (GameCanvas.isPointerHoldIn(this.xPop, num4, this.wPop, this.hPop) && GameCanvas.isPointerDown)
			{
				this.GetVecTypeSv(this.select_Area, b);
				break;
			}
			b = (sbyte)((int)b + 1);
		}
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00099F94 File Offset: 0x00098194
	private void UpdTouch_NewUI_Popup()
	{
		if (GameCanvas.isPointer(this.xPopUp_Area, this.yBox, this.wBox, this.hBox) && GameCanvas.isPointerJustRelease)
		{
			this.isPaint_select_area = !this.isPaint_select_area;
			this.isPaint_select_lang = false;
			GameCanvas.isPointerJustRelease = false;
		}
		if (this.isPaint_select_area)
		{
			sbyte b = 0;
			while ((int)b < this.strArea.Length)
			{
				int num = this.yPopUp_Area + (int)b * this.htext;
				if (GameCanvas.isPointerHoldIn(this.xPopUp_Area, num, this.wBox, this.htext) && GameCanvas.isPointerDown)
				{
					if (this.isChooseArea)
					{
						this.select_Area = b;
					}
					else
					{
						this.SetNewSelectMenu((int)b, (int)this.select_typeSv);
					}
					this.isPaint_select_lang = (this.isPaint_select_area = false);
					break;
				}
				b = (sbyte)((int)b + 1);
			}
		}
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0009A084 File Offset: 0x00098284
	private void Load_NewUI()
	{
		if (!GameCanvas.isTouch)
		{
			return;
		}
		if (Rms.loadRMS("area_select") == null)
		{
			this.isChooseArea = true;
			this.cmdChooseArea = new Command(mResources.OK, this, 999, null);
			this.cmdChooseArea.x = GameCanvas.hw - 38;
			this.cmdChooseArea.y = GameCanvas.hh + 50;
			this.vecServer = new MyVector();
			this.vecServer.addElement(this.cmdChooseArea);
			this.yBox = GameCanvas.hh - 30;
			this.wBox = 70;
			this.hBox = 20;
		}
		else
		{
			this.isChooseArea = false;
			this.Load_RMS_Area();
			this.SetNewSelectMenu((int)this.select_Area, (int)this.select_typeSv);
		}
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0009A150 File Offset: 0x00098350
	private void Save_RMS_Area()
	{
		Rms.saveRMS("area_select", new sbyte[]
		{
			this.select_Area,
			this.select_Lang
		});
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0009A184 File Offset: 0x00098384
	private void Load_RMS_Area()
	{
		sbyte[] array = Rms.loadRMS("area_select");
		try
		{
			this.select_Area = array[0];
			this.select_Lang = array[1];
		}
		catch (Exception ex)
		{
			this.select_Area = (this.select_Lang = 0);
		}
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0009A1DC File Offset: 0x000983DC
	public void Sort_NewSv()
	{
		for (int i = 0; i < this.vecServer.size() - 1; i++)
		{
			Command command = (Command)this.vecServer.elementAt(i);
			for (int j = i + 1; j < this.vecServer.size(); j++)
			{
				Command command2 = (Command)this.vecServer.elementAt(j);
				if (command2.isPaintNew && !command.isPaintNew)
				{
					Command command3 = command2;
					command2 = command;
					command = command3;
					this.vecServer.setElementAt(command, i);
					this.vecServer.setElementAt(command2, j);
				}
			}
		}
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0009A284 File Offset: 0x00098484
	public void loadIconHead()
	{
		if (ServerScr.iconHead != null)
		{
			return;
		}
		ServerScr.iconHead = new Image[3];
		for (int i = 0; i < ServerScr.iconHead.Length; i++)
		{
			ServerScr.iconHead[i] = GameCanvas.loadImage("/iconHead_" + i + ".png");
		}
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0009A2E0 File Offset: 0x000984E0
	public void paintShowAllCheck(mGraphics g)
	{
		int num = this.xinfo;
		int num2 = this.yinfo + this.hinfo + 2;
		g.setColor(16777215);
		g.fillRect(num, num2, this.wCheck, this.wCheck);
		if (ServerScr.isShowSv_HaveChar)
		{
			g.setColor(3329330);
			g.fillRect(num + 1, num2 + 1, this.wCheck - 2, this.wCheck - 2);
		}
		mFont.tahoma_7b_dark.drawString(g, this.strShowAll, num + this.wCheck + 2, num2, 0);
	}

	// Token: 0x040012C7 RID: 4807
	private int mainSelect;

	// Token: 0x040012C8 RID: 4808
	private MyVector vecServer = new MyVector();

	// Token: 0x040012C9 RID: 4809
	private Command cmdCheck;

	// Token: 0x040012CA RID: 4810
	public const int icmd = 100;

	// Token: 0x040012CB RID: 4811
	private int wc;

	// Token: 0x040012CC RID: 4812
	private int hc;

	// Token: 0x040012CD RID: 4813
	private int w2c;

	// Token: 0x040012CE RID: 4814
	private int numw;

	// Token: 0x040012CF RID: 4815
	private int numh;

	// Token: 0x040012D0 RID: 4816
	private Command cmdGlobal;

	// Token: 0x040012D1 RID: 4817
	private Command cmdVietNam;

	// Token: 0x040012D2 RID: 4818
	private const string RMS_SELECT_AREA = "area_select";

	// Token: 0x040012D3 RID: 4819
	public bool isChooseArea;

	// Token: 0x040012D4 RID: 4820
	public bool isPaintNewUi;

	// Token: 0x040012D5 RID: 4821
	private ListNew list;

	// Token: 0x040012D6 RID: 4822
	public sbyte select_Area;

	// Token: 0x040012D7 RID: 4823
	public sbyte select_Lang;

	// Token: 0x040012D8 RID: 4824
	public sbyte select_typeSv;

	// Token: 0x040012D9 RID: 4825
	private Command cmdChooseArea;

	// Token: 0x040012DA RID: 4826
	private bool isPaint_select_area;

	// Token: 0x040012DB RID: 4827
	private bool isPaint_select_lang;

	// Token: 0x040012DC RID: 4828
	private int x;

	// Token: 0x040012DD RID: 4829
	private int y;

	// Token: 0x040012DE RID: 4830
	private int w;

	// Token: 0x040012DF RID: 4831
	private int h;

	// Token: 0x040012E0 RID: 4832
	private int xName;

	// Token: 0x040012E1 RID: 4833
	private int yName;

	// Token: 0x040012E2 RID: 4834
	private int xsub;

	// Token: 0x040012E3 RID: 4835
	private int ysub;

	// Token: 0x040012E4 RID: 4836
	private int wsub;

	// Token: 0x040012E5 RID: 4837
	private int hsub;

	// Token: 0x040012E6 RID: 4838
	private int xsubpaint;

	// Token: 0x040012E7 RID: 4839
	private int ysubpaint;

	// Token: 0x040012E8 RID: 4840
	private int xPop;

	// Token: 0x040012E9 RID: 4841
	private int yPop;

	// Token: 0x040012EA RID: 4842
	private int wPop;

	// Token: 0x040012EB RID: 4843
	private int hPop;

	// Token: 0x040012EC RID: 4844
	private int xinfo;

	// Token: 0x040012ED RID: 4845
	private int yinfo;

	// Token: 0x040012EE RID: 4846
	private int winfo;

	// Token: 0x040012EF RID: 4847
	private int hinfo;

	// Token: 0x040012F0 RID: 4848
	private int yBox;

	// Token: 0x040012F1 RID: 4849
	private int wBox;

	// Token: 0x040012F2 RID: 4850
	private int hBox;

	// Token: 0x040012F3 RID: 4851
	private int ntypeSv;

	// Token: 0x040012F4 RID: 4852
	private int wCheck;

	// Token: 0x040012F5 RID: 4853
	private int xPopUp_Area;

	// Token: 0x040012F6 RID: 4854
	private int yPopUp_Area;

	// Token: 0x040012F7 RID: 4855
	private int xPopUp_Lang;

	// Token: 0x040012F8 RID: 4856
	private int yPopUp_Lang;

	// Token: 0x040012F9 RID: 4857
	private int htext = 15;

	// Token: 0x040012FA RID: 4858
	private string[] strLang = new string[]
	{
		"Tiếng Việt",
		"English",
		"Indo"
	};

	// Token: 0x040012FB RID: 4859
	private string[] strArea = new string[]
	{
		"VIỆT NAM",
		"GLOBAL"
	};

	// Token: 0x040012FC RID: 4860
	private string[] strTypeSV = new string[]
	{
		"Máy chủ tiêu chuẩn",
		"Máy chủ Super"
	};

	// Token: 0x040012FD RID: 4861
	private string[] strTypeSV_info = new string[]
	{
		"Máy chủ tiêu chuẩn:\nTiến trình game bình thường.",
		"Máy chủ Super:\n -Không thể giao dịch vàng.\n x3 Sức mạnh\n x3 Tiềm năng\n x3 Vàng\n x3 Vật phẩm khác"
	};

	// Token: 0x040012FE RID: 4862
	private string strShowAll = "Chỉ hiện thị máy chủ đã chơi.";

	// Token: 0x040012FF RID: 4863
	public int cmy;

	// Token: 0x04001300 RID: 4864
	public static Image[] iconHead;

	// Token: 0x04001301 RID: 4865
	public static bool isShowSv_HaveChar;
}
