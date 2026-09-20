using System;
using System.Collections.Generic;
using System.Threading;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x0200010A RID: 266
	public class AutoItem : IActionListener, IChatable
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x000093A2 File Offset: 0x000075A2
		public static AutoItem getInstance()
		{
			if (AutoItem._Instance == null)
			{
				AutoItem._Instance = new AutoItem();
			}
			return AutoItem._Instance;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000A66F0 File Offset: 0x000A48F0
		public static void Update()
		{
			if (AutoItem.listItemAuto.Count <= 0)
			{
				return;
			}
			for (int i = 0; i < AutoItem.listItemAuto.Count; i++)
			{
				AutoItem.Item item = AutoItem.listItemAuto[i];
				if (mSystem.currentTimeMillis() - item.LastTimeUse > (long)(item.Delay * 1000))
				{
					item.LastTimeUse = mSystem.currentTimeMillis();
					Service.gI().useItem(0, 1, -1, (short)item.Id);
					return;
				}
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000A6768 File Offset: 0x000A4968
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() == null || ChatTextField.gI().tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
			{
				ChatTextField.gI().isShow = false;
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoItem.inputDelay[0]))
			{
				try
				{
					int num = int.Parse(ChatTextField.gI().tfChat.getText());
					AutoItem.itemToAuto.Delay = num;
					GameScr.info1.addInfo(string.Concat(new object[]
					{
						"Auto ",
						AutoItem.itemToAuto.Name,
						": ",
						num,
						" giây"
					}), 0);
					AutoItem.listItemAuto.Add(AutoItem.itemToAuto);
				}
				catch
				{
					GameScr.info1.addInfo("Delay Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
				}
				AutoItem.ResetChatTextField();
				return;
			}
			if (ChatTextField.gI().strChat.Equals(AutoItem.inputBuyQuantity[0]))
			{
				try
				{
					int quantity = int.Parse(ChatTextField.gI().tfChat.getText());
					AutoItem.itemToAuto.Quantity = quantity;
					new Thread(delegate()
					{
						this.AutoBuy(AutoItem.itemToAuto);
					}).Start();
				}
				catch
				{
					GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
				}
				AutoItem.ResetChatTextField();
				return;
			}
			if (!ChatTextField.gI().strChat.Equals(AutoItem.inputSellQuantity[0]))
			{
				return;
			}
			try
			{
				int quantity2 = int.Parse(ChatTextField.gI().tfChat.getText());
				AutoItem.itemToAuto.Quantity = quantity2;
				new Thread(delegate()
				{
					AutoItem.AutoSell(AutoItem.itemToAuto);
				}).Start();
			}
			catch
			{
				GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
			}
			AutoItem.ResetChatTextField();
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000045ED File Offset: 0x000027ED
		public void onCancelChat()
		{
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000A6974 File Offset: 0x000A4B74
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				AutoItem.OpenTFAutoUseItem((AutoItem.Item)p);
				return;
			case 2:
				AutoItem.RemoveItemAuto((int)p);
				return;
			case 3:
				AutoItem.OpenTFAutoTradeItem((AutoItem.Item)p);
				return;
			case 4:
				AutoItem.set1.Add(((global::Item)p).getFullName());
				return;
			case 5:
				AutoItem.set2.Add(((global::Item)p).getFullName());
				return;
			case 6:
				AutoItem.set1.Remove(((global::Item)p).getFullName());
				return;
			case 7:
				AutoItem.set2.Remove(((global::Item)p).getFullName());
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00009276 File Offset: 0x00007476
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000A6A24 File Offset: 0x000A4C24
		public static bool isAutoUse(int templateId)
		{
			for (int i = 0; i < AutoItem.listItemAuto.Count; i++)
			{
				if (AutoItem.listItemAuto[i].Id == templateId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000A6A5C File Offset: 0x000A4C5C
		private static void RemoveItemAuto(int templateId)
		{
			for (int i = 0; i < AutoItem.listItemAuto.Count; i++)
			{
				if (AutoItem.listItemAuto[i].Id == templateId)
				{
					AutoItem.listItemAuto.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000A6AA0 File Offset: 0x000A4CA0
		private static void OpenTFAutoUseItem(AutoItem.Item item)
		{
			AutoItem.itemToAuto = item;
			ChatTextField.gI().strChat = AutoItem.inputDelay[0];
			ChatTextField.gI().tfChat.name = AutoItem.inputDelay[1];
			GameCanvas.panel.isShow = false;
			ChatTextField.gI().startChat2(AutoItem.getInstance(), string.Empty);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000A6AFC File Offset: 0x000A4CFC
		private static void OpenTFAutoTradeItem(AutoItem.Item item)
		{
			AutoItem.itemToAuto = item;
			GameCanvas.panel.isShow = false;
			if (item.IsSell)
			{
				ChatTextField.gI().strChat = AutoItem.inputSellQuantity[0];
				ChatTextField.gI().tfChat.name = AutoItem.inputSellQuantity[1];
			}
			else
			{
				ChatTextField.gI().strChat = AutoItem.inputBuyQuantity[0];
				ChatTextField.gI().tfChat.name = AutoItem.inputBuyQuantity[1];
			}
			ChatTextField.gI().startChat2(AutoItem.getInstance(), string.Empty);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000A6B88 File Offset: 0x000A4D88
		private static void AutoSell(AutoItem.Item item)
		{
			Thread.Sleep(100);
			short index = item.Index;
			while (item.Quantity > 0)
			{
				if (global::Char.myCharz().arrItemBag[(int)index] == null || (global::Char.myCharz().arrItemBag[(int)index] != null && global::Char.myCharz().arrItemBag[(int)index].template.id != (short)item.Id))
				{
					GameScr.info1.addInfo("Không Tìm Thấy Item!", 0);
					return;
				}
				Service.gI().saleItem(0, 1, (short)(index + 3));
				Thread.Sleep(100);
				Service.gI().saleItem(1, 1, index);
				Thread.Sleep(1000);
				item.Quantity--;
				if (global::Char.myCharz().xu > 1963100000L)
				{
					GameScr.info1.addInfo("Xong!", 0);
					return;
				}
			}
			GameScr.info1.addInfo("Xong!", 0);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000A6C70 File Offset: 0x000A4E70
		private void AutoBuy(AutoItem.Item item)
		{
			while (item.Quantity > 0 && !GameScr.gI().isBagFull())
			{
				Service.gI().buyItem((sbyte)((!item.IsGold) ? 1 : 0), item.Id, 0);
				item.Quantity--;
				Thread.Sleep(1000);
			}
			GameScr.info1.addInfo("Xong!", 0);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000A6CDC File Offset: 0x000A4EDC
		public static void useSet(int type)
		{
			if (AutoItem.isChangingSet)
			{
				GameScr.info1.addInfo("Đang Mặc Đồ!", 0);
				return;
			}
			new Thread(delegate()
			{
				if (type == 0)
				{
					AutoItem.ChangeSet(AutoItem.set1);
				}
				if (type == 1)
				{
					AutoItem.ChangeSet(AutoItem.set2);
				}
			}).Start();
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000A6D24 File Offset: 0x000A4F24
		private static void ChangeSet(List<string> set)
		{
			if (AutoItem.isChangingSet)
			{
				GameScr.info1.addInfo("Đang Mặc Đồ!", 0);
				return;
			}
			AutoItem.isChangingSet = true;
			for (int i = global::Char.myCharz().arrItemBag.Length - 1; i >= 0; i--)
			{
				global::Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && set.Contains(item.getFullName()))
				{
					Service.gI().getItem(4, (sbyte)i);
					Thread.Sleep(100);
				}
			}
			AutoItem.isChangingSet = false;
		}

		// Token: 0x040015FA RID: 5626
		private static AutoItem _Instance;

		// Token: 0x040015FB RID: 5627
		private static List<AutoItem.Item> listItemAuto = new List<AutoItem.Item>();

		// Token: 0x040015FC RID: 5628
		private static AutoItem.Item itemToAuto;

		// Token: 0x040015FD RID: 5629
		public static List<string> set1 = new List<string>();

		// Token: 0x040015FE RID: 5630
		public static List<string> set2 = new List<string>();

		// Token: 0x040015FF RID: 5631
		private static bool isChangingSet;

		// Token: 0x04001600 RID: 5632
		private static string[] inputDelay = new string[]
		{
			"Nhập delay",
			"giây"
		};

		// Token: 0x04001601 RID: 5633
		private static string[] inputSellQuantity = new string[]
		{
			"Nhập số lượng bán",
			"số lượng"
		};

		// Token: 0x04001602 RID: 5634
		private static string[] inputBuyQuantity = new string[]
		{
			"Nhập số lượng mua",
			"số lượng"
		};

		// Token: 0x0200010B RID: 267
		public class Item
		{
			// Token: 0x06000B63 RID: 2915 RVA: 0x00003FF8 File Offset: 0x000021F8
			public Item()
			{
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x000093C7 File Offset: 0x000075C7
			public Item(int id, string name)
			{
				this.Id = id;
				this.Name = name;
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x000093DD File Offset: 0x000075DD
			public Item(int id, short isGold, bool index, bool isSell)
			{
				this.Id = id;
				this.IsGold = index;
				this.Index = isGold;
				this.IsSell = isSell;
			}

			// Token: 0x04001603 RID: 5635
			public int Id;

			// Token: 0x04001604 RID: 5636
			public string Name;

			// Token: 0x04001605 RID: 5637
			public int Quantity;

			// Token: 0x04001606 RID: 5638
			public short Index;

			// Token: 0x04001607 RID: 5639
			public bool IsGold;

			// Token: 0x04001608 RID: 5640
			public bool IsSell;

			// Token: 0x04001609 RID: 5641
			public int Delay;

			// Token: 0x0400160A RID: 5642
			public long LastTimeUse;
		}
	}
}
